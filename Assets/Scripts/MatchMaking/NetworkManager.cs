using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public enum GAMETYPE 
{
    FREENET,
    LIVENET,
    MAX
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("LoginPanel")]
    public Image logingPanel;


    [Header("LobbyPanel")]
    public Image lobbyPanel;
    public Button matchPanelButton;
    public Button nomalMatchButton;
    public Image matchButtonPanel;
    public TextMeshProUGUI myNickName;
    public Image userIcon;

    public Image settingsPanel;
    public Button settingsCloseButton;
    public Button settingsPanelButton;

    [Header("LoadingPanel")]
    public Image loadingPanel;
    public Image loadingImg;
    public Image[] loadingPepleImg;
    public TextMeshProUGUI metchingText;
    public TextMeshProUGUI metchingSecText;
    public TextMeshProUGUI metchingCurPlyaerText;

    
    public TextMeshProUGUI statusText;

    PhotonView PV;
    RoomOptions room;
    private string gameScene;

    public TMP_InputField ChatInput;
    public TextMeshProUGUI[] ChatText;

    public ChatManager chatmanager = null;

    private void Awake()
    {
        Screen.SetResolution(480,480,false);
        PhotonNetwork.AutomaticallySyncScene = true;
        room = new RoomOptions();
        gameScene = "SyncUnit";
    }

    private void Start()
    {
        Connect();
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings();
    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        int rnd = Random.Range(0, 2147483645);

        lobbyPanel.gameObject.SetActive(true);

        logingPanel.gameObject.SetActive(false);

        Debug.Log(Database.Instance.userInfo.username);
        myNickName.text = Database.Instance.userInfo.username;
        PhotonNetwork.NickName = Database.Instance.userInfo.username + rnd.ToString();

        Debug.Log(PhotonNetwork.NickName);
        chatmanager.enabled = true;
        PV = photonView;
        //PhotonNetwork.LocalPlayer.NickName = Database.Instance.userInfo.NickName;
    }

    public void joinFreeNet()
    {
        GameManager.Inst.setType(GAMETYPE.FREENET,gameObject);
        JoinRandomOrCreateRoom();

    }

    public void joinLiveNet()
    {
        GameManager.Inst.setType(GAMETYPE.LIVENET, gameObject);
        JoinRandomOrCreateRoom();
    }

    public void JoinRandomOrCreateRoom()
    {
        nomalMatchButton.interactable = false;
        room.MaxPlayers = 4;

        if (PhotonNetwork.IsConnected)
        {
            statusText.text = "Connecting to Random Room...";
            room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();

            PhotonNetwork.JoinRandomOrCreateRoom(
                expectedCustomRoomProperties: new ExitGames.Client.Photon.Hashtable(), expectedMaxPlayers: room.MaxPlayers, // ������ ���� ����.
                roomOptions: room // ������ ���� ����.
                );

        }
        else
        {
            statusText.text = "offline : Connetion Disabled - Try reconnecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }


    public override void OnCreatedRoom()
    {
        statusText.text = ($"metching + {PhotonNetwork.CurrentRoom.PlayerCount}");

    }


    public override void OnJoinedRoom()
    {
        UpdatePlayerCount();
       
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
        UpdatePlayerCount();

        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            PhotonNetwork.LoadLevel(gameScene);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerCount();
    }
    private void UpdatePlayerCount()
    {
        loadingPanel.gameObject.SetActive(true);
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            loadingPepleImg[i].color = Color.black;
        }
        metchingCurPlyaerText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}";
    }

    
    float time = 0f;

    void Update()
    {
        time += Time.deltaTime;
        if (loadingPanel.gameObject.activeSelf)
        {
            loadingImg.transform.Rotate(new Vector3(0, 0, 80f * Time.deltaTime));
            if (time > 1f) metchingSecText.text = ((int)time).ToString();
        }
        else
        {
            time = 0;
        }

        //loadingImg
    }

    public void OnClick_MatchPanel()
    {
        if (!matchButtonPanel.gameObject.activeSelf)
        {
            matchButtonPanel.gameObject.SetActive(true);
        }
        else
        {
            matchButtonPanel.gameObject.SetActive(false);
        }
    }

    public void OnClick_OnOff_SettingPanel()
    {
        if (!settingsPanel.gameObject.activeSelf)
        {
            settingsPanel.gameObject.SetActive(true);
        }
        else
        {
            settingsPanel.gameObject.SetActive(false);
        }
    }



}
