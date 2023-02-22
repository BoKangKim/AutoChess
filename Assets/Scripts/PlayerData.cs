using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    // ID
    public string playerName;
    //���
    public int gold;
    //����
    public int playerLevel;
    //hp
    public int CurHP;
    private int MaxHP;
    //����ġ
    public float[] MaxExp;
    public float CurExp;

    public PlayerData()
    {
        Initializing();
    }

    private void Initializing()
    {
        playerName = "name";
        gold = 0;
        playerLevel = 1;
        MaxHP = 10;
        CurHP = MaxHP;
        MaxExp = new float[10] { 0, 2, 6, 10, 20, 36, 56, 70, 80, 100 };
        CurExp = 0;
    }

    // ���� ������ �����Ҷ� ��� �����Ҷ����
    // �÷��̾� ������ ����ġ�� ��á���� �÷��̾� ������ maxexp�迭���ε����� ���� ����ġ ���� �����´�.
    // �ƽ� hp�� hpbar�� ����Ҷ�����
    // ���� hp�ٴ� ����ȭ�������
    // �ƽ� ����ġ�� ���������� �޶�����
    // ���� ����ġ�� �ƽ�����ġ�� ���ų� �Ѿ����� ���������ϰ� �ƽ�����ġ���� �������ġ�� ����.

}
