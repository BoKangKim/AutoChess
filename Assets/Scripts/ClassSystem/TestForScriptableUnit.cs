using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Object/Unit Data", order = int.MaxValue)]
public class TestForScriptableUnit : ScriptableObject
{
    [SerializeField] private string unitName;
    public string GetUnitName { get { return unitName; } }

    [SerializeField] private int hp;
    public int GetHp { get { return hp; } }

    [SerializeField] private int mp;
    public int GetMp { get { return mp; } }

    [SerializeField] private int grade;
    public int GetGrade { get { return grade; } }  

    [SerializeField] private int damage;
    public int GetDamage { get { return damage; } }

    [SerializeField] private int ablityPower;
    public int GetAblityPower { get { return ablityPower; } }

    [SerializeField] private float attackRange;
    public float GetAttackRange { get { return attackRange; } }

    [SerializeField] private float moveSpeed;
    public float GetMoveSpeed { get { return moveSpeed; } }

    [SerializeField] private ScriptableObject unitclass;
    public ScriptableObject GetUnitClass { get { return unitclass; } }

    [SerializeField] private ScriptableObject unitSpecies;
    public ScriptableObject GetUnitSpecies { get { return unitSpecies; } }
}

