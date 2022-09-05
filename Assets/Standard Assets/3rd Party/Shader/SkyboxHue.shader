Shader "Skybox/SkyboxHue"
{
	Properties
	{
		_Color1("Color 1", Color) = (1.0, 0.5, 0.5, 1.0)
		_Color2("Color 2", Color) = (0.5, 0.5, 1.0, 1.0)
		_Scale("Scale", Float) = 1.0
		_Intensity("Intensity", Range(0, 5)) = 1
		_HueShift("Hue Shift", Range(0, 1)) = 0
	}

	SubShader
	{
		Cull Off
		ZWrite Off

		Tags
		{
			"Queue" = "Geometry"
			"PreviewType" = "Skybox"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag

			float4 _Color1;
			float4 _Color2;
			float4 _Hue;
			float  _Scale;
			float _Intensity;
			float _HueShift;

			struct a2v
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				float4 position : TEXCOORD0;
			};

			struct f2g
			{
				float4 color : SV_TARGET;
			};

			void Vert(a2v i, out v2f o)
			{
				o.vertex = o.position = UnityObjectToClipPos(i.vertex);
			}

			float Epsilon = 1e-10;
			float3 RGBtoHCV(in float3 RGB)
			{
				float4 P = (RGB.g < RGB.b) ? float4(RGB.bg, -1.0, 2.0 / 3.0) : float4(RGB.gb, 0.0, -1.0 / 3.0);
				float4 Q = (RGB.r < P.x) ? float4(P.xyw, RGB.r) : float4(RGB.r, P.yzx);
				float C = Q.x - min(Q.w, Q.y);
				float H = abs((Q.w - Q.y) / (6 * C + Epsilon) + Q.z);
				return float3(H, C, Q.x);
			}
			float3 HUEtoRGB(in float H)
			{
				float R = abs(H * 6 - 3) - 1;
				float G = 2 - abs(H * 6 - 2);
				float B = 2 - abs(H * 6 - 4);
				return saturate(float3(R, G, B));
			}
			float3 HSVtoRGB(in float3 HSV)
			{
				float3 RGB = HUEtoRGB(HSV.x);
				return ((RGB - 1) * HSV.y + 1) * HSV.z;
			}
			float3 RGBtoHSV(in float3 RGB)
			{
				float3 HCV = RGBtoHCV(RGB);
				float S = HCV.y / (HCV.z + Epsilon);
				return float3(HCV.x, S, HCV.z);
			}

			void Frag(v2f i, out f2g o)
			{
				float4 color1 = _Color1 * _Intensity;
				float4 color2 = _Color2 * _Intensity;

				float3 hsv = RGBtoHSV(color1.rgb.xyz);
				hsv.x += _HueShift;
				color1.rgb = half3(HSVtoRGB(hsv));

				hsv = RGBtoHSV(color2.rgb.xyz);
				hsv.x += _HueShift;
				color2.rgb = half3(HSVtoRGB(hsv));

				o.color = lerp(color1, color2, length(i.position.xy / i.position.w) * _Scale);
			}
			ENDCG
		} // Pass
	} // SubShader
} // Shader