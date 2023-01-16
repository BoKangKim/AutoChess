using UnityEngine;
namespace UnitClass
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private ScriptableUnit UnitData = null;
        //[SerializeField] private ScriptableClass ClassData = null;
        //[SerializeField] private ScriptableSpecies SpeciesData = null;
        [SerializeField] private ScriptableUnitType type01 = null;
        [SerializeField] private ScriptableUnitType type02 = null;
        [SerializeField] private GameObject Equipment01 = null;
        [SerializeField] private GameObject Equipment02 = null;
        [SerializeField] private GameObject Equipment03 = null;

        [SerializeField] private string unitName; // test��
        private float grade;
        private string unitName = "aa";
        public float grade;
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
        private float magicCastingTime; //��ų�� ĳ���� �ϴ� �ð�
        private float crowdControlTime; //CC(�������� = �����̻�)�ð�
        private float tenacity; //������ -> �ѿ��� CC�⸦ �ٿ��ִ� ���� 100%
        private float attackTarget; //���ݰ����� Ÿ�� ��
        private float barrier; //ü�´�� �������� ���� ��ȣ��
        private float stunTime; //���� �ð�(CC��)
        private float blindnessTime; //�Ǹ� �ð�(CC��)
        private float weakness; //��� �ð�(CC��)
        private string speciesName;
        private string className;

        public string GetSpeciesName { get { return speciesName; } }
        public string GetClassName { get { return className; } }

        public ScriptableUnitType GetUnitType01 { get { return type01; } }
        public ScriptableUnitType GetUnitType02 { get { return type02; } }

        //public ScriptableUnit GetUnitData { get { return UnitData; } }
        private string speciesName;
        private string className;
        

        public string GetSpeciesName { get { return speciesName; } }
        public string GetClassName { get { return className; } }

        private void Awake()
        {
            //speciesName = SpeciesData.GetSpecies;
            //className = ClassData.GetSynergeClass;

            unitName = speciesName + className;
            grade = UnitData.GetGrade;
            speciesName = SpeciesData.GetSpecies;
            className = ClassData.GetSynergeClass;
            unitName += grade.ToString() + "" + "";
            grade = UnitData.GetGrade;
            //maxHp = UnitData.GetMaxHp;
            curHp = UnitData.GetMaxHp;
            //maxMp = UnitData.GetMaxMp;
            curMp = UnitData.GetMaxMp;
            //moveSpeed = UnitData.GetMoveSpeed;
            //atk = UnitData.GetAtk;
            //attackRange = UnitData.GetAttackRange;
            //attackSpeed = UnitData.GetAttackSpeed;
            //spellPower = UnitData.GetSpellPower;
            //magicDamage = 0;
            //magicCastingTime = UnitData.GetMagicCastingTime;
            //crowdControlTime = 0;
            //tenacity = 0;
            //attackTarget = 1;
            //barrier = 0;
            //stunTime = 0;
            //blindnessTime = 0;
            //weakness = 0;
            //0������ ���߳� �׳� ���θ� ���� 0�ε� ����
            //this.transform.name = grade.ToString() + "_" + speciesName + "_" + className;
        }

        public void test(string species) //�ó��� ���޹��� ���� ���޹��� �ó����� �°� ü���� ����
        {

        }

        public void test(string species, string Job) //�ó��� ���޹��� ���� ���޹��� �ó����� �°� ü���� ����
        {

        }

    }
}