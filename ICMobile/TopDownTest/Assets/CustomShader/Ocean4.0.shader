// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Ocean4.0" {
	Properties{
		_Color("Diffuse Material Color", Color) = (1,1,1,1)

		_Cube("Reflection Map", Cube) = "" {}

	_MainTex("Texture", 2D) = "white" {}
	_NoiseTex("Noise", 2D) = "white" {}
	amplitud("Amplitud", Range(0.5,15)) = 0
		_DirectionX("DirectionX", Range(-10,10)) = 0               //WATER MOVEMENT
		_DirectionY("DirectionY", Range(-10,10)) = 0

	}
		SubShader{
		Pass{
		Tags{ "LightMode" = "ForwardBase" }
		// pass for first light source


		CGPROGRAM

#pragma vertex vert  
#pragma fragment frag 

#include "UnityCG.cginc"

		uniform float4 _LightColor0;
	// color of light source (from "Lighting.cginc")

	uniform float4 _Color; // define shader property for shaders

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float amplitud;
	sampler2D _NoiseTex;
	float4 _NoiseTex_ST;
	float _DirectionX;          //WATER MOVEMENT 
	float _DirectionY;

	uniform samplerCUBE _Cube;

	struct vertexInput {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		float2 uv : TEXCOORD0;
	};
	struct vertexOutput {
		float4 pos : SV_POSITION;
		float4 col : COLOR;

		float3 normalDir : TEXCOORD0;
		float3 viewDir : TEXCOORD1;
	};

	/*float4 getNewVertPosition(float4 p, float2 uv)
	{
		p.y += tex2Dlod(_NoiseTex, float4(TRANSFORM_TEX(uv, _NoiseTex) + float2(_Time * float2(_DirectionX, _DirectionY)), 0, 0)).rgb / amplitud;
		return p;
	}*/

	vertexOutput vert(vertexInput input)
	{
		vertexOutput output;

		float4x4 modelMatrix = unity_ObjectToWorld;
		float4x4 modelMatrixInverse = unity_WorldToObject;

		output.pos = UnityObjectToClipPos(input.vertex);

		output.pos.y += tex2Dlod(_NoiseTex, float4(TRANSFORM_TEX(input.uv, _NoiseTex) + float2(_Time * float2(_DirectionX, _DirectionY)), 0, 0)).rgb / amplitud; //Change vertex positions
		//output.pos.y = getNewVertPosition(output.pos, input.uv).y;

		float3 tangent = input.normal + float3(0, 0, 1);

		float3 bitangent = cross(input.normal, tangent);

		float4 position = output.pos;
		float4 positionAndTangent = output.pos + float4(tangent,1.0) * 0.01;
		float4 positionAndBitangent =output.pos + float4(bitangent,1.0) * 0.01;

		float4 newTangent = (positionAndTangent - position); // leaves just 'tangent'
		float4 newBitangent = (positionAndBitangent - position); // leaves just 'bitangent'

		float3 newNormal = cross(newTangent, newBitangent);

		float3 normalDirection = normalize(
			mul(float4(newNormal, 0.0), modelMatrixInverse).xyz);
		float3 lightDirection;
		float attenuation;

		float4 texColor = tex2Dlod(_MainTex, float4(TRANSFORM_TEX(input.uv, _MainTex), 0, 0));

		if (0.0 == _WorldSpaceLightPos0.w) // directional light?
		{
			attenuation = 1.0; // no attenuation
			lightDirection = normalize(_WorldSpaceLightPos0.xyz);
		}
		else // point or spot light
		{
			float3 vertexToLightSource = _WorldSpaceLightPos0.xyz
				- mul(modelMatrix, input.vertex).xyz;
			float distance = length(vertexToLightSource);
			attenuation = 1.0 / distance; // linear attenuation 
			lightDirection = normalize(vertexToLightSource);
		}


		float3 diffuseReflection =
			attenuation * _LightColor0.rgb * _Color.rgb * texColor
			* max(0.0, dot(normalDirection, lightDirection));

		output.col = float4(diffuseReflection, 1.0);

		output.viewDir = mul(modelMatrix, input.vertex).xyz
			- _WorldSpaceCameraPos;
		output.normalDir = normalize(mul(float4(normalDirection, 0.0), modelMatrixInverse).xyz);


		return output;
	}

	float4 frag(vertexOutput input) : COLOR
	{
		float3 reflectedDir =
		reflect(input.viewDir, normalize(input.normalDir));
		return input.col * texCUBE(_Cube, reflectedDir);
	}

		ENDCG
	}

		Pass{
		Tags{ "LightMode" = "ForwardAdd" }
		// pass for additional light sources
		Blend One One // additive blending 

		CGPROGRAM

#pragma vertex vert  
#pragma fragment frag 

#include "UnityCG.cginc"

		uniform float4 _LightColor0;

	// color of light source (from "Lighting.cginc")

	uniform float4 _Color; // define shader property for shaders

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float amplitud;
	sampler2D _NoiseTex;
	float4 _NoiseTex_ST;
	float _DirectionX;          //WATER MOVEMENT 
	float _DirectionY;

	uniform samplerCUBE _Cube;

	struct vertexInput {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		float2 uv : TEXCOORD0;
	};
	struct vertexOutput {
		float4 pos : SV_POSITION;
		float4 col : COLOR;

		float3 normalDir : TEXCOORD0;
		float3 viewDir : TEXCOORD1;
	};


	/*float4 getNewVertPosition(float4 p, float2 uv)
	{
		p.y += tex2Dlod(_NoiseTex, float4(TRANSFORM_TEX(uv, _NoiseTex) + float2(_Time * float2(_DirectionX, _DirectionY)), 0, 0)).rgb / amplitud;
		return p;
	}*/

	vertexOutput vert(vertexInput input)
	{
		vertexOutput output;

		float4x4 modelMatrix = unity_ObjectToWorld;
		float4x4 modelMatrixInverse = unity_WorldToObject;

		output.pos = UnityObjectToClipPos(input.vertex);

		output.pos.y += tex2Dlod(_NoiseTex, float4(TRANSFORM_TEX(input.uv, _NoiseTex) + float2(_Time * float2(_DirectionX, _DirectionY)), 0, 0)).rgb / amplitud; //Change vertex positions
		//output.pos.y = getNewVertPosition(output.pos, input.uv).y;


		float3 tangent = input.normal + float3(0, 0, 1);

		float3 bitangent = cross(input.normal, tangent);

		float4 position = output.pos;
		float4 positionAndTangent = output.pos + float4(tangent, 1.0) * 0.01;
		float4 positionAndBitangent = output.pos + float4(bitangent, 1.0) * 0.01;

		float4 newTangent = (positionAndTangent - position); // leaves just 'tangent'
		float4 newBitangent = (positionAndBitangent - position); // leaves just 'bitangent'

		float3 newNormal = cross(newTangent, newBitangent);

		float3 normalDirection = normalize(
			mul(float4(newNormal, 0.0), modelMatrixInverse).xyz);
		float3 lightDirection;
		float attenuation;

		float4 texColor = tex2Dlod(_MainTex, float4(TRANSFORM_TEX(input.uv, _MainTex), 0, 0));

		if (0.0 == _WorldSpaceLightPos0.w) // directional light?
		{
			attenuation = 1.0; // no attenuation
			lightDirection = normalize(_WorldSpaceLightPos0.xyz);
		}
		else // point or spot light
		{
			float3 vertexToLightSource = _WorldSpaceLightPos0.xyz
				- mul(modelMatrix, input.vertex).xyz;
			float distance = length(vertexToLightSource);
			attenuation = 1.0 / distance; // linear attenuation 
			lightDirection = normalize(vertexToLightSource);
		}

		float3 diffuseReflection =
			attenuation * _LightColor0.rgb * _Color.rgb * texColor
			* max(0.0, dot(normalDirection, lightDirection));

		output.col = float4(diffuseReflection, 1.0);
		output.viewDir = mul(modelMatrix, input.vertex).xyz
			- _WorldSpaceCameraPos;
		output.normalDir = normalize(mul(float4(normalDirection, 0.0), modelMatrixInverse).xyz);


		return output;
	}

	float4 frag(vertexOutput input) : COLOR
	{
		float3 reflectedDir =
		reflect(input.viewDir, normalize(input.normalDir));
		return input.col * texCUBE(_Cube, reflectedDir);
	}

		ENDCG
	}
	}
		Fallback "Diffuse"
}
