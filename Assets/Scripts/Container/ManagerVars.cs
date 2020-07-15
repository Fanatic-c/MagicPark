using UnityEngine;

namespace Container {
    [CreateAssetMenu(menuName = "CreateManagerVarsContainer")]
    public class ManagerVars : ScriptableObject {
        /// <summary>
        /// 烟色反应使用的预制件
        /// </summary>
        public GameObject C1A1;

        public GameObject C1A2;
        public GameObject C1Q1;
        public GameObject C1E1;
        public GameObject C1R1;
        public GameObject C1Q2;
        public GameObject C1E2;
        public GameObject C1R2;
        public GameObject C2A1;
        public GameObject C2A2;
        public GameObject C2Q1;
        public GameObject C2E1;
        public GameObject C2R1;
        public GameObject C2Q2;
        public GameObject C2E2;
        public GameObject C2R2;

        /// <summary>
        /// 获取资源文件
        /// </summary>
        public static ManagerVars GetManagerVarsContainer() {
            return Resources.Load<ManagerVars>("ManagerVarsContainer");
        }
    }
}