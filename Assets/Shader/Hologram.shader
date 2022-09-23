Shader "Unlit/Hologram"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TintColor ("Tint Color", Color) = (1,1,1,1)
        _Transparency ("Transparency", Range(0.0,0.5)) = 0.25
        _CutoutThresh ("Cutout Threshold", Range(0.0,1.0)) = 0.2
        _Distace ("Distace", Float) = 1
        _Amplitude ("Amplitude", Float) = 1
        _Speed ("Amplitude", Float) = 1
        _Amount ("Amount", Range(0.0,1.0)) = 1.0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Cull Off
        ZWrite off
        Blend srcAlpha oneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TintColor;
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
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _TintColor;
                col.a = _Transparency;
                // if (col.r < _CutoutThresh) discard;
                clip(col.r - _CutoutThresh);
                return col;
            }
            ENDCG
        }
    }
}
