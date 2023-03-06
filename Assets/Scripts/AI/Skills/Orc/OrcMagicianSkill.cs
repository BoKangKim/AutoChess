using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMagicianSkill : SkillEffect
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
        return false;
    }
    private void OnEnable()
    {
        GameManager.Inst.soundOption.SFXPlay("Orc_Magician_Skill");
    }
    protected override void specialLogic()
    {

    }


}
