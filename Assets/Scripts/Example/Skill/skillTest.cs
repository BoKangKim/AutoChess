using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skills;

public class skillTest : Skill
{
    protected override string initializingName()
    {
        return "Poison";
    }

    protected override float initializingskillTime()
    {
        return 1f;
    }

    protected override void run()
    {
        // ±¸ÇöºÎ
    }

    protected override void casting()
    {
        base.casting();
    }
}
