using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DemonMagicianAI : RangeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name, target.transform.position + Vector3.up, Quaternion.identity).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
    }
}
