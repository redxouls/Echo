Shader "Unlit/Ripple"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", color) = (0.0, 0.0, 0.5, 0.5)
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
        
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float time = _Time * 10;
                float offset = (time - floor(time))/time;
                float currentTime = (time)*(offset);
                
                float3 WaveParams = float3(10.0, 0.8, 0.1);
                float2 rippleCenter = float2(0.3, 0.25);
                
                float2 texCoord = i.uv;
                
                float d = distance(texCoord, rippleCenter);
                fixed4 col = tex2D(_MainTex, texCoord) * _Color;
                
                // The pixel offset distance based on the input parameters
                float diff = (d - currentTime);
                
                // Only distort the pixels within the parameter distance from the center
                if (abs(d - currentTime) < WaveParams.z)
                {
                    
                    float scaleDiff = (1.0 - pow(abs(diff * WaveParams.x), WaveParams.y));
                    float diffTime = (diff  * scaleDiff);
                    
                    // The direction of the distortion
                    float2 diffTexCoord = normalize(texCoord - rippleCenter);
                    
                    // Perform the distortion and reduce the effect over time
                    texCoord += ((diffTexCoord * diffTime) / (currentTime * d * 10.0));
                    col = tex2D(_MainTex, texCoord) * _Color;
                    
                    // Blow out the col and reduce the effect over time
                    col += (col * scaleDiff) / (currentTime * d * 10.0);
                } 
                col.a = clamp(col.a, 0, 1);
                return col;
            }
            ENDCG
        }
    }
}
