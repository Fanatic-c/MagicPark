using DG.Tweening;
using UIFramework.Base;
using UIFramework.Manager;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Panel.MainScene {
    public class MainPanel : BasePanel {
        private MainRoot root;

        public Button StartGameButton;
        public Button TestButton;
        public Button HeroButton;
        public Button GiftButton;
        public Button SocialButton;
        public Button LifeButton;
        public Button SettingsButton;
        public Button ExitButton;

        public override void OnEnter() {
            base.OnEnter();
            //SceneSwitchManager.BackLoadNotActive("GameScenes1");
            root.FillImage.DOFade(0, 4f).OnComplete(() => {
                root.hero.SetActive(true);
                root.effects.SetActive(true);
                root.SwitchAnim.SetActive(false);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            });
        }

        private void Awake() {
            root = GameObject.Find("Canvas").GetComponent<MainRoot>();
        }

        private void Start() {
            StartGameButton.onClick.AddListener(OnStartGameButtonClick);
            TestButton.onClick.AddListener(OnTestButtonClick);
            HeroButton.onClick.AddListener(OnHeroButtonClick);
            GiftButton.onClick.AddListener(OnGiftButtonClick);
            SocialButton.onClick.AddListener(OnSocialButtonClick);
            LifeButton.onClick.AddListener(OnLifeButtonClick);
            SettingsButton.onClick.AddListener(OnSettingsButtonClick);
            ExitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnStartGameButtonClick() {
            UIManager.Instance.PushPanel(UIPanelType.SearchGamePanel);
        }

        private void OnTestButtonClick() { }

        private void OnHeroButtonClick() { }

        private void OnGiftButtonClick() { }

        private void OnSocialButtonClick() { }

        private void OnLifeButtonClick() { }

        private void OnSettingsButtonClick() {
            UIManager.Instance.PushPanel(UIPanelType.SettingsPanel);
        }

        private void OnExitButtonClick() {
            root.SwitchAnim.SetActive(true);
            root.FillImage.DOFade(1, 3f).OnComplete(()=> {
                Application.Quit();
#if UNITY_EDITOR_WIN
                EditorApplication.isPlaying = false;
#endif
            });
        }
    }
}