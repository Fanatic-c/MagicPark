using UnityEngine;

namespace InputFramework {
    public class NormalInputParamter : MonoBehaviour { //最终转换的参数类
        public Vector2 inputMouseLook; //鼠标移动向量
        public Vector2 inputMoveVector; //玩家移动向量
        public float inputMouseScrollWheel; //鼠标滚轮
        public bool inputJump; //跳跃
        public bool inputSprint; //奔跑
        public bool inputCrouch; //下蹲
        public bool inputSkillQ; //技能
        public bool inputSkillE;
        public bool inputSkillR;
        public bool inputNormalAttack1; //普通攻击
        public bool inputNormalAttack2;
        public bool inputHealth; //药包
        public bool inputMagic;
        public bool inputEsc;
        public bool inputTab;
    }
}