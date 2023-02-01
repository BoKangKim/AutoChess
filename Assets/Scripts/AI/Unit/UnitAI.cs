using UnityEngine;
using System;
using BehaviorTree;
using static BehaviorTree.BehaviorTreeMan;

namespace Battle.AI 
{
    public abstract class UnitAI : ParentBT
    {
        protected INode special = null;
        protected float mana = 0f;
        protected float maxMana = 10f;

        protected override float setAttackRange()
        {
            return 1f;
        }

        protected override string initializingMytype()
        {
            return "UnitAI";
        }

        protected override INode initializingSpecialRootNode()
        {
            attackRange = 3f;

            special = Selector
                (
                    IfAction(isFullMana,activeSkill)
                );
            return special;
        }

        protected virtual Func<bool> isFullMana
        {
            get
            {
                return () =>
                {
                    if(mana > maxMana)
                    {
                        return true;
                    }

                    return false;
                };
            }
        }

        protected virtual Action activeSkill 
        {
            get
            {
                return () =>
                {
                    mana = 0f;
                    myAni.SetTrigger("activeSkill");
                    StartSkillEffect();
                };
            }
        }
    }
}

