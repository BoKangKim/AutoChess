using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class DemonAssassinAI : MeleeAI
{
    public override void StartSkillEffect()
    {
        SkillEffect skill = null;

        Vector3 targetPos = target.transform.position;

        skilltarget = getFindEnemies()[Random.Range(0, getFindEnemies().Count)];
        
        Debug.Log($"{skilltarget.name},{skilltarget.transform.position}");
        Vector3 skilltargetPos = new Vector3(skilltarget.transform.position.x, 2f, skilltarget.transform.position.z - 1f);
        PhotonNetwork.Instantiate(skillEffect.gameObject.name , skilltargetPos, Quaternion.LookRotation(skilltarget.transform.position)).TryGetComponent<SkillEffect>(out skill);

        skill.setOwner(this);
    }
}
