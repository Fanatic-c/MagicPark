using Const;
using InputFramework;
using PlayerFramework.AnimationFramework;
using PlayerFramework.AnimationFramework.Base;
using SkillFramework.AjSkill;
using UnityEditor;
using UnityEngine;

namespace PlayerFramework._3rd_PlayerController {
    public enum PlayerState {
        //人物的Fsm状态
        Idel, //战立状态
        WalkFwd, //行走状态
        WalkLeft,
        WalkRight,
        WalkBack,
        Jump, //跳跃
        Crouch, //蹲下状态
        RunFwd, //跑动状态
        RunBack,
        RunLeft,
        RunRight,
        CrouchLeft, //蹲下跑
        CrouchRight,
        CrouchFwd, //蹲下跑
        CrouchBack,
        NormalAttack,
        GroundSkill1,
        GroundSkill2,
        GroundSkill3,
        None, //空状态
    }

    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class ThirdPlayerController : MonoBehaviour {
        private PlayerState _state = PlayerState.None; //人物Fsm状态
        public PlayerState State => PlayerStateUpdate(); //人物状态重新设定的属性
        [HideInInspector] public FsmManager _fsmManager; //人物Fsm状态管理器

        [Tooltip("冲刺速度")] public float sprintSpeed = 6.0f; // 玩家冲刺速度
        [Tooltip("冲刺时的跳跃速度")] public float sprintJumpSpeed = 8.0f; //冲刺跳跃速度
        [Tooltip("行走速度")] public float normalSpeed = 4.0f; //普通行走速度
        [Tooltip("行走状态跳跃速度")] public float normalJumpSpeed = 7.0f; //普通跳跃速度
        [Tooltip("下蹲状态行走速度")] public float crouchSpeed = 1.0f; //蹲下行走速度
        [Tooltip("下蹲状态跳跃速度")] public float crouchJumpSpeed = 5.0f; //蹲下跳跃速度
        private float _standradCameraYOffset; //标准的高度偏移
        [Tooltip("下蹲相机下降的高度")] public float crouchDeltaHeight = 0.25f; //蹲下下降高度
        private float _crouchingCameraYOffset; //相机下降后的高度
        [Tooltip("下降重力量模拟")] public float gravity = 20f; //下降的重力
        [Tooltip("相机下降上升速度")] public float cameraMoveSpeed = 4.0f; //相机移动速度
        public float magic = 100; // 法
        public float hp = 100; // 血
        private AudioClip _jumpAudio; //跳跃音效
        private AudioClip _walkAudio; //走路音效
        private float _speed; //玩家速度;
        private float _jumpSpeed; //玩家跳跃速度
        private Transform _mainCamera; //摄像机
        private bool _grounded; //玩家是否在地面
        private bool _walking; //玩家行走标志
        private bool _crouching; //玩家下蹲标志
        private bool _stopCrouching; //结束下蹲
        private bool _running; //玩家奔跑
        [HideInInspector] public bool enterSkill; //玩家处于技能释放状态
        private PlayerState lastSkillState; //玩家上次释放的技能
        private Quaternion _enterSkillCameraQuaternion; //玩家释放技能的摄像机朝向
        private AjPropManager _propManager; //技能控制脚本
        private bool _movingFwd; //玩家行进方向标志
        private bool _movingBack; //玩家行进方向标志
        private bool _moveSingleAxisLeft;
        private bool _moveSingleAxisRight;
        private Vector3 _normalControllerCenter; // 角色控制器默认中心位置
        private float _normalControllerHeight; // 角色控制器默认高度
        private CharacterController _controller; // 角色控制器
        private AudioSource _audioSource; // 玩家脚步声播放源
        private Animator _avatar; //动画组件
        private NormalInputParamter _parameter; // 玩家输入参数
        private CameraFllow _cameraFllow; // 相机管理脚本
        private Vector3 _moveDirection; // 人物移动方向
        private float _reDirSpeed; //经过计算后得到的人物转身速度
        [Tooltip("人物正常转身速度（90<夹角<180）")] public float normalReDirSpped = 2; //正常转身速度
        [Tooltip("人物正常转身速度（0<夹角<90）")] public float fastReDirSpped = 4; //快速转身

        void Start() {
            NormalInput.LockCursor = true;
            //组件获取
            _audioSource = GetComponent<AudioSource>();
            _controller = GetComponent<CharacterController>();
            _avatar = GetComponent<Animator>();
            _parameter = GameObject.FindGameObjectWithTag(Tags.InputManager).GetComponent<NormalInputParamter>();
            _cameraFllow = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<CameraFllow>();
            _propManager = transform.GetComponent<AjPropManager>();
            //状态标志位初始化
            _crouching = false;
            _running = false;
            _walking = false;
            _stopCrouching = false;
            _running = false;
            enterSkill = false;
            lastSkillState = PlayerState.None;
            //设定速度
            _speed = normalSpeed;
            _jumpSpeed = normalJumpSpeed;
            // 玩家的移动方向
            _moveDirection = Vector3.zero;
            //摄像机以及摄像机标准和蹲下高度计算
            _mainCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera).transform;
            _standradCameraYOffset = _cameraFllow.cameraYOffset;
            _crouchingCameraYOffset = _standradCameraYOffset - crouchDeltaHeight;
            //角色控制器高度,中心
            _normalControllerCenter = _controller.center;
            _normalControllerHeight = _controller.height;
            _normalControllerCenter = new Vector3(0, 0.9f, 0);
            _normalControllerHeight = 2;
            //声音初始化
            _jumpAudio = Resources.Load<AudioClip>("Jump");
            _walkAudio = Resources.Load<AudioClip>("Footstep01");
            //_jumpAudio = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Audio/Jump.wav");
            //_walkAudio = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Audio/Footstep01.wav");
            _audioSource.clip = _walkAudio;
            //初始化Fsm管理器添加动画状态
            AnimalInitialize();
        }

