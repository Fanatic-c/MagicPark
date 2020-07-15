using System.Collections.Generic;
using ConnectBridge;
using ConnectBridge.Mapper;
using EventFramework;
using ExitGames.Client.Photon;
using LitJson;

namespace NetworkFramework.Request {
    public class RegisterRequest : RequestBase {
        private User registerUser;

        private void Awake() {
            OpCode = OperationCode.Register;
            EventCenter.AddListener<User>(EventDefine.RegisterRequestUserVal, RegisterUserVal);
        }

        private void RegisterUserVal(User user) {
            registerUser = user;
        }

        public override void DefaultRequest() {
            Dictionary<byte, object> registerInfo = new Dictionary<byte, object>();
            if (registerUser == null || !registerUser.RegisterInfoComplete()) {
                EventCenter.Broadcast(EventDefine.RegisterRequestErr, "出现内部错误,请重试");
                EventCenter.Broadcast(EventDefine.RegisterBtnEnable);
            }

            registerInfo[(byte) ParameterCode.RegisterUser] = JsonMapper.ToJson(registerUser);
            PhotonEngine.Peer.OpCustom((byte) OpCode, registerInfo, true);
        }

        public override void OnOperationResponse(OperationResponse resp) {
            ReturnCode returnCode = (ReturnCode) resp.ReturnCode;
            switch (returnCode) {
                case ReturnCode.RegisterSuccess: // 注册成功
                    EventCenter.Broadcast(EventDefine.RegisterPanelClose);
                    EventCenter.Broadcast(EventDefine.RegisterRequestErr, "");
                    break;
                case ReturnCode.RegisterUserExist: // 用户名已存在
                    EventCenter.Broadcast(EventDefine.RegisterRequestErr, "您输入的用户名已存在");
                    EventCenter.Broadcast(EventDefine.RegisterBtnEnable);
                    break;
                case ReturnCode.RegisterInfoNotComplete: //用户信息不全
                    EventCenter.Broadcast(EventDefine.RegisterRequestErr, "您输入的信息不完善");
                    EventCenter.Broadcast(EventDefine.RegisterBtnEnable);
                    break;
                case ReturnCode.RegisterServerTearDown: // 数据库服务器错误
                    EventCenter.Broadcast(EventDefine.RegisterRequestErr, "服务器存在问题,稍后重试");
                    break;
            }
        }

        private void OnDisable() {
            EventCenter.RemoveListener<User>(EventDefine.RegisterRequestUserVal, RegisterUserVal);
        }
    }
}