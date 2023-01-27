using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;

public class DwarfRangeSkill : SkillEffect
{
    protected override float setSpeed()
    {
        return 15f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return false;
    }

    protected override float setDestroyTime()
    {
        return 1f;
    }

    public override void setDirection(Vector3 targetPosition)
    {
        Vector3 target = targetPosition + new Vector3(0f, gameObject.transform.position.y, 0f);
        base.direction = (target - gameObject.transform.position).normalized;
    }

    protected override void specialLogic()
    {
        
    }
}
