using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //���
    public int gold;
    //����
    public int playerLevel;
    //hp
    public int CurHP;
    public int MaxHP;
    //����ġ
    public int[] MaxExp;
    public int CurExp;

    private void Awake()
    {
        gold = 0;
        playerLevel = 1;
        MaxHP = 100;
        CurHP = MaxHP;
        MaxExp = new int[10] { 0, 2, 6, 10, 20, 36, 56, 70 , 80, 100 };
        CurExp = 0;
    }
    // ���� ������ �����Ҷ� ��� �����Ҷ����
    // �÷��̾� ������ ����ġ�� ��á���� �÷��̾� ������ maxexp�迭���ε����� ���� ����ġ ���� �����´�.
    // �ƽ� hp�� hpbar�� ����Ҷ�����
    // ���� hp�ٴ� ����ȭ�������
    // �ƽ� ����ġ�� ���������� �޶�����
    // ���� ����ġ�� �ƽ�����ġ�� ���ų� �Ѿ����� ���������ϰ� �ƽ�����ġ���� �������ġ�� ����.

}
