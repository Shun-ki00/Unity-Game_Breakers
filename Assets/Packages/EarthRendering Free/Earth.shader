Shader "Earth"
{
    Properties 
    {
        _AtmosphereColor ("Atmosphere Color", Color) = (0.1, 0.35, 1.0, 1.0)
        _AtmospherePow ("Atmosphere Power", Range(1.5, 8)) = 2
        _AtmosphereMultiply ("Atmosphere Multiply", Range(1, 3)) = 1.5

        _DiffuseTex("Diffuse", 2D) = "white" {}
        _CloudAndNightTex("Cloud And Night", 2D) = "black" {}

        // 追加: ライト方向をマテリアル側から制御可能にする
        _LightDir("Light Direction", Vector) = (0, 0, -1, 0)
    }

    SubShader 
    {
        ZWrite On
        ZTest LEqual

        pass
        {
        CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert 
            #pragma fragment frag
            
            sampler2D _DiffuseTex;
            sampler2D _CloudAndNightTex;

            float4 _AtmosphereColor;
            float _AtmospherePow;
            float _AtmosphereMultiply;

            // 追加: C#やマテリアルで制御するライトの方向
            float4 _LightDir;

            struct vertexInput 
            {
                float4 pos       : POSITION;
                float3 normal    : NORMAL;
                float2 uv        : TEXCOORD0;
            };

            struct vertexOutput 
            {
                float4 pos           : POSITION;
                float2 uv            : TEXCOORD0;
                half diffuse         : TEXCOORD1;
                half night           : TEXCOORD2;
                half3 atmosphere     : TEXCOORD3;
            };
            
            vertexOutput vert(vertexInput input) 
            {
                vertexOutput output;

                // モデル空間からクリップ空間への変換
                output.pos = UnityObjectToClipPos(input.pos);

                output.uv = input.uv;

                // 修正: ライト方向を _LightDir から取得（normalize 必須）
                float3 localLightDir = normalize(_LightDir.xyz);

                // ライト方向と法線の内積で拡散反射を計算
                output.diffuse = saturate(dot(localLightDir, input.normal) * 1.2);

                // night 値は diffuse を反転したもの（夜側の影響）
                output.night = 1 - saturate(output.diffuse * 2);

                // ビュー方向と法線の角度に応じて大気散乱を加える
                half3 viewDir = normalize(ObjSpaceViewDir(input.pos));
                half3 normalDir = input.normal;

                output.atmosphere = output.diffuse * _AtmosphereColor.rgb 
                    * pow(1 - saturate(dot(viewDir, normalDir)), _AtmospherePow)
                    * _AtmosphereMultiply;

                return output;
            }

            half4 frag(vertexOutput input) : Color
            {
                // 地球の地表テクスチャを取得
                half3 colorSample = tex2D(_DiffuseTex, input.uv).rgb;

                // 雲と夜の情報を取得（r=雲, g/b=夜）
                half3 cloudAndNightSample = tex2D(_CloudAndNightTex, input.uv).rgb;

                // 雲と夜テクスチャのチャンネルを分離
                half3 nightSample = cloudAndNightSample.ggb;
                half cloudSample = cloudAndNightSample.r;

                // 最終色計算: 昼の色 + 雲 + 夜 + 大気散乱
                half4 result;
                result.rgb = (colorSample + cloudSample) * input.diffuse 
                           + nightSample * input.night 
                           + input.atmosphere;

                result.a = 1;
                return result;
            }
        ENDCG
        }
    }

    Fallback "Diffuse"
}
