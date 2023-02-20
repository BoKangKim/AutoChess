using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaWarriorSkill : SkillEffect
{
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
                Debug.Log("µûÄá~");
                //collision.gameObject.GetComponent<Battle.AI.ParentBT>().doDamage((this.gameObject.GetComponent<UnitClass.Unit>().GetTotalSpellPower / 100) * 100 + (attackDamage * 1.5f));
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
