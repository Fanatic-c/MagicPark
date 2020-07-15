using UnityEngine;

namespace PostEffectsFramework {
    public class MotionBlur : PostEffectsBase { //运动模糊后期处理

        public Shader motionBlurShader; //用于运动模糊的Shader
        private Material motionBlurMaterial = null; //生成的材质

        public Material material {
            get {
                motionBlurMaterial = CheckShaderAndCreateMaterial(motionBlurShader, motionBlurMaterial);
                return motionBlurMaterial;
            }
        }

        [Range(0.0f, 0.9f)] public float blurAmount = 0.25f; //模糊系数

        private RenderTexture accumulationTexture; //用于模糊的渲染图

        void OnDisable() {
            DestroyImmediate(accumulationTexture); //脚本失效立即销毁渲染图
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest) {
            if (material != null) {
                if (accumulationTexture == null || accumulationTexture.width != src.width ||
                    accumulationTexture.height != src.height) { //创建用于模糊的渲染图
                    DestroyImmediate(accumulationTexture);
                    accumulationTexture = new RenderTexture(src.width, src.height, 0);
                    accumulationTexture.hideFlags = HideFlags.HideAndDontSave; //渲染图不出现在Hierarchy和Project仅作为临时量出现
                    Graphics.Blit(src, accumulationTexture); //src与accumulationTexture叠加
                }

                accumulationTexture.MarkRestoreExpected(); //渲染纹理恢复

                material.SetFloat("_BlurAmount", 1.0f - blurAmount);

                Graphics.Blit(src, accumulationTexture, material); //accumulationTexture与material叠加
                Graphics.Blit(accumulationTexture, dest); //accumulationTexture与最终结果叠加
            }
            else { //不支持此类型渲染
                Graphics.Blit(src, dest);
            }
        }
    }
}