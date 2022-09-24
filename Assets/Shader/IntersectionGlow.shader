// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/IntersectionGlow"
{
	Properties
	{
		_Color ("Color", Color) = (1,0,0,1)
        _Multy ("Multy", float) = 4.08
        _Offset ("Offset", range(0.0,0.1)) = 0.85
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 100
		Blend One One // additive blending for a simple "glow" effect
		Cull Off // render backfaces as well
		ZWrite Off // don't write into the Z-buffer, this effect shouldn't block objects
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 screenPos : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _CameraDepthTexture; // automatically set up by Unity. Contains the scene's depth buffer
			fixed4 _Color;
            float _Multy;
            float _Offset;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// Get the distance to the camera from the depth buffer for this point
                // float sceneZ = Linear01Depth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)).r);
                float2 screenPosUV = i.screenPos.xy / i.screenPos.w;
                float depth = Linear01Depth((SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,screenPosUV)));
                float _Camera_FarPlane = _ProjectionParams.z;
                
                // Actual distance to the camera
                float fragZ = i.screenPos.a;

                float factor = 1 - ((depth * _Camera_FarPlane) - (fragZ - _Offset));
                factor = smoothstep(0, 1, factor * _Multy);
                
				return fixed4(_Color.rgb, factor);
                // return fixed4(1,0,0,factor);
			}
			ENDCG
		}
	}
}
