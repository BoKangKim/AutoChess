using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;
using Battle.EFFECT;
using Photon.Realtime;
using Photon.Pun;

public class MeleeAI : UnitAI
{

    public override void StartEffect()
    {
        if(photonView.IsMine == false)
        {
            return;
        }
        mana += manaRecovery;
        Effect attack = null;
        if(PhotonNetwork.Instantiate(standardAttackEffect.gameObject.name, target.transform.position + Vector3.up, Quaternion.LookRotation(transform.forward)).TryGetComponent<Effect>(out attack))
        {
            attack.setAttackDamage(unitData.GetUnitData.GetAtk);
            attack.setOwner(this);
        }
    }

    public override void StartSkillEffect()
    {
        if (photonView.IsMine == false)
        {
            return;
        }
        SkillEffect skill = null;
        Vector3 targetPos = target.transform.position;
        if(PhotonNetwork.Instantiate(skillEffect.gameObject.name, new Vector3(targetPos.x, 0f, targetPos.z), Quaternion.LookRotation(transform.forward)).TryGetComponent<SkillEffect>(out skill))
        {
            skill.setOwner(this);
        }
    }
}

