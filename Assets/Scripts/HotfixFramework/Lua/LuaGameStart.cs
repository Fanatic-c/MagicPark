using UnityEngine;

namespace HotfixFramework.Lua {
    public class LuaGameStart : MonoBehaviour {
        void Start() {
            LuaHelper.GetInstance().DoString("require 'StartGame'");
        }
    }
}