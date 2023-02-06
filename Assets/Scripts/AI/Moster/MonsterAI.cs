using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;

public class MonsterAI : ParentBT
{
    protected override string initializingMytype()
    {
        return "Monster";
    }

    protected override float setAttackRange()
    {
        return 2f;
    }

}
