using System.IO;
using ABFW;
using UnityEditor;

namespace HotfixFramework.AssetBundle.Editor {
    public class BuildAssetBundle {
        /// <summary>
        /// 打包生成所有的AssetBundles(包)
        /// </summary>
        [MenuItem("AssetBundelTools/BuildAllAssetBundles")]
        public static void BuildAllAB() {
            //打包AB输出路径
            string strABOutPathDIR = string.Empty;

            //获取"StreamingAssets"数值
            strABOutPathDIR = PathTools.GetABOutPath();

            //判断生成输出目录文件夹
            if (!Directory.Exists(strABOutPathDIR)) {
                Directory.CreateDirectory(strABOutPathDIR);
            }

            //打包生成
            BuildPipeline.BuildAssetBundles(strABOutPathDIR, BuildAssetBundleOptions.None,
                BuildTarget.StandaloneWindows64);
        }
    }
}