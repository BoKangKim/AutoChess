using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaRangeAI : RangeAI
{
    public override void StartSkillEffect()
    {
        if (myAni.GetParameter(2).name.CompareTo("activeSkill") == 0)
        {
            myAni.SetTrigger("activeSkill");
        }

        SkillEffect skill = null;
        Instantiate(skillEffect.gameObject, effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<SkillEffect>(out skill);
        skill.setOwnerName(nickName);
    }
}
