using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaMagicianSkill : SkillEffect
{
    float damageTime = 0f;
    int count = 0;
    private void Awake()
    {
        GameManager.Inst.soundOption.SFXPlay("Mecha_Magician_Skill");
    }
    protected override float setDestroyTime()
    {
        return 2f;
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
        Battle.AI.ParentBT skillTarget = null;

        if (collision.gameObject.TryGetComponent<Battle.AI.ParentBT>(out skillTarget))
        {
            //if (owner.getMyNickName() != collision.gameObject.GetComponent<Battle.AI.ParentBT>().getMyNickName())
            {
                if (count > 4)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;

                    return;
                }

                if (damageTime >= 1f)
                {
                    skillTarget.doDamage((owner.getSpellPower() / 100) * 40);
                    damageTime = 0f;
                    ++count;

                }
            }
        }
    }

}
