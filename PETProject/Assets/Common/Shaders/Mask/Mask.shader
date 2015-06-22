Shader "Custom/Mask" {
	SubShader {
		Tags{ "Queue" = "Geometry+10"}
		
			ColorMask 0
			Zwrite On
			
			Pass{}		
		}
} 

