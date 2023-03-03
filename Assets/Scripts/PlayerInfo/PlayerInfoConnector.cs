using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerInfoConnector : MonoBehaviourPun
{
    private GameObject endingCanvas = null;
    private int playerUnitCount = 0;
    private PlayerData player = null;
    private int rank = 0;

    private void Start()
    {
        player = new PlayerData();

        if (photonView.IsMine == true)
        {
            GameManager.Inst.SetPlayerInfoConnector(this);
            endingCanvas = GameObject.Find("Ending");
            endingCanvas.SetActive(false);
        }
    }

    #region Player Info
    public void SyncOwnerHP()
    {
        photonView.RPC("RPC_SyncHP",RpcTarget.Others,player.CurHP);
    }
    
    [PunRPC]
    public void RPC_SyncHP(int hp)
    {
        this.player.CurHP = hp;
    }

    public void SetRank(int rank)
    {
        photonView.RPC("RPC_SetRank",RpcTarget.All,rank);
    }

    [PunRPC]
    public void RPC_SetRank(int rank)
    {
        this.rank = rank;
        if(photonView.IsMine == true)
        {
            endingCanvas.SetActive(true);
            GameManager.Inst.GetEnding().RankStart(rank);
        }
    }

    public void SyncTimer(string time)
    {
        photonView.RPC("RPC_SyncTimer",RpcTarget.All,time);
    }

    [PunRPC]
    public void RPC_SyncTimer(string time)
    {
        GameManager.Inst.GetUIManager().SetTimeText(time);
    }

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
