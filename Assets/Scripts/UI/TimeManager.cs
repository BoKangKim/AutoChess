using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Timer;
    [SerializeField] Image TimerIcon;
    protected float maximumTime = 30;
    public float currentTime;
    public bool IsTimeOver { get; set; }
    public bool IsNextRound { get; set; }

    private void Start()
    {
        IsTimeOver = false;
        IsNextRound = false;

        currentTime = maximumTime;
        TimerIcon.fillAmount = maximumTime;

    }
    private void Update()
    {
        // Game Timer
        //photonView.RPC(nameof(TimeFlow), RpcTarget.AllBuffered); // nameof 참조중인지 확인하기 위함
        if (IsTimeOver)
        {            
            currentTime = 0;
        }
        else 
        {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                if (IsNextRound)
                {
                    // 경험치 골드 지급 시점
                    // user.GetExp += 4;
                    // user.Getgold += 10;
                    currentTime = maximumTime;
                    IsNextRound = false;
                }
            }
        }
        TimerIcon.fillAmount = (currentTime / maximumTime);
        Timer.text = Mathf.Floor(currentTime).ToString();
        
    }
    
   
}
