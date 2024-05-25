#include "00_Global.fx"
#include "00_Light.fx"
#include "00_Terrain.fx"


float4 PS(VertexTerrain input) : SV_Target0
{
    float3 diffuse = GetLayerColor(input.Uv);
    float NdotL = dot(normalize(input.Normal), -GlobalLight.Direction);
    
    float4 brush = GetBrushColor(input.wPosition);
    float4 lineColor = GetLineColor(input.wPosition);
        
    return float4(diffuse * NdotL, 1) + brush + lineColor;

}

//-----------±íÀÌ----------------------
float4 PS_Depth(VertexTerrain input) : SV_Target0
{
	float depth = input.Position.z / input.Position.w;

	return float4(depth, depth, depth, 1.0f);

}

//------------±×¸²ÀÚ---------------------
float4 PS_Shadows(TerrainOutput input) : SV_Target0
{
	NormalMapping(input.Uv, input.Normal, input.Tangent);
	Texture(Material.Diffuse, DiffuseMap, input.Uv);
	Texture(Material.Specular, SpecularMap, input.Uv);

	MaterialDesc result = MakeMaterial();
	MaterialDesc output = MakeMaterial();

	ComputeLight(output, input.Normal, input.wPosition);
	AddMaterial(result, output);

	ComputePointLight(output, input.Normal, input.wPosition);
	AddMaterial(result, output);

	ComputeSpotLight(output, input.Normal, input.wPosition);
	AddMaterial(result, output);

	float4 color = float4(MaterialToColor(result), 1.0f);

	input.sPosition.xyz /= input.sPosition.w;

	[flatten]
	if (input.sPosition.x < -1.0f || input.sPosition.x > 1.0f ||
		input.sPosition.y < -1.0f || input.sPosition.y > 1.0f ||
		input.sPosition.z < 0.0f || input.sPosition.z > 1.0f)
		return color;

	input.sPosition.x = input.sPosition.x * 0.5f + 0.5f;
	input.sPosition.y = -input.sPosition.y * 0.5f + 0.5f;
	input.sPosition.z -= ShadowBias;

	float depth = 0.0f;
	float factor = 0.0f;

	[branch]
	if (ShadowQuality == 0)
	{
		depth = ShadowMap.Sample(LinearSampler, input.sPosition.xy).r;
		factor = (float)input.sPosition.z <= depth;
	}
	else if (ShadowQuality == 1)
	{
		depth = input.sPosition.z;
		factor = ShadowMap.SampleCmpLevelZero(ShadowSampler, input.sPosition.xy, depth).r;
	}
	else if (ShadowQuality == 2)
	{
		depth = input.sPosition.z;

		float2 size = 1.0f / ShadowMapSize;
		float2 offsets[] =
		{
			float2(-size.x, -size.y), float2(0.0f, -size.y), float2(+size.x, -size.y),
			float2(-size.x, 0.0f), float2(0.0f, 0.0f), float2(+size.x, 0.0f),
			float2(-size.x, +size.y), float2(0.0f, +size.y), float2(+size.x, +size.y)
		};

		float2 uv = 0;
		float2 sum = 0;

		[unroll(9)]
		for (int i = 0; i < 9; i++)
		{
			uv = input.sPosition.xy + offsets[i];
			sum += ShadowMap.SampleCmpLevelZero(ShadowSampler, uv, depth).r;
		}

		factor = sum / 9.0f;

	}

	factor = saturate(factor + depth);
	return float4(color.rgb * factor, 1);

}






technique11 T0
{

    P_VP(P0, VS, PS)    
    P_RS_VP(P1, FillMode_WireFrame, VS, PS)
	P_RS_VP(P2, FrontCounterCloskwise_True, VS_Depth_Terrain, PS_Depth)
	P_VP(P3, VS_TerrainShadow, PS_Shadows)
    
}