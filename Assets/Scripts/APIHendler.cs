using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
enum MODE
{
    TEST,
    PRODUCTION,
    MAX
}

public class APIHandler : MonoBehaviour
{
    #region SingleTon
    private static APIHandler Instance = null;

    public static APIHandler Inst
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<APIHandler>();
                if (Instance == null)
                    Instance = new GameObject("APIHandler").AddComponent<APIHandler>();
            }

            return Instance;
        }
    }
    #endregion

    // TEST 모드 일 때만 나중에 다운받아서 Path 확인 후 고쳐야 됨....

    readonly MODE mode = MODE.TEST;
    string path = "C:/Users/User/AppData/Local/Osiris-SAT/app_launcher.exe";
    string SetupURL = "";
    string FullAppsURL = "";
    // TomatoFestival Project API_KEY
    string API_KEY = "qSGCSgX53RhbhBBit2u97";

    // Respone 받을 클래스 변수들
    Res_GetUserProfile playerProfile = null;
    Res_GetSessionID sessionId = null;
    Res_Settings betSettings = null;
    // UI 표시를 위한 클래스
    StartUIManager uiManager = null;

    //상대방 정보
    string otherPlayerName = null;
    string otherPlayerSessionId = null;

    //Betting 정보
    string betting_id = null;

    // WinAmount
    int amount_won = 0;
    int loseCoin = 0;

    public void InitSetValues()
    {
        uiManager = null;
        otherPlayerName = null;
        otherPlayerSessionId = null;
        betting_id = null;
        amount_won = 0;
        loseCoin = 0;
    }

    string getBaseURL()
    {
        return FullAppsURL;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SetURL();
    }

    void SetURL()
    {
        if (mode == MODE.TEST)
        {
            FullAppsURL = "https://odin-api-sat.browseosiris.com";
            SetupURL = "https://osiris-v2-test.s3.ap-southeast-1.amazonaws.com/osirisR2/sat/Osiris+Setup-Staging+v2.2.2.53.exe";
        }
        else if (mode == MODE.PRODUCTION)
        {
            FullAppsURL = "https://odin-api.browseosiris.com";
            SetupURL = "https://www.browseosiris.com/";
        }
    }

    public void ClearOtherPlayerInfo()
    {
        otherPlayerName = null;
        otherPlayerSessionId = null;
        //resBettingPlaceBet = null;
    }

    public void SetOtherPlayerInfo(string profile, string sessionID)
    {
        this.otherPlayerName = profile;
        this.otherPlayerSessionId = sessionID;
    }

    public Res_GetUserProfile GetMyProfile()
    {
        return playerProfile;
    }

    public Res_GetSessionID GetMySessionID()
    {
        return sessionId;
    }

    public string GetBettingID()
    {
        return betting_id;
    }

    public void SetBettingID(string betting_id)
    {
        this.betting_id = betting_id;
    }

    public void SetUIManager(StartUIManager uIManager)
    {
        this.uiManager = uIManager;
    }

    public string GetOhterProfile()
    {
        return otherPlayerName;
    }

    public void GetUserProfile()
    {
        StartCoroutine(ResGetUserProfile());
    }

    public void GetZeraBalaneAndBetSettings()
    {
        StartCoroutine(ResGetBettingSettings());
    }

    public void ReqBetting()
    {
        StartCoroutine(ReqBettingZera());
    }

    public void BettingDisconnect()
    {
        StartCoroutine(ReqDisconect());
    }

    public void DeclareWinner(string winner_player_id, bool nomalShutdown)
    {
        StartCoroutine(ReqDeclareWinner(winner_player_id, nomalShutdown));
    }

    IEnumerator ResGetUserProfile()
    {
        yield return RequestUserProfile((response) =>
        {
            if (response != null)
            {
                playerProfile = response;
                StartCoroutine(ResGetSessionID());
                Debug.Log(playerProfile.ToString());
            }
            else
            {
                try
                {
                    System.Diagnostics.Process.Start(path);
                }
                catch
                {
                    Application.OpenURL(SetupURL);
                }
            }
        });
    }

    IEnumerator ResGetSessionID()
    {
        yield return RequestSessionID((response) =>
        {
            if (response != null)
            {
                sessionId = response;
                uiManager.SetStartPanel(playerProfile.userProfile.username, playerProfile.userProfile.email_id, true);
                Debug.Log(sessionId.ToString());
            }
            else
            {

                string Error = "Can't Response SessionID, Please Check Your DappX Wallet And Retry Start";
                uiManager.SetErrorPanel(Error);
            }
        });
    }

    IEnumerator ResGetZeraBalance()
    {
        yield return RequestZeraBalance(sessionId.sessionId, (response) => {
            if (response != null)
            {
                bool isPosibleStart = false;
                if (betSettings.data.bets[0].amount > response.data.balance)
                {
                    isPosibleStart = false;
                }
                else if (betSettings.data.bets[0].amount <= response.data.balance)
                {
                    isPosibleStart = true;
                }
                Debug.Log(response.data.balance + " Zera Balance");
                uiManager.SetBalancePanel(response.ToString(), betSettings.data.bets[0].amount.ToString(), isPosibleStart);
            }
            else
            {
                Debug.LogError("Can't Response Zera Balance");
            }
        });
    }

    IEnumerator ResGetBettingSettings()
    {
        yield return RequestBettingSettings((response) =>
        {
            if (response != null)
            {
                Debug.Log(response.ToString());
                betSettings = response;
                StartCoroutine(ResGetZeraBalance());
            }
            else
            {
                Debug.LogError("Can't Response Zera Balance");
            }
        });
    }

    IEnumerator ReqBettingZera()
    {
        ReqBettingPlaceBet reqBettingPlaceBet = new ReqBettingPlaceBet();
        reqBettingPlaceBet.players_session_id = new string[] { sessionId.sessionId, otherPlayerSessionId };
        Debug.Log(sessionId.sessionId + " 내 Session ID");
        Debug.Log(otherPlayerSessionId + " Other Session ID");
        reqBettingPlaceBet.bet_id = betSettings.data.bets[0]._id;
        yield return RequestCoinPlaceBet(reqBettingPlaceBet, (response) =>
        {
            if (response != null)
            {
                FindObjectOfType<UserInfo>().photonView.RPC("SetBettingID", Photon.Pun.RpcTarget.All, response.data.betting_id);
            }
        });
    }

    IEnumerator ReqDisconect()
    {
        ReqBettingDisconnect reqBettingDisconect = new ReqBettingDisconnect();
        reqBettingDisconect.betting_id = this.betting_id;
        yield return RequestBettingDisconnect(reqBettingDisconect, (response) =>
        {
            if (response != null)
            {
                Debug.Log("## CoinDisconnect : " + response.message);
                UserInfo info = FindObjectOfType<UserInfo>();
                if (info != null)
                {
                    info.photonView.RPC("CancleMatch", Photon.Pun.RpcTarget.All);
                }

                ClearOtherPlayerInfo();
            }
            else
            {
                Debug.LogError("Can't CoinDisconnect Check JsonData");
            }
        });
    }

    IEnumerator ReqDeclareWinner(string winner_Player_id, bool nomalShutdown)
    {
        ReqBettingDeclareWinner reqBettingDeclareWinner = new ReqBettingDeclareWinner();
        reqBettingDeclareWinner.betting_id = this.betting_id;
        reqBettingDeclareWinner.winner_player_id = winner_Player_id;
        yield return RequestDeclareWinner(reqBettingDeclareWinner, (response) =>
        {
            if (response != null)
            {
                Debug.Log("## DeclareWinner Message : " + response.message);
                this.amount_won = response.data.amount_won;
                this.loseCoin = amount_won - betSettings.data.bets[0].amount;

                GameOver go = FindObjectOfType<GameOver>();
                if (nomalShutdown == true)
                {
                    go.photonView.RPC("SetEndGame", Photon.Pun.RpcTarget.All, amount_won.ToString(), loseCoin.ToString());
                }
                else
                {
                    go.LeftOnePlayerWin(amount_won.ToString());
                }
            }
            else
            {
                Debug.LogError("Can't DeclareWinner Check JsonData");
            }
        });

    }

    // 유저 프로필 가져오기
    delegate void CallBackUserProfile(Res_GetUserProfile response);
    IEnumerator RequestUserProfile(CallBackUserProfile callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8546/api/getuserprofile"))
        {
            yield return www.SendWebRequest();
            Res_GetUserProfile resPlayerProfile = JsonUtility.FromJson<Res_GetUserProfile>(www.downloadHandler.text);
            callback(resPlayerProfile);
        }
    }

    // 세션아이디 가져오기
    delegate void CallBackSessionID(Res_GetSessionID response);
    IEnumerator RequestSessionID(CallBackSessionID callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8546/api/getsessionid "))
        {
            yield return www.SendWebRequest();
            Res_GetSessionID resSessionID = JsonUtility.FromJson<Res_GetSessionID>(www.downloadHandler.text);
            callback(resSessionID);
        }
    }


    // 요청한 플레어이의 남은 ZERA
    delegate void CallBackZeraBalance(Res_BalanceInfo response);
    IEnumerator RequestZeraBalance(string sessionID, CallBackZeraBalance callback)
    {
        string url = getBaseURL() + ("/v1/betting/" + "zera" + "/balance/" + sessionID);
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("api-key", API_KEY);
            yield return www.SendWebRequest();
            Res_BalanceInfo res = JsonUtility.FromJson<Res_BalanceInfo>(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
            callback(res);
        }
    }

    delegate void CallBackBettingSettings(Res_Settings response);
    IEnumerator RequestBettingSettings(CallBackBettingSettings callback)
    {
        string url = getBaseURL() + "/v1/betting/settings";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("api-key", API_KEY);
            yield return www.SendWebRequest();
            Res_Settings res = JsonUtility.FromJson<Res_Settings>(www.downloadHandler.text);
            callback(res);
        }
    }


    // 세션 아이디 
    // 유저 네임
    delegate void CallBackPlaceBet(ResBettingPlaceBet response);
    IEnumerator RequestCoinPlaceBet(ReqBettingPlaceBet req, CallBackPlaceBet callback)
    {

        string url = getBaseURL() + "/v1/betting/" + "zera" + "/place-bet";
        string reqJsonData = JsonUtility.ToJson(req);
        using (UnityWebRequest www = UnityWebRequest.Post(url, reqJsonData))
        {
            byte[] buff = System.Text.Encoding.UTF8.GetBytes(reqJsonData);
            www.uploadHandler = new UploadHandlerRaw(buff);
            www.SetRequestHeader("api-key", API_KEY);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            Debug.Log(JsonUtility.ToJson(JsonUtility.FromJson<ResBettingPlaceBet>(www.downloadHandler.text)));
            ResBettingPlaceBet res = JsonUtility.FromJson<ResBettingPlaceBet>(www.downloadHandler.text);
            callback(res);
            www.downloadHandler.Dispose();
            www.uploadHandler.Dispose();
            www.Dispose();
        }

    }

    //로딩씬 넘어가기 전에 나가면 돈돌려줄거
    delegate void CallBackDisconnect(ResBettingDisconnect response);
    IEnumerator RequestBettingDisconnect(ReqBettingDisconnect req, CallBackDisconnect callback)
    {
        string url = getBaseURL() + "/v1/betting/" + "zera" + "/disconnect";
        string reqJsonData = JsonUtility.ToJson(req);

        using (UnityWebRequest www = UnityWebRequest.Post(url, reqJsonData))
        {
            byte[] buff = System.Text.Encoding.UTF8.GetBytes(reqJsonData);
            www.uploadHandler = new UploadHandlerRaw(buff);
            www.SetRequestHeader("api-key", API_KEY);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            ResBettingDisconnect res = JsonUtility.FromJson<ResBettingDisconnect>(www.downloadHandler.text);
            callback(res);
            www.downloadHandler.Dispose();
            www.uploadHandler.Dispose();
            www.Dispose();
        }

    }

    delegate void CallBackDeclareWinner(ResBettingDeclareWinner response);
    IEnumerator RequestDeclareWinner(ReqBettingDeclareWinner req, CallBackDeclareWinner callback)
    {
        string url = getBaseURL() + "/v1/betting/" + "zera" + "/declare-winner";
        string reqJsonData = JsonUtility.ToJson(req);

        using (UnityWebRequest www = UnityWebRequest.Post(url, reqJsonData))
        {
            byte[] buff = System.Text.Encoding.UTF8.GetBytes(reqJsonData);
            www.uploadHandler = new UploadHandlerRaw(buff);
            www.SetRequestHeader("api-key", API_KEY);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            ResBettingDeclareWinner res = JsonUtility.FromJson<ResBettingDeclareWinner>(www.downloadHandler.text);
            callback(res);
        }
    }

}