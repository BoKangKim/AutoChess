using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;

public class OrcRangeAI : RangeAI
{
    public override void StartSkillEffect()
    {
        if (myAni.GetParameter(2).name.CompareTo("activeSkill") == 0)
        {
            myAni.SetTrigger("activeSkill");
        }
        OrcRangeSkill skill = null;
        Instantiate(skillEffect.gameObject, effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<OrcRangeSkill>(out skill);
        skill.setOwnerName(nickName);
        skill.initOwner(this);
        skill.initSpeedAndArrange(myAni.speed, base.attackRange);
    }
}
