using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonsterParentsAI
{
    // ü��
    [SerializeField] float hp = 100;
    float speed = 2f;

    // Ÿ��
    GameObject target;
    // �� �޾ƿ� �迭
    GameObject[] enermies;

    float attackRange = 1f;
    float moveRange = 10f;


    private void Awake()
    {
    }

    protected override Action IsIdle
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
    protected override Func<bool> IsFind
    {
        get 
        {
            return () =>
            {
                Debug.Log("Ÿ�� ã��");
                target = GameObject.FindGameObjectWithTag("Target");
                if (target==null)
                {
                    Debug.Log("��ã��");
                    return true;
                }
                else
                {
                    Debug.Log("ã��");
                    return false;
                }
            };
        }
    }

   
    protected override Action IsMove
    {
        get
        {
            return () =>
            {
                Debug.Log("�̵�");
                myAni.SetBool("IsMove", true);
                myAni.SetBool("IsAttack", false);
                gameObject.transform.LookAt(target.transform);
                gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                
            };
        }
    }
    protected override Func<bool> IsArrange
    {
        get
        {
            return () =>
            {
                Debug.Log("���� ���� ����");
                float Distance = Vector3.Distance(target.transform.position, gameObject.transform.position);
                if (Distance <= attackRange)
                {
                    Debug.Log("���ݹ��� ���� �ֳ�");
                    myAni.SetBool("IsMove", false);
                    return false;
                }
                else
                {
                    return true;
                }

            };
        }
    }

    protected override Action IsAttack
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
    
    protected override Action IsHit
    {
        get
        {
            return () =>
            {
                Debug.Log("�´´�");
                myAni.SetTrigger("IsHit");
                hp -= wepon.damage;
                if (hp <= 0) 
                { 
                    hp = 0;
                }
            };
        }
    }
   
    protected virtual Func<bool> IsDead
    {
        get
        {
            return () =>
            {
                if (hp <= 0)
                {
                    Debug.Log("����");
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }
    }
    protected override Action IsDrop // ������ ������ ���
    {
        get
        {
            return () =>
            {
                Debug.Log("���");
                myAni.SetBool("IsDead", true);
                Item.SetActive(true);
                //this.gameObject.SetActive(false);

            };
        }
    }
}
