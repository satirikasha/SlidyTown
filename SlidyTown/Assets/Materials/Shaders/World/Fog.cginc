
float4 _FogColor;
fixed _FogHeight;
fixed _FogPower;

void ApplyFog(inout fixed4 color, float3 worldPos, float4 screenPos) {
	float fog = saturate(pow(screenPos.y / screenPos.w, _FogPower) - worldPos.g * _FogHeight);
	float3 fogColor = lerp(0.5, _FogColor.rgb, fog);
	float3 result = color;
	result = lerp(fogColor - 0.5 + result * (1.5 - fogColor), result * (fogColor + 0.5), fogColor > 0.5);
	color = fixed4(result, color.a);
}