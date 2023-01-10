using UnityEngine;

[CreateAssetMenu(fileName = "Species", menuName = "Scriptable Object/Species", order = int.MaxValue)]
public class TestForScriptableSpecies : ScriptableObject
{
    [SerializeField] private string species;
    public string GetSpecies { get { return species; } }

    [SerializeField] private float attackSpeedPercentage;
    public float GetAttackSpeedPercentage { get { return attackSpeedPercentage; } }

    [SerializeField] private float barrierPercentage; //배리어가 hp의 비율%에 따라 적용
    public float GetBarrierPercentage { get { return barrierPercentage; } }

    [SerializeField] private float atkPercentage;
    public float GetAtkPercentage { get { return atkPercentage; } }

    [SerializeField] private float bonusatkPercentage;
    public float BonusatkPercentage { get { return bonusatkPercentage; } }



}


