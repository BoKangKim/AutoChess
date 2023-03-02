using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MechaAssassinAI : MeleeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        PhotonNetwork.Instantiate(skillEffect.gameObject.name,new Vector3(transform.position.x,6f,transform.position.z),Quaternion.Euler(new Vector3(90f,0f, 0f))).TryGetComponent<SkillEffect>(out skill);
        skill.setOwner(this);

        skilltarget = getFindEnemies()[Random.Range(0, getFindEnemies().Count)];

        skill.setDirection(skilltarget.transform.position);
    }
}
