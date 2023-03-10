using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.EFFECT;
using Battle.AI;

public class OrcRangeSkill : SkillEffect
{
    private float sTime = 0f;
    private const float skillTime = 3f;
    private float initAttackSpeed = 0f;
    private float initAttackRange = 0f;
    private Animator ownerAni = null;

    private void Start()
    {
        owner.TryGetComponent<Animator>(out ownerAni);
    }

    protected override float setSpeed()
    {
        return 0f;
    }

    protected override float setDestroyTime()
    {
        return 4f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return true;
    }

    public void initSpeedAndArrange(float attackSpeed, float attackArrange)
    {
        this.initAttackSpeed = attackSpeed;
        this.initAttackRange = attackArrange;
    }

    protected override void specialLogic()
    {
        if(sTime == 0f)
        {
            ownerAni.speed = initAttackSpeed;
            owner.setAttackRange(initAttackRange);

            ownerAni.speed = 5f;
            owner.setAttackRange(initAttackRange + 1f);
        }

        sTime += Time.deltaTime;

        if(sTime >= skillTime)
        {
            ownerAni.speed = initAttackSpeed;
            owner.setAttackRange(initAttackRange);

            Destroy(gameObject);
        }

    }
}
