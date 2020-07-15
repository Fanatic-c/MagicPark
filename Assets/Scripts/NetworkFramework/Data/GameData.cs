using ConnectBridge.Mapper;
using UnityEngine;

namespace NetworkFramework.Data {
    public class GameData : MonoBehaviour {
        public static GameData Instance;

        public User loginUser;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this) {
                Destroy(gameObject);
            }
        }
    }
}