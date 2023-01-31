using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.EFFECT;

public class OrcAssassinSkill : SkillEffect
{
    protected override float setSpeed()
    {
        return 0f;
    }

    protected override float setDestroyTime()
    {
        return 2.5f;
    }

    protected override void specialLogic()
    {
        // 지속뎀
        // 실명
    }
}
