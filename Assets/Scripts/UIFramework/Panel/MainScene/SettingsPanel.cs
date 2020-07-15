using DG.Tweening;
using UIFramework.Base;
using UIFramework.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Panel.MainScene {
    public class SettingsPanel : BasePanel {
        public Button ResetButton;
        public Button ApplyButton;
        public Button ExitButton;
        public Text VoiceProgressText;
        public Slider VoiceProgressSlider;
        public Text SpeedProgressText;
        public Slider SpeedProgressSlider;

        public override void OnEnter() {
            base.OnEnter();
            RectTransform rectTransform = transform.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(Screen.width, 0, 0);
            rectTransform.DOLocalMoveX(0, 1f).SetEase(Ease.OutBounce);
        }

        public override void OnExit() {
            base.OnExit();
            transform.GetComponent<RectTransform>().DOLocalMoveX(Screen.width, 1f)
                .SetEase(Ease.OutBounce);
        }

        void Start() {
            ResetButton.onClick.AddListener(OnResetButtonClick);
            ApplyButton.onClick.AddListener(OnApplyButtonClick);
            ExitButton.onClick.AddListener(OnExitButtonClick);
            OnVoiceValueChanged(VoiceProgressSlider.value);
            OnSpeedValueChanged(SpeedProgressSlider.value);
            VoiceProgressSlider.onValueChanged.AddListener(OnVoiceValueChanged);
            SpeedProgressSlider.onValueChanged.AddListener(OnSpeedValueChanged);
        }


        private void OnVoiceValueChanged(float value) {
            VoiceProgressText.text = value + "%";
        }

        private void OnSpeedValueChanged(float value) {
            SpeedProgressText.text = value + "%";
        }

        private void OnResetButtonClick() {
            OnVoiceValueChanged(50f);
            OnSpeedValueChanged(50f);
            VoiceProgressSlider.value = 50f;
            SpeedProgressSlider.value = 50f;
            OnApplyButtonClick();
        }

        private void OnApplyButtonClick() {
            // TODO: 保存音量设置
        }

        private void OnExitButtonClick() {
            UIManager.Instance.PopPanel();
        }
  
    }
}