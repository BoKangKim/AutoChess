using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MechaRangeAI : RangeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name, effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
    }
}
