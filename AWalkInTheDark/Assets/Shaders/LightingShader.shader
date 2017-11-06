Shader "Custom/Lighting Shader" {
	Properties {
		_Tint ("Tint", Color) = (1, 1, 1, 1)
		_MainTex("Texture", 2D) = "white" {}
	}
	SubShader {
		Pass {
			CGPROGRAM

			#pragma vertex VertexProgram
			#pragma fragment FragmentProgram

			#include "UnityStandardBRDF.cginc"

			float4 _Tint;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct Interpolators {
				float4 position : SV_POSITION;
				float3 normal : TEXCOORD1;
				float2 uv : TEXCOORD0;
			};

			struct VertexData {
				float4 position : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			Interpolators VertexProgram(
				VertexData v
			) {
				Interpolators i;
				i.position = UnityObjectToClipPos(v.position);
				i.uv = TRANSFORM_TEX(v.uv, _MainTex);
				i.normal = UnityObjectToWorldNormal(v.normal);
				i.normal = normalize(i.normal)
				return i;
			}

			float4 FragmentProgram(Interpolators i) : SV_TARGET {
				float3 lightDir = _WorldSpaceLightPos0.xyz;
				return DotClamped(lightDir, i.normal));
			}

			ENDCG
		}
	}
}