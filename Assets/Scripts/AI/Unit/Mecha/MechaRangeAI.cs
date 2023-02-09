using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaRangeAI : RangeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Instantiate(skillEffect.gameObject, effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
    }
}
