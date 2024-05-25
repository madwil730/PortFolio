cbuffer CB_PerFrame
{
    matrix View;
    matrix Projection;
};
matrix World;


//-------------------------------------------------------------------------------------------------
//Structures
//-------------------------------------------------------------------------------------------------
struct VertexInput
{
    float4 Position : POSITION0;
    float2 Uv : UV0;
};

struct VertexOutput
{
    float4 Position : SV_POSITION0;
    float2 Uv : UV0;
};

//-------------------------------------------------------------------------------------------------
//Vertex Shader
//-------------------------------------------------------------------------------------------------
VertexOutput VS(VertexInput input)
{
    VertexOutput output;
    output.Position = mul(input.Position, World);
    output.Position = mul(output.Position, View);
    output.Position = mul(output.Position, Projection);

    output.Uv = input.Uv;

    return output;
}

//-------------------------------------------------------------------------------------------------
//Pixel Shader
//-------------------------------------------------------------------------------------------------
//Texture
Texture2D Map;
SamplerState Sampler;

float3 HSVtoRGB(float3 color)
{
    float4 k = float4(1.0f, 0.66f, 0.33f, 3.0f); //골드색
    float3 p = abs(frac(color.xxx + k.xyz) * 6.0f - k.www);
    return color.b * lerp(k.xxx, clamp(p - k.xxx, 0.0f, 1.0f), color.g);
    
}

float4 PS(VertexOutput input) : SV_TARGET0
{
    float3 col = float3(input.Uv.x, 1.0f, 1.0f);
    float alpha = Map.Sample(Sampler, input.Uv).a;
    float3 base = Map.Sample(Sampler, input.Uv).rgb;

    return float4(HSVtoRGB(col) + base, alpha);
}












//------------------------------------------------------------------------------------------------------------

//Bloom
SamplerState BaseSampler;
float BaseSaturation = 2.0f/*0.5f*/;
float BaseIntensity = 2.0f/*3.0f*/;

SamplerState BloomSampler;
float BloomSaturation = 1.0f;
float BloomIntensity = 2.0f;

float4 AdjustSaturation(float4 color, float saturation)
{
    float4 cDotg = dot(color, float4(1, 1, 1, 1));
    return lerp(cDotg, color, saturation);
}

float4 alphachange(float4 color, float saturation ,float a)
{

    float4 cDotg = dot(color, float4(1, 1, 1, a)); // 내적구하기 
    return lerp(cDotg, color, saturation); // 선형보간 
}

float4 PS_Bloom(VertexOutput input) : SV_TARGET0
{
    float4 base = Map.Sample(BaseSampler, input.Uv);
    float4 bloom = Map.Sample(BloomSampler, input.Uv);

    base = AdjustSaturation(base, BaseSaturation) * BaseIntensity;
    bloom = AdjustSaturation(bloom, BloomSaturation) * BloomIntensity;

    base *= (1 - saturate(bloom));

    return base + bloom;
}

//-------------------------------------------------------------------------------------------------
//RS State
//-------------------------------------------------------------------------------------------------
RasterizerState Cull
{
    CullMode = None; //반시계 방향 그려줌
    DepthClipEnable = false; //z축으로 밀린 그려줌
};

BlendState AlphaBlend
{
    AlphaToCoverageEnable = false;
    BlendEnable[0] = true;
    DestBlend[0] = INV_SRC_ALPHA;
    SrcBlend[0] = SRC_ALPHA;
    BlendOp[0] = Add;
    
    SrcBlendAlpha[0] = One;
    DestBlendAlpha[0] = One;
    RenderTargetWriteMask[0] = 0x0F;
};


//-------------------------------------------------------------------------------------------------
//Technique Pass
//-------------------------------------------------------------------------------------------------
technique11 T0
{
    pass P0
    {
        SetVertexShader(CompileShader(vs_5_0, VS()));
        SetPixelShader(CompileShader(ps_5_0, PS()));

        SetRasterizerState(Cull);
        SetBlendState(AlphaBlend, float4(0, 0, 0, 0), 0xFF);
    }

    pass P1
    {
        SetVertexShader(CompileShader(vs_5_0, VS()));
        SetPixelShader(CompileShader(ps_5_0, PS_Bloom()));

        SetRasterizerState(Cull);
        SetBlendState(AlphaBlend, float4(0, 0, 0, 0), 0xFF);
    }
    
}