using System.Collections.Generic;
using Const;
using Container;
using InputFramework;
using SkillFramework.AreaCheck;
using UnityEngine;

namespace SkillFramework.AjSkill {
    [RequireComponent(typeof(CharacterController))]
    public class AjPropManager : MonoBehaviour { //Aj玩家技能的管理脚本
        public List<PropBase> normalSkills = new List<PropBase>(); //玩家的所有的普通技能
        public List<PropBase> propSkillsGroup1 = new List<PropBase>(); //玩家的特殊技能组别1(地面)
        public List<PropBase> propSkillsGroup2 = new List<PropBase>(); //玩家的特殊技能组别2(空中)
        public List<PropBase> currPropSkillsGroup; //当前生效的特殊技能组别引用
        public PropBase currProp; //当前状态下的技能
        private CharacterController character; //当前生效的特殊技能组别引用
        private ManagerVars vars; //资源管理器
        private NormalInputParamter paramter;


        void Start() {
            currProp = null; //开始将当前的技能设为空，玩家不释放技能
            vars = ManagerVars.GetManagerVarsContainer(); //获取资源管理器
            character = GetComponent<CharacterController>();
            paramter = GameObject.FindWithTag(Tags.InputManager).GetComponent<NormalInputParamter>();
            currPropSkillsGroup = propSkillsGroup1; //初始生效的技能组别为第一组
            InitializeProps(); //初始化所有技能，设定技能参数
        }

        private void InitializeProps() { //初始化技能列表的方法
            AjPropNormalAttack1 normalAttack1 = new AjPropNormalAttack1 { //普通攻击1
                prop = vars.C1A1,
                maxHurt = 60,
                magicLess = 5,
                ColdTime = 0,
                enable = true,
                maxColdTime = 3f
            };
            normalAttack1.SetRectProp(new RectProp {width = 1f, height = 10f}); //设定普通攻击使用矩形范围检测计算伤害
            normalSkills.Add(normalAttack1); //将普通攻击1填入技能列表
            AjPropNormalAttack2 normalAttack2 = new AjPropNormalAttack2 { //普通攻击2
                prop = vars.C1A2,
                maxHurt = 60,
                magicLess = 5,
                ColdTime = 0,
                enable = true,
                maxColdTime = 3f
            };
            normalAttack2.SetRectProp(new RectProp {width = 1f, height = 10f}); //设定普通攻击使用矩形范围检测计算伤害
            normalSkills.Add(normalAttack2); //将普通攻击1填入技能列表

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            AjGroundSkillQ1 groundSkillQ1 = new AjGroundSkillQ1() {
                prop = vars.C1Q1,
                maxHurt = 60,
                magicLess = 10,
                ColdTime = 0,
                enable = true,
                maxColdTime = 3f
            };
            groundSkillQ1.SetRectProp(new RectProp {width = 1f, height = 10f});
            propSkillsGroup1.Add(groundSkillQ1);
            AjGroundSkillE1 groundSkillE1 = new AjGroundSkillE1() {
                prop = vars.C1E1,
                maxHurt = 60,
                magicLess = 20,
                ColdTime = 0,
                enable = true,
                maxColdTime = 3f
            };
            groundSkillE1.SetRectProp(new RectProp {width = 1f, height = 10f});
            propSkillsGroup1.Add(groundSkillE1);
            AjGroundSkillR1 groundSkillR1 = new AjGroundSkillR1() {
                prop = vars.C1R1,
                maxHurt = 40,
                magicLess = 10,
                ColdTime = 0,
                enable = true,
                maxColdTime = 3f
            };
            groundSkillR1.SetRectProp(new RectProp {width = 1f, height = 10f});
            propSkillsGroup1.Add(groundSkillR1);
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            AjFlySkillQ2 flySkillQ2 = new AjFlySkillQ2() {
                prop = vars.C1Q2,
                maxHurt = 60,
                magicLess = 10,
                ColdTime = 0,
                enable = true,
                maxColdTime = 3f
            };
            flySkillQ2.SetRectProp(new RectProp {width = 1f, height = 10f});
            propSkillsGroup2.Add(flySkillQ2);
            AjFlySkillE2 flySkillE2 = new AjFlySkillE2() {
                prop = vars.C1E2,
                maxHurt = 60,
                magicLess = 20,
                ColdTime = 0,
                enable = true,
                maxColdTime = 3f
            };
            flySkillE2.SetRectProp(new RectProp {width = 1f, height = 10f});
            propSkillsGroup2.Add(flySkillE2);
            AjFlySkillR2 flySkillR2 = new AjFlySkillR2() {
                prop = vars.C1R2,
                maxHurt = 40,
                magicLess = 10,
                ColdTime = 0,
                enable = true,
                maxColdTime = 3f
            };
            flySkillR2.SetRectProp(new RectProp {width = 1f, height = 10f});
            propSkillsGroup2.Add(flySkillR2);
        }

        private void FixedUpdate() {
            currPropSkillsGroup = paramter.inputSprint ? propSkillsGroup2 : propSkillsGroup1; //判断玩家是否位于地面，保证技能组的正确切换
        }

        void Update() {
            foreach (var normalSkill in normalSkills) { //为所有技能重新设定冷却时间
                if (!Mathf.Approximately(normalSkill.ColdTime, 0)) {
                    normalSkill.ColdTime -= Time.unscaledDeltaTime;
                }
            }

            foreach (var propSkill in currPropSkillsGroup) {
                if (!Mathf.Approximately(propSkill.ColdTime, 0)) {
                    propSkill.ColdTime -= Time.unscaledDeltaTime;
                }
            }
        }
    }
}