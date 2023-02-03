using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMagicianSkill : SkillEffect
{
    protected override float setDestroyTime()
    {
        return 3f;
    }

    protected override float setSpeed()
    {
        return 0f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return true;
    }

    protected override void specialLogic()
    {
    }
}
