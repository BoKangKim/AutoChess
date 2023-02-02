using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.Stage;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TIME = null;
    
    private StageControl sc = null;
    private const float STAGE_TIME = 5f;
    private float nowTime = 0f;

    public float getNowTime()
    {
        return nowTime;
    }

    private void Awake()
    {
        sc = FindObjectOfType<StageControl>();
        if(sc == null)
        {
            sc = new GameObject("StageControl").AddComponent<StageControl>();
        }
    }

    private void Update()
    {
        nowTime += Time.deltaTime;
        TIME.text = "TIME : " + (int)nowTime;

        if(nowTime >= STAGE_TIME)
        {
            sc.checkNextStageInfo();
            nowTime = 0f;
        }
    }
}
