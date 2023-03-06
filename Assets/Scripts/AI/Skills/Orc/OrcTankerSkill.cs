using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcTankerSkill : SkillEffect
{
    protected override float setDestroyTime()
    {
        return 3f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return true;
    }

    protected override float setSpeed()
    {
        return 0f;
    }

    protected override void specialLogic()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine("CO_EnterOwner");
        GameManager.Inst.soundOption.SFXPlay("Orc_Tanker_Skill");
    }

    IEnumerable CO_EnterOwner()
    {
        yield return new WaitUntil(() => owner != null);
        owner.setShield((owner.getUnitData().GetTotalMaxHp / 100) * 30);
    }
}
