Shader "Unlit/HealthBarShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Health ("Health", Float) = 0
    }
    SubShader
    {

        Pass{
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
            
                struct v2f {
                    float4 spos : POSITION;
                    float2 uv : TEXCOORD0;
                    float4 lpos : TEXCOORD1;
                };

                v2f vert(appdata_base v) {
                    v2f o;
                    o.spos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord;
                    o.lpos = v.vertex;
                    return o;
                }

                sampler2D _MainTex;
                float _Health;

                float4 frag(v2f i) : COLOR{
                    float4 col = tex2D(_MainTex,i.uv);
                    return col;
                }
            ENDCG
        }
    }
}
