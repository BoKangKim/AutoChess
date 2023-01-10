using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Object/Unit Data", order = int.MaxValue)]
public class TestForScriptableUnit : ScriptableObject
{
    [SerializeField] private string unitName;
    public string UnitName { get { return unitName; } }

    [SerializeField] private int hp;
    public int Hp { get { return hp; } }

    [SerializeField] private int mp;
    public int Mp { get { return mp; } }

    [SerializeField] private int grade;
    public int Grade { get { return grade; } }  

    [SerializeField] private int damage;
    public int Damage { get { return damage; } }

    [SerializeField] private int ablityPower;
    public int AblityPower { get { return ablityPower; } }

    [SerializeField] private float attackRange;
    public float AttackRange { get { return attackRange; } }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField] private ScriptableObject unitclass;
    public ScriptableObject UnitClass { get { return unitclass; } }

    [SerializeField] private ScriptableObject unitSpecies;
    public ScriptableObject UnitSpecies { get { return unitSpecies; } }
}

