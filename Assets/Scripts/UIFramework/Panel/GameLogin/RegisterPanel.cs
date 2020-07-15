using ConnectBridge.Mapper;
using DG.Tweening;
using EventFramework;
using NetworkFramework.Request;
using UIFramework.Base;
using UIFramework.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Panel.GameLogin {
    public class RegisterPanel : BasePanel {
        public InputField PhoneNumber;
        public InputField Username;
        public InputField Password;
        public InputField PasswordAgain;
        public Button RegisterButton;
        public Button closeButton;
        public Text errMsg;
        private GameLoginRoot root;
        private RegisterRequest registerRequest;

        public override void OnEnter() {
            base.OnEnter();
            root.FillImage.DOFade(0f, 3f).OnComplete(() => {
                transform.DOScale(Vector3.one, 1.5f);
                root.SwitchAnim.SetActive(false);
                RegisterButton.enabled = true;
            });
        }

        public override void OnExit() {
            base.OnExit();
            transform.DOScale(Vector3.zero, 1f).OnComplete(() => { CloseRegisterPanel(); });
        }

        private void Awake() {
            root = GameObject.Find("Canvas").GetComponent<GameLoginRoot>();
            registerRequest = GetComponent<RegisterRequest>();
            EventCenter.AddListener<string>(EventDefine.RegisterRequestErr, RegisterRequestErr);
            EventCenter.AddListener(EventDefine.RegisterBtnEnable, RegisterBtnEnable);
            EventCenter.AddListener(EventDefine.RegisterPanelClose, CloseRegisterPanel);
        }


        private void Start() {
            RegisterButton.onClick.AddListener(OnRegisterButtonClick);
            closeButton.onClick.AddListener(CloseRegisterPanel);
            ShowErrMsg("");
        }

        private void OnRegisterButtonClick() {
            RegisterButton.enabled = false;

            if (string.IsNullOrEmpty(PhoneNumber.text.Trim()) || string.IsNullOrEmpty(Username.text.Trim()) ||
                string.IsNullOrEmpty(Password.text.Trim()) || string.IsNullOrEmpty(PasswordAgain.text.Trim())) {
                ShowErrMsg("请填写完整您的信息");
                RegisterButton.enabled = true;
                return;
            }

            if (!Password.text.Equals(PasswordAgain.text)) {
                ShowErrMsg("两次输入的密码不同");
                RegisterButton.enabled = true;
                return;
            }

            User registerUser = new User {
                username = Username.text,
                password = Password.text,
                phone = PhoneNumber.text
            };
            EventCenter.Broadcast(EventDefine.RegisterRequestUserVal, registerUser);
            registerRequest.DefaultRequest();
        }

        private void CloseRegisterPanel() {
            UIManager.Instance.PopPanel();
            UIManager.Instance.PushPanel(UIPanelType.LoginPanel);
        }

        private void ShowErrMsg(string msg) {
            errMsg.text = msg;
        }

        private void RegisterRequestErr(string msg) {
            ShowErrMsg(msg);
        }

        private void RegisterBtnEnable() {
            RegisterButton.enabled = true;
        }

        private void OnDisable() {
            EventCenter.RemoveListener<string>(EventDefine.RegisterRequestErr, RegisterRequestErr);
            EventCenter.RemoveListener(EventDefine.RegisterBtnEnable, RegisterBtnEnable);
            EventCenter.RemoveListener(EventDefine.RegisterPanelClose, CloseRegisterPanel);
        }
    }
}