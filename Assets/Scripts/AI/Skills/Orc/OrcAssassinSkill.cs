using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.EFFECT;

public class OrcAssassinSkill : SkillEffect
{
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
        StartCoroutine("CO_EnterOwner");
        GameManager.Inst.soundOption.SFXPlay("Orc_Assassin_Skill");
    }

    IEnumerable CO_EnterOwner()
    {
        yield return new WaitUntil(() => owner != null);
        owner.getTarget().doDamage(owner.getAttackDamage() * 6f);
    }
}
