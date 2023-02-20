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
    public Battle.Stage.STAGETYPE nowStage { get; set; } = Battle.Stage.STAGETYPE.PREPARE;
    [HideInInspector] public float time = 0f;

    private PlayerData player = null;
    // À¯´Ö ÃÑ °¹¼ö °ü¸®
    // Manager ±Þ Å¬·¡½ºµé ¿©±â´Ù°¡

    private void Awake()
    {
        DontDestroyOnLoad(this);
        pool = FindObjectOfType<Pool>();
    }

    private void Start()
    {
        if(TryGetComponent<PlayerData>(out player) == false)
        {
            Debug.LogError("Not Found Player Data");
        }
    }

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

    public void SyncStageIndex(int row , int col)
    {
        stageIndex.row = row;
        stageIndex.col = col;
    }

    public (int row,int col) getStageIndex()
    {
        return stageIndex;
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

    public PlayerData GetPlayer()
    {
        return player;
    }
}
