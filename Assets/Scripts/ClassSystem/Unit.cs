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
        private int grade;
        private string unitName;
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
        private string speciesName;
        private string className;
        private string synergyName;
        #endregion

        #region 프로퍼티
        public string GetSpeciesName { get { return speciesName; } }
        public string GetClassName { get { return className; } }
        public ScriptableUnit GetUnitData { get { return UnitData; } }
        public string GetSynergyName { get { return synergyName; } }
        public float GetGrade { get { return grade; } }
        public int GetEquipmentCount { get { return equipmentCount; } }

        #endregion
        private void Awake()
        {
            synergyName = SpeciesData.GetSpecies + " " + ClassData.GetClass;
            grade = UnitData.GetGrade;
            //여기서 시리얼라이즈필드된거 초기화 해줘야함
            //speciesName = SpeciesData.GetSpecies;
            //className = ClassData.GetSynergeClass;
            unitName = speciesName + className;
            curHp = UnitData.GetMaxHp;
            curMp = UnitData.GetMaxMp;
            //maxHp = UnitData.GetMaxHp;
            //maxMp = UnitData.GetMaxMp;
            //moveSpeed = UnitData.GetMoveSpeed;
            //atk = UnitData.GetAtk;
            //attackRange = UnitData.GetAttackRange;
            //attackSpeed = UnitData.GetAttackSpeed;
            //spellPower = UnitData.GetSpellPower;
            //magicDamage;
            //magicCastingTime = UnitData.GetMagicCastingTime;
            //crowdControlTime;
            //tenacity;
            //attackTarget = 1;
            //barrier;
            //stunTime;
            //blindnessTime;
            //weakness;
        }

        public float GetAttackSpeed()
        {
            return attackSpeed * SpeciesData.GetAttackSpeedPercentage;
        }

        public int Upgrade() //일단은 머지시 두배씩 증가함 - >
        {
            return grade++;
        }

        public int EquipCount()
        {
            return equipmentCount++;
        }

        public void EquipItem(int equipmentCount)
        {
            switch (equipmentCount)
            {
                case 0:
                    equipment01 = transform.GetChild(equipmentCount).gameObject;
                    EquipCount();
                    break;
                case 1:
                    equipment02 = transform.GetChild(equipmentCount).gameObject;
                    EquipCount();
                    break;
                case 2:
                    equipment03 = transform.GetChild(equipmentCount).gameObject;
                    EquipCount();
                    break;
            }
        }

        public void GetItemStat()
        {
            for (int i = 0; i < equipmentCount; i++) // 이거 공통된부분 캐싱해서 쓰는걸로 바꿔야함
            {
                this.atk += transform.GetChild(i).GetComponent<Equipment>().GetEquipmentAtk;
                this.spellPower += transform.GetChild(i).GetComponent<Equipment>().GetEquipmentSpellPower;
                this.attackSpeed += transform.GetChild(i).GetComponent<Equipment>().GetEquipmentAttackSpeed;
                this.maxHp += transform.GetChild(i).GetComponent<Equipment>().GetEquipmentHp;
                this.maxMp -= transform.GetChild(i).GetComponent<Equipment>().GetEquipmentMp;
            }
        }

        public void GetSynergyData()
        {
            
        }

    }
}