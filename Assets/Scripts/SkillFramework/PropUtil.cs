using UnityEngine;

namespace SkillFramework {
    public class PropUtil { //技能释放工具类，此类规定了技能的生成以及运动过程
        private static Transform mainCamera = Camera.main?.transform;

        public static EffectSettings CreateNormalSkill(GameObject propPrefab, Transform player, float effectSpeed,
            float time, Vector3 offset) { //从玩家位置出发，直接向前移动的技能打击实现
            propPrefab = GameObject.Instantiate(propPrefab, player.position + offset, Quaternion.identity); //生成的技能粒子系统
            EffectSettings effectSettings = propPrefab.GetComponent<EffectSettings>();
            GameObject targetPos = new GameObject(); //存储玩家打击目标位置的临时对象
            targetPos.name = "Aim"; //修改目标位置临时对象
            RaycastHit hit; //射线碰撞信息
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, 1000, 1 << 9)) { //对摄像机的前方发射一条射线
                targetPos.transform.position = hit.point; //发射的射线与非Environment层的其他物体发生了碰撞，直接将目标位置设定到碰撞点
                effectSettings.Target = targetPos;
            }
            else { //射线没有接触其他类型物体
                targetPos.transform.parent = mainCamera; //规定到摄像机坐标系的前方
                targetPos.transform.localPosition = Vector3.forward * 100f;
                effectSettings.Target = targetPos;
            }

            effectSettings.MoveSpeed = effectSpeed; //修正移动速度
            effectSettings.CollisionEnter += (n, e) => {
                if (e.Hit.transform != null) Debug.Log("击中： " + e.Hit.transform.name);
                else Debug.Log("未击中...");
                targetPos.SetActive(false);
                GameObject.Destroy(targetPos, time);
                GameObject.Destroy(propPrefab, time);
            };
            return effectSettings;
        }


        public static bool CreatePointSkill(GameObject tempProp, float time, Vector3 tempVector3) { //直接在目标点生成，撞上之后消失
            var aim = new GameObject();
            aim.name = "Aim";
            Ray ray = new Ray(mainCamera.position, mainCamera.forward);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 1000, 1 << 9)) {
                if (hit.point != null) {
                    aim.transform.position = hit.point;
                    GameObject prop =
                        GameObject.Instantiate(tempProp, aim.transform.position + tempVector3,
                            tempProp.transform.rotation);
                    EffectSettings effectSettings = prop.GetComponent<EffectSettings>();
                    effectSettings.CollisionEnter += (n, e) => {
                        if (e.Hit.transform != null) Debug.Log("击中： " + e.Hit.transform.name);
                        else Debug.Log("未击中...");
                        aim.SetActive(false);
                        GameObject.Destroy(aim, time);
                        GameObject.Destroy(prop, time);
                    };
                    return true;
                }
            }

            return false;
        }

        public static bool CreateKriptoFX(GameObject tempProp, float time, Vector3 tempVector3) { //直接生成直接没了
            Ray ray = new Ray(mainCamera.position, mainCamera.forward);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 1000, 1 << 9)) {
                if (hit.point != null) {
                    GameObject prop = GameObject.Instantiate(tempProp, hit.point + tempVector3, Quaternion.identity);
                    GameObject.Destroy(prop, time);
                    return true;
                }
            }

            return false;
        }

        public static bool CreateKriptoFXonFoot(Transform player, GameObject tempProp, float time, Vector3 tempVector3) {
            //脚下生成的
            GameObject prop =
                GameObject.Instantiate(tempProp, player.transform.position - Vector3.up * 0.25f + tempVector3,
                    tempProp.transform.rotation);
            GameObject.Destroy(prop, time);
            return true;
        }

        public static bool CreateLineKriptoFX(Transform player, GameObject tempProp, float time, Vector3 tempVector3) {
            //生成射线的
            var line = GameObject.Instantiate(tempProp);
            line.transform.parent = player.transform;
            line.transform.localRotation = Quaternion.Euler(Vector3.up * -90f);
            line.transform.localPosition = tempVector3;
            GameObject.Destroy(line, time);
            return false;
        }

        public static bool CreateBuff(Transform player, GameObject tempProp, float time, Vector3 tempVector3) { //红蓝的
            GameObject prop =
                GameObject.Instantiate(tempProp, player.transform.position + tempVector3,
                    tempProp.transform.rotation);
            prop.transform.parent = player.transform;
            GameObject.Destroy(prop, time);
            return true;
        }
    }
}