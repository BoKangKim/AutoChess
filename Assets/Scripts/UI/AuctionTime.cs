using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionTime : TimeManager
{
    protected override void Start()
    {
        base.Start();
        maximumTime = 15f;
    }
}
