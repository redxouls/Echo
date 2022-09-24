Shader "Unlit/Hologram"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TintColor ("Tint Color", Color) = (1,1,1,1)
        _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)
        _Transparency ("Transparency", Range(0.0,0.5)) = 0.25
        _CutoutThresh ("Cutout Threshold", Range(0.0,1.0)) = 0.2
        _Distace ("Distace", Float) = 1
        _Amplitude ("Amplitude", Float) = 1
        _Speed ("Amplitude", Float) = 1
        _Amount ("Amount", Range(0.0,1.0)) = 1.0
        _OutlineColor("Outline Color", Color)=(1,1,1,1)
        _OutlineSize("OutlineSize", Range(1.0,1.5))=1.1
    }
    SubShader
    {
        Name "Film like Layer"
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Cull Off
        ZWrite off
        Blend srcAlpha oneMinusSrcAlpha

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
                float4 screenSpace: TEXTCORD1;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float4 _MainTex_ST;
            float4 _TintColor;
            float4 _Color1;
            float4 _Color2;
            float _Transparency;
            float _CutoutThresh;
            float _Distace;
            float _Amplitude;
            float _Speed;
            float _Amount;

            v2f vert (appdata v)
            {
                v2f o;
                // v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distace * _Amount;
                // v.vertex *= _SinTime.z;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenSpace = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _TintColor;
                // fixed4 col = tex2D(_MainTex, i.uv);
                col.a = _Transparency;
                // if (col.r < _CutoutThresh) discard;
                // clip(col.r - _CutoutThresh);
                // float2 screenSpaceUV = i.screenSpace.xy / i.screenSpace.w;
                // float depth = Linear01Depth((SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,screenSpaceUV)));
                // if (depth - _CutoutThresh >= 0.01) discard;
                // clip(depth - _CutoutThresh);
                // float3 mixColor = lerp(_Color1, _Color2, depth);
                // clip(mixColor.r - _CutoutThresh);
                // return fixed4(col.a,0,0,1);
                return col;
            }
            ENDCG
        }
    }
}
