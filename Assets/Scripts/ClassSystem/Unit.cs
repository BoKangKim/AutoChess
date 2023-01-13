using UnityEngine;
namespace UnitClass
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private ScriptableUnit UnitData = null;
        [SerializeField] private ScriptableClass ClassData = null;
        [SerializeField] private ScriptableSpecies SpeciesData = null;
        [SerializeField] private GameObject Equipment01 = null;
        [SerializeField] private GameObject Equipment02 = null;
        [SerializeField] private GameObject Equipment03 = null;

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

        private void Awake()
        {
            //grade = UnitData.GetGrade;
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

            //0선언은 왜했냐 그냥 냅두면 원래 0인디 ㅋㅋ
        }

    }
}

