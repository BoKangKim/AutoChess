using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    
    private float minMp = 10f; // ��ų��뿡 �ʿ��� �ּ� MP

    private float moveSpeed; // �̵� �ӵ�
    private float atk; // ���ݷ�
    private float attackRange; // �������ݷ�
    private float atkDamage; // �� ������
    private float attackSpeed; // ���� �ӵ�
    private float spellPower; // 
    private float magicDamage; // �� ������

    private float magicCastingTime; //��ų�� ĳ���� �ϴ� �ð� (��ų�� ������� �ƴ���)
    private float crowdControlTime; //CC(�������� = �����̻�)�ð�
    private float stunTime; //���� �ð�(CC��)
    private float blindnessTime; //�Ǹ� �ð�(CC��)
    private float weaknessTime; //��� �ð�(CC��)

    private float tenacity; //������ -> �ѿ��� CC�⸦ �ٿ��ִ� ���� 100%
    private float attackTarget; //���ݰ����� Ÿ�� ��
    private float barrier; //ü�´�� �������� ���� ��ȣ��

    //-------------------------------------------------------------------------------------

    Unit_test unit;

    private float coolTime { get; set; } // ��Ÿ��
    private float skillTime { get; set; } // ��ų ���� Ÿ�� - ���߿� �޾ƿ;���

    bool availableSkill; // ��ų��� �������� üũ - true�϶� ��ų ��� ����
    bool usingSkill; // ��ų ��� ���� üũ 
    bool abnormalState; // �����̻� üũ - true �϶� �ൿ���� 
    bool specialAbnormalState; // ���� �����̻� üũ
    
    private void Awake()
    {
        unit = FindObjectOfType<Unit_test>();
    }
    private void Start()
    {       
        availableSkill = true;
        usingSkill = false;
        abnormalState = false;                
    }

    private void Update()
    {
        
        if (unit.isDead==true)
        {
            Debug.Log("����");
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateMP(unit.mp, 10f);
            Debug.Log("UNIT MP : " + unit.mp);
            UseSkill();
        }
        if (usingSkill)
        {
            Timer();
        }
    }
    public void Timer()
    {
        skillTime -= Time.deltaTime;
        coolTime -= Time.deltaTime;
    }


    // ��ų ���
    public void UseSkill()
    {
        if (unit.isDead) return;

        if (availableSkill)
        {
            Debug.Log("��ų ���");
            StartSkill();
        }
        if (!availableSkill)
        {
            Debug.Log("���� ����");
            Debug.Log("UNIT MP : " + unit.mp);
        }
    }


    // ��ų ��� ����
    public void StartSkill()
    {
        Debug.Log("��ų ����Ʈ ���");        
        if (skillTime >= 0)
        {
            usingSkill = true; // ��ų �����
            
            CCTime(coolTime); // ��Ÿ�� üũ
            if (usingSkill)
            {                 
                CCTime(stunTime); // ���� �ð� üũ

                if (unit.IsHit) // ������ �޾��� ��
                {
                    // �����̻��� ��� -> ���ֿ��� Ȯ�ΰ���
                    if (abnormalState)
                    {
                        AbnormalState();
                    }
                    // �ൿ ���� �����̻� �ɷ��� ���
                    if (specialAbnormalState)
                    {
                        // �÷��̾� ��� ���� - �������� ��� ���� ����                        
                        SpecialAbnormalState();
                    }
                }
                
            }
        }
    }

    public void CalculateMP(float mp, float useMp)
    {
            Debug.Log("UNIT MP : "+unit.mp);
        mp -= useMp;
        Debug.Log("UNIT MP : " + unit.mp);
        if (mp <= minMp)
        {
            Debug.Log("���� ����");
            availableSkill = false;
        }
        else
        {
            Debug.Log("���� ���");
            availableSkill = true;
        }
    }


    // exception handling
    #region exception handling


    // ��Ÿ�� �� ��ų ���� �ð��� ���� ���������� ��ų ��� �Ұ���
    // coolTime, stunTime
    public void CCTime(float time)
    {
        if (time <= 0)
        {
            availableSkill = true;
        }
        else
        {
            availableSkill = false;
        }
    }

    public void AbnormalState() 
    {
        // ��ų ��� �Ұ�, �Ϲ� ���� ����
        Debug.Log("���� �̻� ȿ�� ����");
        availableSkill = false;
    }
    public void SpecialAbnormalState() 
    {
        // ��ų ��� �Ұ�, ��� �ൿ ����
        Debug.Log("�ൿ ���� & ���� �̻� ȿ�� ����");
        unit.Idle();
        availableSkill = false;
    }
    
    #endregion


}
