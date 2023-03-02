using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.EFFECT;

public class OrcAssassinSkill : SkillEffect
{

    private void Awake()
    {
        GameManager.Inst.soundOption.SFXPlay("Orc_Assassin_Skill");
    }
    protected override float setSpeed()
    {
        return 1f;
    }

    protected override float setDestroyTime()
    {
        return 2.5f;
    }

    protected override void specialLogic()
    {
        
    }
    private void OnEnable()
    {
        Debug.Log("µûÄá!");
        owner.getTarget().doDamage(owner.getAttackDamage() * 6f);
    }
}
