// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Outline"
{
    Properties
    {
        _OutlineMod ("Outline Modifier", Float) = 2.0
        _OutlineColor ("Outline Color", Color) = (1, 1, 1, 1)
        _Cutoff ("Cutoff", Float) = 0.5
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
            uniform float _OutlineMod;
            uniform float4 _OutlineColor;
            uniform float _Cutoff;
//           uniform float4 _WorldSpaceCameraPos;
//           uniform float4 unity_ObjectToWorld;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD1;
            };

            v2f vert (appdata v){
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize((_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz)*_OutlineMod);
                return o;
            }

            fixed4 frag (v2f i) : SV_TARGET
            {
                float ndotv = 1.0 - dot(i.normal, i.viewDir);
                if(ndotv < _Cutoff) ndotv = 0;
                ndotv *= _OutlineMod;
                return float4(_OutlineColor.r * ndotv, _OutlineColor.g * ndotv, _OutlineColor.b * ndotv, 0);
            }

            ENDCG
        }
    }
}
