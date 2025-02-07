﻿Shader "Unlit/UIGlowEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurredTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
       Cull Off ZWrite Off ZTest Always
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _BlurredTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 bTcol = tex2D(_BlurredTex, i.uv);
                bTcol.rgb *= bTcol.a;
                col.rgb += bTcol.rgb * 0.1;
                return col;
            }
            ENDCG
        }
    }
}
