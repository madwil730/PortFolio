#include "00_Global.fx"
#include "00_Light.fx"
#include "00_Model.fx"

float4 PS(MeshOutput input) : SV_Target0
{
	return PS_AllLight(input);
}


struct FixedCamOutput
{
	float4 Position : SV_Position0;
	float4 cPosition : Position1;

	float2 Uv : Uv0;
	float3 Normal : Normal0;
};

float4 PS_FixedCam(FixedCamOutput input) : SV_Target0
{
	float3 diffuse = DiffuseMap.Sample(LinearSampler, input.Uv).rgb;
	float NdotL = dot(normalize(input.Normal), float3(1,0,0));

	return float4(diffuse * NdotL, 1);

}

FixedCamOutput VS_FixedCam(VertexModel input)
{
	FixedCamOutput output;

	SetAnimationWorld(World, input);

	output.Position = WorldPosition(input.Position); 
	output.Position = mul(output.Position, CamView);
	output.Position = mul(output.Position, CamProjection);
	
	output.cPosition = output.Position;
	output.Normal = WorldNormal(input.Normal); 
	output.Uv = input.Uv; 

	return output;

}

technique11 T0
{
    P_RS_VP(P0, FrontCounterCloskwise_True, VS_Depth_Mesh, PS_Depth)
    P_RS_VP(P1, FrontCounterCloskwise_True, VS_Depth_Model, PS_Depth)
    P_RS_VP(P2, FrontCounterCloskwise_True, VS_Depth_Animation, PS_Depth)

    ///P_VP(P0, VS_Depth_Mesh, PS_Depth)
    ///P_VP(P1, VS_Depth_Model, PS_Depth)
    ///P_VP(P2, VS_Depth_Animation, PS_Depth)

    P_VP(P3, VS_Mesh, PS)
    P_VP(P4, VS_Model, PS)
    P_VP(P5, VS_Animation, PS)
    P_VP(P6, VS_FixedCam, PS_FixedCam)
}