using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private float grade;
    private float maxHp = 100f;
    private float maxMp = 100f;
    private float minMp = 10f; // 스킬사용에 필요한 최소 MP

    private float moveSpeed; // 이동 속도
    private float atk; // 공격력
    private float attackRange; // 물리공격력
    private float atkDamage; // 총 데미지
    private float attackSpeed; // 공격 속도
    private float spellPower; // 
    private float magicDamage; // 총 데미지

    private float magicCastingTime; //스킬을 캐스팅 하는 시간 (스킬이 즉발인지 아닌지)
    private float crowdControlTime; //CC(군중제어 = 상태이상)시간
    private float stunTime; //기절 시간(CC기)
    private float blindnessTime; //실명 시간(CC기)
    private float weaknessTime; //허약 시간(CC기)

    private float tenacity; //강인함 -> 롤에서 CC기를 줄여주는 비율 100%
    private float attackTarget; //공격가능한 타겟 수
    private float barrier; //체력대신 데미지를 입을 보호막

    //-------------------------------------------------------------------------------------

    Unit_test unit;

    private float coolTime { get; set; } // 쿨타임
    private float skillTime { get; set; } // 스킬 시전 타임

    bool availableSkill; // 스킬사용 가능한지 체크 - true일때 스킬 사용 가능
    bool usingSkill; // 스킬 사용 중임 체크 
    bool abnormalState; // 상태이상 체크 - true 일때 행동제어

    private void Awake()
    {
        unit = FindObjectOfType<Unit_test>();
    }
    private void Start()
    {
        if (unit.isDead) return;
        availableSkill = true;
        usingSkill = false;
        abnormalState = false;

        unit.hp = maxHp;
        unit.mp = maxMp;

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSkill();
        }

    }
    public void Timer()
    {
        skillTime += Time.deltaTime;

        coolTime -= Time.deltaTime;

    }


    // 스킬 사용
    public void UseSkill()
    {

        // 예외처리 - 죽음, 시간 체크
        if (unit.isDead) return;
        // 스킬 사용가능이면 스킬 시작
        if (availableSkill)
        {
            Debug.Log("스킬씀");
            StartSkill();
        }
        else
        {
            Debug.Log("스킬안씀");
        }
    }


    // 스킬 사용 시작
    public void StartSkill()
    {
        // 애니메이션 및 이펙트 출력
        Debug.Log("스킬 시작");

        // 현재 스킬 사용 중
        if (skillTime > 0)
        {
            usingSkill = true;
            CalculateMP(unit.mp);
            CCTime(coolTime);
            // 마나 사용, 쿨타임체크 후에 상태이상 체크
            if (abnormalState) // 상태이상인 경우 - 공격을 받았을 때
            {
                // 대기 상태
                Idle();
                // 모든 상태 해제
                // 스킬 사용 불가
            }
            else
            {
                // 상태이상이 아닌 경우
                // 스킬 사용 가능
                
            }
            
        }
        else
        {
            usingSkill = false;
        }
    }

    // 공격을 받은거 체크

    // 스킬 사용 시 Mp 차감
    public void CalculateMP(float mp)
    {
        mp -= 10f;
        if (mp <= minMp)
        {
            Debug.Log("스킬사용불가");
            availableSkill = false;
        }
        else
        {
            Debug.Log("스킬사용가능");
            availableSkill = true;
        }
    }


    // exception handling
    #region exception handling


    // 쿨타임 및 스킬 제어상태 시간이 아직 돌고있으면 스킬 사용 불가능
    // coolTime, stunTime, skillTime
    public void CCTime(float time)
    {
        if (time <= 0)
        {
            if (!abnormalState) // 상태이상이 아닌상태
            {
            availableSkill = true; // 스킬 사용 가능

            }
        }
        else
        {
            if (abnormalState) 
            {
                availableSkill = false;
            }            
            
        }
    }

    // 상태이상에 걸렸는지 확인하는 게 먼저 필요함

    // 상태이상 체크
    public void abnormalStatus()
    {
        if (usingSkill) // 스킬 사용중일 때
        {
            // 상태이상 시간을 체크하고 난 다음
            CCTime(stunTime);
            CCTime(skillTime);

            // 상대방 스킬 받음 - 제어 상태 적용

            // 사용중인 스킬을 끊고 대기 상태로     
            Idle();

        }
        else // 스킬 사용중이 아닐 때
        {
            Debug.Log("상대방 스킬 받음");
        }
    }

    // 만약 스킬을 사용하는 유닛 타입이 본인일 경우에만 본인만의 스킬 사용이 가능함
    public void TypeCheck(string tag)
    {
        if (gameObject.tag == tag)
        {
            Debug.Log(gameObject.tag);
            Debug.Log(tag);
            availableSkill = true;
        }
        else
        {
            availableSkill = false;
        }
    }

    #endregion

    public void Hit() // 상태이상 받아오기
    {
        // 공격받았을 때 상태이상 적용
        abnormalState = true;
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
