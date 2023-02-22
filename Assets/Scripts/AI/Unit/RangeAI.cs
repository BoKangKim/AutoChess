using Battle.AI;
using UnityEngine;
using Battle.EFFECT;
using Photon.Pun;
using Photon.Realtime;

public class RangeAI : UnitAI
{
    [SerializeField] protected Transform effectStartPos = null;
    [SerializeField] protected Effect projectile = null;

    public override void StartEffect()
    {
        if(photonView.IsMine == false)
        {
            return;
        }

        mana += manaRecovery;
        if (mana > maxMana)
        {
            return;
        }

        Effect flash = null;
        Effect project = null;
        if (PhotonNetwork.Instantiate(standardAttackEffect.gameObject.name, effectStartPos.transform.position, Quaternion.identity).TryGetComponent<Effect>(out flash))
        {
            flash.setOwner(this);
        }

        if(PhotonNetwork.Instantiate(projectile.gameObject.name, effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<Effect>(out project))
        {
            project.setOwner(this);
            project.setDirection(target.transform.position);
        }
    }

    public override void StartSkillEffect()
    {
        SkillEffect skill = null;
        if(PhotonNetwork.Instantiate(skillEffect.gameObject.name, effectStartPos.transform.position, Quaternion.LookRotation(transform.forward)).TryGetComponent<SkillEffect>(out skill))
        {
            skill.setOwner(this);
            skill.setDirection(target.transform.position);
        }
    }
}
