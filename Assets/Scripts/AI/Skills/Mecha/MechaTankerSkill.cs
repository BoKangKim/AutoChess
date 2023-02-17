using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaTankerSkill : SkillEffect
{
    private void Awake()
    {
        Debug.Log("Èú~");
        owner.SetRecoveryCurrentHP((owner.GetUnitData().GetTotalMaxHp / 100) * 20);
    }
    protected override float setDestroyTime()
    {
        return 5f;
    }

    protected override float setSpeed()
    {
        return 0f;
    }

    protected override bool setIsNonAttackEffect()
    {
        return true;
    }

    protected override void specialLogic()
    {
    }
}
