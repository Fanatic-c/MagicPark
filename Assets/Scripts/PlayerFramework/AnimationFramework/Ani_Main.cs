using DG.Tweening;
using UnityEngine;

namespace PlayerFramework.AnimationFramework {
    public class Ani_Main : StateMachineBehaviour {
        private GameObject effect;
        private GameObject iceball;
        private Vector3 vector3up, vector3down;
        public float time;
        public bool beginState;

        private void OnEnable() {
            iceball = GameObject.Find("Iceball");
            vector3down = iceball.transform.localPosition;
            vector3up = vector3down + Vector3.up * 10f;
            effect = GameObject.Find("FrozenMine");
            effect.SetActive(false);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (stateInfo.IsName("ice")) {
                Debug.Log("进入了Animator-fire");
            }

            if (stateInfo.IsName("begin")) {
                Debug.Log("进入了Animator-begin");
                beginState = true;
                iceball.SetActive(false);
                iceball.transform.localPosition = vector3down;
                iceball.SetActive(true);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (stateInfo.IsName("begin")) {
                beginState = false;
                time = 0;
                effect.SetActive(false);
                effect.SetActive(true);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (beginState) {
                time += Time.deltaTime;
                if (time >= 10f) {
                    iceball.transform.DOMove(vector3up, 1f).OnComplete(() => {
                        iceball.transform.DOMove(effect.transform.Find("pos").position, 1f);
                    });
                    time = 0;
                }
            }
        }
    }
}