        private void AnimalInitialize() {
            //Fsm初始化
            _fsmManager = new FsmManager((byte) PlayerState.None);
            _fsmManager.AddState(new AjIdel(_avatar));
            _fsmManager.AddState(new AjWalkFwd(_avatar));
            _fsmManager.AddState(new AjWalkLeft(_avatar));
            _fsmManager.AddState(new AjWalkRight(_avatar));
            _fsmManager.AddState(new AjWalkBack(_avatar));
            _fsmManager.AddState(new AjJump(_avatar));
            _fsmManager.AddState(new AjCrouch(_avatar));
            _fsmManager.AddState(new AjRunFwd(_avatar));
            _fsmManager.AddState(new AjRunBack(_avatar));
            _fsmManager.AddState(new AjRunLeft(_avatar));
            _fsmManager.AddState(new AjRunRight(_avatar));
            _fsmManager.AddState(new AjCrouchLeft(_avatar));
            _fsmManager.AddState(new AjCrouchRight(_avatar));
            _fsmManager.AddState(new AjCrouchFwd(_avatar));
            _fsmManager.AddState(new AjCrouchBack(_avatar));
            _fsmManager.AddState(new AjNormalAttack1(_avatar));
            _fsmManager.AddState(new AjGroundSkill1(_avatar));
            _fsmManager.AddState(new AjGroundSkill2(_avatar));
            _fsmManager.AddState(new AjGroundSkill3(_avatar));
        }

