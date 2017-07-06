// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Ocean" {
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_NoiseTex("Noise", 2D) = "white" {}
		amplitud("Amplitud", Range(0.5,15)) = 0
		_DirectionX("DirectionX", Range(-10,10)) = 0
		_DirectionY("DirectionY", Range(-10,10)) = 0
		_Color("Color", Color) = (1.0,1.0,1.0,1.0)
	}
		SubShader
		{	
			Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Tags{ "LightMode" = "ForwardBase" }
			CGPROGRAM
#pragma target 2.0
	#pragma vertex vertFunc
	#pragma fragment fragFunc
	#include "UnityCG.cginc"
		float amplitud;
		sampler2D _MainTex;
		sampler2D _NoiseTex;
		float _DirectionX;
		float _DirectionY;
		float4 _Color;
		float4 _LightColor0;

		struct appdata
	{
		float4 position : POSITION;
		float2 uv : TEXCOORD0;
		float3 normal : NORMAL;
	};

	struct v2f
	{
		float4 position : SV_POSITION;
		float2 uv : TEXCOORD0;
		float3 normal : NORMAL;
	};


	v2f vertFunc(appdata v)
	{
		v2f o;
		o.position = UnityObjectToClipPos(v.position);
		//o.position.y += sin(tex2Dlod(_NoiseTex, float4(v.uv + frac(_Time.y * float2(_DirectionX, _DirectionY)),0,0)) * _Time * magnitude)/amplitud;
		o.position.y += tex2Dlod(_NoiseTex, float4(v.uv + float2(_Time * float2(_DirectionX,_DirectionY)), 0, 0)).rgb/amplitud;
		o.uv = v.uv;
		o.normal = normalize(mul(v.normal, unity_WorldToObject));

		return o;
	}

	fixed4 fragFunc(v2f i) : SV_Target
	{
		// sample the texture
	fixed4 col = tex2D(_MainTex, i.uv) * _Color;
	//fixed4 col = tex2D(_NoiseTex, i.uv + float2(_Time * float2(_DirectionX, _DirectionY))) * _Color;
	float4 ambientLight = UNITY_LIGHTMODEL_AMBIENT;
	float4 lightDirection = normalize(_WorldSpaceLightPos0);
	float4 diffuseTerm = saturate(dot(lightDirection, i.normal));
	float4 diffuseLight = diffuseTerm * _LightColor0;

	float4 cameraPosition = normalize(float4(_WorldSpaceCameraPos, 1) - i.position);

	// Blinn-Phong
	//float4 halfVector = normalize(lightDirection + cameraPosition);
	//float4 specularTerm = pow(saturate(dot(i.normal, halfVector)), 25);

	// Phong
	float4 reflectionVector = reflect(-lightDirection, float4(i.normal,1));
	float4 specularTerm = pow(saturate(dot(reflectionVector, cameraPosition)),15);

	//https://digitalerr0r.wordpress.com/2015/10/26/unity-5-shader-programming-3-specular-light/

	return col * (ambientLight + diffuseLight + specularTerm);
	}
		ENDCG
	}
		}
}
