using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_test : MonoBehaviour
{

    private float grade; // 등급

    [SerializeField] private float curHp;
    [SerializeField] private float curMp;
    private float maxHp = 100f;
    private float maxMp = 100f;

    [SerializeField] private bool IsDead;
    public bool IsSpecial; // 특수스킬인지 확인

    public bool isDead { get; set; }
    public bool IsHit; // 공격받았나 체크

    public float hp { get; set; }
    public float mp { get; set; }

    private void Awake()
    {
    }
    private void Start()
    {
        IsDead = false;
        IsHit = false;
        hp = maxHp;
        mp = maxMp;
    }

    private void Update()
    {
        // 죽었으면 스킬 사용 불가
        if (isDead) return;

    }
    public void normalSkill()
    {
        Debug.Log("일반 스킬 사용");
        IsSpecial = false;
    }
    public void specialSkill()
    {
        Debug.Log("특수 스킬 사용");
        IsSpecial = true;
    }

    // 종족, 클래스가 일치하는지 확인하고
    // 스킬을 사용하는 유닛 타입이 본인일 경우에만 본인만의 스킬 사용이 가능함
    public void TypeCheck(string type)
    {
        if (gameObject.tag == type)
        {
            Debug.Log(gameObject.tag);
            Debug.Log(type);
            //availableSkill = true;
        }
        else
        {
            //availableSkill = false;
        }
    }

    public void Hit()
    {
        IsHit = true;
        // 특수 공격받았을 때 스킬에 상태이상이 있다면 상태이상 적용 - 
        if (IsSpecial)
        {
            //abnormalState = true;
            Debug.Log("상태이상");

        }
        else
        {
            //abnormalState = false;
            Debug.Log("상태정상");
        }
    }
    public void Idle()
    {
        Debug.Log("대기");
    }
    public void Move()
    {
        Debug.Log("이동");
    }
    public void Dead()
    {
        Debug.Log("죽음");
    }

}
