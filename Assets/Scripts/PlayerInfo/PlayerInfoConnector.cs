using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInfoConnector : MonoBehaviourPun
{
    private int playerUnitCount = 0;
    private PlayerData player = null;

    private void Awake()
    {
        if(photonView.IsMine == true)
        {
            player = new PlayerData();
            GameManager.Inst.SetPlayerInfoConnector(this);
        }
    }

    #region Player Info
    public void PlusUnitCount()
    {
        playerUnitCount++;
    }

    public void MinusUnitCount()
    {
        playerUnitCount--;
    }

    public void ResetUnitCount()
    {
        playerUnitCount = 0;
    }

    public int GetUnitCount()
    {
        return playerUnitCount;
    }

    public PlayerData GetPlayer()
    {
        return player;
    }
    #endregion

}
