using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private ScriptableUnit UnitData = null;
    [SerializeField] private ScriptableClass[] ClassData = null;
    [SerializeField] private ScriptableSpecies[] SpeciesData = null;
    [SerializeField] private ScriptableEquipment[] Equipment01Data = null;
    [SerializeField] private ScriptableEquipment[] Equipment02Data = null;
    [SerializeField] private ScriptableEquipment[] Equipment03Data = null;


    private float grade;
    private float maxHp;
    private float curHp;
    private float maxMp;
    private float curMp;
    private float moveSpeed;
    private float atk;
    private float attackRange;
    private float atkDamage;
    private float attackSpeed;
    private float spellPower;
    private float magicDamage;
    private float magicCastingTime; //스킬을 캐스팅 하는 시간
    private float crowdControlTime; //CC(군중제어 = 상태이상)시간
    private float tenacity; //강인함 -> 롤에서 CC기를 줄여주는 비율 100%
    private float attackTarget; //공격가능한 타겟 수
    private float barrier; //체력대신 데미지를 입을 보호막
    private float stunTime; //기절 시간(CC기)
    private float blindnessTime; //실명 시간(CC기)
    private float weakness; //허약 시간(CC기)

    //텔레포트때 쓸지 안쓸지는 몰라서 일단 vector3는 선언 안했습니다.


    //지역변수들 전부 기획서에 맞게 변경 할 것


    private void Awake()
    {
        //hp = UnitData.GetHp + Equipment01Data.GetEquipmentHp + Equipment02Data.GetEquipmentHp + Equipment03Data.GetEquipmentHp;
        //attackSpeed = UnitData.GetAttackSpeed + Equipment01Data.GetEquipmentAttackSpeed + Equipment02Data.GetEquipmentAttackSpeed + Equipment03Data.GetEquipmentAttackSpeed;
        //Debug.Log("계산된 hp값은 : " + hp);
        //Debug.Log("계산된 공속값은 : " + attackSpeed);
    }

    //장착 아이템은 해당 놈만 하고

    // 새로 배치시에는 시너지 검사 해서 
    

}
