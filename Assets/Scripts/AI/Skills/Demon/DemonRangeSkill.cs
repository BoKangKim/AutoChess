using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonRangeSkill : SkillEffect
{
    protected override float setDestroyTime()
    {
        return 2f;
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
}
