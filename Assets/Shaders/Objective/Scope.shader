Shader "Unlit/Scope"
{
    Properties
    {
        _RenderTex ("Render Texture", 2D) = "white" {}
        _ScopeTex ("Scope Overlay", 2D) = "white" {}
        _Strength ("Strength", float) = .5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        ZWrite Off
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "distort.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 grabUV : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D   _RenderTex,
                        _ScopeTex;
            float4 _RenderTex_ST;
            float4 _ScopeTex_ST;
            float _Strength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _ScopeTex);
                o.grabUV = UNITY_PROJ_COORD(ComputeGrabScreenPos(o.vertex));
                return o;
            }

            

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.grabUV.xy / i.grabUV.w;
                uv = distortUV(i.uv, _Strength);

                fixed4 col;
                fixed4 grab =  tex2D(_RenderTex, uv);
                fixed4 scope = tex2D(_ScopeTex, i.uv);

                col =   grab * (1 - scope.a) +
                        scope * scope.a;
                return col;
            }
            ENDCG
        }
    }
}
