namespace HotfixFramework.Lua.HotUpdateProcess {
    public class HotUpdatePathTool {
        /* 路径常量 */
        //定义拷贝Json文件的路径
        public const string JSON_EDITOR_PATH = "/Conf_Resources"; //Json文件的编辑区

        public const string JSON_DEPLOY_PATH = "/Configurations"; //(StreamAsset 目录下)发布配置文件(Json)

        //定义拷贝lua文件的路径
        public const string LUA_EDITOR_PATH = "/Scripts/LuaScripts/src"; //lua文件编辑器

        //public const string LUA_EDITOR_FRAMEWORK_PATH = "/xxxxx/src";//Lua框架编辑器
        public const string LUA_DEPLOY_PATH = "/LUA";

        //定义HTTP 服务器链接地址
        public const string SERVER_URL = "http://127.0.0.1:8080/UpdateAssets";

        //定义热更新的校验文件名称
        public const string PROJECT_VERIFY_FILE = "/ProjectVerifyFile";

        //定义热更新接受信息常量
        public const string RECEIVE_INFO_START_RUNING = "ReceiveInfoStartRuning";
    }
}