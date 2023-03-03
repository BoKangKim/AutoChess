using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerInfoConnector : MonoBehaviourPun
{
    [SerializeField] private GameObject rankPanel = null;
    [SerializeField] private TextMeshProUGUI rankText = null;
    private int playerUnitCount = 0;
    private PlayerData player = null;
    private int rank = 0;

    private void Awake()
    {
        player = new PlayerData();

 

    }
    private void Start()
    {
        player.playerName = PhotonNetwork.NickName;
        if (photonView.IsMine == true)
        {
            GameManager.Inst.SetPlayerInfoConnector(this);

            GameManager.Inst.UIManager.PlayerInfoUpdate();
            SyncNickName();

            //rankPanel = GameObject.Find("RankPanel");
            //rankText = GameObject.Find("RankText").GetComponent<TextMeshProUGUI>();

            //rankPanel.SetActive(false);
        }

    }

    #region Player Info
    public void SyncOwnerHP()
    {
        photonView.RPC("RPC_SyncHP",RpcTarget.Others,player.CurHP);
        GameManager.Inst.UIManager.SyncPlayerUI();

    }

    [PunRPC]
    public void RPC_SyncHP(int hp)
    {
        this.player.CurHP = hp;
    }

    public void SyncLevel()
    {
        photonView.RPC("RPC_SyncLevel", RpcTarget.Others, player.playerLevel);
        GameManager.Inst.UIManager.SyncPlayerUI();

    }

    [PunRPC]
    public void RPC_SyncLevel(int level)
    {
        this.player.playerLevel = level;
    }

    public void SyncNickName()
    {
        photonView.RPC("SyncNickName", RpcTarget.Others, player.playerName);
        GameManager.Inst.UIManager.SyncPlayerUI();

    }

    [PunRPC]
    public void SyncNickName(string name)
    {
        this.player.playerName = name;
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
            //rankPanel.SetActive(true);
            //rankText.text = "My Rank : " + this.rank;
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
