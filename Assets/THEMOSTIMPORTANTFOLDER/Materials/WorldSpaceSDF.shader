Shader "Unlit/WorldSpaceSDF"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVertBubble
            #pragma fragment SpriteFragBubble
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            
            #include "UnitySprites.cginc"
            
            float4 _CenterPoint;
            float4 _EnemyColor;
            
            #define CENTER _CenterPoint.xyz
            #define RADIUS _CenterPoint.w


            struct v2fBubble
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;

                float4 worldPos : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            
            v2fBubble SpriteVertBubble(appdata_t IN)
            {
                v2fBubble OUT;

                UNITY_SETUP_INSTANCE_ID (IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPos = mul (unity_ObjectToWorld, IN.vertex);

                OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
                OUT.vertex = UnityObjectToClipPos(OUT.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color * _RendererColor;

                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }
            
            fixed4 SpriteFragBubble(v2fBubble IN) : SV_Target
            {
                fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
                float distance = length( IN.worldPos - CENTER );
                float multiplier = step( distance, RADIUS );

                c.rgb = lerp(c.rgb, _EnemyColor, multiplier);

                c.rgb *= c.a;
                return c;
            }

        ENDCG
        }
    }
}
