using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks // ���� �Լ��� ����� �� ����
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
        // ���� ���� �κ� ���� ���� �ش� �ؽ�Ʈ ǥ��
        if (SceneManagerHelper.ActiveSceneBuildIndex == (int)MyScene.Lobby)
        {
            StateText.text = PhotonNetwork.NetworkClientState.ToString();
        }
    }

    public override void OnConnectedToMaster() // ���� ����
    {
        Debug.Log("Connect Success");
    }
    public void JoinRoom() // �� ���� & 2�� ������ �� ��� ����
    {
        PhotonNetwork.JoinOrCreateRoom("A", new RoomOptions { MaxPlayers = 4 }, null);

    }
    public override void OnJoinedRoom() // �� ������
    {
        PhotonNetwork.LoadLevel((int)MyScene.Loading);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer) // �濡 �̹� ������ ��
    {
        // ���� �濡 �÷��̾ 2�� �̻��� ��
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            PhotonNetwork.LoadLevel((int)MyScene.Ingame);
        }
    }

}
