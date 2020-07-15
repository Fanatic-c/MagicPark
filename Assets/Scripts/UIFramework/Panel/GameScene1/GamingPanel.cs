using Const;
using PlayerFramework._3rd_PlayerController;
using UIFramework.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Panel.GameScene1 {
    public class GamingPanel : BasePanel {
        public Text UsernameText;
        public Slider HpSlider;
        public Slider MagicSlider;
        private ThirdPlayerController controller;

        // Start is called before the first frame update
        void Start() {
            controller = GameObject.FindWithTag(Tags.Player).GetComponent<ThirdPlayerController>();
        }

        // Update is called once per frame
        void Update() {
            HpSlider.value = controller.hp;
            MagicSlider.value = controller.magic;
        }
    }
}