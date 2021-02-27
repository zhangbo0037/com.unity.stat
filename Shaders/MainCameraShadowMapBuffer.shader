Shader "Custom/MainCameraShadowMapBuffer"
{
	Properties
	{
		//[HideInInspector] _MainTex( "Screen", 2D ) = "black" {}
	}
	SubShader
	{
		Cull Off
		ZWrite Off
		ZTest Always

		Tags { "RenderType" = "Opaque" }
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			//#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
			//#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCA

			#include "UnityCG.cginc"
			//#include "AutoLight.cginc"
			//#include "Lighting.cginc"

			//sampler2D _MainTex;
			sampler2D _MainLightShadowmapTexture;

			struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
			
			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			float LinearizeDepth(float z, float n, float f)
			{
				return (2.0 * n) / (f + n - z * (f - n));
			}

			fixed4 frag(v2f i) : SV_Target
			{
				i.uv.x /= 2.0; // We just display the shadow map of mipmap level 0
				float depth = tex2D(_MainLightShadowmapTexture, i.uv); //get depth from depth texture
				return 1.0 - depth;
			}
			ENDCG
		}
	}
}
