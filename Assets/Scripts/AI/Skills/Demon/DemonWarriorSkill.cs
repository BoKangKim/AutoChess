using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonWarriorSkill : SkillEffect
{
    private void Awake()
    {
        GameManager.Inst.soundOption.SFXPlay("Demon_Warrior_Skill");
    }
    protected override float setSpeed()
    {
        return 0f;
    }

    protected override float setDestroyTime()
    {
        return 1.5f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return true;
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        DemonTankerAI skilltarget = null;

        if (collision.gameObject.TryGetComponent<DemonTankerAI>(out skilltarget))
        {
            //if (owner.getMyNickName() != collision.gameObject.GetComponent<Battle.AI.ParentBT>().getMyNickName())
            {
                skilltarget.doDamage((owner.getSpellPower() / 100) * 100 + (owner.getAttackDamage() * 1.5f));
            }
        }
    }
}
