using Const;
using Container;
using PlayerFramework._3rd_PlayerController;
using PlayerFramework.AnimationFramework.Base;
using SkillFramework.AjSkill;
using UnityEngine;

namespace PlayerFramework.AnimationFramework {
    public class AjIdel : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjIdel(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 1);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjWalkFwd : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjWalkFwd(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 2);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjWalkLeft : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjWalkLeft(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 3);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjWalkRight : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjWalkRight(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 4);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjWalkBack : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjWalkBack(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 5);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjJump : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjJump(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 6);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjCrouch : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjCrouch(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 7);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjRunFwd : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjRunFwd(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 8);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjRunBack : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjRunBack(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 9);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjRunLeft : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjRunLeft(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 10);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjRunRight : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjRunRight(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 11);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjCrouchLeft : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjCrouchLeft(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 12);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjCrouchRight : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjCrouchRight(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 13);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjCrouchFwd : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjCrouchFwd(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 14);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjCrouchBack : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        public AjCrouchBack(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            _avater.SetInteger(StateIndex, 15);
        }

        public override void OnLeave() { }

        public override void Update() { }
    }

    public class AjNormalAttack1 : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        private GameObject player = //玩家
            GameObject.FindGameObjectWithTag(Tags.Player);

        private GameObject enemy = //敌人
            GameObject.FindGameObjectWithTag(Tags.Enemy);

        private ThirdPlayerController playerState = //第三人称控制器脚本
            GameObject.FindGameObjectWithTag(Tags.Player).transform.GetComponent<ThirdPlayerController>();

        private AjPropManager propManager = //技能控制脚本
            GameObject.FindGameObjectWithTag(Tags.Player).transform.GetComponent<AjPropManager>();

        public AjNormalAttack1(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            playerState.enterSkill = true;
            _avater.SetInteger(StateIndex, 16);
        }

        public override void OnLeave() { }

        public override void Update() {
            playerState.UpdateDir();
            if (_avater.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack") &&
                _avater.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f) {
                propManager.currProp.ReleaseSkill(player.transform, enemy.transform, ref playerState.magic);
                playerState.enterSkill = false;
            }
        }
    }

    public class AjGroundSkill1 : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");

        private ThirdPlayerController playerState = //第三人称控制器脚本
            GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<ThirdPlayerController>();

        private GameObject player = //玩家
            GameObject.FindGameObjectWithTag(Tags.Player);

        private GameObject enemy = //敌人
            GameObject.FindGameObjectWithTag(Tags.Enemy);

        private AjPropManager propManager = //技能控制脚本
            GameObject.FindGameObjectWithTag(Tags.Player).transform.GetComponent<AjPropManager>();

        public AjGroundSkill1(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            playerState.enterSkill = true;
            _avater.SetInteger(StateIndex, 17);
        }

        public override void OnLeave() { }

        public override void Update() {
            playerState.UpdateDir();
            if (_avater.GetCurrentAnimatorStateInfo(0).IsName("GroundSkill1") &&
                _avater.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f) {
                propManager.currProp.ReleaseSkill(player.transform, enemy.transform, ref playerState.magic);
                playerState.enterSkill = false;
            }
        }
    }

    public class AjGroundSkill2 : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");
        private ManagerVars vars = ManagerVars.GetManagerVarsContainer(); //资源管理器

        private GameObject player = //玩家
            GameObject.FindGameObjectWithTag(Tags.Player);

        private GameObject enemy = //敌人
            GameObject.FindGameObjectWithTag(Tags.Enemy);

        private ThirdPlayerController playerState = //第三人称控制器脚本
            GameObject.FindGameObjectWithTag(Tags.Player).transform.GetComponent<ThirdPlayerController>();

        private AjPropManager propManager = //技能控制脚本
            GameObject.FindGameObjectWithTag(Tags.Player).transform.GetComponent<AjPropManager>();


        public AjGroundSkill2(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            playerState.enterSkill = true;
            _avater.SetInteger(StateIndex, 18);
        }

        public override void OnLeave() { }

        public override void Update() {
            playerState.UpdateDir();
            if (_avater.GetCurrentAnimatorStateInfo(0).IsName("GroundSkill2") &&
                _avater.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f) {
                propManager.currProp.ReleaseSkill(player.transform, enemy.transform, ref playerState.magic);
                playerState.enterSkill = false;
            }
        }
    }

    public class AjGroundSkill3 : FsmBase {
        private Animator _avater;
        private static readonly int StateIndex = Animator.StringToHash("StateIndex");
        private ManagerVars vars = ManagerVars.GetManagerVarsContainer(); //资源管理器

        private GameObject player = //玩家
            GameObject.FindGameObjectWithTag(Tags.Player);

        private GameObject enemy = //敌人
            GameObject.FindGameObjectWithTag(Tags.Enemy);

        private ThirdPlayerController playerState = //第三人称控制器脚本
            GameObject.FindGameObjectWithTag(Tags.Player).transform.GetComponent<ThirdPlayerController>();

        private AjPropManager propManager = //技能控制脚本
            GameObject.FindGameObjectWithTag(Tags.Player).transform.GetComponent<AjPropManager>();

        public AjGroundSkill3(Animator tempAnimal) {
            _avater = tempAnimal;
        }

        public override void OnEnter() {
            playerState.enterSkill = true;
            _avater.SetInteger(StateIndex, 19);
        }

        public override void OnLeave() { }

        public override void Update() {
            playerState.UpdateDir();
            if (_avater.GetCurrentAnimatorStateInfo(0).IsName("GroundSkill3") &&
                _avater.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f) {
                float magic = 100;
                propManager.currProp.ReleaseSkill(player.transform, enemy.transform, ref playerState.magic);
                playerState.enterSkill = false;
            }
        }
    }
}