using Const;
using InputFramework;
using UnityEngine;

namespace PlayerFramework._3rd_PlayerController {
    public class CameraFllow : MonoBehaviour {
        private NormalInputParamter _paramter;//监听输入得到的参数
        private Transform _target;//旋转中心坐标
        public float cameraFwdOffset = 0.25f;
        public float cameraRightOffset = 0.1f;
        public float cameraYOffset = 1.0f;//相机Y轴便宜
        public float xSpeed = 100;//旋转速度
        public float ySpeed = 100;
        public float mmSpeed = 10;//扩大缩小视角速度
        public float xMinLimit = 5;//旋转限制
        public float xMaxLimit = 60;
        public float distance = 5;//旋转半径
        public float minDistance = 3;//半径限制
        public float maxDistance = 20;
        public bool isNeedDamping = true;//是否Damping
        public float damping = 8f;//Damping速度
        public float xOriginAngle = 30f;//旋转角
        public float yOriginAngle = 0f;

        private void Start() {
            _paramter = GameObject.FindWithTag(Tags.InputManager).GetComponent<NormalInputParamter>();
            _target = GameObject.FindWithTag(Tags.Player).transform;
        }

        void FixedUpdate() {
            if (_target) {
                yOriginAngle += _paramter.inputMouseLook.x * xSpeed * Time.deltaTime;//通过参数计算旋转角
                xOriginAngle -= _paramter.inputMouseLook.y * ySpeed * Time.deltaTime;
                xOriginAngle = ClampAngle(xOriginAngle, xMinLimit, xMaxLimit);//限制旋转角范围

                //TODO：这里在进行相机半径计算时应该动态修改distance的限制范围，避免人物遮挡，或者开启透视模式
                distance -= _paramter.inputMouseScrollWheel * mmSpeed;//重新计算半径
                distance = Mathf.Clamp(distance, minDistance, maxDistance);//限制半径取值范围
                Quaternion rotation = Quaternion.Euler(xOriginAngle, yOriginAngle, 0.0f);//相机旋转角
                Vector3 disVector = new Vector3(0.0f, cameraYOffset, -distance);
                Vector3 position = rotation * disVector + _target.position;//重新计算相机位置，使用四元数与距离向量乘积进行向量方向变换
                if (isNeedDamping) {//判断是否需要Damping
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * damping);//插值计算旋转方位和摄像机距离
                    transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * damping)+ _target.transform.forward*cameraFwdOffset +_target.transform.right * cameraRightOffset;
                }
                else {//不需要Damping
                    transform.rotation = rotation;//直接赋值，不进行插值计算
                    transform.position = position + _target.transform.forward*cameraFwdOffset +_target.transform.right * cameraRightOffset;
                }
            }
        }

        static float ClampAngle(float angle, float min, float max) {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }
    }
}