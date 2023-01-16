using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("LoginPanel")]
    public Image logingPanel;
    public TMP_InputField nickNameField;

    [Header("LobbyPanel")]
    public Image lobbyPanel;
    public Button matchPanelButton;
    public Image matchButtonPanel;
    public TextMeshProUGUI myNickName;
    public Image userIcon;

    public Image settingsPanel;
    public Button settingsCloseButton;
    public Button settingsPanelButton;


    public TextMeshProUGUI statusText;
    

    private void Awake()
    {
        
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();
    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();



    public override void OnJoinedLobby()
    {
        lobbyPanel.gameObject.SetActive(true);
        logingPanel.gameObject.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = nickNameField.text;
    }




    void Update()
    {
        statusText.text = PhotonNetwork.NetworkClientState.ToString();
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
