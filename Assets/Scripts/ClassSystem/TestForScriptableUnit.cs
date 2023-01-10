using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Object/Unit Data")]
public class TestForScriptableUnit : ScriptableObject
{
    [SerializeField] private string unitName;
    public string GetUnitName { get { return unitName; } }

    [SerializeField] private int maxHp;
    public int GetMaxHp { get { return maxHp; } }

    [SerializeField] private int maxMp;
    public int GetMaxMp { get { return maxMp; } }

    [SerializeField] private int grade;
    public int GetGrade { get { return grade; } }

    [SerializeField] private int atk;
    public int GetAtk { get { return atk; } }

    [SerializeField] private int attackSpeed;
    public int GetAttackSpeed { get { return attackSpeed; } }

    [SerializeField] private float attackRange;
    public float GetAttackRange { get { return attackRange; } }

    [SerializeField] private int spellPower;
    public int GetSpellPPower { get { return spellPower; } }

    [SerializeField] private float moveSpeed;
    public float GetMoveSpeed { get { return moveSpeed; } }

    [SerializeField] private float magicCastingTime;
    public float GetMagicCastingTime { get { return magicCastingTime; } }

    [SerializeField] private float crowdControlTime;
    public float GetCrowdControlTime { get { return crowdControlTime; } }

    [SerializeField] private float tenacity;
    public float GetTenacity { get { return tenacity; } }

    [SerializeField] private ScriptableObject unitClass;
    public ScriptableObject GetUnitClass { get { return unitClass; } }

    [SerializeField] private ScriptableObject unitSpecies;
    public ScriptableObject GetUnitSpecies { get { return unitSpecies; } }

    [SerializeField] private ScriptableObject unitequipment01;
    public ScriptableObject GetUnitEqupment01 { get { return unitequipment01; } }

    [SerializeField] private ScriptableObject unitequipment02;
    public ScriptableObject GetUnitEqupment02 { get { return unitequipment02; } }

    [SerializeField] private ScriptableObject unitequipment03;
    public ScriptableObject GetUnitEqupment03 { get { return unitequipment03; } }
}

