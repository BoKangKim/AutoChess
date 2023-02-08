using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTime : TimeManager
{ 
    
    protected override void Start()
    {
        base.Start();
        maximumTime = 30f;
    }
}
