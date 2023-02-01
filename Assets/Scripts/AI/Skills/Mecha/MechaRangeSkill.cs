using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaRangeSkill : SkillEffect
{
    protected override float setDestroyTime()
    {
        return 1.5f;
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
}
