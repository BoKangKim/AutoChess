using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_test : MonoBehaviour
{

    private float grade; // ���

    [SerializeField] private float curHp;
    [SerializeField] private float curMp;
    private float maxHp = 100f;
    private float maxMp = 100f;

    [SerializeField] private bool IsDead;
    public bool IsSpecial; // Ư����ų���� Ȯ��

    public bool isDead { get; set; }
    public bool IsHit; // ���ݹ޾ҳ� üũ

    public float hp { get; set; }
    public float mp { get; set; }

    private void Awake()
    {
    }
    private void Start()
    {
        IsDead = false;
        IsHit = false;
        hp = maxHp;
        mp = maxMp;
    }

    private void Update()
    {
        // �׾����� ��ų ��� �Ұ�
        if (isDead) return;

    }
    public void normalSkill()
    {
        Debug.Log("�Ϲ� ��ų ���");
        IsSpecial = false;
    }
    public void specialSkill()
    {
        Debug.Log("Ư�� ��ų ���");
        IsSpecial = true;
    }

    // ����, Ŭ������ ��ġ�ϴ��� Ȯ���ϰ�
    // ��ų�� ����ϴ� ���� Ÿ���� ������ ��쿡�� ���θ��� ��ų ����� ������
    public void TypeCheck(string type)
    {
        if (gameObject.tag == type)
        {
            Debug.Log(gameObject.tag);
            Debug.Log(type);
            //availableSkill = true;
        }
        else
        {
            //availableSkill = false;
        }
    }

    public void Hit()
    {
        IsHit = true;
        // Ư�� ���ݹ޾��� �� ��ų�� �����̻��� �ִٸ� �����̻� ���� - 
        if (IsSpecial)
        {
            //abnormalState = true;
            Debug.Log("�����̻�");

        }
        else
        {
            //abnormalState = false;
            Debug.Log("��������");
        }
    }
    public void Idle()
    {
        Debug.Log("���");
    }
    public void Move()
    {
        Debug.Log("�̵�");
    }
    public void Dead()
    {
        Debug.Log("����");
    }

}
