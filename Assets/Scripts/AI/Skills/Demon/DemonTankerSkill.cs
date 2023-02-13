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
}
