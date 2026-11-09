Shader "UI/CRT"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        
        _Curvature ("Screen Curvature", Range(0.0, 0.5)) = 0.15
        _ScanlineIntensity ("Scanline Darkening", Range(0.0, 1.0)) = 0.35
        _ScanlineCount ("Scanline Count", Float) = 450.0
        _ScanlineSpeed ("Scanline Scroll Speed", Float) = 2.0
        _MaskStrength ("RGB Mask Strength", Range(0.0, 1.0)) = 0.2

        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest LEqual // Allows 3D world objects to occlude/block this UI element
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 uv       : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            
            float _Curvature;
            float _ScanlineIntensity;
            float _ScanlineCount;
            float _ScanlineSpeed;
            float _MaskStrength;

            float2 RadialDistortion(float2 uv)
            {
                uv = uv * 2.0 - 1.0;
                float2 distortion = uv.yx * _Curvature;
                uv = uv + uv * (distortion * distortion);
                return uv * 0.5 + 0.5;
            }

            v2f vert(appdata_t v)
            {
                v2f o;
                o.worldPosition = v.vertex;
                o.vertex = UnityObjectToClipPos(o.worldPosition);
                o.uv = v.texcoord;
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 warpedUV = RadialDistortion(i.uv);

                if (warpedUV.x < 0.0 || warpedUV.x > 1.0 || warpedUV.y < 0.0 || warpedUV.y > 1.0)
                {
                    return fixed4(0.0, 0.0, 0.0, 0.0);
                }

                fixed4 originalColor = tex2D(_MainTex, warpedUV) * i.color;

                float scanlineSim = sin((warpedUV.y + (_Time.y * _ScanlineSpeed * 0.01)) * _ScanlineCount);
                float scanlineFactor = lerp(1.0 - _ScanlineIntensity, 1.0, (scanlineSim + 1.0) * 0.5);

                float maskX = i.vertex.x; 
                float3 rgbMask = float3(1.0, 1.0, 1.0);
                
                if (fmod(maskX, 3.0) < 1.0) {
                    rgbMask = float3(1.0, 1.0 - _MaskStrength, 1.0 - _MaskStrength);
                } else if (fmod(maskX, 3.0) < 2.0) {
                    rgbMask = float3(1.0 - _MaskStrength, 1.0, 1.0 - _MaskStrength);
                } else {
                    rgbMask = float3(1.0 - _MaskStrength, 1.0 - _MaskStrength, 1.0);
                }

                fixed3 finalRGB = originalColor.rgb * scanlineFactor * rgbMask;

                return fixed4(finalRGB, originalColor.a);
            }
            ENDCG
        }
    }
}
