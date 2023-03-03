using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region Singleton
    private static GameManager instance = null;
    public static GameManager Inst 
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if(instance == null)
                {
                    instance = new GameObject("GameTypeManagement").AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }
    #endregion
    private (int row, int col) stageIndex = (0, -1);
    private Pool pool = null;
    private Battle.Stage.STAGETYPE nowStage { get; set; } = Battle.Stage.STAGETYPE.PREPARE;
    [HideInInspector] public float time = 0f;
    private PlayerInfoConnector connecter = null;
    private PlayerInfoConnector[] players = null;
    private Timer timer = null;

    [Header("UIManager")]
    public UIManage UIManage;
    [Header("NetworkManager")]
    public NetworkManager networkManager;
    [Header("DataBase")]
    public Database dataBase;
    [Header("MataTrendAPI")]
    public MetaTrendAPI metaTrendAPI;
    [Header("SoundOption")]
    public SoundOption soundOption;

    public UIManager UIManager = null;
    private FadeIn ending = null;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        pool = FindObjectOfType<Pool>();
    }

    #region GAMETYPE
    private GAMETYPE type = GAMETYPE.MAX;

    public GAMETYPE getType()
    {
        return type;
    }

    public void setType(GAMETYPE type,GameObject networkManager)
    {
        NetworkManager net = null;
        if (networkManager.TryGetComponent<NetworkManager>(out net) == false)
        {
            return;
        }

        this.type = type;
    }
    #endregion

    #region SyncMasterInfo
    public void SyncStageIndex(int row , int col)
    {
        stageIndex.row = row;
        stageIndex.col = col;
    }

    public (int row,int col) getStageIndex()
    {
        return stageIndex;
    }

    public void SetNowStage(Battle.Stage.STAGETYPE nowStage)
    {
        this.nowStage = nowStage;

        connecter.SyncLevel();

        if (PhotonNetwork.IsMasterClient == true
            && nowStage == Battle.Stage.STAGETYPE.PREPARE)
        {
            if (players == null)
            {
                players = FindObjectsOfType<PlayerInfoConnector>();
            }

            for(int i = 0; i < players.Length; i++)
            {
                if(players[i].GetPlayer().CurHP <= 0)
                {
                    timer.SetIsPlaying(false);
                    CalcRank();
                    break;
                }
            }
        }
    }

    public void CalcRank()
    {
        PlayerInfoConnector temp = null;

        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < players.Length - 1; j++)
            {
                if (players[j].GetPlayer().CurHP < players[j + 1].GetPlayer().CurHP)
                {
                    temp = players[j];
                    players[j] = players[j + 1];
                    players[j + 1] = temp;
                }
            }
        }

        for(int i = 0; i < players.Length; i++)
        {
            players[i].SetRank(i + 1);
        }
        
    }

    public Battle.Stage.STAGETYPE GetNowStage()
    {
        return nowStage;
    }


    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Instantiate("StageControl",Vector3.zero,Quaternion.identity);
            Timer timer = FindObjectOfType<Timer>();
            timer.setNowTime(time);
            timer.findStageControl();
        }

    }

    #endregion

    public void SetPlayerInfoConnector(PlayerInfoConnector player)
    {
        this.connecter = player;
    }

    public PlayerInfoConnector GetPlayerInfoConnector()
    {
        return connecter;
    }

    public PlayerInfoConnector[] GetPlayers()
    {
        if(players == null)
        {
            players = FindObjectsOfType<PlayerInfoConnector>();
        }

        return players;
    }

    public void SetUIManager(UIManager uiManager)
    {
        this.UIManager = uiManager;
    }

    public UIManager GetUIManager()
    {

        return UIManager;
    }

    public void SetTimer(Timer timer)
    {
        this.timer = timer;
    }

    public FadeIn GetEnding()
    {
        return ending;
    }

    public void SetEnding(FadeIn ending)
    {
        this.ending = ending;
    }
}
