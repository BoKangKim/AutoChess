using UnityEngine;
using BehaviorTree;
using static BehaviorTree.BehaviorTreeMan;
using System;

public abstract class ParentBT : MonoBehaviour
{
    private INode root = null;
    private INode specialRoot = null;
    protected Animator myAni = null;

    private string nickName = "";
    protected AIObject target = null;

    private string stageType = "PVP";

    private void Awake()
    {
        InitializingRootNode();
        initializingSpecialRootNode();
    }

    private void Start()
    {
        myAni = GetComponent<Animator>();
    }

    private void Update()
    {
        root.Run();
        
        if(specialRoot == null)
        {
            return;
        }

        specialRoot.Run();
    }

    private void InitializingRootNode()
    {
        root = Selector
            (
                IfAction(isDeath, death),

                Sequence
                (
                    ActionN(idle),
                    NotIf(findEnemy)
                ),
               
                IfElseAction(isArangeIn, attack, move)
            );
    }

    protected virtual void initializingSpecialRootNode() {}

    #region AI Behavior
    protected virtual Action idle 
    {
        get 
        {
            return () => 
            {
            };
        }
    }

    protected virtual Func<bool> findEnemy 
    {
        get
        {
            return () => 
            {
                return true; 
            };
        }
    }

    protected virtual Action move 
    {
        get 
        {
            return () => 
            {
            };
        }
    }

    protected virtual Func<bool> isArangeIn 
    {
        get 
        {
            return () => 
            { 
                return false; 
            };
        }
    }

    protected virtual Func<bool> isAttackAble
    {
        get 
        {

            return () =>
            {
                return false;
            };
        }
    }

    protected virtual Action attack 
    {
        get
        {
            return () => 
            {
            };
        }
    }

    protected virtual Func<bool> isDeath
    {
        get
        {
            return () => 
            {
                return false; 
            };
        }
    }

    protected virtual Action death 
    {
        get
        {
            return () => 
            {
                
            };
        }
    }


    #endregion
}
