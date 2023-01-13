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
        skillTime -= Time.deltaTime;

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
        if (!availableSkill)
        {
            Debug.Log("��ų�Ⱦ�");
        }
    }


    // ��ų ��� ����
    public void StartSkill()
    {
        // �ִϸ��̼� �� ����Ʈ ���
        Debug.Log("��ų ����");
        // ��ų ���۰� ���ÿ� �ð��� �帣���� �� ��
        // ���� ��ų ��� ��
        if (skillTime >= 0)
        {
            usingSkill = true; // ��ų ����� üũ
            CalculateMP(unit.mp,10f); // ���� ���� - ���Ǵ� ���� ���߿� ������ �ޱ�
            CCTime(coolTime); // ��Ÿ�� üũ
            if (usingSkill) // ��ų ������϶�
            { 
                // �����̻�(����) �ð��� üũ�ϰ� �� ���� 
                CCTime(stunTime);

                if (unit.IsHit) // ������ �޾��� ��
                {
                    // �����̻��� ��� -> ���ֿ��� Ȯ�ΰ���
                    if (abnormalState)
                    {
                        // ��� ���� - �������� ��� ���� ����
                        unit.Idle();
                        // ��ų ��� �Ұ�
                        availableSkill = false;
                    }
                    else // �����̻��� �ƴ� ���
                    {
                        // ��ų ��� ����
                        availableSkill = true;
                    }

                }
                
            }
        }
        /*else
        {
            usingSkill = false;
        }*/
    }

    // ��ų ��� �� ���� ����
    // ���� ������ ���� - ���Ǵ� ������
    public void CalculateMP(float mp, float useMp)
    {
        mp -= useMp;
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
    // coolTime, stunTime
    public void CCTime(float time)
    {
        if (time <= 0)
        {
            availableSkill = true; // ��ų ��� ����
        }
        else
        {
            // ���� �����̻� �ɷȾ ���ų�� �ƴ� ��� 

            availableSkill = false;
        }
    }
    
    #endregion


}
