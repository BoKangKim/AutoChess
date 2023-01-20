using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;

public class OnClickStartButton : MonoBehaviour
{
    private ParentBT[] units = null;

    private void Awake()
    {
        units = FindObjectsOfType<ParentBT>();
    }

    public void OnClickStart()
    {
        for(int i = 0; i < units.Length; i++)
        {
            units[i].SetState(Battle.Stage.STAGETYPE.PVP);
        }
    }
}
