Shader "Custom/Highlight"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _RimColor("Rim Color", Color) = (0.26,0.19,0.16,0.0)
        _RimPower("Rim Power", Range(0,8.0)) = 3.0
        [Toggle] _IsEmission("Emission", Float) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

                CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;

            struct Input
            {
                float2 uv_MainTex;
                float3 viewDir;
            };

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;
            float4 _RimColor;
            float _RimPower;
            float _IsEmission;

            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_INSTANCING_BUFFER_END(Props)

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                // Albedo comes from a texture tinted by color
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c.a;

                // turn Emission off or on 
                if (_IsEmission == 1)
                {
                    half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
                    o.Emission = _RimColor.rgb * pow(rim, _RimPower);
                }
            }
        ENDCG

                }
                FallBack "Diffuse"
}