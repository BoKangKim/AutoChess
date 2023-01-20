using UnityEngine;

public class Equipment : MonoBehaviour
{
    //enum EQUIPMENTDATA
    //{
    //    sword, shield, dagger, robe, wand
    //}
    [SerializeField] ScriptableEquipment equipmentData = null;

    [SerializeField] private string equipmentName; //인스펙터 창에서 데이터 들어왔나 보려고 [SerializeField]함

    [SerializeField] private int equipmentAtk;
    [SerializeField] private int equipmentSpellPower; //주문력
    [SerializeField] private int equipmentAttackSpeed;
    [SerializeField] private int equipmentHp;
    [SerializeField] private int equipmentMp;

    public string EQUIPMENTNAME { get { return equipmentName; } }
    public int EQUIPMENTATK { get { return equipmentAtk; } }
    public int EQUIPMENTSPELLPOWER { get { return equipmentSpellPower; } }
    public int EQUIPMNETATTACKSPEED { get { return equipmentAttackSpeed; } }
    public int EQUIPMENTHP { get { return equipmentHp; } }
    public int EQUIPMENTMP { get { return equipmentMp; } }

    private void Awake()
    {
        equipmentName = equipmentData.GetEquipmentName;
        equipmentAtk = equipmentData.GetEquipmentAtk;
        equipmentAttackSpeed = equipmentData.GetEquipmentAttackSpeed;
        equipmentHp = equipmentData.GetEquipmentHp;
        equipmentMp = equipmentData.GetEquipmentMp;
        equipmentSpellPower = equipmentData.GetEquipmentSpellPower;
    }

}
