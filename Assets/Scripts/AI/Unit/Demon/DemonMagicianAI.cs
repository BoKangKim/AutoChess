using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMagicianAI : RangeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Instantiate(skillEffect.gameObject, target.transform.position + Vector3.up, Quaternion.identity).TryGetComponent<SkillEffect>(out skill);
        skill.setOwnerName(nickName);
    }
}
