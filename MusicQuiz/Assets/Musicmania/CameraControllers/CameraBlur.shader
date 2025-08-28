Shader "Custom/CameraBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }
        Pass
        {
            ZWrite Off Cull Off ZTest Always

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BlurAmount;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 texel = _MainTex_TexelSize.xy * (_BlurAmount * 10.0);

                fixed4 col = fixed4(0,0,0,0);
                col += tex2D(_MainTex, uv + texel * float2(-1.0, -1.0));
                col += tex2D(_MainTex, uv + texel * float2(-1.0,  1.0));
                col += tex2D(_MainTex, uv + texel * float2( 1.0, -1.0));
                col += tex2D(_MainTex, uv + texel * float2( 1.0,  1.0));
                return col * 0.25;
            }
            ENDHLSL
        }
    }
}
