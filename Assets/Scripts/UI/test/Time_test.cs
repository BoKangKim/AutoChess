using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_test : MonoBehaviour,ITime
{
    public int IngameTimer { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TimeProcess() 
    {
        Debug.Log("time");
    }
}
