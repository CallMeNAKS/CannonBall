Shader "Custom/URP_BurningEffect"
{
    Properties
    {
        _BaseMap ("Base Texture", 2D) = "white" {}
        _BurnRamp ("Burn Ramp", 2D) = "white" {}
        _BurnColor ("Burn Color", Color) = (1, 0, 0, 1)
        _BurnAmount ("Burn Amount", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline"="UniversalRenderPipeline" }
        LOD 200

        Pass
        {
            Name "MainPass"
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            TEXTURE2D(_BurnRamp);
            SAMPLER(sampler_BurnRamp);

            float4 _BurnColor;
            float _BurnAmount;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS);
                output.uv = input.uv;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                // Основная текстура объекта
                float4 baseColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv);

                // Градиент прожигания
                float burnMask = SAMPLE_TEXTURE2D(_BurnRamp, sampler_BurnRamp, float2(input.uv.x, _BurnAmount)).r;

                // Цвет края прожигания
                float edge = smoothstep(0.4, 0.6, burnMask);
                float4 burnColor = lerp(baseColor, _BurnColor, edge);

                // Управление прозрачностью
                float alpha = step(burnMask, _BurnAmount);

                return float4(burnColor.rgb, alpha);
            }
            ENDHLSL
        }
    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}
