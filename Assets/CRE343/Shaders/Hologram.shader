Shader "Custom/HologramShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.1, 0.9, 1, 1)
        _FresnelColor ("Fresnel Color", Color) = (0, 1, 1, 1)
        _MainTex ("Main Texture", 2D) = "white" {}
        _ScanlineTex ("Scanline Texture", 2D) = "white" {}
        _FresnelPower ("Fresnel Power", Range(0, 10)) = 3.0
        _ScrollSpeed ("Scroll Speed", Float) = 1.0
        _DistortionStrength ("Distortion Strength", Float) = 0.05
        _CamPos ("Camera Position", Vector) = (0, 0, 0, 0) // Rename the camera position variable
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Pass
        {
            Name "HologramPass"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Uniforms
            float4 _BaseColor;
            float4 _FresnelColor;
            float _FresnelPower;
            float _ScrollSpeed;
            float _DistortionStrength;

            sampler2D _MainTex;          // Main texture for the object
            sampler2D _ScanlineTex;      // Scanline texture for hologram effect
            float4 _MainTex_ST;          // Scale and offset for main texture

            // Renamed variable for camera position
            float3 _CamPos;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 normalWS : TEXCOORD1;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD2;
                float3 viewDirWS : TEXCOORD3;
            };

            Varyings vert (Attributes input)
            {
                Varyings output;

                // Transform object space vertex position to clip space
                output.positionCS = TransformObjectToHClip(input.positionOS);

                // Calculate world-space normal and position
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.worldPos = TransformObjectToWorld(input.positionOS).xyz;

                // Calculate view direction: vector from world position to camera position
                output.viewDirWS = normalize(_CamPos - output.worldPos);

                // Transform UV coordinates
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);

                return output;
            }

            half4 frag (Varyings input) : SV_Target
            {
                // Sample main texture
                half4 baseTexColor = tex2D(_MainTex, input.uv) * _BaseColor;

                // Apply scrolling effect to scanline texture
                float2 scrollUV = input.uv + float2(0, _Time.y * _ScrollSpeed);
                half4 scanlineColor = tex2D(_ScanlineTex, scrollUV);

                // Fresnel Effect: Calculate intensity based on view direction and normal
                float fresnelFactor = pow(1.0 - abs(dot(input.normalWS, input.viewDirWS)), _FresnelPower);
                half4 fresnelColor = _FresnelColor * fresnelFactor;

                // Combine base color, scanline effect, and fresnel glow
                half4 finalColor = baseTexColor + scanlineColor + fresnelColor;

                // Add distortion effect based on the normal map or noise
                float3 distortion = input.normalWS * _DistortionStrength;
                finalColor.rg += distortion.xy;

                // Set alpha for transparency
                finalColor.a = baseTexColor.a;

                return finalColor;
            }
            ENDHLSL
        }
    }
    FallBack Off
}
