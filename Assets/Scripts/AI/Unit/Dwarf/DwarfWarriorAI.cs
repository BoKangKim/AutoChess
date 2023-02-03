using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfWarriorAI : MeleeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Vector3 targetPos = target.transform.position;
        Instantiate(skillEffect.gameObject, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity).TryGetComponent<SkillEffect>(out skill);
        skill.setOwnerName(nickName);
    }

}
