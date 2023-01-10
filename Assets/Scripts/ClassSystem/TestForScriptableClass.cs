using UnityEngine;

[CreateAssetMenu(fileName = "SynergeClass", menuName = "Scriptable Object/SynergeClass", order = int.MaxValue)]
public class TestForScriptableClass : ScriptableObject
{
    [SerializeField] private string synergeclass;
    public string GetSynergeClass { get { return synergeclass; } }

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
