using UnityEngine;
using BehaviorTree;
using static BehaviorTree.BehaviorTreeMan;
using System;

public abstract class ParentBT : MonoBehaviour
{
    private INode root = null;

    private void Awake()
    {
        initializingRootNode();
    }

    private void Update()
    {
        root.Run();
    }

    protected abstract void initializingRootNode();

    protected virtual Action idle 
    {
        get 
        {
            return () => { };
        }
    }

    protected virtual Action findEnemy 
    {
        get
        {
            return () => { };
        }
    }

    protected virtual Action move 
    {
        get 
        {
            return () => { };
        }
    }

    protected virtual Action attack 
    {
        get
        {
            return () => { };
        }
    }

    protected virtual Action death 
    {
        get
        {
            return () => { };
        }
    }

   
}
