Shader "Custom/Ocean" {
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_NoiseTex("Noise", 2D) = "white" {}
		magnitude("Magnitud", float) = 0
	}
		SubShader
		{
			Pass
		{
			CGPROGRAM
	#pragma vertex vertFunc
	#pragma fragment fragFunc
	#include "UnityCG.cginc"
		float magnitude;
		sampler2D _MainTex;
		sampler2D _NoiseTex;


		struct appdata
	{
		float4 position : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float4 position : SV_POSITION;
		float2 uv : TEXCOORD0;
	};


	v2f vertFunc(appdata v)
	{
		v2f o;
		o.position = mul(UNITY_MATRIX_MVP, v.position);
		o.position.y += sin(tex2Dlod(_NoiseTex, v.position).r * _Time * magnitude);
		o.uv = v.uv;

		return o;
	}

	fixed4 fragFunc(v2f i) : SV_Target
	{
		// sample the texture
		fixed4 col = tex2D(_MainTex, i.uv);

	return col;
	}
		ENDCG
	}
		}
}
