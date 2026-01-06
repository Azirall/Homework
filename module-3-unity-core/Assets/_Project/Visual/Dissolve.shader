Shader "Custom/MyDissolve"
{
    Properties
    {
        _Color     ("Color", Color) = (1,1,1,1)
        _NoiseTex  ("Noise", 2D)    = "white" {}

        _Cutoff    ("Cutoff", Range(0,1))      = 0.0
        _EdgeWidth ("Edge Width", Range(0,0.5)) = 0.05
        [HDR] _EdgeColor ("Edge Color", Color) = (1,0.5,0,1)

        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="TransparentCutout"
        }

        Cull [_Cull]
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;

            float4 _Color;
            float4 _EdgeColor;
            float  _Cutoff;
            float  _EdgeWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv  = TRANSFORM_TEX(v.uv, _NoiseTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float noise = tex2D(_NoiseTex, i.uv).r;

                if (noise < _Cutoff)
                    discard;

                float w = max(_EdgeWidth, 1e-5);
                float edge = saturate((_Cutoff + w - noise) / w);

                fixed4 col = _Color;
                col.rgb = lerp(col.rgb, _EdgeColor.rgb, edge);

                return col;
            }
            ENDCG
        }
    }

    FallBack Off
}
