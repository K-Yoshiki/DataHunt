#ifndef MATERIALSURFACE_
#define MATERIALSURFACE_
float4 _Color;
float _Opacity;
float4 _AmbColor;
float4 _SpecularColor;
float _Shininess;
sampler2D _MainTex;

struct EditorSurfaceOutput
{
	half3 Albedo;
	half3 Normal;
	half3 Emission;
	half3 Gloss;
	half Specular;
	half Alpha;
	half4 Custom;
};

inline half4 LightingMMD(EditorSurfaceOutput s, half3 lightDir,half3 viewDir, half atten)
{
	float4 lightColor = _LightColor0 * 1.2 * atten;
	
	float specularStrength = s.Specular;
	float dirDotNormalHalf = max(0,dot(s.Normal,normalize(lightDir + viewDir)));
	float dirSpecularWeight = pow(dirDotNormalHalf,_Shininess);
	float4 dirSpecular = _SpecularColor * lightColor * dirSpecularWeight;
	
	float4 color = saturate(_AmbColor + (_Color * lightColor));
	color *= s.Custom;
	color += dirSpecular;
	color.a = s.Alpha;
	return color;
}

struct Input
{
	float2 uv_MainTex;
};

void surf(Input IN,inout EditorSurfaceOutput o)
{
	o.Albedo = 0.0;
	o.Emission = 0.0;
	o.Gloss = 0.0;
	o.Specular = 0.0;
	
	float2 uv_coord = float2(IN.uv_MainTex.x,IN.uv_MainTex.y);
	float4 tex_color = tex2D(_MainTex,uv_coord);
	
	float3 viewNormal = normalize(mul(UNITY_MATRIX_MV,float4(normalize(o.Normal),0.0)).xyz);
	
	o.Custom = tex_color;
	o.Custom.a = 1.0;
	o.Alpha = _Opacity + tex_color.a;
}

#endif