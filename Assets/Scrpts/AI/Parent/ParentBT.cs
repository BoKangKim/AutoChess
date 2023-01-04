using UnityEngine;
using BehaviorTree;
using static BehaviorTree.BehaviorTreeMan;
using System;

public abstract class ParentBT : MonoBehaviour
{
    private INode root = null;
    private INode specialRoot = null;

    protected Animator myAni = null;

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
            ) ;
    }

    protected virtual void initializingSpecialRootNode() {}

    protected virtual Action idle 
    {
        get 
        {
            return () => 
            {
                myAni.SetBool("isMove",false);
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
                myAni.SetBool("isMove",true);
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
                Debug.Log("Attack");
                myAni.SetTrigger("isAttack");
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
                myAni.SetTrigger("isDeath");
            };
        }
    }

   
}
