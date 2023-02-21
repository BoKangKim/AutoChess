using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Scriptable Object/Equipment", order = int.MaxValue)]
public class ScriptableEquipment : ScriptableObject
{
    [SerializeField] private string equipmentName;
    public string GetEquipmentName { get { return equipmentName; } }

    [SerializeField] private int equipmentAtk;
    public int GetEquipmentAtk { get { return equipmentAtk; } }

    [SerializeField] private int equipmentSpellPower;
    public int GetEquipmentSpellPower { get { return equipmentSpellPower; } }

    [SerializeField] private int equipmentAttackSpeed;
    public int GetEquipmentAttackSpeed { get { return equipmentAttackSpeed; } }

    [SerializeField] private int equipmentMpRecovery;
    public int GetEquipmentMpRecovery { get { return equipmentMpRecovery; } }

    [SerializeField] private int equipmentHp;
    public int GetEquipmentHp { get { return equipmentHp; } }

    //추후 클래스에 따른 추가 시너지가 들어갈 공간

    //
}