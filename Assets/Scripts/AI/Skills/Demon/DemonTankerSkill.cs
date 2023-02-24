using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonTankerSkill : SkillEffect
{
    protected override float setSpeed()
    {
        return 1f;
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
        Battle.AI.ParentBT skilltarget = null;

        if (collision.gameObject.TryGetComponent<Battle.AI.ParentBT>(out skilltarget))
        {
            //if (owner.getMyNickName() == skilltarget.getMyNickName())
            {
                Debug.Log(skilltarget.gameObject.name + "Èú");
                skilltarget.setRecoveryCurrentHP((skilltarget.getMaxHP() * 100)*10 );
            }
        }
    }

}
