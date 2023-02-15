using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;
using BehaviorTree;
using System;
using Photon.Pun;
using Photon.Realtime;
using static BehaviorTree.BehaviorTreeMan;

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

    protected override INode initializingSpecialRootNode()
    {
        INode Special = Selector
            (
                IfAction(isPrepare,destroyMonster)
            );

        return Special;
    }

    private Func<bool> isPrepare 
    {
        get 
        {
            return () =>
            {
                Debug.Log(stageType + " " + myType);
                return stageType == Battle.Stage.STAGETYPE.PREPARE;
            };
        }
    }

    private Action destroyMonster
    {
        get
        {
            return () =>
            {
                PhotonNetwork.Destroy(this.gameObject);
            };
        }
    }
}
