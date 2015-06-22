Shader "Custom/RimLight HeightMap" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap("Bumpmap",2D) = "bump"{}
		_RimColor ("Rim Color",Color) = (0.26,0.19,0.16,0.0)
		_RimPower("Rim Power",Range(0.5,8.0)) = 3.0
		_HeightMap("HeightMap",2D) = "gray"{}
		_HeightmapStrength("Heightmap Strength",Float) = 1.0
		_HeightmapDimX("Heightmap Width",Float) = 2048
		_HeightmapDimY("Heightmap Height",Float) = 2048
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		
		CGPROGRAM
		#pragma surface surf Lambert

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 viewDir;
		};
		
		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _HeightMap;
		float4 _RimColor;
		float _RimPower;
		float _HeightmapStrength,_HeightmapDimX,_HeightmapDimY;

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex,IN.uv_MainTex).rgb;
			
			float3 normal = UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap));
			
			float me = tex2D(_HeightMap,IN.uv_MainTex).x;
			float n = tex2D(_HeightMap,float2(IN.uv_MainTex.x,IN.uv_MainTex.y+1.0/_HeightmapDimY)).x;
			float s = tex2D(_HeightMap,float2(IN.uv_MainTex.x,IN.uv_MainTex.y-1.0/_HeightmapDimY)).x;
			float e = tex2D(_HeightMap,float2(IN.uv_MainTex.x-1.0/_HeightmapDimX,IN.uv_MainTex.y)).x;
			float w = tex2D(_HeightMap,float2(IN.uv_MainTex.x+1.0/_HeightmapDimX,IN.uv_MainTex.y)).x;
			
			float3 norm = normal;
			float3 temp = norm;
			if(norm.x == 1)
				temp.y += 0.5;
			else
				temp.y += 0.5;
			
			float3 perp1 = normalize(cross(norm,temp));
			float3 perp2 = normalize(cross(norm,perp1));
			
			float3 normalOffset = -_HeightmapStrength * (((n -me)-(s-me)) * perp1 + ((e - me) - (w -me)) * perp2);
			norm += normalOffset;
			norm = normalize(norm);
			
			o.Normal = norm;
			
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir),o.Normal));
			o.Emission = _RimColor.rgb * pow(rim,_RimPower);
		}
		
		inline fixed4 LightingNormalHeight(SurfaceOutput s,fixed3 lightDir,fixed3 viewDir,fixed atten)
		{
			viewDir = normalize(viewDir);
			lightDir = normalize(lightDir);
			s.Normal = normalize(s.Normal);
			float NdotL = dot(s.Normal,lightDir);
			_LightColor0.rgb = _LightColor0.rgb;
			
			fixed4 c;
			c.rgb = float3(0.5) * saturate(NdotL) * _LightColor0.rgb * atten;
			c.a = 1.0;
			return c;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
