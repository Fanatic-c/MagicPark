using ConnectBridge;
using ExitGames.Client.Photon;
using UnityEngine;

namespace NetworkFramework.Event {
    public abstract class EventBase : MonoBehaviour {
        [HideInInspector] public EventCode eventCode; //事件码
        public abstract void OnEvent(EventData eventData); //响应

        private void Start() {
            PhotonEngine.Instance.AddEvent(this);
        }

        private void OnDestroy() {
            PhotonEngine.Instance.RemoveEvent(this);
        }
    }
}