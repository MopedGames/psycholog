

Shader "Unlit/Rotating" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _RotationSpeed ("Rotation Speed", Float) = 2.0
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM
            // use "vert" function as the vertex shader
            #pragma vertex vert
            // use "frag" function as the pixel (fragment) shader
            #pragma fragment frag

            // vertex shader inputs
            struct appdata
            {
                float4 vertex : POSITION; // vertex position
                float2 uv : TEXCOORD0; // texture coordinate
            };

            // vertex shader outputs ("vertex to fragment")
            struct v2f
            {
                float2 uv : TEXCOORD0; // texture coordinate
                float4 vertex : SV_POSITION; // clip space position
            };

            // vertex shader
            float _RotationSpeed;
            v2f vert (appdata v)
            {
                v2f o;
                // transform position to clip space
                // (multiply with model*view*projection matrix)
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

               	//Rotation
               	v.uv.xy -=0.5;
                float sinX = sin ( _RotationSpeed * _Time );
	            float cosX = cos ( _RotationSpeed * _Time );
	            float2x2 rotationMatrix = float2x2( cosX, -sinX, sinX, cosX);
	            rotationMatrix *=0.5;
	            rotationMatrix +=0.5;
	            rotationMatrix = rotationMatrix * 2-1;

	            // just pass the texture coordinate
                o.uv.xy = mul (v.uv.xy, rotationMatrix);
                o.uv.xy += 0.5;
                return o;
            }


	
            
            // texture we will sample
            sampler2D _MainTex;

            // pixel shader; returns low precision ("fixed4" type)
            // color ("SV_Target" semantic)
            fixed4 frag (v2f i) : SV_Target
            {
                // sample texture and return it
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }


    FallBack "Unlit"
}
 
			
