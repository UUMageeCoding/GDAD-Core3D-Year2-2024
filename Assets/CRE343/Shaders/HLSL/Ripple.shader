Shader "Custom/RippleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RippleFrequency ("Ripple Frequency", Float) = 2.0
        _RippleAmplitude ("Ripple Amplitude", Float) = 0.05
        _RippleSpeed ("Ripple Speed", Float) = 1.0
        _RippleCenter ("Ripple Center", Vector) = (0.5, 0.5, 0, 0)
    }
    SubShader
    {
        Tags { "RenderPipeline"="UniversalRenderPipeline" }
        LOD 200

        Pass
        {
            Name "RipplePass"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // Include URP shader library for necessary functions and definitions
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            sampler2D _MainTex;                 // Main texture
            float _RippleFrequency;             // Frequency of the ripple effect
            float _RippleAmplitude;             // Amplitude of the ripple effect
            float _RippleSpeed;                 // Speed at which ripples animate
            float4 _RippleCenter;               // Center position of the ripple in UV space
            float4 _MainTex_ST;                 // Texture scale and offset

            struct Attributes
            {
                float4 positionOS : POSITION;   // Vertex position in object space
                float3 normalOS : NORMAL;       // Vertex normal in object space
                float2 uv : TEXCOORD0;          // UV coordinates
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;  // Clip space position
                float2 uv : TEXCOORD0;            // Transformed UV coordinates
            };

            // Vertex shader
            Varyings vert (Attributes input)
            {
                Varyings output;

                // Transform the UV coordinates using _MainTex_ST
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);

                // Calculate distance from the ripple center to the current vertex
                float2 rippleCenter = _RippleCenter.xy;
                float distanceFromCenter = distance(output.uv, rippleCenter);

                // Calculate ripple effect based on distance and time
                float time = _Time.y * _RippleSpeed;  // Use _Time.y for time-based animation
                float rippleEffect = sin(distanceFromCenter * _RippleFrequency - time) * _RippleAmplitude;

                // Modify vertex position along the normal direction
                float4 modifiedPosition = input.positionOS;
                modifiedPosition.xyz += input.normalOS * rippleEffect;

                // Transform to clip space
                output.positionCS = TransformObjectToHClip(modifiedPosition);

                return output;
            }

            // Fragment shader
            half4 frag (Varyings input) : SV_Target
            {
                // Sample the texture using the transformed UV coordinates
                return tex2D(_MainTex, input.uv);
            }
            ENDHLSL
        }
    }
    FallBack Off
}
