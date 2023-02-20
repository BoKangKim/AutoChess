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
        gameObject.GetComponent<BoxCollider>().enabled = true;
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
    protected override void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<Battle.AI.ParentBT>() != null)
        {
            if (owner.getMyNickName() != collision.gameObject.GetComponent<Battle.AI.ParentBT>().getMyNickName())
            {
                Debug.Log("µûÄá!");
                collision.gameObject.GetComponent<Battle.AI.ParentBT>().doDamage(attackDamage * 6);
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }

        }





    }
}
