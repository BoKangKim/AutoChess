using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaWarriorSkill : SkillEffect
{
    private void Awake()
    {
        GameManager.Inst.soundOption.SFXPlay("Mecha_Warrior_Skill");
    }
    protected override float setDestroyTime()
    {
        return 1.5f;
    }

    protected override float setSpeed()
    {
        return 0f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return false;
    }

    protected override void specialLogic()
    {
    }
    private void OnEnable()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;

    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Battle.AI.ParentBT>() != null)
        {
            //if (owner.getMyNickName() != collision.gameObject.GetComponent<Battle.AI.ParentBT>().getMyNickName())
            {
                collision.gameObject.GetComponent<Battle.AI.ParentBT>().doDamage((owner.getSpellPower() / 100) * 100 + (owner.getAttackDamage() * 1.5f));
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
