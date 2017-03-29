// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/HologramSampler" {

    Properties{
        _HoloColor ("Hologram Color", Color) = (0,0,0,0.5)
        _MainTex ("Main Texture", 2D) = "black" {}
        _Color ("Main Color", Color) = (1,1,1,1)
    }


        SubShader{
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
        Pass {

        CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

        fixed4 _HoloColor;
        fixed4 _DefaultHolo;

        sampler2D _ScanerPing;
        sampler2D _Gridline;
        sampler2D _Noise;
        float _NoiseDensity;

        float4 _CharacterPos;
		float _UnscaledTime;

        float _ScanerIntensity;
        float _ScanerStartTime;
        float _ScanerFrequency;
        float _ScanerSpeed;
        float _ScanerVerticalMod;
        float _ScannerGridResonanse;

        float _GridlineIntensity;
        float _GridlineDensity;

        float _AmbientIntencity;
        float _AmpientNear;
        float _AmbientDepth;

        struct v2f {
            float4 pos : SV_POSITION;
            float4 uv : TEXCOORD0;
            float4 worldPos: TEXCOORD2;
            float4 wNormal : TEXCOORD3;
            float4 projPos : TEXCOORD1;
        };



        v2f vert (appdata_full v) {
            v2f o;
            o.worldPos = mul (unity_ObjectToWorld, v.vertex);
            o.wNormal = mul (unity_ObjectToWorld, v.normal);

            o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
            o.uv = float4(v.texcoord.xy, 0, 0);
            o.projPos = ComputeScreenPos (o.pos);
            return o;
        }

        fixed4 frag (v2f i) : Color{

            float gridX = tex2D (_Gridline, i.worldPos.yz * _GridlineDensity).r;
            float gridY = tex2D (_Gridline, i.worldPos.xz * _GridlineDensity).r;
            float gridZ = tex2D (_Gridline, i.worldPos.xy * _GridlineDensity).r;

            i.worldPos.y *= _ScanerVerticalMod;

            float gridline = lerp (lerp (gridZ, gridX, abs (dot (i.wNormal, fixed3 (1, 0, 0)))), gridY, abs (dot (i.wNormal, fixed3 (0, 1, 0)))) * _GridlineIntensity;
            //return float4(gridline, gridline, gridline, 1);

            float noise = tex2D (_Noise, i.worldPos.xz * _NoiseDensity).r;
            //return float4(noise, noise, noise, 1);

            float interference = noise * 0.75;
            //return float4(interference, interference, interference, 1);

            float dist = (distance (i.worldPos.xyz, _CharacterPos.xyz) / _ScanerSpeed) + interference;
            //return float4(dist, dist, dist, 1);

            float3 viewDir = normalize (_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
            //return float4(viewDir, 1);

            float time = _UnscaledTime - _ScanerStartTime;
            //return float4(time, time, time, 1);

            float border = clamp ((time - dist) * 5, 0, 1);
            //return float4(border, border, border, 1);

            float scaner = tex2D (_ScanerPing, float2(0, (dist - time) * _ScanerFrequency)).r * _ScanerIntensity;
            //return float4(scaner, scaner, scaner, 1);

            float fresnel = 1 - dot (viewDir, i.wNormal.xyz);
            //return float4(fresnel, fresnel, fresnel, 1);

            float ambient = _AmbientIntencity * (fresnel + (i.projPos.z - _AmpientNear) / _AmbientDepth) / 2;
            //return float4(ambient, ambient, ambient, 1);

            float resonance = gridline * scaner * _ScannerGridResonanse;
            //return float4(resonance, resonance, resonance, 1);


            fixed4 color = lerp (_DefaultHolo, _HoloColor, _HoloColor.a);
            fixed4 hologram = color * (ambient + scaner + gridline + resonance);

            fixed4 result = lerp (0, hologram, border);

            return pow (result, 1.5) * 2;
        }
        ENDCG
        }
        }

            SubShader{
                Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
                Pass{

                Blend SrcAlpha One

                CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

                fixed4 _HoloColor;

                float _ScanerStartTime;
                float _ScanerSpeed;
                float4 _CharacterPos;

                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed4 _Color;

                struct v2f {
                    float4 worldPos: TEXCOORD2;
                    float4 vertex : SV_POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord : TEXCOORD0;
                };



                v2f vert (appdata_full v) {
                    v2f o;
                    o.worldPos = mul (unity_ObjectToWorld, v.vertex);
                    o.vertex = mul (UNITY_MATRIX_MVP, v.vertex);
                    o.color = v.color * _Color;
                    o.texcoord = TRANSFORM_TEX (v.texcoord,_MainTex);
                    return o;
                }

                fixed4 frag (v2f i) : Color{
                   
                    float dist = (distance (i.worldPos.xyz, _CharacterPos.xyz) / _ScanerSpeed);
                    float time = _Time.y - _ScanerStartTime;
                    float border = clamp ((time - dist) * 5, 0, 1);

                    fixed4 col = i.color;
                    col.a *= tex2D (_MainTex, i.texcoord).a * _HoloColor.a * border;
                    return col;
                }
                ENDCG
            }
        }
}