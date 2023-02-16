using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaMagicianSkill : SkillEffect
{
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
        return 0f;
    }

    protected override void specialLogic()
    {

    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Debug.Log("µûÄá~");
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
