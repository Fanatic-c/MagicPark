using Const;
using InputFramework;
using UIFramework.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Panel.MainScene {
    public class MainRoot : MonoBehaviour {
        public GameObject SwitchAnim;
        public Image FillImage;
        public GameObject hero;
        public GameObject effects;
        private NormalInputParamter paramter;

        private void Start() {
            UIManager.Instance.PushPanel(UIPanelType.MainPanel);
            paramter = GameObject.FindWithTag(Tags.InputManager).GetComponent<NormalInputParamter>();
        }

        private void Update() {
            if (UIManager.Instance.PanelCount > 0) {
                MainPanel panel = UIManager.Instance.PeekPanel() as MainPanel;
                if (panel == null && paramter.inputEsc) {
                    UIManager.Instance.PopPanel();
                }
            }
        }

        private void OnDisable() {
            UIManager.Instance.ClearPanelStack();
        }
    }
}