        private PlayerState PlayerStateUpdate() {
            //修改人物状态的方法
            if (enterSkill) {
                //玩家释放技能过程中，需要将上一个技能状态返回
                return lastSkillState;
            }

            Vector3 wantedFwd = Vector3.Normalize(new Vector3(_mainCamera.forward.x, 0, _mainCamera.forward.z));
            _enterSkillCameraQuaternion = Quaternion.LookRotation(wantedFwd);
            if (_parameter.inputNormalAttack1 && _propManager.normalSkills[0].CanReleaseSkill(magic)) {
                //玩家释放了技能
                enterSkill = true; //玩家正在释放技能
                lastSkillState = PlayerState.NormalAttack; //玩家状态为普通攻击
                _propManager.currProp = _propManager.normalSkills[0]; //修改当前技能
                return PlayerState.NormalAttack;
            }

            if (_parameter.inputNormalAttack2 && _propManager.normalSkills[1].CanReleaseSkill(magic)) {
                //玩家释放了技能
                enterSkill = true;
                lastSkillState = PlayerState.NormalAttack;
                _propManager.currProp = _propManager.normalSkills[1]; //修改当前技能
                return PlayerState.NormalAttack;
            }

            if (_parameter.inputSkillQ && _propManager.currPropSkillsGroup[0].CanReleaseSkill(magic)) {
                enterSkill = true;
                lastSkillState = PlayerState.GroundSkill1;
                _propManager.currProp = _propManager.currPropSkillsGroup[0]; //修改当前技能
                return PlayerState.GroundSkill1;
            }

            if (_parameter.inputSkillE && _propManager.currPropSkillsGroup[1].CanReleaseSkill(magic)) {
                enterSkill = true;
                lastSkillState = PlayerState.GroundSkill2;
                _propManager.currProp = _propManager.currPropSkillsGroup[1]; //修改当前技能
                return PlayerState.GroundSkill2;
            }

            if (_parameter.inputSkillR && _propManager.currPropSkillsGroup[2].CanReleaseSkill(magic)) {
                enterSkill = true;
                lastSkillState = PlayerState.GroundSkill3;
                _propManager.currProp = _propManager.currPropSkillsGroup[2]; //修改当前技能
                return PlayerState.GroundSkill3;
            }

            if (_crouching && _moveSingleAxisLeft) //下蹲左移
                return PlayerState.CrouchLeft;
            if (_crouching && _moveSingleAxisRight) //下蹲右移
                return PlayerState.CrouchRight;
            if (_crouching && _movingFwd) //下蹲前移
                return PlayerState.CrouchFwd;
            if (_crouching && _movingBack) //下蹲后移
                return PlayerState.CrouchBack;
            if (_crouching) //下蹲
                return PlayerState.Crouch;
            if (_walking && _moveSingleAxisLeft) //单朝向左移动
                return PlayerState.WalkLeft;
            if (_walking && _moveSingleAxisRight) //单朝向右移动
                return PlayerState.WalkRight;
            if (_walking && _movingFwd) //正常速度向前移动
                return PlayerState.WalkFwd;
            if (_walking && _movingBack) //正常速度向后移动
                return PlayerState.WalkBack;
            if (_parameter.inputJump) //跳跃
                return PlayerState.Jump;
            if (_running && _moveSingleAxisLeft) //奔跑
                return PlayerState.RunLeft;
            if (_running && _moveSingleAxisRight) //奔跑
                return PlayerState.RunRight;
            if (_running && _movingFwd) //奔跑
                return PlayerState.RunFwd;
            if (_running && _movingBack) //奔跑
                return PlayerState.RunBack;
            return PlayerState.Idel; //站立
        }

        void FixedUpdate() {
            UpdateMove();
            _fsmManager.Update(); //更新动画状态
            AudioManagement();
        }

        private void CurrentSpeed() {
            //根据玩家状态重新修改玩家速度
            if (enterSkill) {
                ChangeFsmStateSpeed(lastSkillState, normalSpeed, normalJumpSpeed); //普通速度
                return;
            }

            switch (State) {
                case PlayerState.Idel: //站立状态
                    ChangeFsmStateSpeed(PlayerState.Idel, normalSpeed, normalJumpSpeed); //普通速度
                    break;
                case PlayerState.WalkFwd: //行走状态
                    ChangeFsmStateSpeed(PlayerState.WalkFwd, normalSpeed, normalJumpSpeed); //行走速度
                    break;
                case PlayerState.WalkLeft: //行走状态
                    ChangeFsmStateSpeed(PlayerState.WalkLeft, normalSpeed, normalJumpSpeed); //向左行走速度
                    break;
                case PlayerState.WalkRight: //行走状态
                    ChangeFsmStateSpeed(PlayerState.WalkRight, normalSpeed, normalJumpSpeed); //向右行走速度
                    break;
                case PlayerState.WalkBack: //行走状态
                    ChangeFsmStateSpeed(PlayerState.WalkBack, normalSpeed, normalJumpSpeed); //向后行走速度
                    break;
                case PlayerState.Jump: //行走状态
                    ChangeFsmStateSpeed(PlayerState.Jump, normalSpeed, normalJumpSpeed); //跳跃速度
                    break;
                case PlayerState.Crouch: //蹲下
                    ChangeFsmStateSpeed(PlayerState.Crouch, crouchSpeed, crouchJumpSpeed); //下蹲速度
                    break;
                case PlayerState.RunFwd: //向前跑步
                    ChangeFsmStateSpeed(PlayerState.RunFwd, sprintSpeed, sprintJumpSpeed); //奔跑速度
                    break;
                case PlayerState.RunBack: //向前跑步
                    ChangeFsmStateSpeed(PlayerState.RunBack, sprintSpeed, sprintJumpSpeed); //奔跑速度
                    break;
                case PlayerState.RunLeft: //向前跑步
                    ChangeFsmStateSpeed(PlayerState.RunLeft, sprintSpeed, sprintJumpSpeed); //奔跑速度
                    break;
                case PlayerState.RunRight: //向前跑步
                    ChangeFsmStateSpeed(PlayerState.RunRight, sprintSpeed, sprintJumpSpeed); //奔跑速度
                    break;
                case PlayerState.CrouchLeft: //蹲左走
                    ChangeFsmStateSpeed(PlayerState.CrouchLeft, crouchSpeed, crouchJumpSpeed); //下蹲速度
                    break;
                case PlayerState.CrouchRight: //蹲右走
                    ChangeFsmStateSpeed(PlayerState.CrouchRight, crouchSpeed, crouchJumpSpeed); //下蹲速度
                    break;
                case PlayerState.CrouchFwd: //蹲左走
                    ChangeFsmStateSpeed(PlayerState.CrouchFwd, crouchSpeed, crouchJumpSpeed); //下蹲速度
                    break;
                case PlayerState.CrouchBack: //蹲右走
                    ChangeFsmStateSpeed(PlayerState.CrouchBack, crouchSpeed, crouchJumpSpeed); //下蹲速度
                    break;
                case PlayerState.NormalAttack: //普通攻击
                    ChangeFsmStateSpeed(PlayerState.NormalAttack, normalSpeed, normalJumpSpeed); //正常速度
                    break;
                case PlayerState.GroundSkill1: //1技能
                    ChangeFsmStateSpeed(PlayerState.GroundSkill1, normalSpeed, normalJumpSpeed); //正常速度
                    break;
                case PlayerState.GroundSkill2: //2技能
                    ChangeFsmStateSpeed(PlayerState.GroundSkill2, normalSpeed, normalJumpSpeed); //正常速度
                    break;
                case PlayerState.GroundSkill3: //3技能
                    ChangeFsmStateSpeed(PlayerState.GroundSkill3, normalSpeed, normalJumpSpeed); //正常速度
                    break;
            }
        }

