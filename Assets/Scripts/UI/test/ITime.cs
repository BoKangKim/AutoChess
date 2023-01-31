using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface ITime
{
    int IngameTimer { get; set; }
    void TimeProcess() { }
}
