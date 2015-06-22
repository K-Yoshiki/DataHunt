Shader "Custom/OutLine" {
	Properties {
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_Opacity("Opacity",Float) = 1.0
		_OutlineColor("Outline Color",Color) = (0,0,0,1)
		_Outline("Outline width",Range(0.002,0.03)) = 0.005
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	
	
	
	
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	struct appdata
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};
	
	struct v2f 
	{
		float4 pos : POSITION;
		float4 color : COLOR;
	};
	
	uniform float _Outline;
	uniform float4 _OutlineColor;
	
	v2f vert(appdata v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		
		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV,v.normal);
		float2 offset = TransformViewToProjection(norm.xy);
		
		o.pos.xy += offset * o.pos.z * _Outline;
		
		o.color = _OutlineColor;
		return o;
	}
	
	
	ENDCG
	
	SubShader{
		Tags{"RenderType" = "Transparent"
		"Queue" = "Transparent+1"}
		
		
		
		UsePass "Custom/OutLine-Base/BASE"
		Pass{
			Name"OUTLINE"
			Tags{"LightMode" = "Always"}
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			half4 frag(v2f i) : COLOR{return i.color;}
			ENDCG
		}
	}
	
	
	
	SubShader
	{
		Tags{"RenderType" = "Transparent"
		"Queue" = "Transparent+1"}
		
		Cull Back
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha
		
		
		UsePass "Custom/OutLine-Base/BASE"
		Pass{
			Name "OUTLINE"
			Tags{"LightMode" = "Always"}
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			
			CGPROGRAM
			#pragma vertex vert
			#pragma exclude_renderers shaderonly
			ENDCG
			//SetTexture[_MainTex]{combine primary}
		}
	}
	
	SubShader
	{
	
		Tags{ "Queue" = "Transparent"}
		CGPROGRAM
		#pragma surface surf Lambert alpha
		#include "UnityCG.cginc"
		
	
		sampler _MainTex;
		float _Opacity;
		
		struct Input
		{
			float2 uv_MainTex;
			float4 color : COLOR;
		};
	
		void surf(Input IN, inout SurfaceOutput o)
		{
			half4 uv_color = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = uv_color.rgb;
			o.Alpha = uv_color + _Opacity;
		}
	
		ENDCG
		
	}
	
	Fallback "Custom/OutLine-Base/BASE"
}
