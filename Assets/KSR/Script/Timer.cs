using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class Timer : MonoBehaviourPunCallbacks
{

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] Image timerImage;

    float MaxTime = 30;
    float CurTime;

    void Start()
    {
        CurTime = MaxTime;        
        timerImage.fillAmount = MaxTime;
    }
    void Update()
    {
        //photonView.RPC(nameof(TimeFlow), RpcTarget.AllBuffered); // nameof 참조중인지 확인하기 위함
        CurTime -= 1 * Time.deltaTime;
        timerImage.fillAmount = (CurTime / MaxTime);
        if (CurTime <= 0)
        {
            CurTime = 0;
        }
        timer.text = Mathf.Floor(CurTime).ToString();
    }

    //[PunRPC]
    public void TimeFlow()
    {
        CurTime -= 1 * Time.deltaTime;
        timerImage.fillAmount = (CurTime / MaxTime);
        if (CurTime <= 0)
        {
            CurTime = 0;
        }
        timer.text = Mathf.Floor(CurTime).ToString();
    }
}
