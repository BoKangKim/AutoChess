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
        [SerializeField] private GameObject Equipment01 = null;
        [SerializeField] private GameObject Equipment02 = null;
        [SerializeField] private GameObject Equipment03 = null;

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

    }
}