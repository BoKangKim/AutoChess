using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfAssassinDotDam : SkillEffect
{
    private float damageTime = 0f;
    private ParticleSystem particle = null;

    protected override float setSpeed()
    {
        return 0f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return false;
    }

    protected override float setDestroyTime()
    {
        return 3f;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        particle = GetComponent<ParticleSystem>();
        Debug.Log("Damage");
    }

    private void OnCollisionStay(Collision collision)
    {
        damageTime += Time.deltaTime;

        if(damageTime >= 1f)
        {
            particle.Play();
            Debug.Log("Damage");
            damageTime = 0f;
            // µ¥¹ÌÁö
        }
    }
}