        private void ChangeFsmStateSpeed(PlayerState fsmState, float speed, float jumpSpeed) {
            //修改人物动作、移动跳跃速度
            _fsmManager.ChangeState((sbyte) fsmState); //修改人物状态
            _speed = speed; //修改移动速度
            _jumpSpeed = jumpSpeed; //修改跳跃速度
        }

        private void AudioManagement() {
            //修改人物播放的声音
            if (State == PlayerState.WalkFwd) {
                //行走
                _audioSource.pitch = 1.0f; //声音正常倍速播放
                if (!_audioSource.isPlaying) {
                    _audioSource.Play(); //播放行走声音
                }
            }
            else if (State == PlayerState.RunFwd) {
                //跑
                _audioSource.pitch = 1.3f; //声音高倍速播放
                if (!_audioSource.isPlaying) {
                    _audioSource.Play(); //提高行走声音播放频率，给人奔跑的感觉
                }
            }
            else if (State == PlayerState.Crouch && _parameter.inputMoveVector != Vector2.zero) {
                //蹲下跑
                _audioSource.pitch = 0.7f; //声音低倍速播放
                if (!_audioSource.isPlaying) {
                    _audioSource.Play(); //放缓行走声音播放频率
                }
            }
            else {
                _audioSource.Stop(); //关闭脚步声
            }
        }

        // 下蹲相机状态管理
        private void UpdateCrouch() {
            if (_crouching) {
                //下蹲
                if (_cameraFllow.cameraYOffset > _crouchingCameraYOffset) //大于蹲下相机高度，需要将相机平滑下降
                    _cameraFllow.cameraYOffset =
                        Mathf.Clamp(_cameraFllow.cameraYOffset - crouchDeltaHeight * Time.deltaTime * cameraMoveSpeed,
                            _crouchingCameraYOffset, _standradCameraYOffset);
                else _cameraFllow.cameraYOffset = _crouchingCameraYOffset;
            }
            else {
                //相机上升
                if (_cameraFllow.cameraYOffset < _standradCameraYOffset) //小于标准相机高度，需要将相机平滑上升
                    _cameraFllow.cameraYOffset =
                        Mathf.Clamp(_cameraFllow.cameraYOffset + crouchDeltaHeight * Time.deltaTime * cameraMoveSpeed,
                            _crouchingCameraYOffset, _standradCameraYOffset);
                else _cameraFllow.cameraYOffset = _standradCameraYOffset;
            }
        }

