using BehaviorTree;
using Photon.Pun;
using System;
using static BehaviorTree.BehaviorTreeMan;

namespace Battle.AI
{
    public abstract class UnitAI : ParentBT
    {
        protected INode special = null;

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
            special = Selector
                (
                     IfAction(isFullMana, activeSkill)
                );
            return special;
        }

        protected virtual Func<bool> isFullMana
        {
            get
            {
                return () =>
                {
                    if (mana > maxMana)
                    {
                        return true;
                    }

                    return false;
                };
            }
        }

        protected virtual Func<bool> activeTrigger
        {
            get
            {
                return () =>
                {
                    return false;
                };
            }
        }

        protected virtual Action activeSynergy
        {
            get
            {
                return () => { };
            }
        }

        protected virtual Action activeSkill
        {
            get
            {
                return () =>
                {
                    mana = 0f;
                    if (myAni.GetParameter(2).name.CompareTo("activeSkill") == 0)
                    {
                        photonView.RPC("RPC_SetTriggerActiveSkill", RpcTarget.All);
                    }
                    StartSkillEffect();
                };
            }
        }

        [PunRPC]
        public void RPC_SetTriggerActiveSkill()
        {
            myAni.SetTrigger("activeSkill");
        }
    }
}

