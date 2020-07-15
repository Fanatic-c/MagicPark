using System;
using ConnectBridge.Mapper;
using DG.Tweening;
using EventFramework;
using NetworkFramework.Request;
using SceneManager;
using UIFramework.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Panel.GameLogin {
    public class LoginPanel : BasePanel {
        public InputField UsernameInputField;
        public InputField PasswordInputField;
        public Button LoginButton;
        public Text errMsg;
        private GameLoginRoot root;
        private LoginRequest loginRequest;

        public override void OnEnter() {
            base.OnEnter();
            transform.GetComponent<CanvasGroup>().DOFade(1, 2.5f);
            LoginButton.enabled = true;
        }

        private void Awake() {
            root = GameObject.Find("Canvas").GetComponent<GameLoginRoot>();
            loginRequest = GetComponent<LoginRequest>();
            EventCenter.AddListener<string>(EventDefine.LoginRequestErr, LoginRequestErr);
            EventCenter.AddListener(EventDefine.LoginBtnEnable, LoginBtnEnable);
            EventCenter.AddListener(EventDefine.LoginSuccessSceneSwitcher, LoginSuccessSceneSwitcher);
        }


        private void Start() {
            ShowErrMsg("");
            LoginButton.onClick.AddListener(OnLoginButtonClick);
            LoginButton.onClick.AddListener(OnLoginButtonClick);
            SceneSwitchManager.BackLoadNotActive(SceneName.Main);
        }

        private void OnLoginButtonClick() {
            LoginButton.enabled = false;
            if (string.IsNullOrEmpty(UsernameInputField.text.Trim()) ||
                string.IsNullOrEmpty(PasswordInputField.text.Trim())) {
                ShowErrMsg("请填写完整您的信息");
                LoginButton.enabled = true;
                return;
            }

            User loginUser = new User {
                username = UsernameInputField.text,
                password = PasswordInputField.text
            };
            EventCenter.Broadcast(EventDefine.LoginRequestUserVal, loginUser);
            loginRequest.DefaultRequest();
        }

        private void LoginSuccessSceneSwitcher() {
            root.SwitchAnim.SetActive(true);
            root.FillImage.DOFade(1, 3f).OnComplete(SceneSwitchManager.ActiveCurrentScene);
        }

        private void LoginRequestErr(string msg) {
            ShowErrMsg(msg);
        }

        private void ShowErrMsg(string msg) {
            errMsg.text = msg;
        }

        private void LoginBtnEnable() {
            LoginButton.enabled = true;
        }

        private void OnDisable() {
            EventCenter.AddListener<string>(EventDefine.LoginRequestErr, LoginRequestErr);
            EventCenter.RemoveListener(EventDefine.LoginBtnEnable, LoginBtnEnable);
            EventCenter.RemoveListener(EventDefine.LoginSuccessSceneSwitcher, LoginSuccessSceneSwitcher);
        }
    }
}