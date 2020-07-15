using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace InputFramework {
    public class NormalInput : MonoBehaviour {
        //按键集合
        private Dictionary<string, KeyCode> _buttons = new Dictionary<string, KeyCode>();

        //自定义的轴向  
        private Dictionary<string, NormalInputAxis> _axis = new Dictionary<string, NormalInputAxis>();

        //Unity中的轴向
        private List<string> _unityAxis = new List<string>();

        public static bool LockCursor {
            //获取鼠标是否被锁定
            get { return Cursor.lockState == CursorLockMode.Locked; }
            set {
                Cursor.visible = true; //设置鼠标显示状态
                Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None; //设定鼠标锁定状态
            }
        }

        //脚本初始化回调函数
        private void Start() {
            _buttons.Clear();
            _axis.Clear();
            _unityAxis.Clear();
            SetupDefaultNormalInput(); //将用户输入设置为初始状态
        }

        //添加按键的方法
        private void AddButton(string buttonName, KeyCode button) {
            if (_buttons.ContainsKey(buttonName)) _buttons[buttonName] = button;
            else _buttons.Add(buttonName, button);
        }

        //添加自定义轴的方法
        private void AddAxis(string axisName, NormalInputAxis inputAxis) {
            if (_axis.ContainsKey(axisName)) _axis[axisName] = inputAxis;
            else _axis.Add(axisName, inputAxis);
        }

        //添加Unity中默认轴的方法
        private void AddUnityAxis(string unityAxisName) {
            if (!_unityAxis.Contains(unityAxisName)) _unityAxis.Add(unityAxisName);
        }

        //设置初始输入的方法
        private void SetupDefaultNormalInput(NormalInputType type = NormalInputType.Default) {
            if (type == NormalInputType.Default || type == NormalInputType.Button) {
                AddButton("Sprint", KeyCode.LeftShift); //奔跑
                AddButton("Jump", KeyCode.Space); //跳跃
                AddButton("Crouch", KeyCode.LeftControl); //蹲
                AddButton("Skill1", KeyCode.Q); //技能
                AddButton("Skill2", KeyCode.E);
                AddButton("Skill3", KeyCode.R);
                AddButton("NormalAttack1", KeyCode.Mouse0); //普攻
                AddButton("NormalAttack2", KeyCode.Mouse1);
                AddButton("Health", KeyCode.Alpha1); //药包
                AddButton("Magic", KeyCode.Alpha2);
                AddButton("Esc", KeyCode.Escape);
                AddButton("Tab", KeyCode.Tab);
            }

            if (type == NormalInputType.Default || type == NormalInputType.Axis) {
                AddAxis("Horizontal", new NormalInputAxis(KeyCode.W, KeyCode.S)); //水平方向
                AddAxis("Vertical", new NormalInputAxis(KeyCode.A, KeyCode.D)); //竖直方向
            }

            if (type == NormalInputType.Default || type == NormalInputType.UnityAxis) {
                AddUnityAxis("Mouse X"); //鼠标水平方向
                AddUnityAxis("Mouse Y"); //鼠标竖直方向
                AddUnityAxis("Horizontal"); //水平轴
                AddUnityAxis("Vertical"); //竖直轴
                AddUnityAxis("Mouse ScrollWheel"); //鼠标滚轮
            }
        }

        public bool GetButton(string buttonName) {
            return _buttons.ContainsKey(buttonName) && Input.GetKey(_buttons[buttonName]);
        }

        //按钮抬起
        public bool GetButtonUp(string buttonName) {
            return _buttons.ContainsKey(buttonName) && Input.GetKeyUp(_buttons[buttonName]);
        }

        //按钮按下
        public bool GetButtonDown(string buttonName) {
            return _buttons.ContainsKey(buttonName) && Input.GetKeyDown(_buttons[buttonName]);
        }

        //具有平滑变化的Unity轴向检测
        public float GetUnityAxis(string axis) {
            if (_unityAxis.Contains(axis)) {
                return Input.GetAxis(axis);
            }

            return 0;
        }

        //不具有平滑变化的轴向检测
        public float GetAxisRaw(string axis) {
            if (_axis.ContainsKey(axis)) //检查自定义轴
            {
                if (Input.GetKey(_axis[axis].Positive)) //获取正向输入
                    return 1;

                if (Input.GetKey(_axis[axis].Negative)) //获取反向输入
                    return -1;
                return 0;
            }

            if (_unityAxis.Contains(axis)) //检查Unity轴
                return Input.GetAxisRaw(axis);
            return 0;
        }

        private void OnDestroy() {
            _buttons.Clear();
            _axis.Clear();
            _unityAxis.Clear();
        }
    }
}