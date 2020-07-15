using UnityEngine;

namespace SkillFramework.AjSkill {
    public class AjPropNormalAttack1 : PropBase {
        public override void ReleaseSkill(Transform player, Transform enemy, ref float currMagic) {
            base.ReleaseSkill(player, enemy, ref currMagic);
            PropUtil.CreateNormalSkill(prop, player, 15f, 1f, Vector3.up);
        }
    }

    public class AjPropNormalAttack2 : PropBase {
        public override void ReleaseSkill(Transform player, Transform enemy, ref float currMagic) {
            base.ReleaseSkill(player, enemy, ref currMagic);
            PropUtil.CreateNormalSkill(prop, player, 15f, 1f, Vector3.up);
        }
    }

    public class AjGroundSkillQ1 : PropBase {
        public override void ReleaseSkill(Transform player, Transform enemy, ref float currMagic) {
            base.ReleaseSkill(player, enemy, ref currMagic);
            PropUtil.CreateKriptoFX(prop, 5f, Vector3.zero);
        }
    }

    public class AjGroundSkillE1 : PropBase {
        public override void ReleaseSkill(Transform player, Transform enemy, ref float currMagic) {
            base.ReleaseSkill(player, enemy, ref currMagic);
            PropUtil.CreateBuff(player, prop, 5f, -Vector3.up * 0.25f);
        }
    }

    public class AjGroundSkillR1 : PropBase {
        public override void ReleaseSkill(Transform player, Transform enemy, ref float currMagic) {
            base.ReleaseSkill(player, enemy, ref currMagic);
            PropUtil.CreateNormalSkill(prop, player, 8f, 15f, Vector3.zero);
        }
    }

    public class AjFlySkillQ2 : PropBase {
        public override void ReleaseSkill(Transform player, Transform enemy, ref float currMagic) {
            base.ReleaseSkill(player, enemy, ref currMagic);
            PropUtil.CreateKriptoFXonFoot(player, prop, 10f, -Vector3.up * 0.25f + player.forward * 5f);
        }
    }

    public class AjFlySkillE2 : PropBase {
        public override void ReleaseSkill(Transform player, Transform enemy, ref float currMagic) {
            base.ReleaseSkill(player, enemy, ref currMagic);
            PropUtil.CreateBuff(player, prop, 8f, Vector3.zero);
        }
    }

    public class AjFlySkillR2 : PropBase {
        public override void ReleaseSkill(Transform player, Transform enemy, ref float currMagic) {
            base.ReleaseSkill(player, enemy, ref currMagic);
            PropUtil.CreateNormalSkill(prop, player, 8f, 15f, Vector3.zero);
        }
    }
}