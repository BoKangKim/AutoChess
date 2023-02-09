using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcWarriorAI : MeleeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Vector3 targetPos = target.transform.position;
        Instantiate(skillEffect.gameObject, transform.position + (transform.forward * 3.5f), Quaternion.LookRotation(targetPos - transform.position).normalized).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
      
    }
}
