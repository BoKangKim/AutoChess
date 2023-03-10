using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;
using Photon.Pun;
using Photon.Realtime;

public class OnClickStartButton : MonoBehaviour
{
    private ParentBT[] units = null;

    public void OnClickStart()
    {
        units = FindObjectsOfType<ParentBT>();
        for (int i = 0; i < units.Length; i++)
        {
            units[i].SetState(Battle.Stage.STAGETYPE.MONSTER);
        }
    }

    public void OnClickStart_1()
    {
        Battle.Stage.StageControl stage = FindObjectOfType<Battle.Stage.StageControl>();

    }
}
