using Photon.Pun;

public class PlayerInfoConnector : MonoBehaviourPun
{
    private int playerUnitCount = 0;
    private PlayerData player = null;
    private string nickName = "";

    private void Awake()
    {
        player = new PlayerData();
        GameManager.Inst.SetPlayerInfoConnector(this);
        nickName = PhotonNetwork.NickName;
    }

    public string GetNickName()
    {
        return nickName;
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

    #region SyncPlayerInfo

    public void DamageHP(int damage)
    {
        photonView.RPC("RPC_DamageHP",RpcTarget.All,damage);
    }

    [PunRPC]
    public void RPC_DamageHP(int damage)
    {
        this.player.CurHP -= damage;
        if(player.CurHP <= 0)
        {
            PhotonNetwork.LeaveRoom();
        }
    }


    #endregion

}
