using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;
using Battle.EFFECT;

public class ExampleAI_2 : ParentBT, Unit
{

    protected override string initializingMytype()
    {
        attackRange = 1f;
        return typeof(Unit).ToString();
    }

    public override void StartEffect()
    {
        Effect attack = null;
        Instantiate(standardAttackEffect.gameObject, target.transform.position + Vector3.up, Quaternion.LookRotation(transform.forward)).TryGetComponent<Effect>(out attack);
        attack.setOwnerName(nickName);
    }

}

