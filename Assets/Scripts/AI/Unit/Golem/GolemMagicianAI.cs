using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMagicianAI : RangeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Instantiate(skillEffect.gameObject, target.transform.position + Vector3.up, Quaternion.Euler(new Vector3(-90f, 0f, 0f))).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
    }
}
