using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private TestForScriptableUnit UnitData = null;
    [SerializeField] private TestForScriptableClass[] ClassData = null;
    [SerializeField] private TestForScriptableSpecies[] SpeciesData = null;
    [SerializeField] private TestForScriptableEquipment[] Equipment01Data = null;
    [SerializeField] private TestForScriptableEquipment[] Equipment02Data = null;
    [SerializeField] private TestForScriptableEquipment[] Equipment03Data = null;


    private float grade;
    private float maxHp;
    private float curHp;
    private float maxMp;
    private float curMp;
    private float moveSpeed;
    private float atk;
    private float attackRange;
    private float atkDamage;
    private float attackSpeed;
    private float spellPower;
    private float magicDamage;
    private float magicCastingTime; //��ų�� ĳ���� �ϴ� �ð�
    private float crowdControlTime; //CC(�������� = �����̻�)�ð�
    private float tenacity; //������ -> �ѿ��� CC�⸦ �ٿ��ִ� ���� 100%
    private float attackTarget; //���ݰ����� Ÿ�� ��
    private float barrier; //ü�´�� �������� ���� ��ȣ��
    private float stunTime; //���� �ð�(CC��)
    private float blindnessTime; //�Ǹ� �ð�(CC��)
    private float weakness; //��� �ð�(CC��)

    //�ڷ���Ʈ�� ���� �Ⱦ����� ���� �ϴ� vector3�� ���� ���߽��ϴ�.


    //���������� ���� ��ȹ���� �°� ���� �� ��


    private void Awake()
    {
        //hp = UnitData.GetHp + Equipment01Data.GetEquipmentHp + Equipment02Data.GetEquipmentHp + Equipment03Data.GetEquipmentHp;
        //attackSpeed = UnitData.GetAttackSpeed + Equipment01Data.GetEquipmentAttackSpeed + Equipment02Data.GetEquipmentAttackSpeed + Equipment03Data.GetEquipmentAttackSpeed;
        //Debug.Log("���� hp���� : " + hp);
        //Debug.Log("���� ���Ӱ��� : " + attackSpeed);
    }

    //���� �������� �ش� �� �ϰ�

    // ���� ��ġ�ÿ��� �ó��� �˻� �ؼ� 
    

}
