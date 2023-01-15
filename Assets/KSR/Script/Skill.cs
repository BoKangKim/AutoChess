using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    
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
    private float skillTime { get; set; } // 스킬 시전 타임 - 나중에 받아와야함

    bool availableSkill; // 스킬사용 가능한지 체크 - true일때 스킬 사용 가능
    bool usingSkill; // 스킬 사용 중임 체크 
    bool abnormalState; // 상태이상 체크 - true 일때 행동제어 
    bool specialAbnormalState; // 제어 상태이상 체크
    
    private void Awake()
    {
        unit = FindObjectOfType<Unit_test>();
    }
    private void Start()
    {       
        availableSkill = true;
        usingSkill = false;
        abnormalState = false;                
    }

    private void Update()
    {
        
        if (unit.isDead==true)
        {
            Debug.Log("죽음");
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateMP(unit.mp, 10f);
            Debug.Log("UNIT MP : " + unit.mp);
            UseSkill();
        }
        if (usingSkill)
        {
            Timer();
        }
    }
    public void Timer()
    {
        skillTime -= Time.deltaTime;
        coolTime -= Time.deltaTime;
    }


    // 스킬 사용
    public void UseSkill()
    {
        if (unit.isDead) return;

        if (availableSkill)
        {
            Debug.Log("스킬 사용");
            StartSkill();
        }
        if (!availableSkill)
        {
            Debug.Log("마나 없음");
            Debug.Log("UNIT MP : " + unit.mp);
        }
    }


    // 스킬 사용 시작
    public void StartSkill()
    {
        Debug.Log("스킬 이펙트 출력");        
        if (skillTime >= 0)
        {
            usingSkill = true; // 스킬 사용중
            
            CCTime(coolTime); // 쿨타임 체크
            if (usingSkill)
            {                 
                CCTime(stunTime); // 기절 시간 체크

                if (unit.IsHit) // 공격을 받았을 때
                {
                    // 상태이상인 경우 -> 유닛에서 확인가능
                    if (abnormalState)
                    {
                        AbnormalState();
                    }
                    // 행동 제어 상태이상에 걸렸을 경우
                    if (specialAbnormalState)
                    {
                        // 플레이어 대기 상태 - 진행중인 모든 상태 해제                        
                        SpecialAbnormalState();
                    }
                }
                
            }
        }
    }

    public void CalculateMP(float mp, float useMp)
    {
            Debug.Log("UNIT MP : "+unit.mp);
        mp -= useMp;
        Debug.Log("UNIT MP : " + unit.mp);
        if (mp <= minMp)
        {
            Debug.Log("마나 부족");
            availableSkill = false;
        }
        else
        {
            Debug.Log("마나 충분");
            availableSkill = true;
        }
    }


    // exception handling
    #region exception handling


    // 쿨타임 및 스킬 적용 시간이 아직 돌고있으면 스킬 사용 불가능
    // coolTime, stunTime
    public void CCTime(float time)
    {
        if (time <= 0)
        {
            availableSkill = true;
        }
        else
        {
            availableSkill = false;
        }
    }

    public void AbnormalState() 
    {
        // 스킬 사용 불가, 일반 공격 가능
        Debug.Log("상태 이상 효과 적용");
        availableSkill = false;
    }
    public void SpecialAbnormalState() 
    {
        // 스킬 사용 불가, 모든 행동 제어
        Debug.Log("행동 제어 & 상태 이상 효과 적용");
        unit.Idle();
        availableSkill = false;
    }
    
    #endregion


}
