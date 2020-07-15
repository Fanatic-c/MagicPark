namespace SkillFramework.AreaCheck {
    public class RectProp { //矩形技能
        //  ----------------------
        //  |                    |
        //  |                    |     | -> height * 2
        //  |                    |
        //  ----------------------    -- -> width * 2
        //          Player
        public float width; //矩形技能的宽
        public float height; //矩形技能的长
    }

    public class UmbrellaProp { //伞形技能
        //    @@@@@@@
        //    \    /
        //     \  /        -->  R
        //      \/     \/  -->  Angle
        public float radius; //伞形区域半径
        public float angle; //伞形区域夹角
        public bool canAttack3D; //是否支持高度差攻击
    }
}