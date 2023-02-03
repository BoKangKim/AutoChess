using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Object/Unit Data")]
public class ScriptableUnit : ScriptableObject
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

    [SerializeField] private int spellPower;
    public int GetSpellPower { get { return spellPower; } }

    [SerializeField] private float moveSpeed;
    public float GetMoveSpeed { get { return moveSpeed; } }

    [SerializeField] private float magicCastingTime;
    public float GetMagicCastingTime { get { return magicCastingTime; } }
}

