using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAI : ParentBT
{
    private int hp = 100;
    private float time = 0f;
    private float attackTime = 0.5f;
    private GameObject[] enemies = null;
    private GameObject target = null;


    protected override Func<bool> isDeath
    {
        get 
        {
            return () => hp <= 0;
        }
    }

    protected override Func<bool> isAttackAble
    {
        get 
        {
            time += Time.deltaTime;
            return () =>
            {
                if(time >= attackTime)
                {
                    time = 0f;
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }
    }

    protected override Func<bool> findEnemy
    {
        get 
        {
            return () =>
            {
                Debug.Log("findEnemy");

                enemies = GameObject.FindGameObjectsWithTag("Enemy");

                if(enemies.Length <= 0 )
                {
                    return false;
                }
                else
                {
                    target = enemies[0];
                    return true;
                }

            };
        }
    }

    protected override Action move
    {
        get
        {
            return () =>
            {
                myAni.SetBool("isMove",true);
                gameObject.transform.LookAt(target.transform);
                gameObject.transform.Translate(Vector3.forward * 0.1f);
            };
        }
    }

    protected override Func<bool> isArangeIn
    {
        get
        {
            return () =>
            {
                if (Vector3.Distance(target.transform.position, gameObject.transform.position) < 0.5f)
                {
                    myAni.SetBool("isMove", false);
                    return true;
                }
                else
                {
                    return false;
                }

            };
        }
    }
}
