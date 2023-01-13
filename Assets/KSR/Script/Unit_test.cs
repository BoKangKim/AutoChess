using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_test : MonoBehaviour
{
    [SerializeField] private float curHp;
    [SerializeField] private float curMp;
    [SerializeField] private bool IsDead;

    public bool isDead { get; set; }
    public float hp{ get; set; }
    public float mp { get; set; }

    private void Awake()
    {
        IsDead = false;
    }

    private void Update()
    {
        // 죽었으면 스킬 사용 불가
        if (isDead)
        {

        }
    }
    
}
