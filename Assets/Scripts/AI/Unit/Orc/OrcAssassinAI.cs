using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OrcAssassinAI : MeleeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name, target.transform.position, Quaternion.LookRotation(target.transform.position)).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
    }
}
