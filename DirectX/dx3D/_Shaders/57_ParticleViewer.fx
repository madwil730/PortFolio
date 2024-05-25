#include "00_Global.fx"
#include "00_Deffered.fx"
#include "00_Model.fx"
#include "00_Sky.fx"

///////////////////////////////////////////////////////////////////////////////
// PreRender
///////////////////////////////////////////////////////////////////////////////
float4 PS(MeshOutput input) : SV_Target
{
    return PS_Shadow(input, PS_AllLight(input));
}

technique11 T0
{
    //Deffered-Depth
    P_RS_VP(P0, FrontCounterCloskwise_True, VS_Depth_Mesh, PS_Depth)
    P_RS_VP(P1, FrontCounterCloskwise_True, VS_Depth_Model, PS_Depth)
    P_RS_VP(P2, FrontCounterCloskwise_True, VS_Depth_Animation, PS_Depth)

    //Sky
    P_VP(P3, VS_Scattering, PS_Scattering)
    P_VP(P4, VS_Dome, PS_Dome)
    P_BS_VP(P5, AlphaBlend, VS_Moon, PS_Moon)
    P_BS_VP(P6, AlphaBlend, VS_Cloud, PS_Cloud)

    //Render
    P_VP(P7, VS_Mesh, PS)
    P_VP(P8, VS_Model, PS)
    P_VP(P9, VS_Animation, PS)    
}

