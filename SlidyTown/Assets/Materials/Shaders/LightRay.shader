// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Effects/LightRay"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
		ZTest Always
		Cull Back
		Blend SrcAlpha One
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
		float4 normal : NORMAL;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
		float4 posWorld : TEXCOORD2;
		float3 normal : TEXCOORD1;
	};

	float4 _Color;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.uv;
		o.posWorld = mul(unity_ObjectToWorld, v.vertex);
		o.normal = normalize(mul(v.normal, unity_WorldToObject).xyz);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
		float fresnel = pow(dot(viewDirection, -i.normal), 2);
		fixed4 col = float4(_Color.rgb, _Color.a * (1 - i.uv.g) * fresnel);
	    return col;
	}
		ENDCG
	}
	}
}
