using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_test : MonoBehaviour
{
    protected int gold;
    public int GetGold { get; set; }
    protected int exp;
    public int GetEXP { get; set; }
    protected int level;
    public int GetLevel { get; set; }

    private void Awake()
    {
        gold = GetGold;
        exp = GetEXP;
        level = GetLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        GetGold = 1000;
        GetEXP = 0;
        GetLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
