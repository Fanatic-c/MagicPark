using UnityEngine;

namespace PostEffectsFramework {
    public class GaussianBlur : PostEffectsBase { //高斯模糊

        public Shader gaussianBlurShader; //高斯模糊Shader
        private Material gaussianBlurMaterial = null;

        public Material material {
            get {
                gaussianBlurMaterial = CheckShaderAndCreateMaterial(gaussianBlurShader, gaussianBlurMaterial);
                return gaussianBlurMaterial;
            }
        }

        [Range(0, 4)] public int iterations = 3; //高斯模糊迭代次数

        [Range(0.2f, 3.0f)] public float blurSpread = 0.6f; //高斯模糊模糊范围

        [Range(1, 8)] public int downSample = 2; //高斯模糊缩放系数

        void OnRenderImage(RenderTexture src, RenderTexture dest) {
            if (material != null) {
                int rtW = src.width / downSample;
                int rtH = src.height / downSample;

                RenderTexture buffer0 = RenderTexture.GetTemporary(rtW, rtH, 0); //缓存 第一个Pass的输入
                buffer0.filterMode = FilterMode.Bilinear;

                Graphics.Blit(src, buffer0);

                for (int i = 0; i < iterations; i++) {
                    material.SetFloat("_BlurSize", 1.0f + i * blurSpread);

                    RenderTexture buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0); //缓存 第一个Pass的输出

                    Graphics.Blit(buffer0, buffer1, material, 0); //进行垂直高斯模糊计算

                    RenderTexture.ReleaseTemporary(buffer0);
                    buffer0 = buffer1;
                    buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);

                    Graphics.Blit(buffer0, buffer1, material, 1); //进行水平高斯模糊的计算

                    RenderTexture.ReleaseTemporary(buffer0); //释放缓存
                    buffer0 = buffer1;
                }

                Graphics.Blit(buffer0, dest);
                RenderTexture.ReleaseTemporary(buffer0);
            }
            else { //不支持高斯模糊
                Graphics.Blit(src, dest);
            }
        }
    }
}