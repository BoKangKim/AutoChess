using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DemonRangeAI : RangeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name, transform.position, Quaternion.identity).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
    }
}
