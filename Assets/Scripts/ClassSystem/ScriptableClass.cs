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
}
