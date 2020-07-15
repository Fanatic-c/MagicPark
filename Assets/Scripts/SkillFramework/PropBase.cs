using SkillFramework.AreaCheck;
using UnityEngine;

namespace SkillFramework {
    public class PropBase { //技能基类
        public GameObject prop; //释放的技能预制件
        public float maxHurt; //技能最高伤害
        public float magicLess; //技能消耗的魔法值
        public bool enable; //技能是否已经解锁
        private float coldTime; //当前技能冷却时间

        public float ColdTime { //冷却技能值的设定
            get => coldTime;
            set => coldTime = Mathf.Clamp(value: value, min: 0, max: maxColdTime);
        }

        public float maxColdTime; //技能冷却最大时间
        private bool isRectProp; //此技能的伤害判定使用矩形模式
        private RectProp rectArea; //技能判定伤害的矩形范围

        public void SetRectProp(RectProp rectArea) { //设定此技能的伤害判定模式为矩形模式
            isRectProp = true;
            isUmbrellaProp = false;
            this.rectArea = rectArea;
            umbrellaArea = null;
        }

        private bool isUmbrellaProp; //此技能的伤害判定使用伞形模式
        private UmbrellaProp umbrellaArea; //技能判定伤害的伞形范围

        public void SetUmbrellaProp(UmbrellaProp umbrellaArea) { //设定此技能的伤害判定模式为伞形模式
            isRectProp = false;
            isUmbrellaProp = true;
            this.umbrellaArea = umbrellaArea;
            rectArea = null;
        }

        public bool CanReleaseSkill(float currMagic) {
            return enable && Mathf.Approximately(ColdTime, 0) &&
                   (Mathf.Approximately(currMagic, magicLess) || currMagic > magicLess);
        }

        public virtual void ReleaseSkill(Transform player, Transform enemy, ref float currMagic) {
            //释放技能的虚方法，技能的运动效果应该重载此方法的实现，并调用base.ReleaseSkill(),使技能释放的基本逻辑生效
            currMagic -= magicLess; //减少玩家的魔法值
            ColdTime = maxColdTime; //将技能恢复为冷却状态
            float propHurt = maxHurt; //敌人收到的伤害值
            if (isRectProp && rectArea != null) { //此技能使用矩形攻击模式判断伤害
                bool wasHurt =
                    AreaAttack.RectAttack(player, enemy, rectArea.width, rectArea.height, ref propHurt); //进行矩形范围的伤害检测
                if (wasHurt) { //此时玩家将会受到攻击伤害
                    Debug.Log(wasHurt + "伤害大小为 ： " + propHurt);
                }
            }
            else if (isUmbrellaProp && umbrellaArea != null) { //此技能使用伞形攻击模式判断伤害
                bool wasHurt = AreaAttack.UmbrellaAttack(player, enemy, umbrellaArea.radius, umbrellaArea.angle,
                    ref propHurt,
                    umbrellaArea.canAttack3D); //进行伞形范围的伤害检测
                if (wasHurt) { //此时玩家将会受到攻击伤害
                    Debug.Log("伤害大小为 ： " + propHurt);
                }
            }
        }
    }
}