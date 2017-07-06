// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Ocean2.0" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Diffuse Material Color", Color) = (1,1,1,1)
		_SpecColor("Specular Material Color", Color) = (1,1,1,1)   //LIGHTING
		_Shininess("Shininess", Float) = 10

		_NoiseTex("Noise", 2D) = "white" {}
		amplitud("Amplitud", Range(0.5,15)) = 0
		_DirectionX("DirectionX", Range(-10,10)) = 0               //WATER MOVEMENT
		_DirectionY("DirectionY", Range(-10,10)) = 0
	}
		SubShader{
		Pass{
		Tags{ "LightMode" = "ForwardBase" }
		// pass for ambient light and first light source

		CGPROGRAM

#pragma vertex vert addshadow
#pragma fragment frag 

#include "UnityCG.cginc"
		uniform float4 _LightColor0;
		//color of light source (from "Lighting.cginc")

		// User-specified properties
		uniform float4 _Color;
		uniform float4 _SpecColor;  //LIGHTING
		uniform float _Shininess;
		sampler2D _MainTex;

		float amplitud;
		sampler2D _NoiseTex;
		float _DirectionX;          //WATER MOVEMENT 
		float _DirectionY;

		struct vertexInput {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float2 uv : TEXCOORD0;
		};
		struct vertexOutput {
			float4 pos : SV_POSITION;
			float4 posWorld : TEXCOORD0;
			float3 normalDir : TEXCOORD1;
		};

		vertexOutput vert(vertexInput input)
		{
			vertexOutput output;

			float4x4 modelMatrix = unity_ObjectToWorld;
			float4x4 modelMatrixInverse = unity_WorldToObject;

			output.posWorld = mul(modelMatrix, input.vertex); //Initialize text coord positions with the model
			output.normalDir = normalize(mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);//Initialize normals
			output.pos = UnityObjectToClipPos(input.vertex); //Initialize vertex posstions

			output.posWorld.xy = input.uv; //Set the uvs 
			output.pos.y += tex2Dlod(_NoiseTex, float4(input.uv + float2(_Time * float2(_DirectionX, _DirectionY)), 0, 0)).rgb / amplitud; //Change vertex positions

			return output;
		}

		float4 frag(vertexOutput input) : COLOR
		{
			fixed4 col = tex2D(_MainTex, input.posWorld.xy);
			float3 normalDirection = normalize(input.normalDir);
			float3 viewDirection = normalize(_WorldSpaceCameraPos - input.posWorld.xyz);
			float3 lightDirection;
			float attenuation;

			if (0.0 == _WorldSpaceLightPos0.w) // directional light?
			{
				attenuation = 1.0; // no attenuation
				lightDirection = normalize(_WorldSpaceLightPos0.xyz);
			}
			else // point or spot light
			{
				float3 vertexToLightSource =
					_WorldSpaceLightPos0.xyz - input.posWorld.xyz;
				float distance = length(vertexToLightSource);
				attenuation = 1.0 / distance; // linear attenuation 
				lightDirection = normalize(vertexToLightSource);
			}

			float3 ambientLighting =
				UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;

			float3 diffuseReflection =
				attenuation * _LightColor0.rgb * _Color.rgb
				* max(0.0, dot(normalDirection, lightDirection));

			float3 specularReflection;
			if (dot(normalDirection, lightDirection) < 0.0)
				// light source on the wrong side?
			{
				specularReflection = float3(0.0, 0.0, 0.0);
				// no specular reflection
			}
			else // light source on the right side
			{
				specularReflection = attenuation * _LightColor0.rgb
					* _SpecColor.rgb * pow(max(0.0, dot(
						reflect(-lightDirection, normalDirection),
						viewDirection)), _Shininess);
			}

			return float4(ambientLighting + diffuseReflection + specularReflection + col, 1.0);
		}
			ENDCG
		}

		//----------------------------------------------------------------------------------------------------------------------------
			Pass{
			Tags{ "LightMode" = "ForwardAdd" }
			// pass for additional light sources
			Blend One One // additive blending 

			CGPROGRAM

	#pragma vertex vert  addshadow
	#pragma fragment frag 

	#include "UnityCG.cginc"
			uniform float4 _LightColor0;
		// color of light source (from "Lighting.cginc")

		// User-specified properties
		uniform float4 _Color;
		uniform float4 _SpecColor;
		uniform float _Shininess;

		sampler2D _MainTex;
		float amplitud;
		sampler2D _NoiseTex;
		float _DirectionX;          //WATER MOVEMENT 
		float _DirectionY;

		struct vertexInput {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float2 uv : TEXCOORD0;
		};
		struct vertexOutput {
			float4 pos : SV_POSITION;
			float4 posWorld : TEXCOORD0;
			float3 normalDir : TEXCOORD1;
		};

		vertexOutput vert(vertexInput input)
		{
			vertexOutput output;

			float4x4 modelMatrix = unity_ObjectToWorld;
			float4x4 modelMatrixInverse = unity_WorldToObject;

			output.posWorld = mul(modelMatrix, input.vertex);
			output.normalDir = normalize(mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);
			output.pos = UnityObjectToClipPos(input.vertex);

			//output.posWorld.xy = input.uv; //Set the uvs 
			output.pos.y += tex2Dlod(_NoiseTex, float4(input.uv + float2(_Time * float2(_DirectionX, _DirectionY)), 0, 0)).rgb / amplitud; //Change vertex positions

			return output;
		}

		float4 frag(vertexOutput input) : COLOR
		{
			fixed4 col = tex2D(_MainTex, input.posWorld.xy);
			float3 normalDirection = normalize(input.normalDir);

			float3 viewDirection = normalize(
				_WorldSpaceCameraPos - input.posWorld.xyz);
			float3 lightDirection;
			float attenuation;

			if (0.0 == _WorldSpaceLightPos0.w) // directional light?
			{
				attenuation = 1.0; // no attenuation
				lightDirection = normalize(_WorldSpaceLightPos0.xyz);
			}
			else // point or spot light
			{
				float3 vertexToLightSource =
					_WorldSpaceLightPos0.xyz - input.posWorld.xyz;
				float distance = length(vertexToLightSource);
				attenuation = 1.0 / distance; // linear attenuation 
				lightDirection = normalize(vertexToLightSource);
			}

			float3 diffuseReflection =
				attenuation * _LightColor0.rgb * _Color.rgb
				* max(0.0, dot(normalDirection, lightDirection));

			float3 specularReflection;
			if (dot(normalDirection, lightDirection) < 0.0)
				// light source on the wrong side?
			{
				specularReflection = float3(0.0, 0.0, 0.0);
				// no specular reflection
			}
			else // light source on the right side
			{
				specularReflection = attenuation * _LightColor0.rgb
					* _SpecColor.rgb * pow(max(0.0, dot(
						reflect(-lightDirection, normalDirection),
						viewDirection)), _Shininess);
			}

			return float4(diffuseReflection
				+ specularReflection, 1.0);
			// no ambient lighting in this pass
		}

			ENDCG
		}
		}
			Fallback "Specular"
}
