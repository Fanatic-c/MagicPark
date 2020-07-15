using DG.Tweening;
using SceneManager;
using UIFramework.Base;
using UIFramework.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Panel.MainScene {
    public class SearchGamePanel : BasePanel {
        public Button StartGameButton;
        public Button BackButton;
        public Button CreateHouseButton;
        private MainRoot root;

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
            root = GameObject.Find("Canvas").GetComponent<MainRoot>();
            StartGameButton.onClick.AddListener(OnStartGameButtonClick);
            BackButton.onClick.AddListener(OnBackButtonClick);
            CreateHouseButton.onClick.AddListener(OnCreateHouseButtonClick);
        }

        private void OnStartGameButtonClick() {
            SceneSwitchManager.BackLoadNotActive(SceneName.GameScenes1);
            root.SwitchAnim.SetActive(true);
            root.FillImage.DOFade(1, 3f).OnComplete(() => {
                SceneSwitchManager.ActiveCurrentScene();
            });
        }

        private void OnBackButtonClick() {
            UIManager.Instance.PopPanel();
        }

        private void OnCreateHouseButtonClick() {
            UIManager.Instance.PushPanel(UIPanelType.CreateGamePanel);
        }
    }
}