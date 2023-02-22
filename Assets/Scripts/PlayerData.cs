using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
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

    private void Awake()
    {
        Initializing();
    }

    private void Initializing()
    {
        playerName = "name";
        gold = 0;
        playerLevel = 1;
        MaxHP = 100;
        CurHP = MaxHP;
        MaxExp = new float[10] { 0, 2, 6, 10, 20, 36, 56, 70, 80, 100 };
        CurExp = 0;
    }

}
