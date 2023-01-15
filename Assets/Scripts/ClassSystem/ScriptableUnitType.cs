using UnityEngine;

[CreateAssetMenu(fileName = "UnitTypeData", menuName = "Scriptable Object/UnitTypeData")]
public class ScriptableUnitType : ScriptableObject
{
    [SerializeField] private string typeName;
    public string GetTypeName { get { return typeName; } }
}
