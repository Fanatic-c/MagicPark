using DG.Tweening;
using UIFramework.Base;
using UIFramework.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Panel.MainScene {
    public class CreateGamePanel : BasePanel {
        public Button BackButton;

        public override void OnEnter() {
            base.OnEnter();
            RectTransform rectTransform = transform.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, Screen.height, 0);
            rectTransform.DOLocalMoveY(0, 1f).SetEase(Ease.OutBounce);
        }

        public override void OnExit() {
            base.OnExit();
            transform.GetComponent<RectTransform>().DOLocalMoveY(Screen.height, 1f)
                .SetEase(Ease.OutBounce);
        }


        void Start() {
            BackButton.onClick.AddListener(OnBackButtonClick);
        }

        private void OnBackButtonClick() {
            UIManager.Instance.PopPanel();
        }
    }
}