using UnityEngine;

[CreateAssetMenu(fileName = "SynergeClass", menuName = "Scriptable Object/SynergeClass", order = int.MaxValue)]
public class TestForScriptableClass : ScriptableObject
{
    [SerializeField] private string synergeclass;
    public string GetSynergeClass { get { return synergeclass; } }
}
