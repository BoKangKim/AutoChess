using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DemonAssassinAI : MeleeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Vector3 targetPos = target.transform.position;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name,Vector3.zero,skillEffect.transform.rotation).TryGetComponent<SkillEffect>(out skill);
        skill.gameObject.transform.position = new Vector3(targetPos.x, 0f, targetPos.z);
        skill.setOwner(this);
    }
}
