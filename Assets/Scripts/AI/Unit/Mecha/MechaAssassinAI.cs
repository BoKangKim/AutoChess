using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaAssassinAI : MeleeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Instantiate(skillEffect.gameObject,new Vector3(transform.position.x,6f,transform.position.z),Quaternion.Euler(new Vector3(90f,0f, 0f))).TryGetComponent<SkillEffect>(out skill);
        skill.setOwnerName(nickName);
        skill.setDirection(target.transform.position);
    }
}
