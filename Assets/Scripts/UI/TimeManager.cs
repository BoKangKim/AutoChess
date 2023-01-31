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
    protected float currentTime;
    protected bool IsTimeOver { get; set; }

    private void Start()
    {
        IsTimeOver = false;

        currentTime = maximumTime;
        TimerIcon.fillAmount = maximumTime;
    }
    private void Update()
    {
        // Game Timer
        //photonView.RPC(nameof(TimeFlow), RpcTarget.AllBuffered); // nameof 참조중인지 확인하기 위함
        if (IsTimeOver == true) return;
        currentTime -= 1 * Time.deltaTime;
        TimerIcon.fillAmount = (currentTime / maximumTime);
        if (currentTime <= 0)
        {
            IsTimeOver = true;
            currentTime = 0;
        }
        Timer.text = Mathf.Floor(currentTime).ToString();
    }
}
