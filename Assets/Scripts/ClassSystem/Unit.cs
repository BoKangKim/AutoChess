using System.Collections.Generic;
using UnityEngine;

namespace UnitClass
{

    public class Unit : MonoBehaviour
    {
        #region SerializeField
        [Header("유닛에 장착할 데이터")]
        [SerializeField] private ScriptableUnit UnitData = null;
        [SerializeField] private ScriptableClass ClassData = null;
        [SerializeField] private ScriptableSpecies SpeciesData = null;
        [SerializeField] private GameObject equipment01 = null;
        [SerializeField] private GameObject equipment02 = null;
        [SerializeField] private GameObject equipment03 = null;
        [Header("장착한 아이템 갯수")]
        [SerializeField] private int equipmentCount;
        #endregion

        #region 유닛 지역변수

        // 설명란
        // origin + stat -> 등급에 의한 기본 데이터
        // eq + stat -> 장비에 의해 추가될 데이터
        // total stat -> 전투에서 사용될 데이터 -> origin + eq로 계속 갱신됨(시너지가 바뀌거나 장비 장착이 빈번히 변경되서 따로 선언)
        // 추가로 기획서 상에서 장비 장착한 데이터까지 계산되는 경우도 있고 기본 공격력의 X% 인경우도 있어서 분리해서 가지고 있게 할 예정 -> 좋은 방법이 있다면 의견좀


        [SerializeField] private int grade; // 인스펙터에서 등급 볼라고
        private float originMaxHp;
        private float originCurHp;
        private float originMaxMp;
        private float originCurMp;
        private float originMpRecovery;
        private float originMoveSpeed;
        private float originAtk;
        private float originAttackRange;
        private float originAtkDamage;
        private float originAttackSpeed;
        private float originSpellPower;
        private float originMagicDamage;
        private float originMagicCastingTime; //스킬을 캐스팅 하는 시간
        private float originCrowdControlTime; //CC(군중제어 = 상태이상)시간
        private float originTenacity; //강인함 -> 롤에서 CC기를 줄여주는 비율 100%
        private float originAttackTarget; //공격가능한 타겟 수
        private float originBarrier; //체력대신 데미지를 입을 보호막
        private float originStunTime; //기절 시간(CC기)
        private float originBlindnessTime; //실명 시간(CC기)
        private float originWeakness; //허약 시간(CC기)
        private string speciesName;
        private string className;
        [SerializeField] private bool speciesSynergy1Grade = false;
        [SerializeField] private bool speciesSynergy2Grade = false;
        [SerializeField] private bool classSynergy1Grade = false;
        [SerializeField] private bool classSynergy2Grade = false;
        private string synergyName;

        private float eqHp; // 장비로 인해 추가될 스텟
        private float eqMpRecovery;
        private float eqAtk;
        private float eqAttackSpeed;
        private float eqSpellPower;

        private float totalMaxHp; // total stats -> 안쓰는건 나중에 지워야함
        private float totalCurHp;
        private float totalMaxMp;
        private float totalCurMp;
        private float totalMpRecovery;
        private float totalMoveSpeed;
        private float totalAtk;
        private float totalAttackRange;
        private float totalAtkDamage;
        private float totalAttackSpeed;
        private float totalSpellPower;
        private float totalMagicDamage;
        private float totalMagicCastingTime; //스킬을 캐스팅 하는 시간
        private float totalCrowdControlTime; //CC(군중제어 = 상태이상)시간
        private float totalTenacity; //강인함 -> 롤에서 CC기를 줄여주는 비율 100%
        private float totalAttackTarget; //공격가능한 타겟 수
        private float totalBarrier; //체력대신 데미지를 입을 보호막
        private float totalStunTime; //기절 시간(CC기)
        private float totalBlindnessTime; //실명 시간(CC기)
        private float totalWeakness; //허약 시간(CC기)
        #endregion

