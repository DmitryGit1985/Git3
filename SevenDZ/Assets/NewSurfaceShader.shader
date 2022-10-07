Shader "Custom/SquareBender" {
    Properties{
        _MainTex("Tex", 2D) = "" {}
        _SphereCenter("SphereCenter", Vector) = (0, 0, 0, 1)
        _SphereRadius("SphereRadius", Float) = 5
    }

        SubShader{
            Pass {
                CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag

                    #include "UnityCG.cginc"

                    struct appdata {
                       float2 uv     : TEXCOORD0;
                       float2 lonLat : TEXCOORD1;
                    };

                    struct v2f
                    {
                        float4 pos  : SV_POSITION;
                        float3 norm : NORMAL;
                        float2 uv   : TEXCOORD0;
                    };

                    float4 _SphereCenter;
                    float _SphereRadius;

                    v2f vert(appdata v)
                    {
                        v2f o;
                        float lon = v.lonLat.x;
                        float lat = v.lonLat.y;

                        fixed4 posOffsetWorld = fixed4(
                            _SphereRadius * cos(lat) * cos(lon),
                            _SphereRadius * sin(lat),
                            _SphereRadius * cos(lat) * sin(lon), 0);

                        float4 posObj = mul(unity_WorldToObject,
                                posOffsetWorld + _SphereCenter);

                        o.pos = UnityObjectToClipPos(posObj);
                        o.uv = v.uv;
                        o.norm = mul(unity_WorldToObject, posOffsetWorld);
                        return o;
                    }

                    sampler2D _MainTex;

                    float4 frag(v2f IN) : COLOR
                    {
                        fixed4 col = tex2D(_MainTex, IN.uv);
                        return col;
                    }
                ENDCG
            }
        }
            FallBack "VertexLit"
}
