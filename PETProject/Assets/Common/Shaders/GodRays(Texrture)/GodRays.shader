Shader "Custom/GodRays" {
	Properties {
		tDiffuse("Base (RGB)",2D) = "white"{}
		fX("fX",Float) = 0.5
		fY("fY",Float) = 0.5
		fExposure("fExposure",Float) = 0.6
		fDecay("fDecay",Float) = 0.93
		fDensity("fDensity",Float) = 0.96
		fWeight("fWeight",Float) = 0.4
		fClamp("fClamp",Float) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Cull Off
		
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert

		sampler2D tDiffuse;
		float fX,fY,fExposure,fDecay,fDensity,fWeight,fClamp,iSamples;

		struct Input {
			float2 uvtDiffuse;
			float4 screenPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			int iSamples = 100;
			float2 vUv = IN.uvtDiffuse;
			float2 deltaTexCoord = float2(vUv - float2(fX,fY));
			deltaTexCoord *= 1.0 / float(iSamples) * fDensity;
			float2 coord = vUv;
			float illuminationDecay = 1.0;
			float4 FragColor = float4(0.0);
			for(int i = 0; i < iSamples; i++)
			{
				coord -= deltaTexCoord;
				float4 texel = tex2D(tDiffuse,coord);
				texel *= illuminationDecay * fWeight;
				FragColor += texel;
				illuminationDecay *= fDecay;
			}
			FragColor *= fExposure;
			FragColor = clamp(FragColor,0.0,fClamp);
			float4 c = FragColor;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
