using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HotfixFramework.AssetBundle {
    public class AssetLoader : IDisposable {
        //当前Assetbundle 
        private UnityEngine.AssetBundle _CurrentAssetBundle;

        //缓存容器集合
        private Hashtable _Ht;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abObj">给定WWW加载的AssetBundle 实例</param>
        public AssetLoader(UnityEngine.AssetBundle abObj) {
            if (abObj != null) {
                _CurrentAssetBundle = abObj;
                _Ht = new Hashtable();
            }
            else {
                Debug.Log(GetType() + "/ 构造函数 AssetBundle()/ 参数 abObj==null! ,请检查");
            }
        }


        /// <summary>
        /// 加载当前包中指定的资源
        /// </summary>
        /// <param name="assetName">资源的名称</param>
        /// <param name="isCache">是否开启缓存</param>
        /// <returns></returns>
        public Object LoadAsset(string assetName, bool isCache = false) {
            return LoadResource<Object>(assetName, isCache);
        }

        /// <summary>
        /// 加载当前AB包的资源，带缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName">资源的名称</param>
        /// <param name="isCache">是否需要缓存处理</param>
        /// <returns></returns>
        private T LoadResource<T>(string assetName, bool isCache) where T : Object {
            //是否缓存集合已经存在
            if (_Ht.Contains(assetName)) {
                return _Ht[assetName] as T;
            }

            //正式加载
            T tmpTResource = _CurrentAssetBundle.LoadAsset<T>(assetName);
            //加入缓存集合
            if (tmpTResource != null && isCache) {
                _Ht.Add(assetName, tmpTResource);
            }
            else if (tmpTResource == null) {
                Debug.LogError(GetType() + "/LoadResource<T>()/参数 tmpTResources==null, 请检查！");
            }

            return tmpTResource;
        }


        /// <summary>
        /// 卸载指定的资源
        /// </summary>
        /// <param name="asset">资源名称</param>
        /// <returns></returns>
        public bool UnLoadAsset(Object asset) {
            if (asset != null) {
                Resources.UnloadAsset(asset);
                return true;
            }

            Debug.LogError(GetType() + "/UnLoadAsset()/参数 asset==null ,请检查！");
            return false;
        }

        /// <summary>
        /// 释放当前AssetBundle内存镜像资源
        /// </summary>
        public void Dispose() {
            _CurrentAssetBundle.Unload(false);
        }

        /// <summary>
        /// 释放当前AssetBundle内存镜像资源,且释放内存资源。
        /// </summary>
        public void DisposeALL() {
            _CurrentAssetBundle.Unload(true);
        }

        /// <summary>
        /// 查询当前AssetBundle中包含的所有资源名称。
        /// </summary>
        /// <returns></returns>
        public string[] RetriveAllAssetName() {
            return _CurrentAssetBundle.GetAllAssetNames();
        }
    }
}