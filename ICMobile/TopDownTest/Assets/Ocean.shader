Shader "Custom/Ocean" {
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_NoiseTex("Noise", 2D) = "white" {}
		magnitude("Magnitud", float) = 0
		amplitud("Amplitud", float) = 0
		_DirectionX("DirectionX", float) = 0
		_DirectionY("DirectionY", float) = 0
	}
		SubShader
		{
			Tags{ "RenderType" = "Opaque" }
			Pass
		{

			CGPROGRAM
	#pragma vertex vertFunc
	#pragma fragment fragFunc
	#include "UnityCG.cginc"
		float magnitude;
		float amplitud;
		sampler2D _MainTex;
		sampler2D _NoiseTex;
		float _DirectionX;
		float _DirectionY;

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
		//o.position.y += sin(tex2Dlod(_NoiseTex, float4(v.uv + frac(_Time.y * float2(_DirectionX, _DirectionY)),0,0)) * _Time * magnitude)/amplitud;
		o.position.y += tex2Dlod(_NoiseTex, float4(v.uv + float2(_Time * float2(_DirectionX, _DirectionY)), 0, 0)).rgb/amplitud;
		o.uv = v.uv; 

		return o;
	}

	fixed4 fragFunc(v2f i) : SV_Target
	{
		// sample the texture
		//fixed4 col = tex2D(_MainTex, i.uv);
	    fixed4 col = tex2D(_NoiseTex, i.uv + float2(_Time * float2(_DirectionX, _DirectionY)));

	return col;
	}
		ENDCG
	}
		}
}
