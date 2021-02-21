Shader "Custom/MainCameraZBuffer"
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

			#include "UnityCG.cginc"

			//sampler2D _MainTex;
			sampler2D _CameraDepthTexture;

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
				//const float n = 0.3; // camera z near
				//const float f = 1000.0; // camera z far
				float depth = tex2D(_CameraDepthTexture, i.uv).r; //get depth from depth texture
				depth = 1.0 - depth;
				//depth = LinearizeDepth(depth, n, f);
				depth = pow(depth, 2.0 * 2.71828); // Just for better visualization 
				return fixed4(depth, depth, depth, 1);
			}
			ENDCG
		}
	}
}
