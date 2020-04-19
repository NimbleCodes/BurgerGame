Shader "Unlit/Blur" {

	Properties{
		_MainTex("Texture", 2D) = "white" {}
	}

	SubShader{
		Cull Off
		ZWrite Off
		ZTest Always

		Pass{
			CGPROGRAM
			#pragma vertex vertFunc
			#pragma fragment fragFunc
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vertFunc(appdata_base v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;

			float4 boxBlur(sampler2D tex, float2 uv, float4 size) {
				float4 c = tex2D(tex, uv + float2(-size.x, size.y))
					+ tex2D(tex, uv + float2(0, size.y))
					+ tex2D(tex, uv + float2(size.x, size.y))

					+ tex2D(tex, uv + float2(-size.x, 0))
					+ tex2D(tex, uv + float2(0, 0))
					+ tex2D(tex, uv + float2(size.x, 0))

					+ tex2D(tex, uv + float2(-size.x, -size.y))
					+ tex2D(tex, uv + float2(0, -size.y))
					+ tex2D(tex, uv + float2(size.x, -size.y));

				return c / 9;
			}

			float4 fragFunc(v2f i) : SV_Target{
				float4 col = boxBlur(_MainTex, i.uv, _MainTex_TexelSize);
				return col;	
			}
			ENDCG
		}
	}
}