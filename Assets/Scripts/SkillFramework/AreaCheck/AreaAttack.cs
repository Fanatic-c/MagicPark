using UnityEngine;

namespace SkillFramework.AreaCheck {
    public class AreaAttack //矩形攻击
    {
        public static bool RectAttack(Transform player, Transform enemy, float width, float height, ref float hurt) { //矩形攻击
            Vector3 deltaVec = enemy.position - player.position; //计算得到敌人与玩家的向量
            float dotFwd = Vector3.Dot(player.forward, deltaVec); //与fwd向量的投影
            if (dotFwd > 0 && dotFwd < height) { //enemy位于player前方
                float dotRight = Vector3.Dot(player.right, deltaVec); //与right向量的投影
                if (Mathf.Abs(dotRight) < width) { //玩家位于矩形范围
                    float hurtRatio =
                        Mathf.Clamp01(Mathf.Clamp01(1 - (dotFwd + dotRight) / (height + width)) +
                                      Random.Range(0, 0.1f)); //计算伤害系数,最低伤害系数取决于随机数的大小
                    hurt = hurt * hurtRatio; //将最终伤害与伤害系数相乘
                    return true; //enemy位于矩形位置中
                }
            }

            hurt = 0; //玩家没有受到伤害
            return false; //不在攻击区域
        }

        public static bool UmbrellaAttack(Transform player, Transform enemy, float radius, float angle, ref float hurt,
            bool canAttack3D) { //伞形攻击
            Vector3 deltaVec = enemy.position - player.position; //敌人与玩家的向量
            deltaVec.y = canAttack3D ? 0 : deltaVec.y; //判断是否要降维
            float playerEnemyAngle = Vector3.Angle(Vector3.Normalize(deltaVec), player.forward); //敌人与玩家角度
            float magnitudeDeltaVec = Vector3.Magnitude(deltaVec);
            if (Mathf.Abs(playerEnemyAngle) <= angle / 2 && magnitudeDeltaVec <= radius) { //敌人在伞形区域内
                float hurtRatio =
                    Mathf.Clamp01(1 - (magnitudeDeltaVec / radius) + Random.Range(0, 0.1f)); //计算伤害系数,最低伤害系数取决于随机数的大小
                hurt = hurt * hurtRatio; //将最终伤害与伤害系数相乘
                return true;
            }

            hurt = 0; //玩家没有受到伤害
            return false;
        }
    }
}