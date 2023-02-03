using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;
using Battle.EFFECT;

public class MeleeAI : UnitAI
{

    public override void StartEffect()
    {
        mana += manaRecovery;
        Effect attack = null;
        Instantiate(standardAttackEffect.gameObject, target.transform.position + Vector3.up, Quaternion.LookRotation(transform.forward)).TryGetComponent<Effect>(out attack);
        attack.setAttackDamage(unitData.GetUnitData.GetAtk);
        attack.setOwnerName(nickName);
    }

    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Vector3 targetPos = target.transform.position;
        Instantiate(skillEffect.gameObject, new Vector3(targetPos.x, 0f, targetPos.z), Quaternion.LookRotation(transform.forward)).TryGetComponent<SkillEffect>(out skill);
        skill.setOwnerName(nickName);
    }
}

