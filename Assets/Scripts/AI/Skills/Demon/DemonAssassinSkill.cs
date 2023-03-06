using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAssassinSkill : SkillEffect
{
    protected override float setSpeed()
    {
        return 1f;
    }

    protected override float setDestroyTime()
    {
        return 2f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return true;
    }
    private void OnEnable()
    {
        
        StartCoroutine("CO_EnterOwner");
        GameManager.Inst.soundOption.SFXPlay("Demon_Assassin_Skill");

    }

    IEnumerable CO_EnterOwner()
    {
        yield return new WaitUntil(() => owner != null);
        owner.getSkillTarget().doDamage(owner.getAttackDamage() * 5);
    }
}
