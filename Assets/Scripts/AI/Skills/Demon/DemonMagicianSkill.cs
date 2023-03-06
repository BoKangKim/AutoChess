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

    private void OnEnable()
    {
        StartCoroutine("CO_EnterOwner");
        GameManager.Inst.soundOption.SFXPlay("Demon_Magician_Skill");
    }


    IEnumerable CO_EnterOwner()
    {
        yield return new WaitUntil(() => owner != null);
        for (int i = 0; i < owner.getFindMyUnits().Count; i++)
        {
            owner.getFindMyUnits()[i].setAttackDamage(5f);
            owner.getFindMyUnits()[i].setSpellPower(5f);
        }
    }


}






