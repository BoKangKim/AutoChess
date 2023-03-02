using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemRangeSkill : SkillEffect
{
    private void Awake()
    {
        GameManager.Inst.soundOption.SFXPlay("Golem_RangeDealer_Skill");
    }
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
