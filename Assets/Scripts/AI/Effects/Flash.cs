using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.EFFECT;

public class Flash : Effect
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
        return 0f;
    }
}
