using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonsterParentsAI
{
    // 체력
    [SerializeField] float hp = 100;
    float speed = 2f;

    // 타겟
    GameObject target;
    // 적 받아올 배열
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
                Debug.Log("대기");
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
                Debug.Log("타겟 찾기");
                target = GameObject.FindGameObjectWithTag("Target");
                if (target==null)
                {
                    Debug.Log("못찾아");
                    return true;
                }
                else
                {
                    Debug.Log("찾아");
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
                Debug.Log("이동");
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
                Debug.Log("범위 내에 있음");
                float Distance = Vector3.Distance(target.transform.position, gameObject.transform.position);
                if (Distance <= attackRange)
                {
                    Debug.Log("공격범위 내에 있나");
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
                Debug.Log("공격");
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
                Debug.Log("맞는다");
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
                    Debug.Log("죽음");
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }
    }
    protected override Action IsDrop // 죽으면 아이템 드롭
    {
        get
        {
            return () =>
            {
                Debug.Log("드롭");
                myAni.SetBool("IsDead", true);
                Item.SetActive(true);
                //this.gameObject.SetActive(false);

            };
        }
    }
}
