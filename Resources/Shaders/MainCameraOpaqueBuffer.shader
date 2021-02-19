Shader "Custom/MainCameraZBuffer"
{
	Properties
	{
		//[HideInInspector] _MainTex( "Screen", 2D ) = "black" {}
		//[HideInInspector] _CameraOpaqueTexture("Depth Texture", 2D) = "white" {}
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
			sampler2D _CameraOpaqueTexture;

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
			
			fixed4 frag(v2f i) : SV_Target
			{
				return tex2D(_CameraOpaqueTexture, i.uv);
			}
			ENDCG
		}
	}
}
