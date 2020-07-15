using UnityEngine;

namespace PostEffectsFramework {
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class PostEffectsBase : MonoBehaviour { //后期处理基类

        protected void CheckResources() { //检查GPU驱动是否支持后期处理 Srart()中调用
            bool isSupported = CheckSupport();

            if (isSupported == false) { //不支持后期处理
                NotSupported();
            }
        }

        protected bool CheckSupport() { //检查GPU驱动是否支持后期处理的方法
            if (SystemInfo.supportsImageEffects == false || SystemInfo.supportsRenderTextures == false) {
                Debug.LogWarning("This platform does not support image effects or render textures.");
                return false;
            }

            return true;
        }

        protected void NotSupported() { //GPU驱动无法使用后期处理，直接禁用脚本
            enabled = false;
        }

        protected void Start() {
            CheckResources();
        }

        protected Material CheckShaderAndCreateMaterial(Shader shader, Material material) { //检查Shader和材质是否被支持
            if (shader == null) {
                return null;
            }

            if (shader.isSupported && material && material.shader == shader)
                return material;

            if (!shader.isSupported) {
                return null;
            }
            else {
                material = new Material(shader);
                material.hideFlags = HideFlags.DontSave;
                if (material)
                    return material;
                else
                    return null;
            }
        }
    }
}