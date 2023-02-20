using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DemonRangeAI : RangeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        skilltarget = target;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name, skilltarget.transform.position, Quaternion.identity).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
    }
}
