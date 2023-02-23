using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RemainTime : MonoBehaviour
{
    DateTime Onedaystart = new DateTime(2023, 2, 17, 0, 30, 0);  
    DateTime Onedayend = new DateTime(2023, 2, 18, 0, 0, 0);    
    DateTime Sevendaystart = new DateTime(2023, 2, 15, 0, 30, 0);  
    DateTime Sevendayend = new DateTime(2023, 2, 22, 0, 0, 0);    
    DateTime Monthstart = new DateTime(2023, 2, 15, 0, 30, 0);  
    DateTime Monthend = new DateTime(2023, 3, 18, 0, 0, 0);   
    WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);
    [SerializeField] private MetaTrendAPI metaTrendAPI;
    [SerializeField] private TextMeshProUGUI Oneday;
    [SerializeField] private TextMeshProUGUI Sevenday;
    [SerializeField] private TextMeshProUGUI Monthday;
    [SerializeField] private TextMeshProUGUI Onedaylimit;
    [SerializeField] private TextMeshProUGUI Sevendaylimit;
    [SerializeField] private TextMeshProUGUI Monthdaylimit;
     private int Onepool;
     private int Sevenpool;
     private int Monthpool;

    private Records oneDayData = null;
    private Records sevenDayData = null;
    private Records monthDayData = null;


    private IEnumerator importTournamentPoolData()
    {

        yield return new WaitUntil(() => metaTrendAPI.res_DummyTournamentPool != null);

        oneDayData = metaTrendAPI.res_DummyTournamentPool.data.records[0];
        sevenDayData = metaTrendAPI.res_DummyTournamentPool.data.records[1];
        monthDayData = metaTrendAPI.res_DummyTournamentPool.data.records[2];


        Onepool = oneDayData.amount;
        Sevenpool = sevenDayData.amount;
        Monthpool = monthDayData.amount;



        Onedaystart = new DateTime(int.Parse(oneDayData.startTime.Split('T')[0].Split('-')[0]),
                                   int.Parse(oneDayData.startTime.Split('T')[0].Split('-')[1]),
                                   int.Parse(oneDayData.startTime.Split('T')[0].Split('-')[2]),
                                   int.Parse(oneDayData.startTime.Split('T')[1].Split(':')[0]),
                                   int.Parse(oneDayData.startTime.Split('T')[1].Split(':')[1]),
                                   0);

        Onedayend = new DateTime(int.Parse(oneDayData.endTime.Split('T')[0].Split('-')[0]),
                                   int.Parse(oneDayData.endTime.Split('T')[0].Split('-')[1]),
                                   int.Parse(oneDayData.endTime.Split('T')[0].Split('-')[2]),
                                   int.Parse(oneDayData.endTime.Split('T')[1].Split(':')[0]),
                                   int.Parse(oneDayData.endTime.Split('T')[1].Split(':')[1]),
                                   0);
        Sevendaystart = new DateTime(int.Parse(sevenDayData.startTime.Split('T')[0].Split('-')[0]),
                                   int.Parse(sevenDayData.startTime.Split('T')[0].Split('-')[1]),
                                   int.Parse(sevenDayData.startTime.Split('T')[0].Split('-')[2]),
                                   int.Parse(sevenDayData.startTime.Split('T')[1].Split(':')[0]),
                                   int.Parse(sevenDayData.startTime.Split('T')[1].Split(':')[1]),
                                   0);
        Sevendayend = new DateTime(int.Parse(sevenDayData.endTime.Split('T')[0].Split('-')[0]),
                                   int.Parse(sevenDayData.endTime.Split('T')[0].Split('-')[1]),
                                   int.Parse(sevenDayData.endTime.Split('T')[0].Split('-')[2]),
                                   int.Parse(sevenDayData.endTime.Split('T')[1].Split(':')[0]),
                                   int.Parse(sevenDayData.endTime.Split('T')[1].Split(':')[1]),
                                   0);
        Monthstart = new DateTime(int.Parse(monthDayData.startTime.Split('T')[0].Split('-')[0]),
                                   int.Parse(monthDayData.startTime.Split('T')[0].Split('-')[1]),
                                   int.Parse(monthDayData.startTime.Split('T')[0].Split('-')[2]),
                                   int.Parse(monthDayData.startTime.Split('T')[1].Split(':')[0]),
                                   int.Parse(monthDayData.startTime.Split('T')[1].Split(':')[1]),
                                   0);
        Monthend = new DateTime(int.Parse(monthDayData.endTime.Split('T')[0].Split('-')[0]),
                                   int.Parse(monthDayData.endTime.Split('T')[0].Split('-')[1]),
                                   int.Parse(monthDayData.endTime.Split('T')[0].Split('-')[2]),
                                   int.Parse(monthDayData.endTime.Split('T')[1].Split(':')[0]),
                                   int.Parse(monthDayData.endTime.Split('T')[1].Split(':')[1]),
                                   0);

    }

    private IEnumerator Onetictok()
    {
        while (DateTime.Now < Onedayend)
        {
            TimeSpan remainingTime = Onedayend - DateTime.Now;
            string timeString = string.Format($"{(int)remainingTime.TotalHours}:{remainingTime.Minutes}:{remainingTime.Seconds}");
            Oneday.text = "Left time" + Environment.NewLine + timeString;
            Onedaylimit.text = "Start Time" + "\n" + Onedaystart.ToString("yyyy-MM-dd  hh:mm:ss")+" am" + "\n" + "End Time" + "\n" +
            Onedayend.ToString("yyyy-MM-dd  hh:mm:ss")+ " am" + "\n" + "\n" + "Current Pool" + "\n" + Onepool;
            yield return waitForOneSecond;
        }
        if (DateTime.Now > Onedayend)
        {
            Oneday.text = "End";
            Onedaylimit.text = "";
        }
    }
    private IEnumerator Seventictok()
    {
        while (DateTime.Now < Sevendayend)
        {
            TimeSpan remainingTime = Sevendayend - DateTime.Now;
            string timeString = string.Format($"{(int)remainingTime.TotalHours}:{remainingTime.Minutes}:{remainingTime.Seconds}");
            Sevenday.text = "Left time" + Environment.NewLine + timeString;
            Sevendaylimit.text = "Start Time" + "\n" + Sevendaystart.ToString("yyyy-MM-dd  hh:mm:ss")+ " am" + "\n" + "End Time" + "\n" +
            Sevendayend.ToString("yyyy-MM-dd  hh:mm:ss")+ " am" + "\n" + "\n" + "Current Pool" + "\n" + Sevenpool;
            yield return waitForOneSecond;
        }
        if (DateTime.Now > Sevendayend)
        {
            Sevenday.text = "End";
            Sevendaylimit.text = "";
        }
    }
    private IEnumerator Monthtictok()
    {
        while (DateTime.Now < Monthend)
        {
            TimeSpan remainingTime = Monthend - DateTime.Now;
            string timeString = string.Format($"{(int)remainingTime.TotalHours}:{remainingTime.Minutes}:{remainingTime.Seconds}");
            Monthday.text = "Left time" + Environment.NewLine + timeString;
            Monthdaylimit.text = "Start Time" + "\n" + Monthstart.ToString("yyyy-MM-dd  hh:mm:ss")+ " am" + "\n" + "End Time" + "\n" +
            Monthend.ToString("yyyy-MM-dd  hh:mm:ss")+ " am" + "\n" + "\n" + "Current Pool" + "\n" + Monthpool;
            yield return waitForOneSecond;
        }
        if (DateTime.Now > Monthend)
        {
            Sevenday.text = "End";
            Sevendaylimit.text = "";
        }
    }
    private void Awake()
    {
        StartCoroutine(importTournamentPoolData());


    }
    private void OnEnable()
    {

        StartCoroutine(Onetictok());
        StartCoroutine(Seventictok());
        StartCoroutine(Monthtictok());

    }
}
