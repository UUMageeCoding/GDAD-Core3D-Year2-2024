Shader "Custom/WaveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Frequency ("Frequency", Float) = 1.0
        _Amplitude ("Amplitude", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderPipeline"="UniversalRenderPipeline" }
        LOD 200

        Pass
        {
            Name "WavePass"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // Include URP's shader library for necessary definitions and helpers
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            sampler2D _MainTex;       // Main texture
            float _Frequency;         // Frequency of the wave
            float _Amplitude;         // Amplitude of the wave
            float4 _MainTex_ST;       // Scale and offset for the main texture

            struct Attributes
            {
                float4 positionOS : POSITION;   // Object space position
                float3 normalOS : NORMAL;       // Object space normal
                float2 uv : TEXCOORD0;          // Texture coordinates
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION; // Clip space position
                float2 uv : TEXCOORD0;           // Transformed UV coordinates
            };

            // Vertex Shader
            Varyings vert (Attributes input)
            {
                Varyings output;

                // Apply UV transformation manually
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);

                // Calculate wave displacement along the Z-axis based on a sine wave
                float timeValue = _Time.y;      // Time in seconds
                float angle = (timeValue + output.uv.x) * _Frequency; // Calculate angle
                float displacement = _Amplitude * sin(angle); // Calculate displacement

                // Apply displacement to the Z-axis of the vertex position
                float4 modifiedPosition = input.positionOS;
                modifiedPosition.z += displacement;

                // Transform the modified position from object space to clip space
                output.positionCS = TransformObjectToHClip(modifiedPosition);

                return output;
            }

            // Fragment Shader
            half4 frag (Varyings input) : SV_Target
            {
                // Sample the main texture using the transformed UV coordinates
                return tex2D(_MainTex, input.uv);
            }
            ENDHLSL
        }
    }
    FallBack Off
}
