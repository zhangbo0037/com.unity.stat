Shader "Custom/MainCameraZBuffer"
{
	Properties
	{
		//[HideInInspector] _MainTex( "Screen", 2D ) = "black" {}
		//[HideInInspector] _CameraDepthTexture("Depth Texture", 2D) = "white" {}
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
			
			fixed4 frag(v2f i) : SV_Target
			{
				float depth = tex2D(_CameraDepthTexture, i.uv).r; //get depth from depth texture
				//depth = Linear01Depth(depth); //linear depth between camera and far clipping plane
				//depth = depth * _ProjectionParams.z; //depth as distance from camera in units
				return fixed4(depth, depth, depth, 1);
			}
			ENDCG
		}
	}
}
