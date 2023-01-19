using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;

public class ExampleAI_2 : ParentBT, Unit
{

    protected override string initializingMytype()
    {
        attackRange = 1f;
        return typeof(Unit).ToString();
    }

    public override void StartEffect()
    {
        Instantiate<GameObject>(effect, target.transform.position, Quaternion.LookRotation(transform.forward));

    }
}

