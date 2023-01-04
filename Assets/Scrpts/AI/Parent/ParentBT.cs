using UnityEngine;
using BehaviorTree;
using static BehaviorTree.BehaviorTreeMan;
using System;

public abstract class ParentBT : MonoBehaviour
{
    private INode root = null;
    private INode specialRoot = null;

    private void Awake()
    {
        initializingSpecialRootNode();
    }

    private void Update()
    {
        root.Run();

        if(specialRoot == null)
        {
            return;
        }
    }

    private void InitializingRootNode()
    {
        root = Selector
            (
                IfAction(isDeath,death),

                Sequence
                (
                    ActionN(idle),
                    NotIf(findEnemy)
                ),

                IfElseAction(isArangeIn,attack,move)
            );
    }

    protected abstract void initializingSpecialRootNode();

    protected virtual Action idle 
    {
        get 
        {
            return () => { };
        }
    }

    protected virtual Func<bool> findEnemy 
    {
        get
        {
            return () => { return false; };
        }
    }

    protected virtual Action move 
    {
        get 
        {
            return () => { };
        }
    }

    protected virtual Func<bool> isArangeIn 
    {
        get 
        {
            return () => { return false; };
        }
    }

    protected virtual Action attack 
    {
        get
        {
            return () => { };
        }
    }

    protected virtual Func<bool> isDeath
    {
        get
        {
            return () => { return false; };
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
