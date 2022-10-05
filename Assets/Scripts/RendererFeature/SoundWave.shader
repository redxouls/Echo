Shader "Hidden/SoundWave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveSourceMap ("WaveSourceMap", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Delta ("Delta", Range(0, 1.0)) = 0.715
        _Distance ("Distance", Range(0,20)) = 7.8
    }
    SubShader
    {
        // Tags { "RenderType"="Opaque" }
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend srcAlpha oneMinusSrcAlpha

        LOD 100

        Pass
        {
            CGPROGRAM
            // Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
            #pragma exclude_renderers d3d11 gles
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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
            fixed4 _Color;
            float _Distance;
            float _Delta;
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
                o.viewDir = mul (_InvProjectionMatrix, float4 (o.uv * 2.0 - 1.0, 1.0, 1.0));
                return o;
            }

            float intersectWithWave(float3 worldPos) 
            {
                for (int i = 0; i < (_EndIndex + 100 - _StartIndex) % 100; i++)
                {
                    int index = (_StartIndex + i) % 100;
                    float3 r = distance(worldPos.xyz, _Points[index].xyz);
                    float factor = r - _Points[index].w;
                    
                    if (abs(factor) < 0.33 * _Delta) return 1;
                    if (abs(factor) < 0.66 * _Delta)  return 0.66;
                    if (abs(factor) <  _Delta)  return 0.33;
                }
                return 0;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // compute screen UV
                float2 screenPosUV = i.screenPos.xy / i.screenPos.w;
                // float depth = Linear01Depth(tex2D(_CameraDepthTexture, i.uv).r);
                float depth = Linear01Depth((SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,screenPosUV)));
                //Perspective divide and scale by depth to get view-space position
                float3 viewPos = (i.viewDir.xyz / i.viewDir.w) * depth;
                //Transform to world space
                float3 worldPos = mul(_ViewToWorld, float4 (viewPos, 1));

                float factor = intersectWithWave(worldPos.xyz);

                if (factor != 0) 
                {                    
                    col.a = factor;
                    return col;
                }
                else 
                {
                    return fixed4(0,0,0,1);
                }
            }
            ENDCG
        }
    }
}
