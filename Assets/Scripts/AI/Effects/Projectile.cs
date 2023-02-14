using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.EFFECT;

public class Projectile : Effect
{
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
        return 15f;
    }

    public override void setDirection(Vector3 targetPosition)
    {
        Vector3 target = targetPosition + new Vector3(0f, gameObject.transform.position.y ,0f);
        base.direction = (target - gameObject.transform.position).normalized;
    }
}
