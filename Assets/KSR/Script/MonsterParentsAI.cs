
using UnityEngine;
using System;
using BehaviorTree;
using static BehaviorTree.BehaviorTreeMan;

public abstract class MonsterParentsAI : MonoBehaviour
{
    private INode root = null;

    // 나의 애니메이션 가져오기
    protected Animator myAni;

    // 무기
    public Wepon wepon;

    // 드롭 아이템
    public GameObject Item;

    private void Awake()
    {
        root = GetComponent<INode>();

    }
    private void Start()
    {
        myAni = GetComponent<Animator>();
        InitRootNode();
        wepon = FindObjectOfType<Wepon>();
        Item.SetActive(false);
    }

    private void Update()
    {

        root.Run();
    }

    private void InitRootNode()  // 몬스터 AI 기본 행동 로직
    {
        Debug.Log("이닛");
        root = Selector
            (
                IF(IsFind),
                IfElseAction(IsArrange, IsMove, IsAttack),

                 Sequence
                (
                     IfElseAction(IsDead, IsHit, IsDrop)                  
                )
            );

    }


    // 어떤 경우에 대기할건지 정할 것
    protected virtual Action IsIdle
    {
        get
        {
            return () =>
            {
                Debug.Log("대기");
                myAni.SetBool("IsMove", false);
            };
        }
    }

    protected virtual Func<bool> IsFind
    {
        get
        {
            return () =>
            {
                Debug.Log("타겟 찾기");

                return true;
            };
        }
    }


    protected virtual Action IsMove
    {
        get
        {
            return () =>
            {
                Debug.Log("이동");
                myAni.SetBool("IsMove", true);
            };
        }
    }
    protected virtual Func<bool> IsArrange
    {
        get
        {
            return () =>
            {
                Debug.Log("범위 내에 있음");
                return false;
            };
        }
    }
    protected virtual Action IsAttack
    {
        get
        {
            return () =>
            {
                Debug.Log("공격");
                myAni.SetBool("IsAttack", true);

            };
        }
    }
    protected virtual Action IsHit
    {
        get
        {
            return () =>
            {
                Debug.Log("맞는다");
                myAni.SetTrigger("IsHit");

            };
        }
    }
    protected virtual Func<bool> IsDead
    {
        get
        {
            return () =>
            {
                Debug.Log("죽음");
                return false;
            };
        }
    }
    // 죽음 - 맞는다 -> 
    protected virtual Action IsDrop
    {
        get
        {
            return () =>
            {
                Debug.Log("드롭");
            };
        }
    }

}
