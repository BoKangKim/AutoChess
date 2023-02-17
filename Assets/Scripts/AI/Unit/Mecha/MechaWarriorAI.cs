using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MechaWarriorAI : MeleeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name, transform.position + transform.forward + (Vector3.up * 1.5f),Quaternion.LookRotation(target.transform.position)).TryGetComponent<SkillEffect>(out skill);

        skill.setOwner(this);
    }
}
