using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonsterParentsAI
{
    // ü��
    private float hp = 100;
    private float speed = 2f;

    // Ÿ��
    private GameObject target;
  
    private float attackRange = 1f;

    private int per;


    // Action & Func
    #region Action & Func
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
                if (target == null)
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
                    IsDie = true;
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }
    }
    #endregion


    protected override Action IsDrop // ������ ������ ���
    {
        get
        {
            return () =>
            {
                Debug.Log("���");
                myAni.SetBool("IsDead", true);
                
                // ������ �ѹ��� �Ȼѷ����� �׳� ������ ����߰ڴ� �Ƹ��� ? ���� ���� ȣȣ ���� ŰŰŰ ������󸸽�
                
                if (ItemCount == 0)
                {
                    per = 0;
                    DropItem();
                }
                else
                {
                    DropItem();
                }                
            };
        }
    }

  
    int ItemCount = 0;
    private void DropItem()
    {
        
        switch (per)
        {
            case 0:
                Debug.Log("0");
                Item.SetActive(true);
                ItemCount = 1;
                break;
            case 1:
                Debug.Log("1");
                Item.SetActive(false);
                ItemCount = 1;
                break;

        }
    }
   
}