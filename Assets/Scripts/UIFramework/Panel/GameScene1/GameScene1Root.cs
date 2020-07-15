using Const;
using DG.Tweening;
using InputFramework;
using UIFramework.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Panel.GameScene1 {
    public class GameScene1Root : MonoBehaviour {
        public GameObject SwitchAnim;
        public Image FillImage;
        private NormalInputParamter paramter;

        private void Awake() {
            SwitchAnim.SetActive(true);
            FillImage.DOFade(0, 4f).OnComplete(() => { SwitchAnim.SetActive(false); });
        }

        private void Start() {
            UIManager.Instance.PushPanel(UIPanelType.GamingPanel);
            paramter = GameObject.FindWithTag(Tags.InputManager).GetComponent<NormalInputParamter>();
        }

        private void Update() {
            if (UIManager.Instance.PanelCount > 0) {
                GamingPanel panel = UIManager.Instance.PeekPanel() as GamingPanel;
                if (panel == null && paramter.inputEsc) {
                    UIManager.Instance.PopPanel();
                    Debug.Log("popPanel");
                }
                if (panel != null && paramter.inputTab) {
                    UIManager.Instance.PushPanel(UIPanelType.InfoPanel);
                }
            }
        }

        private void OnDisable() {
            UIManager.Instance.ClearPanelStack();
        }
    }
}