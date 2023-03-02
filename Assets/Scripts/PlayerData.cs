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
    public int[] MaxExp;
    public float CurExp;

    public PlayerData()
    {
        Initializing();
    }

    private void Initializing()
    {
        playerName = "name";
        gold = 500;
        playerLevel = 1;
        MaxHP = 100;
        CurHP = MaxHP;
        MaxExp = new int[8] { 0, 2, 6, 10, 20, 36, 56, 70};
        CurExp = 0;
    }

}
