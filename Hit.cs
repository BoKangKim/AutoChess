using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.EFFECT;

[RequireComponent(typeof(BoxCollider))]
public class Hit : Effect
{
    protected override float setDestroyTime()
    {
        return 1f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return false;
    }

    protected override float setSpeed()
    {
        return 0f;
    }

}
