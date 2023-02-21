using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DwarfTankerAI : MeleeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        Vector3 targetPos = target.transform.position;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name, new Vector3(transform.position.x, 1f, transform.position.z), Quaternion.identity).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);
    }

}
