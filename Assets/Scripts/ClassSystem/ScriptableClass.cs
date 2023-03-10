using UnityEngine;

[CreateAssetMenu(fileName = "Class", menuName = "Scriptable Object/Class", order = int.MaxValue)]
public class ScriptableClass : ScriptableObject
{
    [SerializeField] private string Class;
    public string GetClass { get { return Class; } }

    [SerializeField] private float attackRange;
    public float GetAttackRange { get { return attackRange; } }

    [SerializeField] private float mpRecovery;
    public float GetMpRecovery { get { return mpRecovery; } }

    [SerializeField] private float atkPercentage;
    public float GetAtkPercentage { get { return atkPercentage; } }

    [SerializeField] private float hpPercentage;
    public float GetHpPercentage { get { return hpPercentage; } }

    [SerializeField] private float skillDamagePercentage;
    public float GetSkillDamagePercentage { get { return skillDamagePercentage; } }

    [SerializeField] private float attackSpeed;
    public float GetAttackSpeed { get { return attackSpeed; } }
}
