using UnityEngine;

public class Equipment : MonoBehaviour
{

    [SerializeField] ScriptableEquipment equipmentData = null;

    [SerializeField] private string equipmentName;
    [SerializeField] private int equipmentGrade = 1;
    [SerializeField] private int equipmentAtk;
    [SerializeField] private int equipmentSpellPower;
    [SerializeField] private int equipmentAttackSpeed;
    [SerializeField] private int equipmentHp;
    [SerializeField] private int equipmentMpRecovery;

    public int originGrade;

    public string GetEquipmentName { get { return equipmentName; } }
    public int GetEquipmentAtk { get { return equipmentAtk; } }
    public int GetEquipmentSpellPower { get { return equipmentSpellPower; } }
    public int GetEquipmentAttackSpeed { get { return equipmentAttackSpeed; } }
    public int GetEquipmentHp { get { return equipmentHp; } }
    public int GetEquipmentMpRecovery { get { return equipmentMpRecovery; } }
    public int GetEquipmentGrade { get { return equipmentGrade; } }



    private void Awake()
    {
        equipmentName = equipmentData.GetEquipmentName;
        equipmentAtk = equipmentData.GetEquipmentAtk;
        equipmentAttackSpeed = equipmentData.GetEquipmentAttackSpeed;
        equipmentHp = equipmentData.GetEquipmentHp;
        equipmentMpRecovery = equipmentData.GetEquipmentMpRecovery;
        equipmentSpellPower = equipmentData.GetEquipmentSpellPower;
    }

    public bool Upgrade()
    {
        if (equipmentGrade < 4)
        {
            equipmentGrade++;
            return true;
        }
        return false;
    }

    public void SaveGrade()
    {
        originGrade= equipmentGrade;
    }

    public void LoadGrade()
    {
        equipmentGrade = originGrade;
    }

}
