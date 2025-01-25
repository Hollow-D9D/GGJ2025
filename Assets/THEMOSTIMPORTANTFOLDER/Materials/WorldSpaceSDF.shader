Shader "Unlit/WorldSpaceSDF"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _CenterPoint ("Center Point", Vector) = ( .0, .0, .0, 1.0 )
        _Color ("SDF Color", Color) = (.25, .5, .5, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

#define CENTER _CenterPoint.xyz
#define RADIUS _CenterPoint.w

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;

                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _CenterPoint;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;

                o.worldPos = mul (unity_ObjectToWorld, v.vertex);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                float distance = length( i.worldPos - CENTER );
                float multiplier = step( distance, RADIUS );

                col.rgb = lerp(col.rgb, _Color, multiplier);
                
                return col;
            }
            ENDCG
        }
    }
}
