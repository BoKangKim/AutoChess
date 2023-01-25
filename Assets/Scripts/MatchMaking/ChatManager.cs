using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using TMPro;

public static class AppSettingExtensions
{
    public static ChatAppSettings GetChatSettings(this Photon.Realtime.AppSettings appSettings)
    {
        return new ChatAppSettings
        {
            AppIdChat = appSettings.AppIdChat,
            AppVersion = appSettings.AppVersion,
            FixedRegion = appSettings.IsBestRegion ? null : appSettings.FixedRegion,
            NetworkLogging = appSettings.NetworkLogging,
            Protocol = appSettings.Protocol,
            EnableProtocolFallback = appSettings.EnableProtocolFallback,
            Server = appSettings.IsDefaultNameServer ? null : appSettings.Server,
            Port = (ushort)appSettings.Port,
            ProxyServer = appSettings.ProxyServer,
        };
    }
}
public class ChatManager : MonoBehaviour, IChatClientListener
{
    private ChatClient _ChatClient = null;
    private string ChannelName = null;
    [SerializeField] private ScrollRect Scroll = null;
    [SerializeField] private TextMeshProUGUI ChatText = null;
    [SerializeField] private TMP_InputField ChatInput = null;

    private void Start()
    {
        if (PhotonNetwork.InLobby)
        {
            ChannelName = "Lobby";
        }
        else if (PhotonNetwork.InRoom)
        {
            ChannelName = PhotonNetwork.CurrentRoom.Name;
        }

        ChatText.text = "";
        if (_ChatClient == null)
        {
            _ChatClient = new ChatClient(this); // Instantiate
            _ChatClient.UseBackgroundWorkerForSending = true; // Use background sending?
            _ChatClient.AuthValues = new AuthenticationValues(PhotonNetwork.NickName); // Authorized User ID
            _ChatClient.ConnectUsingSettings(PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings()); // Connect
            ChatInput.onEndEdit.AddListener(delegate { EnterMessage(ChatInput.text); });

        }
    }
    private void OnDestroy()
    {
        if (_ChatClient != null)
        {
            _ChatClient.Disconnect();
        }
    }
    private void Update()
    {
        if (_ChatClient != null)
        {
            _ChatClient.Service();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChatInput.ActivateInputField();
        }
    }
    public void EnterMessage(string message)
    {
        if (message == "") return;

        _ChatClient.PublishMessage(ChannelName, message);

        ChatInput.text = "";
        Scroll.verticalNormalizedPosition = 0;
    }
    public void AddChatLine(string userName, string message)
    {
        ChatText.text += $"[{userName}] : {message}\n";
        Scroll.verticalNormalizedPosition = 0;
    }
    // Photon Chat Interface
    public void DebugReturn(DebugLevel level, string message)
    {
        // throw new System.NotImplementedExceptioncon
    }

    public void OnChatStateChange(ChatState state)
    {
        //throw new System.NotImplementedException();
        //AddChatLine("System", "Chatting Server State : " + state);
    }

    public void OnConnected()
    {
        //throw new System.NotImplementedException();
        _ChatClient.Subscribe(ChannelName, 0);
        AddChatLine("System", "Chatting Server Connected");
    }

    public void OnDisconnected()
    {
        //throw new System.NotImplementedException();
        AddChatLine("System", "Chatting Server DisConnected");
    }
    // Get Message Other Player sended
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        //Debug.Log("여기들어오니?");
        //throw new System.NotImplementedException();
        for (int i = 0; i < messages.Length; i++)
        {
           AddChatLine(senders[i], messages[i].ToString());
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        //throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        //throw new System.NotImplementedException();
    }
}