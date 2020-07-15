using DG.Tweening;
using UnityEngine;

namespace UIFramework.Effects {
    public class Ani_Login : StateMachineBehaviour {
        private GameObject effect;
        private GameObject fireball;
        private Vector3 vector3up, vector3down;
        public float time;
        public bool beginState;

        private void OnEnable() {
            fireball = GameObject.Find("Fireball");
            vector3down = fireball.transform.localPosition;
            vector3up = vector3down + Vector3.up * 10f;
            effect = GameObject.Find("Effect3_Optimized");
            effect.SetActive(false);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (stateInfo.IsName("fire")) {
                Debug.Log("进入了Animator-fire");
            }

            if (stateInfo.IsName("begin")) {
                Debug.Log("进入了Animator-begin");
                beginState = true;
                fireball.SetActive(false);
                fireball.transform.localPosition = vector3down;
                fireball.SetActive(true);
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
                    fireball.transform.DOMove(vector3up, 1f).OnComplete(() => {
                        fireball.transform.DOMove(effect.transform.Find("pos").position, 1f);
                    });
                    time = 0;
                }
            }
        }
    }
}