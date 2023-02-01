using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemRangeAI : RangeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Instantiate(skillEffect.gameObject, transform.position + Vector3.up, Quaternion.identity).TryGetComponent<SkillEffect>(out skill);
        skill.setOwnerName(nickName);
    }
}
