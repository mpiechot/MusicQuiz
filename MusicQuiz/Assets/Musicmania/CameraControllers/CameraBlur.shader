Shader "Custom/CameraBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" "RenderPipeline"="UniversalPipeline" }
        Pass
        {
            ZWrite Off Cull Off ZTest Always

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_TexelSize;
            float _BlurAmount;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);
                o.uv = v.uv;
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                float2 uv = i.uv;
                float2 texel = _MainTex_TexelSize.xy * (_BlurAmount * 10.0);

                half4 col = half4(0,0,0,0);
                col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + texel * float2(-1.0, -1.0));
                col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + texel * float2(-1.0,  1.0));
                col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + texel * float2( 1.0, -1.0));
                col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + texel * float2( 1.0,  1.0));
                return col * 0.25;
            }
            ENDHLSL
        }
    }
}
