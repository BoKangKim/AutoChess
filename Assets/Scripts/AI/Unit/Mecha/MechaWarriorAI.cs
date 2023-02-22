using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MechaWarriorAI : MeleeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        //Vector3 targetPos = target.transform.position;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name, transform.position + transform.forward + (Vector3.up * 1.5f), Quaternion.identity).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
    }
}
