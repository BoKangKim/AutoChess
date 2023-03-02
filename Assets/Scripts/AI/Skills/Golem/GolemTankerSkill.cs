using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemTankerSkill : SkillEffect
{
    private void Awake()
    {
        GameManager.Inst.soundOption.SFXPlay("Golem_Tanker_Skill");
    }
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
