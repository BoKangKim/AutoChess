using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfAssassinSkill : SkillEffect
{

    private void Awake()
    {
        GameManager.Inst.soundOption.SFXPlay("Dwarf_Assassin_Skill");
    }

    protected override float setSpeed()
    {
        return 5f;
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
