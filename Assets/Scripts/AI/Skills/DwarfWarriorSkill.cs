using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.EFFECT;

public class DwarfWarriorSkill : SkillEffect
{
    protected override float setSpeed()
    {
        return 0f;
    }

    protected override float setDestroyTime()
    {
        return 2f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return true;
    }
}
