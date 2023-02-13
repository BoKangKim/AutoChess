using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonWarriorSkill : SkillEffect
{
    protected override float setSpeed()
    {
        return 0f;
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
