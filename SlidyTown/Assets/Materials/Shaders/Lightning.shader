// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Effects/Lightning" {
Properties {
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Particle Texture", 2D) = "white" {}
	_NoiseTex("Noise Texture", 2D) = "black" {}
	_NoiseScale("Noise Scale", Range(0.01,1)) = 0.25
	_Distortion("Distortion Intensity", Range(0,2)) = 1
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles

			#include "UnityCG.cginc"


			float4 _MainTex_ST;
			sampler2D _MainTex;
			sampler2D _NoiseTex;
			fixed4 _TintColor;
			half _NoiseScale;
			half _Distortion;
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
			};


			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float noise = tex2D(_NoiseTex, i.worldPos.xz * _NoiseScale) * 2 - 1;
				fixed4 col = 2.0f * i.color * _TintColor * tex2D(_MainTex, i.texcoord + fixed2(0,1) * noise * _Distortion);
				return col;
			}
			ENDCG 
		}
	}	
}
}
