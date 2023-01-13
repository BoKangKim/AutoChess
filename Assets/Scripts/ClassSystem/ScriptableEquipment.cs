using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Scriptable Object/Equipment", order = int.MaxValue)]
public class ScriptableEquipment : ScriptableObject
{
    [SerializeField] private string equipmentName;
    public string GetEquipmentName { get { return equipmentName; } }

    [SerializeField] private int equipmentAtk;
    public int GetEquipmentAtk { get { return equipmentAtk; } }

    [SerializeField] private int equipmentAttackSpeed;
    public int GetEquipmentAttackSpeed { get { return equipmentAttackSpeed; } }

    [SerializeField] private int equipmentMp;
    public int GetEquipmentMp { get { return equipmentMp; } }

    [SerializeField] private int equipmentHp;
    public int GetEquipmentHp { get { return equipmentHp; } }

    //���� Ŭ������ ���� �߰� �ó����� �� ����

    //
}