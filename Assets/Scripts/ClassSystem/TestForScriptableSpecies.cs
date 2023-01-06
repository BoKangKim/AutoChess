using UnityEngine;

[CreateAssetMenu(fileName = "Species", menuName = "Scriptable Object/Species", order = int.MaxValue)]
public class TestForScriptableSpecies : ScriptableObject
{
    [SerializeField] private string species;
    public string GetSpecies { get { return species; } }
}

