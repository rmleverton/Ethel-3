Shader "CustomRenderTexture/CCTVLines"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex("InputTex", 2D) = "white" {}
     }

     SubShader
     {
        Blend One Zero

        Pass
        {
            Name "CCTVLines"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float4      _Color;
            sampler2D   _MainTex;
            
            float SineNoiseMultiplier(float2 uv){
                float sinCol = abs(sin(uv));
                return sinCol;
            }

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float2 uv = IN.localTexcoord.xy;
                float4 color = tex2D(_MainTex, uv) * _Color;
                float sinCol = SineNoiseMultiplier(uv);

                // TODO: Replace this by actual code!
                // uint2 p = uv.xy * 256;
                // return countbits(~(p.x & p.y) + 1) % 2 * float4(uv, 1, 1) * color;
                return  sinCol;
            }
            ENDCG
        }
    }
}
