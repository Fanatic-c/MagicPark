using System.Collections.Generic;
using ConnectBridge;
using ExitGames.Client.Photon;
using NetworkFramework.Event;
using NetworkFramework.Request;
using UnityEngine;

namespace NetworkFramework {
    public class PhotonEngine : MonoBehaviour, IPhotonPeerListener {
        public static PhotonEngine Instance;
        private static PhotonPeer peer;

        public static PhotonPeer Peer => peer;

        private Dictionary<OperationCode, RequestBase> requestDict = new Dictionary<OperationCode, RequestBase>(); //请求字典
        private Dictionary<EventCode, EventBase> eventDict = new Dictionary<EventCode, EventBase>(); //请求字典

        public static string Username;

        public void AddRequest(RequestBase req) { //向字典中添加请求
            requestDict.Add(req.OpCode, req);
        }

        public void RemoveRequest(RequestBase req) { //移除字典中的请求
            requestDict.Remove(req.OpCode);
        }

        public void AddEvent(EventBase e) { //向字典中添加请求
            eventDict.Add(e.eventCode, e);
        }

        public void RemoveEvent(EventBase e) { //移除字典中的请求
            eventDict.Remove(e.eventCode);
        }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this) {
                Destroy(gameObject);
            }
        }

        private void Start() {
            peer = new PhotonPeer(this, ConnectionProtocol.Udp);
            peer.Connect("127.0.0.1:5055", "PosSynServer"); //连接服务器
        }

        private void Update() {
            peer?.Service(); //保证与服务器的连接
        }

        private void OnDestroy() {
            if (peer != null && peer.PeerState == PeerStateValue.Disconnected) { //判断连接状态
                peer.Disconnect(); //断开连接
            }
        }

        public void DebugReturn(DebugLevel level, string message) { }

        //返回至客户端的响应
        public void OnOperationResponse(OperationResponse operationResponse) {
            OperationCode opCode = (OperationCode) operationResponse.OperationCode;
            RequestBase req;
            requestDict.TryGetValue(opCode, out req);
            if (req != null) {
                req.OnOperationResponse(operationResponse);
            }
            else {
                Debug.Log("没有找到对应的响应 处理对象");
            }
        }

        //连接状态发生改变
        public void OnStatusChanged(StatusCode statusCode) {
            Debug.Log(statusCode);
        }

        //服务器向客户端发送事件
        public void OnEvent(EventData eventData) {
            EventCode eventCode = (EventCode) eventData.Code;
            EventBase e;
            eventDict.TryGetValue(eventCode, out e);
            if (e != null) {
                e.OnEvent(eventData);
            }
            else {
                Debug.Log("没有找到对应的响应 处理对象");
            }
        }
    }
}