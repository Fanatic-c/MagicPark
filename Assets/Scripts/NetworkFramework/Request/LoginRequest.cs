using System;
using System.Collections.Generic;
using ConnectBridge;
using ConnectBridge.Mapper;
using EventFramework;
using ExitGames.Client.Photon;
using LitJson;
using NetworkFramework.Data;
using UnityEngine;

namespace NetworkFramework.Request {
    public class LoginRequest : RequestBase {
        private User loginUser;

        private void Awake() {
            OpCode = OperationCode.Login;
            EventCenter.AddListener<User>(EventDefine.LoginRequestUserVal, LoginRequestUserVal);
        }

        private void LoginRequestUserVal(User u) {
            loginUser = u;
        }

        public override void DefaultRequest() {
            Dictionary<byte, object> loginInfo = new Dictionary<byte, object>();
            if (loginUser == null || !loginUser.LoginInfoComplete()) {
                EventCenter.Broadcast(EventDefine.LoginRequestErr, "出现内部错误,请重试");
                EventCenter.Broadcast(EventDefine.LoginBtnEnable);
            }
            
            loginInfo[(byte) ParameterCode.LoginUser] = JsonMapper.ToJson(loginUser);
            PhotonEngine.Peer.OpCustom((byte) OpCode, loginInfo, true);
        }

        public override void OnOperationResponse(OperationResponse resp) {
            ReturnCode returnCode = (ReturnCode) resp.ReturnCode;
            switch (returnCode) {
                case ReturnCode.LoginSuccess: // 登陆成功
                    string loginUserJson = resp.Parameters[(byte) ParameterCode.LoginUser] as string;
                    // TODO:转换long类型失败
                    GameData.Instance.loginUser = JsonMapper.ToObject<User>(loginUserJson);
                    EventCenter.Broadcast(EventDefine.LoginSuccessSceneSwitcher);
                    PhotonEngine.Username = loginUser.username;
                    break;
                case ReturnCode.LoginUserDontExist: // 用户不存在
                    EventCenter.Broadcast(EventDefine.LoginRequestErr, "用户不存在,请重新输入");
                    EventCenter.Broadcast(EventDefine.LoginBtnEnable);
                    break;
                case ReturnCode.LoginPasswordErr: // 密码错误
                    EventCenter.Broadcast(EventDefine.LoginRequestErr, "请检查密码是否正确");
                    EventCenter.Broadcast(EventDefine.LoginBtnEnable);
                    break;
                case ReturnCode.LoginInfoNotComplete: // 登陆信息不全
                    EventCenter.Broadcast(EventDefine.LoginRequestErr, "您输入的信息不完善");
                    EventCenter.Broadcast(EventDefine.LoginBtnEnable);
                    break;
                case ReturnCode.LoginServerTearDown: // 登陆服务器出错
                    EventCenter.Broadcast(EventDefine.RegisterRequestErr, "服务器存在问题,稍后重试");
                    break;
            }
        }

        private void OnDisable() {
            EventCenter.RemoveListener<User>(EventDefine.LoginRequestUserVal, LoginRequestUserVal);
        }
    }
}