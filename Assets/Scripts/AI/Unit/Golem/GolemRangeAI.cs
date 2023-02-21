using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GolemRangeAI : RangeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name, transform.position + Vector3.up, Quaternion.identity).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
    }
}
