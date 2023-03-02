using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum GAMETYPE
{
    FREENET,
    LIVENET,
    MAX
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public UIManage uIManage;
    private IEnumerator tempCoritineu;

    [Header("LobbyPanel")]
    public Button nomalMatchButton;
    public TextMeshProUGUI myNickName;
    public Image[] userIcon;

    [Header("LoadingPanel")]
    public Canvas loadingPanel;
    public TextMeshProUGUI metchingSecText;
    public TextMeshProUGUI metchingCurPlyaerText;


    public TextMeshProUGUI statusText;

    PhotonView PV;
    RoomOptions room;
    private string gameScene;

    [Header("Chatting")]
    public TMP_InputField ChatInput;
    public TextMeshProUGUI[] ChatText;
    public ChatManager chatmanager = null;

    private void Awake()
    {
        Screen.SetResolution(480, 480, false);
        DontDestroyOnLoad(this);
        PhotonNetwork.AutomaticallySyncScene = true;
        room = new RoomOptions();
        gameScene = "InGameUILWH";
    }

    private void Start()
    {
        Connect();
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings();
    //public override void OnConnectedToMaster() => StartCoroutine(Co_JoinLobby());

    public void joinLobby()
    {
        StartCoroutine(Co_JoinLobby());
        GameManager.Inst.UIManage.startbutton.interactable = false;
        GameManager.Inst.soundOption.SFXPlay("ClickSFX");
    }

    private IEnumerator Co_JoinLobby()
    {
        GameManager.Inst.dataBase.GetUserInfo();
        yield return new WaitUntil(() => GameManager.Inst.dataBase.userInfo.username != null);

        int rnd = Random.Range(0,2100000000);

        myNickName.text = GameManager.Inst.dataBase.userInfo.username;
        PhotonNetwork.NickName = GameManager.Inst.dataBase.userInfo.username + rnd.ToString();
        GameManager.Inst.UIManage.userIcon.sprite = GameManager.Inst.UIManage.userIconImage[GameManager.Inst.dataBase.userInfo.userIconIndex];
        PhotonNetwork.JoinLobby();
        StartCoroutine(GameManager.Inst.UIManage.FadeoutStart());
    }

    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.NickName);
        //chatmanager.enabled = true;
        PV = photonView;
        //PhotonNetwork.LocalPlayer.NickName = Database.Instance.userInfo.NickName;
    }

    public void joinFreeNet()
    {
        uIManage.matchingtime.text = "00:00";
        GameManager.Inst.setType(GAMETYPE.FREENET, gameObject);
        JoinRandomOrCreateRoom();
        tempCoritineu = uIManage.MatchTimer();
        StartCoroutine(tempCoritineu);
        GameManager.Inst.UIManage.Letsmatch();
        GameManager.Inst.soundOption.bgmPlay("MatchingBgm");
    }

    public void joinLiveNet()
    {
        GameManager.Inst.setType(GAMETYPE.LIVENET, gameObject);
        JoinRandomOrCreateRoom();
    }

    public void JoinRandomOrCreateRoom()
    {
        room.MaxPlayers = 4;

        if (PhotonNetwork.IsConnected)
        {
            room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();

            PhotonNetwork.JoinRandomOrCreateRoom(
                expectedCustomRoomProperties: new ExitGames.Client.Photon.Hashtable(), expectedMaxPlayers: room.MaxPlayers, 
                roomOptions: room 
                );
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void CancelMatching()
    {
        Debug.Log("매칭 취소.");
        uIManage.matching.gameObject.SetActive(false);
        uIManage.selectbattle.gameObject.SetActive(true);
        Debug.Log("방 떠남.");
        PhotonNetwork.LeaveRoom();
        StopCoroutine(tempCoritineu);
        GameManager.Inst.soundOption.SFXPlay("ClickSFX");
        GameManager.Inst.soundOption.bgmPlay("LobbyBgm");


    }

    public override void OnCreatedRoom()
    {
    }


    public override void OnJoinedRoom()
    {
        //UpdatePlayerCount();
        //PhotonNetwork.LoadLevel(gameScene);


        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {

                PhotonNetwork.LocalPlayer.CustomProperties["PlayerNum"] = i;

                PhotonNetwork.PlayerList[i].SetCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);

                break;
            }
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            PhotonNetwork.LoadLevel(gameScene);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GameManager.Inst.soundOption.SFXPlay("MatchingSFX", GameManager.Inst.UIManage.mainLobby.gameObject);
    }






}





