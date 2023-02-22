using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // ID
    public string name;
    //골드
    public int gold;
    //레벨
    public int playerLevel;
    //hp
    public int CurHP;
    public int MaxHP;
    //경험치
    public float[] MaxExp;
    public float CurExp;

    private void Awake()
    {
        name = "name";
        gold = 0;
        playerLevel = 1;
        MaxHP = 100;
        CurHP = MaxHP;
        MaxExp = new float[10] { 0, 2, 6, 10, 20, 36, 56, 70 , 80, 100 };
        CurExp = 0;
        
    }
    // 골드는 유닛을 구매할때 장비를 구매할때사용
    // 플레이어 레벨은 경험치가 꽉찼을때 플레이어 레벨은 maxexp배열의인덱스의 들어가서 경험치 통을 가져온다.
    // 맥스 hp는 hpbar를 사용할때쓰고
    // 현재 hp바는 동기화해줘야함
    // 맥스 경험치는 레벨에따라 달라진다
    // 현재 경험치는 맥스경험치를 같거나 넘었을때 레벨업을하고 맥스경험치에서 현재경험치를 뺀다.

}