        public void UpdateDir() {
            //更新人物朝向，在人物有移动时调用
            Vector3 playerFwd = Vector3.Normalize(transform.forward); //玩家fwd
            Vector3 wantedFwd = Vector3.Normalize(new Vector3(_mainCamera.forward.x, 0, _mainCamera.forward.z)); //预期fwd
            float fwdDotVal = Vector3.Dot(playerFwd, wantedFwd);
            if (Mathf.Approximately(fwdDotVal, 1)) //向量无限接近
                return; //不在进行重定向计算
            Quaternion currentLookQuaternion = Quaternion.LookRotation(playerFwd); //使用fwd获取四元数用来计算球面插值
            Quaternion wantedLookQuaternion = enterSkill ? _enterSkillCameraQuaternion : Quaternion.LookRotation(wantedFwd);
            _reDirSpeed = fwdDotVal < 0 ? fastReDirSpped : normalReDirSpped; //判断向量之间的夹角，重新确定人物转身速度
            transform.rotation = //进行球面四元数插值，平滑移动人物
                Quaternion.Slerp(currentLookQuaternion, wantedLookQuaternion, Time.deltaTime * _reDirSpeed);
        }

        private void UpdateMove() {
            // 控制人物移动更新人物移动状态标志
            if (enterSkill) {
                //玩家正在释放技能
                _walking = false; //重新设置玩家的状态标志
                _running = false;
                _crouching = false;
                _moveDirection = Vector3.zero; //玩家下降向量
                _moveDirection.y -= gravity * Time.deltaTime; //利用重力系数模拟玩家下降过程
                _moveDirection = transform.TransformDirection(_moveDirection);
                if (!_controller.isGrounded) {
                    //玩家不在地面，需要将玩家移动至地面上
                    _controller.Move(_moveDirection * Time.deltaTime * _jumpSpeed); //移动玩家位置
                }

                return;
            }

            if (_grounded) {
                //玩家在地面上
                _moveDirection = new Vector3(_parameter.inputMoveVector.x, 0, _parameter.inputMoveVector.y); //获取玩家输入方向
                if (!Mathf.Approximately(0, Vector3.Magnitude(_moveDirection))) {
                    //判断玩家是否移动位置
                    _movingFwd = _moveDirection.z > 0; //根据玩家输入Vertical的方向判断玩家是否根据单根轴进行移动
                    _movingBack = _moveDirection.z < 0;
                    _moveSingleAxisLeft = Mathf.Approximately(0, _moveDirection.z) &&
                                          _moveDirection.x < 0; //根据玩家输入Horizontal的方向判断玩家是否根据单根轴进行移动
                    _moveSingleAxisRight = Mathf.Approximately(0, _moveDirection.z) &&
                                           _moveDirection.x > 0;
                }

                if (Mathf.Abs(_moveDirection.z) > 0 && Mathf.Abs(_moveDirection.x) > 0) {
                    //玩家具有两个维度的移动，应将速度减半防止移动过快
                    _moveDirection /= 2;
                }

                _moveDirection = transform.TransformDirection(_moveDirection); //转为相对自身坐标系的方向*速度系数
                _moveDirection *= _speed;
                if (_parameter.inputJump) {
                    //玩家跳跃
                    _moveDirection.y = _jumpSpeed; //设置跳跃高度
                    AudioSource.PlayClipAtPoint(_jumpAudio, transform.position); //播放跳跃声音
                    CurrentSpeed(); //重新设置玩家速度
                }
            }

            _moveDirection.y -= gravity * Time.deltaTime; //下落
            CollisionFlags flags = _controller.Move(_moveDirection * Time.deltaTime); //移动玩家位置
            _grounded = (flags & CollisionFlags.CollidedBelow) != 0; //检测胶囊碰撞体底部是否与地面接触
            if (Mathf.Abs(_moveDirection.x) > 0 && _grounded || Mathf.Abs(_moveDirection.z) > 0 && _grounded) {
                //位于地面有按键输入
                UpdateDir(); //重新确定人物方向
                if (_parameter.inputSprint) {
                    //重新设定人物状态标志位
                    _walking = false;
                    _running = true;
                    _crouching = false;
                }
                else if (_parameter.inputCrouch) {
                    _walking = false;
                    _running = false;
                    _crouching = true;
                }
                else {
                    _walking = true;
                    _running = false;
                    _crouching = false;
                }
            }
            else {
                //无按键输入
                if (_walking) _walking = false;
                if (_running) _running = false;
                if (_parameter.inputCrouch) _crouching = true;
                else _crouching = false;
            }

            if (_crouching) {
                //下蹲,修改碰撞器的高度和中心位置
                _controller.height = _normalControllerHeight - crouchDeltaHeight;
                _controller.center = _normalControllerCenter - new Vector3(0, crouchDeltaHeight / 2, 0);
            }
            else {
                //正常状态
                _controller.height = _normalControllerHeight;
                _controller.center = _normalControllerCenter;
            }

            UpdateCrouch();
            CurrentSpeed();
        }

        private void Update() {
            if (magic < 100) {
                magic += Time.unscaledDeltaTime; //加magic
            }
        }
    }
}