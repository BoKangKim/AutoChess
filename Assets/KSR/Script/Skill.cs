using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private float grade;
    private float maxHp = 100f;
    private float maxMp = 100f;
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
    private float skillTime { get; set; } // ��ų ���� Ÿ��

    bool availableSkill; // ��ų��� �������� üũ - true�϶� ��ų ��� ����
    bool usingSkill; // ��ų ��� ���� üũ 
    bool abnormalState; // �����̻� üũ - true �϶� �ൿ����

    private void Awake()
    {
        unit = FindObjectOfType<Unit_test>();
    }
    private void Start()
    {
        if (unit.isDead) return;
        availableSkill = true;
        usingSkill = false;
        abnormalState = false;

        unit.hp = maxHp;
        unit.mp = maxMp;

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSkill();
        }

    }
    public void Timer()
    {
        skillTime += Time.deltaTime;

        coolTime -= Time.deltaTime;

    }


    // ��ų ���
    public void UseSkill()
    {

        // ����ó�� - ����, �ð� üũ
        if (unit.isDead) return;
        // ��ų ��밡���̸� ��ų ����
        if (availableSkill)
        {
            Debug.Log("��ų��");
            StartSkill();
        }
        else
        {
            Debug.Log("��ų�Ⱦ�");
        }
    }


    // ��ų ��� ����
    public void StartSkill()
    {
        // �ִϸ��̼� �� ����Ʈ ���
        Debug.Log("��ų ����");

        // ���� ��ų ��� ��
        if (skillTime > 0)
        {
            usingSkill = true;
            CalculateMP(unit.mp);
            CCTime(coolTime);
            // ���� ���, ��Ÿ��üũ �Ŀ� �����̻� üũ
            if (abnormalState) // �����̻��� ��� - ������ �޾��� ��
            {
                // ��� ����
                Idle();
                // ��� ���� ����
                // ��ų ��� �Ұ�
            }
            else
            {
                // �����̻��� �ƴ� ���
                // ��ų ��� ����
                
            }
            
        }
        else
        {
            usingSkill = false;
        }
    }

    // ������ ������ üũ

    // ��ų ��� �� Mp ����
    public void CalculateMP(float mp)
    {
        mp -= 10f;
        if (mp <= minMp)
        {
            Debug.Log("��ų���Ұ�");
            availableSkill = false;
        }
        else
        {
            Debug.Log("��ų��밡��");
            availableSkill = true;
        }
    }


    // exception handling
    #region exception handling


    // ��Ÿ�� �� ��ų ������� �ð��� ���� ���������� ��ų ��� �Ұ���
    // coolTime, stunTime, skillTime
    public void CCTime(float time)
    {
        if (time <= 0)
        {
            if (!abnormalState) // �����̻��� �ƴѻ���
            {
            availableSkill = true; // ��ų ��� ����

            }
        }
        else
        {
            if (abnormalState) 
            {
                availableSkill = false;
            }            
            
        }
    }

    // �����̻� �ɷȴ��� Ȯ���ϴ� �� ���� �ʿ���

    // �����̻� üũ
    public void abnormalStatus()
    {
        if (usingSkill) // ��ų ������� ��
        {
            // �����̻� �ð��� üũ�ϰ� �� ����
            CCTime(stunTime);
            CCTime(skillTime);

            // ���� ��ų ���� - ���� ���� ����

            // ������� ��ų�� ���� ��� ���·�     
            Idle();

        }
        else // ��ų ������� �ƴ� ��
        {
            Debug.Log("���� ��ų ����");
        }
    }

    // ���� ��ų�� ����ϴ� ���� Ÿ���� ������ ��쿡�� ���θ��� ��ų ����� ������
    public void TypeCheck(string tag)
    {
        if (gameObject.tag == tag)
        {
            Debug.Log(gameObject.tag);
            Debug.Log(tag);
            availableSkill = true;
        }
        else
        {
            availableSkill = false;
        }
    }

    #endregion

    public void Hit() // �����̻� �޾ƿ���
    {
        // ���ݹ޾��� �� �����̻� ����
        abnormalState = true;
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
