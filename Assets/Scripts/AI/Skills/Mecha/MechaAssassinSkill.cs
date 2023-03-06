using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MechaAssassinSkill : SkillEffect
{
    [SerializeField] private GameObject collisionEffect = null;
    private GameObject inst = null;
    private Vector3 euler = new Vector3(-90f, 0f, 0f);

    
    protected override float setDestroyTime()
    {
        return 3f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return false;
    }

    protected override float setSpeed()
    {
        return 8f;
    }

    public override void setDirection(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(-direction);
    }
    private void OnEnable()
    {
        StartCoroutine("CO_EnterOwner");
        GameManager.Inst.soundOption.SFXPlay("Mecha_Assassin_Skill");
    }


    IEnumerable CO_EnterOwner()
    {
        yield return new WaitUntil(() => owner != null);
        owner.getSkillTarget().doDamage(owner.getAttackDamage() * 6f);
    }


    private void OnDestroy()
    {
        PhotonNetwork.Destroy(inst);
    }
  
    protected override void specialLogic()
    {

        if (transform.position.y <= 2.6f && inst == null)
        {
            inst = PhotonNetwork.Instantiate(collisionEffect.name, new Vector3(transform.position.x, 0.2f, transform.position.z),Quaternion.Euler(euler));
        }
    }
    

  
}
