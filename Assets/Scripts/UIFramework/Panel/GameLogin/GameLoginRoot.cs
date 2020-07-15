using Const;
using InputFramework;
using UIFramework.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Panel.GameLogin {
    public class GameLoginRoot : MonoBehaviour {
        public GameObject SwitchAnim;
        public Image FillImage;
        private NormalInputParamter paramter;

        private void Start() {
            UIManager.Instance.PushPanel(UIPanelType.RegisterPanel);
            paramter = GameObject.FindWithTag(Tags.InputManager).GetComponent<NormalInputParamter>();
        }

        private void Update() {
            if (UIManager.Instance.PanelCount > 0) {
                LoginPanel panel = UIManager.Instance.PeekPanel() as LoginPanel;
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