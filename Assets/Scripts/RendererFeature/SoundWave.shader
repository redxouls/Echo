Shader "Hidden/SoundWave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Thickness ("Delta", Range(0, 1.0)) = 0.715
        _Distance ("Distance", Range(0,20)) = 7.8
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 screenPos: TEXCOORD1;
                float3 normal : TEXCOORD2;
                float4 viewDir : TEXCOORD3;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture; // automatically set up by Unity. Contains the scene's depth buffer

            float4 _MainTex_ST;
            float _Distance;
            float _Thickness;
            int _StartIndex;
            int _EndIndex;
            float4 _Points[100];

            uniform float4x4 _InvProjectionMatrix;    //Pass this in via 'camera.projectionMatrix.inverse'
            uniform float4x4 _ViewToWorld;    //Pass this in via 'camera.cameraToWorldMatrix'

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = mul(_InvProjectionMatrix, float4 (o.uv * 2.0 - 1.0, 1.0, 1.0));
                return o;
            }

            float intersectWithWave(float3 worldPos) 
            {
                // Initial alpha set to 0.0
                float alpha = 0.0;

                for (int i = 0; i < (_EndIndex + 100 - _StartIndex) % 100; i++)
                {
                    int index = (_StartIndex + i) % 100;

                    // Calculate distance to the wave source
                    float3 r = distance(worldPos.xyz, _Points[index].xyz);
                    // Caululate (distance - wave_radius)
                    float delta = r - _Points[index].w;
                    
                    // double side smooth to simulate ripple
                    // if (delta <  _Thickness)
                    // {
                    //     alpha += pow(1 - delta/_Thickness, 3);
                    // }
                    
                    // Single side smooth more sharp on one side
                    if (abs(delta) < _Thickness)
                    {
                        alpha += smoothstep(0, _Thickness, delta);
                    }
                }
                
                // Set maximum alpha to 1
                return clamp(alpha, 0, 1);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // Compute screen UV
                float2 screenPosUV = i.screenPos.xy / i.screenPos.w;
                // float depth = Linear01Depth(tex2D(_CameraDepthTexture, i.uv).r);
                float depth = Linear01Depth((SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,screenPosUV)));
                // Perspective divide and scale by depth to get view-space position
                float3 viewPos = (i.viewDir.xyz / i.viewDir.w) * depth;
                // Transform to world space
                float3 worldPos = mul(_ViewToWorld, float4 (viewPos, 1));
                
                // For debugging only
                // float factor = length(worldPos.xyz) - _Distance;
                
                // Calculate alpha of this pixel
                float alpha = intersectWithWave(worldPos.xyz);

                // Scale the color with alpha
                col *= alpha;
                
                return col;
            }
            ENDCG
        }
    }
}
