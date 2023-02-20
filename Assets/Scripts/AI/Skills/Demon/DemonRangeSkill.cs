using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonRangeSkill : SkillEffect
{

    float damageTime = 0f;
    int count = 0;

    protected override float setDestroyTime()
    {
        return 4f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return true;
    }

    protected override float setSpeed()
    {
        return 0f;
    }

    protected override void specialLogic()
    {
    }

    private void OnEnable()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        Battle.AI.ParentBT skilltarget = null;
        damageTime += Time.deltaTime;
        if (collision.gameObject.TryGetComponent<Battle.AI.ParentBT>(out skilltarget))
        {
            //if (owner.getMyNickName() != collision.gameObject.GetComponent<Battle.AI.ParentBT>().getMyNickName())
            {
                if (count > 3)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    count = 0;
                    return;
                }

                if (damageTime >= 1f)
                {
                    skilltarget.doDamage((owner.getSpellPower() / 30) * 100 + (owner.getAttackDamage() * 0.5f));
                    damageTime = 0f;
                    ++count;
                }
            }
        }
    }
}
