using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaAssassinSkill : SkillEffect
{
    [SerializeField] private GameObject collisionEffect = null;
    private GameObject inst = null;
    private Vector3 euler = new Vector3(-90f, 0f, 0f);

    protected override float setDestroyTime()
    {
        return 2f;
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

    private void OnDestroy()
    {
        Destroy(inst);
    }

    protected override void specialLogic()
    {
        if(transform.position.y <= 2.6f && inst == null)
        {
            inst = Instantiate(collisionEffect, new Vector3(transform.position.x, 0.2f, transform.position.z),Quaternion.Euler(euler));
        }
    }
}
