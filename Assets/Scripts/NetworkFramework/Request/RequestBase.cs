using ConnectBridge;
using ExitGames.Client.Photon;
using UnityEngine;

namespace NetworkFramework.Request {
    public abstract class RequestBase : MonoBehaviour {
        [HideInInspector] public OperationCode OpCode; //操作码
        public abstract void DefaultRequest(); //请求
        public abstract void OnOperationResponse(OperationResponse resp); //响应

        private void Start() {
            PhotonEngine.Instance.AddRequest(this); //添加请求
        }

        private void OnDestroy() {
            PhotonEngine.Instance.RemoveRequest(this); //移除请求
        }
    }
}