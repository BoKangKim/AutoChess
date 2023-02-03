using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.EFFECT;
using Battle.AI;

public class SkillEffect : Effect
{
    protected override float setDestroyTime()
    {
        return 1f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return true;
    }

    protected override float setSpeed()
    {
        return 15f;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (ownerName == null)
        {
            Debug.LogError("Please Set OwnerName");
            return;
        }

        ParentBT target = null;

        if (collision.transform.TryGetComponent<ParentBT>(out target) == true)
        {
            if (target.getMyNickName().CompareTo(ownerName) != 0)
            {
                if (HitEffect != null)
                {
                    Instantiate(HitEffect, gameObject.transform.position, Quaternion.identity);
                }
                // Hit Damage Logic
            }
        }
    }
}
