using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonsterParentsAI
{
    // 체력
    float hp = 100;
    float speed = 2f;

    // 타겟
    GameObject target;
    // 적 받아올 배열
    GameObject[] enermies;

    float attackRange = 1f;


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
                // 데미지 받을 때 콜라이더로 체크하려면
            };
        }
    }
    protected override Func<bool> IsDead // 체력이 0일때 판정
    {
        get
        {
            return () =>
            {

                if (hp <= 0)
                {
                    Debug.Log("죽음");
                    return true;
                    myAni.SetBool("IsDead", true);
                }
                else 
                {
                    return false;
                    myAni.SetBool("IsDead", false);
                }
                
                
            };
        }
    }
}
