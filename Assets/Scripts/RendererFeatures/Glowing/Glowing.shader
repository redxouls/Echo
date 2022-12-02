Shader "Hidden/Glowing"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        // Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        // Blend srcAlpha oneMinusSrcAlpha

        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture; // automatically set up by Unity. Contains the scene's depth buffer

            float4 _MainTex_ST;
        
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float2 screenPosUV = i.screenPos.xy / i.screenPos.w;
                float depth = Linear01Depth((SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenPosUV)));
                return fixed4(0,0,0,depth);
            }
            ENDCG
        }
    }
}

