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
                // float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos: TEXCOORD1;
            };

            sampler2D _MainTex;

            float4 _MainTex_ST;
            float _Distance;
        
            // waves
            float4 _Points[100];
            float _Radius[100];
            float _thickness[100];
            float _Attributes[100]; // DEAD = 0, PLAYER = 1, GRENADE = 2, PASSIVE = 3
            float _AlphaAttenuation[100]; // the age of the wave 0 ~ 1
            float _Weight[] = {0.0, 0.4, 0.3, 0.3}; // weight for each attribute

            // trigger lights
            float4 _TriggerLight[100];
            float _TriggerLightRadius[100];
            int _NofTirggerLight = 0;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);                
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float intersectWithWave(float3 worldPos) 
            {
                // Initial alpha set to 0.0
                float alpha = 0.0;

                // Circular calculation
                for (uint i = 0; i < 100; ++i)
                {
                    if (_Attributes[i] == 0) // DEAD
                    {
                        continue;
                    }
                    // Calculate distance to the wave source
                    float r = distance(worldPos.xyz, _Points[i].xyz);
                    // Caululate (distance - wave_radius)
                    float delta = r - _Radius[i];
                    
                    // double side smooth to simulate ripple
                    // if (delta <  _Thickness)
                    // {
                    //     alpha += pow(1 - delta/_Thickness, 3);
                    // }
                    
                    // Single side smooth more sharp on one side
                    if (abs(delta) < _thickness[i])
                    {
                        // TODO: add _Weight[i]
                        alpha += 0.3 * smoothstep(0, _thickness[i], delta) * _AlphaAttenuation[i];
                    }
                }
                // Set maximum alpha to 1
                return clamp(alpha, 0, 1);
            }

            float intersectWithTriggerLight(float3 worldPos)
            {
                float alpha = 0;
                for (int i = 0; i < _NofTirggerLight; ++i)
                {
                    float r = distance(worldPos.xyz, _TriggerLight[i].xyz);
                    if (r < _TriggerLightRadius[i])
                    {
                        alpha += (1 - pow(r / _TriggerLightRadius[i], 2));
                    }
                }
                return alpha;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 worldPos = i.worldPos;
                
                // // Sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);
                                
                // Calculate alpha of this pixel
                float waveWeight = 0.7, triggerLightWeight = 0.3;
                float waveAlpha = intersectWithWave(worldPos.xyz);
                float triggerLightAlpha = intersectWithTriggerLight(worldPos.xyz);
                float alpha = waveWeight * waveAlpha + triggerLightWeight * triggerLightAlpha;

                // Scale the color with alpha
                fixed4 col = fixed4(0,0,0, 1 - clamp(alpha, 0, 1));
                return col;
            }
            ENDCG
        }
    }
}
