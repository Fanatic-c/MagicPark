using Const;
using DG.Tweening;
using InputFramework;
using PlayerFramework._3rd_PlayerController;
using UIFramework.Base;
using UIFramework.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Panel.GameScene1 {
    public class InfoPanel : BasePanel {
        private ThirdPlayerController controller;
        private CameraFllow fllower;
        public Button SettingsButton;

        public override void OnEnter() {
            base.OnEnter();
            RectTransform rectTransform = transform.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, Screen.height, 0);
            rectTransform.DOLocalMoveY(0, 1f).SetEase(Ease.OutBounce);
            controller = GameObject.FindWithTag(Tags.Player).GetComponent<ThirdPlayerController>();
            fllower = GameObject.FindWithTag(Tags.MainCamera).GetComponent<CameraFllow>();
            controller.enabled = false;
            fllower.enabled = false;
            NormalInput.LockCursor = false;
        }

        public override void OnExit() {
            base.OnExit();
            transform.GetComponent<RectTransform>().DOLocalMoveY(Screen.height, 1f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() => {
                    controller.enabled = true;
                    fllower.enabled = true;
                    NormalInput.LockCursor = true;
                });
        }

        private void Start() {
            SettingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnSettingsButtonClick() {
            UIManager.Instance.PushPanel(UIPanelType.SettingsPanel);
        }
    }
}