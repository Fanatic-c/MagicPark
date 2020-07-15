using System;
using UnityEngine;
using XLua;

namespace HotfixFramework.Lua {
    public class BaseLuaUIForm : MonoBehaviour {
        //定义委托
        [CSharpCallLua]
        public delegate void delLuaStart(GameObject go);

        //声明委托
        delLuaStart luaStart;

        [CSharpCallLua]
        public delegate void delLuaAwake(GameObject go);

        delLuaAwake luaAwake;

        [CSharpCallLua]
        public delegate void delLuaUpdate(GameObject go);

        delLuaUpdate luaUpdate;

        [CSharpCallLua]
        public delegate void delLuaDestroy(GameObject go);

        delLuaDestroy luaDestroy;

        //定义lua表
        private LuaTable luaTable;

        //定义lua环境
        private LuaEnv luaEnv;


        private void Awake() {
            //得到lua的环境
            luaEnv = LuaHelper.GetInstance().GetLuaEnv();
            /*  设置luaTable 的元方法 （“__index”）  */
            luaTable = luaEnv.NewTable();
            LuaTable tmpTab = luaEnv.NewTable(); //临时表
            tmpTab.Set("__index", luaEnv.Global);
            luaTable.SetMetaTable(tmpTab);
            tmpTab.Dispose();
            /* 得到当前脚本所在对象的预设名称，且去除后缀(["（Clone）"]) */
            string prefabName = this.name; //当前脚本所挂载的游戏对象的名称
            if (prefabName.Contains("(Clone)")) {
                prefabName = prefabName.Split(new string[] {"(Clone)"}, StringSplitOptions.RemoveEmptyEntries)[0];
            }

            /* 查找指定路径下lua文件中的方法，映射为委托 */
            luaAwake = luaTable.GetInPath<delLuaAwake>(prefabName + ".Awake");
            luaStart = luaTable.GetInPath<delLuaStart>(prefabName + ".Start");
            luaUpdate = luaTable.GetInPath<delLuaUpdate>(prefabName + ".Update");
            luaDestroy = luaTable.GetInPath<delLuaDestroy>(prefabName + ".OnDestroy");

            //调用委托
            if (luaAwake != null) {
                luaAwake(gameObject);
            }
        }


        void Start() {
            //调用委托
            if (luaStart != null) {
                luaStart(gameObject);
            }
        }

        private void Update() {
            if (luaUpdate != null) {
                luaUpdate(gameObject);
            }
        }

        private void OnDestroy() {
            if (luaDestroy != null) {
                luaDestroy(gameObject);
            }

            luaAwake = null;
            luaStart = null;
            luaUpdate = null;
            luaDestroy = null;
        }
    } //Class_end
} //namespace_end