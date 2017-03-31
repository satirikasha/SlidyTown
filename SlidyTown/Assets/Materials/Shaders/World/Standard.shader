Shader "Enviroment/Standard" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Specular("Specular", Color) = (0,0,0,1)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf StandardSpecular finalcolor:color fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
        #include "Fog.cginc"

		sampler2D _MainTex;

		half _Glossiness;
		fixed4 _Specular;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float4 screenPos;
		};

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c;
			o.Specular = _Specular;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}

		void color(Input IN, SurfaceOutputStandardSpecular o, inout fixed4 color) {
			ApplyFog(color, IN.worldPos, IN.screenPos);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
