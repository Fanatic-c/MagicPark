using ABFW;
using UnityEngine;

namespace HotfixFramework.AssetBundle.Test {
    public class TestClass_ABToolFramework : MonoBehaviour {
        //场景名称
        private string _ScenesName = "prefabs";

        //AB包名称
        private string _AssetBundelName = "prefabs/prefabs.ab";

        //资源名称
        private string _AssetName = "_Eviromens.prefab";


        private void Start() {
            Debug.Log(GetType() + "开始'ABFW'框架测试 ");
            //调用AB包（连锁智能调用AB包【集合】）
            StartCoroutine(AssetBundleMgr.GetInstance()
                .LoadAssetBundlePack(_ScenesName, _AssetBundelName, LoadAllABComplete));
        }

        //回调函数： 所有的AB包都已经加载完毕了
        private void LoadAllABComplete(string abName) {
            Debug.Log(GetType() + "所有的AB包都已经加载完毕了");
            Object tmpObj = null;

            //提取资源
            tmpObj = (Object) AssetBundleMgr.GetInstance()
                .LoadAsset(_ScenesName, _AssetBundelName, _AssetName, false);
            if (tmpObj != null) {
                Instantiate(tmpObj);
            }
        }

        //测试销毁资源
        private void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                Debug.Log(GetType() + " 测试销毁资源");
                AssetBundleMgr.GetInstance().DisposeAllAssets(_ScenesName);
            }
        }
    }
}