using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaTankerSkill : SkillEffect
{


    private void OnEnable()
    {
        StartCoroutine("CO_EnterOwner");
        GameManager.Inst.soundOption.SFXPlay("Mecha_Tanker_Skill");
    }

    IEnumerable CO_EnterOwner()
    {
        yield return new WaitUntil(() => owner != null);
        owner.setRecoveryCurrentHP((owner.getUnitData().GetTotalMaxHp / 100) * 20);
    }


    protected override float setDestroyTime()
    {
        return 5f;
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
