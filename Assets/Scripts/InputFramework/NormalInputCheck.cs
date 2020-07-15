using UnityEngine;

namespace InputFramework {
    [RequireComponent(typeof(NormalInputParamter))]
    [RequireComponent(typeof(NormalInput))]
    public class NormalInputCheck : MonoBehaviour {
        private NormalInputParamter _paramter; //最终转换的参数
        private NormalInput _input; //按键的集合

        private void Start() {
            _paramter = GetComponent<NormalInputParamter>();
            _input = GetComponent<NormalInput>();
            NormalInput.LockCursor = false; //将鼠标锁定
        }

        void Update() {
            InitialInput(); //每帧刷新控制值
        }

        private void InitialInput() { //将监听到的用户输入转换为参数
            //初始化人物移动以及相机旋转向量
            _paramter.inputMoveVector = new Vector2(_input.GetUnityAxis("Horizontal"), _input.GetUnityAxis("Vertical"));
            _paramter.inputMouseLook = new Vector2(_input.GetAxisRaw("Mouse X"), _input.GetAxisRaw("Mouse Y"));
            _paramter.inputMouseScrollWheel = _input.GetUnityAxis("Mouse ScrollWheel");
            //初始化人物动作标志位
            _paramter.inputCrouch = _input.GetButton("Crouch");
            _paramter.inputJump = _input.GetButton("Jump");
            _paramter.inputSprint = _input.GetButton("Sprint");
            //初始化技能标志位
            _paramter.inputSkillQ = _input.GetButtonDown("Skill1");
            _paramter.inputSkillE = _input.GetButtonDown("Skill2");
            _paramter.inputSkillR = _input.GetButtonDown("Skill3");
            _paramter.inputNormalAttack1 = _input.GetButtonDown("NormalAttack1");
            _paramter.inputNormalAttack2 = _input.GetButtonDown("NormalAttack2");
            //初始化药包标志位        
            _paramter.inputHealth = _input.GetButton("Health");
            _paramter.inputMagic = _input.GetButton("Magic");
            _paramter.inputEsc = _input.GetButtonDown("Esc");
            _paramter.inputTab = _input.GetButtonDown("Tab");
        }
    }
}