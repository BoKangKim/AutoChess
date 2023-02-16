using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;
using Photon.Pun;
using Photon.Realtime;

public class OrcRangeAI : RangeAI
{
    public override void StartSkillEffect()
    {
        OrcRangeSkill skill = null;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name, effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<OrcRangeSkill>(out skill);
        skill.setOwner(this);
        skill.initSpeedAndArrange(myAni.speed, base.attackRange);
    }
}
