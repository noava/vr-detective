Shader "Custom/CoiledCable"
{
    Properties
    {
        _BaseColor ("Cable Color", Color) = (0.03, 0.03, 0.03, 1)

        _SpiralCount ("Spiral Count", Float) = 12
        _CableWidth ("Cable Width", Range(0.01, 1.0)) = 0.2
        _Smoothness ("Edge Smoothness", Range(0.001, 0.2)) = 0.02
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "TransparentCutout"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "AlphaTest"
        }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            Cull Off

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
            };

            CBUFFER_START(UnityPerMaterial)

                float4 _BaseColor;
                float _SpiralCount;
                float _CableWidth;
                float _Smoothness;

            CBUFFER_END


            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                OUT.positionHCS =
                    TransformObjectToHClip(IN.positionOS.xyz);

                OUT.uv = IN.uv;

                OUT.normalWS =
                    TransformObjectToWorldNormal(IN.normalOS);

                return OUT;
            }


            half4 frag(Varyings IN) : SV_Target
            {
                float U = IN.uv.x;
                float V = IN.uv.y;

                // Create diagonal spiral
                float spiral =
                    frac(V * _SpiralCount + U);


                // Center the spiral around 0
                float centered =
                    abs(spiral - 0.5) * 2.0;


                // Create the thickness of the cable
                float mask =
                    1.0 - smoothstep(
                        _CableWidth,
                        _CableWidth + _Smoothness,
                        centered
                    );


                // Cut away everything except the spiral
                clip(mask - 0.5);


                // Basic lighting
                float3 normalWS =
                    normalize(IN.normalWS);

                Light mainLight =
                    GetMainLight();

                float NdotL =
                    saturate(
                        dot(normalWS, mainLight.direction)
                    );

                float3 lighting =
                    mainLight.color * NdotL;

                float3 finalColor =
                    _BaseColor.rgb *
                    (lighting + 0.15);


                return half4(finalColor, 1);
            }

            ENDHLSL
        }
    }
}