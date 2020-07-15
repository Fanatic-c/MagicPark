using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManager {
    public class SceneName { //场景名称
        public const string GameLogin = "GameLogin";
        public const string Main = "Main";
        public const string GameScenes1 = "GameScenes1";
    }

    public class SceneSwitchManager { //场景跳转管理类
        private static AsyncOperation currentAsyncOperation; //当前正在进行跳转的关卡进度信息
        public static string currentLoadSceneName; //当前正在加载的场景

        public static void BackLoadNotActive(string loadSceneName) { //后台加载此场景但是不激活
            if (loadSceneName != null) {
                currentLoadSceneName = loadSceneName;
                currentAsyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(currentLoadSceneName); //后台加载场景
                currentAsyncOperation.allowSceneActivation = false; //场景加载至90%时自动停止，不激活此场景
            }
        }

        public static void BackLoadActive(string loadSceneName) { //后台加载此场景后直接激活
            if (loadSceneName != null) {
                currentLoadSceneName = loadSceneName;
                currentAsyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(currentLoadSceneName);
            }
        }

        public static void ActiveCurrentScene() { //激活当前后台加载的场景
            if (currentAsyncOperation != null) {
                currentAsyncOperation.allowSceneActivation = true;
            }
        }

        public static float GetSceneLoadingProgress() { //获取正在后台加载的场景加载进度，若此时没有场景正在加载则返回-1
            if (currentAsyncOperation == null) {
                return -1;
            }

            if (Mathf.Approximately(1, currentAsyncOperation.progress)) {
                currentAsyncOperation = null;
                currentLoadSceneName = null;
            }

            if (currentAsyncOperation != null) {
                return currentAsyncOperation.progress;
            }

            return -1;
        }
    }
}