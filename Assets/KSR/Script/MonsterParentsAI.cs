
using UnityEngine;
using System;
using BehaviorTree;
using static BehaviorTree.BehaviorTreeMan;

public abstract class MonsterParentsAI : MonoBehaviour
{
    private INode root = null;

    // ���� �ִϸ��̼� ��������
    protected Animator myAni;

    private void Awake()
    {
        root = GetComponent<INode>();
        InitRootNode();

    }
    private void Start()
    {
        myAni = GetComponent<Animator>();
    }

    private void Update()
    {
        root.Run();
    }

    private void InitRootNode()  // ���� AI �⺻ �ൿ ����
    {
        Debug.Log("�̴�");
        root = Selector
            (                
                IF(IsFind),               
                IfElseAction(IsArrange, IsMove, IsAttack)
                // ���ݹ޾��� �� Ÿ�� �ִϸ��̼�

            );

    }

    // �������� �޾��� �� üũ�Ϸ���
    // � ���¿����� ���ݹ��� �� ����
    protected virtual Func<bool> ColiderCheck 
    { 
        get
        {
            return () =>
            {
                Debug.Log("����");
                return true;
            };
        }

    }


    // � ��쿡 ����Ұ��� ���� ��
    protected virtual Action IsIdle
    {
        get
        {
            return () =>
            {
                Debug.Log("���");
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
                Debug.Log("Ÿ�� ã��");
                
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
                Debug.Log("�̵�");
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
                Debug.Log("���� ���� ����");
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
                Debug.Log("����");
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
                Debug.Log("�´´�");
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
                Debug.Log("����");
                return false;                
            };
        }
    }
}
