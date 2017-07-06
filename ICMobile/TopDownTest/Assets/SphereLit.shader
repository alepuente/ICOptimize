Shader "ImageCampus/SphereLit"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_SphereMap("Sphere map", 2D) = "black" {}
	}

		SubShader
	{
		Tags{ "RenderType" = "Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			float4 _Color;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float2 uv2 : TEXCOORD1;
			};

			sampler2D _MainTex;
			sampler2D _SphereMap;

			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv2 = mul((float3x3)UNITY_MATRIX_IT_MV, normalize(v.normal)).xy * .5 + .5;
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				// sample the texture
				float4 lit = tex2D(_SphereMap, i.uv2);
				float4 col = tex2D(_MainTex, i.uv);

				col = col * lit * 1.5*_Color;
				col.a = 1;

				return col;
			}
			ENDCG
		}
	}
}