        #region 프로퍼티
        public string GetSynergyName { get { return synergyName; } }
        public float GetGrade { get { return grade; } }
        public int GetEquipmentCount { get { return equipmentCount; } }

        public float GetTotalSpellPower { get { return totalSpellPower; } }
        public float GetTotalMpRecovery { get { return totalMpRecovery; } }
        public float GetTotalMaxHp { get { return totalMaxHp; } }
        public float GetTotalAttackSpeed { get { return totalAttackSpeed; } }


        #endregion
        private void Awake()
        {
            speciesName = SpeciesData.GetSpecies;
            className = ClassData.GetClass;

            synergyName = SpeciesData.GetSpecies + " " + ClassData.GetClass;
            grade = UnitData.GetGrade;
            //여기서 시리얼라이즈필드된거 초기화 해줘야함
            originCurHp = UnitData.GetMaxHp;
            originCurMp = UnitData.GetMaxMp;
            originMaxHp = UnitData.GetMaxHp;
            originMaxMp = UnitData.GetMaxMp;
            originAtk = UnitData.GetAtk;
            originAttackSpeed = UnitData.GetAttackSpeed;
            originSpellPower = UnitData.GetSpellPower;
            originMpRecovery = 5 + ClassData.GetMpRecovery; //나중에 total이랑 정리를 해야함
            SetUnitStat();
        }


        public int Upgrade()
        {
            return grade++;
        }

        public int EquipCount()
        {
            equipmentCount = transform.childCount;
            return equipmentCount;
        }

        public void EquipItem()
        {
            EquipCount();
            switch (GetEquipmentCount)
            {
                case 1:
                    equipment01 = transform.GetChild(0).gameObject;
                    break;
                case 2:
                    equipment01 = transform.GetChild(0).gameObject;
                    equipment02 = transform.GetChild(1).gameObject;
                    break;
                case 3:
                    equipment01 = transform.GetChild(0).gameObject;
                    equipment02 = transform.GetChild(1).gameObject;
                    equipment03 = transform.GetChild(2).gameObject;
                    break;
            }
            GetItemStat();
            SetUnitStat();
        }

        public void GetItemStat() //장비는 등급 * 2씩 증가
        {
            for (int i = 0; i < equipmentCount; i++)
            {
                Equipment eq = transform.GetChild(i).GetComponent<Equipment>();

                this.eqAtk += eq.GetEquipmentAtk * eq.GetEquipmentGrade * 2;
                this.eqSpellPower += eq.GetEquipmentSpellPower * eq.GetEquipmentGrade * 2;
                this.eqAttackSpeed += eq.GetEquipmentAttackSpeed * eq.GetEquipmentGrade * 2;
                this.eqHp += eq.GetEquipmentHp * eq.GetEquipmentGrade * 2;
                this.eqMpRecovery += eq.GetEquipmentMpRecovery * eq.GetEquipmentGrade * 2;
            }
        }

        public void SetUnitStat()
        {
            this.totalAtk = eqAtk + originAtk * grade;
            this.totalSpellPower = eqSpellPower + originSpellPower * grade;
            this.totalAttackSpeed = eqAttackSpeed + originAttackSpeed * grade;
            this.totalMaxHp = eqHp + originMaxHp * grade;
            this.totalMpRecovery = eqMpRecovery + originMpRecovery;
        }

        public void SetSynergy(List<string> activeSynergyList) //나는 병신이야
        {
            for(int i=0;i<activeSynergyList.Count;i++)
            {
                if (activeSynergyList[i] == speciesName)
                {
                    speciesSynergy1Grade = true;
                }

                if (activeSynergyList[i] == className)
                {
                    classSynergy1Grade = true;
                }

                if (activeSynergyList[i] == speciesName + "2")
                {
                    speciesSynergy1Grade = false;
                    speciesSynergy2Grade = true;
                }

                if (activeSynergyList[i] == className + "2")
                {
                    classSynergy1Grade = false;
                    classSynergy2Grade = true;
                }

                
            }
        }

    }
}