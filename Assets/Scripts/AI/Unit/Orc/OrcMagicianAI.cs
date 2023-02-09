using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMagicianAI : RangeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Instantiate(skillEffect.gameObject, target.transform.position, Quaternion.identity).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);

    }
}
