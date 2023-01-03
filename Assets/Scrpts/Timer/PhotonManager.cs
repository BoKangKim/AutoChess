using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks // 내부 함수를 사용할 때 선언
{

    enum MyScene
    {
        Lobby,
        Loading,
        Ingame
    }

    [SerializeField] TextMeshProUGUI InputNickName;
    [SerializeField] TextMeshProUGUI StateText;
    [SerializeField] Button StartButton;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        PhotonNetwork.NickName = InputNickName.text;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Update()
    {
        // 현재 씬이 로비 씬일 때만 해당 텍스트 표시
        if (SceneManagerHelper.ActiveSceneBuildIndex == (int)MyScene.Lobby)
        {
            StateText.text = PhotonNetwork.NetworkClientState.ToString();
        }
    }

    public override void OnConnectedToMaster() // 서버 연결
    {
        Debug.Log("Connect Success");
    }
    public void JoinRoom() // 방 접속 & 2명 이하일 때 대기 상태
    {
        PhotonNetwork.JoinOrCreateRoom("A", new RoomOptions { MaxPlayers = 4 }, null);

    }
    public override void OnJoinedRoom() // 방 접속중
    {
        PhotonNetwork.LoadLevel((int)MyScene.Loading);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer) // 방에 이미 들어왔을 때
    {
        // 현재 방에 플레이어가 2명 이상일 때
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            PhotonNetwork.LoadLevel((int)MyScene.Ingame);
        }
    }

}
