Shader "Custom/DissolveHLSLWithBaseMaterial"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1)
        _BaseMap ("Base Texture", 2D) = "white" {} // Base material texture property
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0.5
        _DissolveTexture ("Dissolve Texture", 2D) = "white" {}
        _EdgeColor ("Edge Color", Color) = (1, 0, 0, 1)
        _EdgeWidth ("Edge Width", Range(0, 0.5)) = 0.1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Pass
        {
            Name "DissolvePass"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Uniforms
            float4 _BaseColor;
            float _DissolveAmount;
            float4 _EdgeColor;
            float _EdgeWidth;
            sampler2D _DissolveTexture;    // Noise texture for dissolve pattern
            sampler2D _BaseMap;            // Base material texture

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert (Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS);
                output.uv = input.uv;
                return output;
            }

            half4 frag (Varyings input) : SV_Target
            {
                // Sample the base material texture
                half4 baseTextureColor = tex2D(_BaseMap, input.uv);

                // Sample the dissolve texture at UV coordinates
                float dissolveValue = tex2D(_DissolveTexture, input.uv).r;

                // Calculate the dissolve threshold based on the dissolve amount
                float dissolveThreshold = dissolveValue - _DissolveAmount;

                // Apply edge width to create a soft dissolve edge
                float edgeFactor = smoothstep(-_EdgeWidth, 0.0, dissolveThreshold);

                // Base color with the sampled base texture
                half4 baseColor = baseTextureColor * _BaseColor;

                // Calculate final color with edge highlighting
                half4 finalColor = lerp(_EdgeColor, baseColor, edgeFactor);

                // Alpha clipping for dissolve effect
                clip(edgeFactor - 0.1);

                // Output final color
                return finalColor;
            }
            ENDHLSL
        }
    }
    FallBack Off
}
