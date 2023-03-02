using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaRangeSkill : SkillEffect
{
    float damageTime = 0f;
    int count = 0;

    private void Awake()
    {
        GameManager.Inst.soundOption.SFXPlay("Mecha_RangeDealer_Skill");
    }
    protected override float setDestroyTime()
    {
        return 3f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return false;
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
        count = 0;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        damageTime += Time.deltaTime;
        if (collision.gameObject.GetComponent<Battle.AI.ParentBT>() != null)
        {
            //if (owner.getMyNickName() != collision.gameObject.GetComponent<Battle.AI.ParentBT>().getMyNickName())
            {   
                if (count > 2)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    return;
                }

                if (damageTime >= 1f)
                {
                    Debug.Log(count);
                    collision.gameObject.GetComponent<Battle.AI.ParentBT>().doDamage((owner.getSpellPower() / 100) * 30 + (owner.getAttackDamage() * 0.5f));
                    damageTime = 0f;
                    ++count;
                }
            }
        }
    }
}
