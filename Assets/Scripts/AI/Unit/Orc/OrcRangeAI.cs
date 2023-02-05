using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;

public class OrcRangeAI : RangeAI
{
    public override void StartSkillEffect()
    {
        OrcRangeSkill skill = null;
        Instantiate(skillEffect.gameObject, effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<OrcRangeSkill>(out skill);
        skill.setOwner(this);
        skill.initSpeedAndArrange(myAni.speed, base.attackRange);
    }
}
