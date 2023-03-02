using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OrcWarriorAI : MeleeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Vector3 targetPos = target.transform.position;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name, transform.position + (transform.forward * 3.5f), Quaternion.LookRotation(targetPos - transform.position).normalized).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
    }
}
