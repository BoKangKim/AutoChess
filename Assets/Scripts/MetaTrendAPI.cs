using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using TMPro;

public class MetaTrendAPI : MonoBehaviour
{
	[SerializeField] TMP_InputField txtInputField;
	[SerializeField] string selectedBettingID;

	[Header("[등록된 프로젝트에서 획득가능한 API 키]")]
	[SerializeField] string API_KEY = "";

	[Header("[Betting Backend Base URL]")]
	[SerializeField] string FullAppsProductionURL = "https://odin-api.browseosiris.com";
	[SerializeField] string FullAppsStagingURL = "https://odin-api-sat.browseosiris.com";


	string path = "C:/Users/User/AppData/Local/Osiris-SAT/app_launcher.exe";
	string SetupURL = "https://www.browseosiris.com/";

    private void Awake()
    {
		DontDestroyOnLoad(this);

	}

	string getBaseURL()
	{
		// 프로덕션 단계라면
		//return FullAppsProductionURL;

		// 스테이징 단계(개발)라면
		return FullAppsStagingURL;
	}
	


	public Res_UserProfile res_UserProfile = null;
	Res_UserSessionID res_UserSessionID = null;
	Res_BettingSetting res_BettingSetting = null;
	Res_dummyTournamentPool res_DummyTournamentPool = null;

	
    private void Start()
    {
		GetUserProfile();
		GetdummyTournamentPool();
	}

    //---------------
    // 유저 정보
    public void GetUserProfile()
	{
		StartCoroutine(processRequestGetUserInfo());
	}
	IEnumerator processRequestGetUserInfo()
	{
		// 유저 정보
		yield return requestGetUserInfo((response) =>
		{
			if (response != null)
			{
				//Debug.Log("## " + response.ToString());
				res_UserProfile = response;
				//Debug.Log("" + res_UserProfile.userProfile.username);
				Database.Instance.userInfo.public_address = res_UserProfile.userProfile.public_address;
				Database.Instance.userInfo.username = res_UserProfile.userProfile.username;
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
	delegate void resCallback_GetUserInfo(Res_UserProfile response);
	IEnumerator requestGetUserInfo(resCallback_GetUserInfo callback)
	{
		// get user profile
		UnityWebRequest www = UnityWebRequest.Get("http://localhost:8544/api/getuserprofile");
		yield return www.SendWebRequest();
		//Debug.Log(www.downloadHandler.text);
		//txtInputField.text = www.downloadHandler.text;
		Res_UserProfile res_getUserProfile = JsonUtility.FromJson<Res_UserProfile>(www.downloadHandler.text);
		callback(res_getUserProfile);
	}

	//---------------
	// dummyTournamentPool
	public void GetdummyTournamentPool()
	{
		StartCoroutine(processRequestGetdummyTournamentPool());
	}
	IEnumerator processRequestGetdummyTournamentPool()
	{
		// 유저 정보
		yield return requestGetdummyTournamentPool((response) =>
		{
			if (response != null)
			{
				//Debug.Log("## " + response.ToString());
				res_DummyTournamentPool = response;
				//Debug.Log("" + res_DummyTournamentPool);
			}
		});
	}
	delegate void resCallback_GetdummyTournamentPool(Res_dummyTournamentPool response);
	IEnumerator requestGetdummyTournamentPool(resCallback_GetdummyTournamentPool callback)
	{
		// get user profile
		UnityWebRequest www = UnityWebRequest.Get("https://odin-api-uat.browseosiris.com/v1/dummy-tournament-pool/get-data");
		yield return www.SendWebRequest();
		//Debug.Log(www.downloadHandler.text);
		//txtInputField.text = www.downloadHandler.text;
		Res_dummyTournamentPool res_dummyTournamentPool = JsonUtility.FromJson<Res_dummyTournamentPool>(www.downloadHandler.text);
		callback(res_dummyTournamentPool);
	}
    //---------------

    #region bettingAPI
    //---------------
    // Session ID
    public void GetSessionID()
	{
		StartCoroutine(processRequestGetSessionID());
	}
	IEnumerator processRequestGetSessionID()
	{
		// 유저 정보
		yield return requestGetSessionID((response) =>
		{
			if (response != null)
			{
				Debug.Log("## " + response.ToString());
				res_UserSessionID = response;
			}
		});
	}
	delegate void resCallback_GetSessionID(Res_UserSessionID response);
	IEnumerator requestGetSessionID(resCallback_GetSessionID callback)
	{
		// get session id
		UnityWebRequest www = UnityWebRequest.Get("http://localhost:8546/api/getsessionid");
		yield return www.SendWebRequest();
		Debug.Log(www.downloadHandler.text);
		//txtInputField.text = www.downloadHandler.text;
		Res_UserSessionID res_getSessionID = JsonUtility.FromJson<Res_UserSessionID>(www.downloadHandler.text);
		callback(res_getSessionID);
	}
	string coin_name;
	string wallet_address;
	int amount_in_integers;
	int balance;
	
	public float GetZera() { return balance; }
	//해당 코인을 얻습니다.
	public void GetCoin(int coinValue)
	{
		coin_name = "zera";
		amount_in_integers = coinValue;
		Debug.Log(coin_name);
		Debug.Log(wallet_address);
		Debug.Log(amount_in_integers);
		StartCoroutine(processRequestGetCoin());
	}
	IEnumerator processRequestGetCoin()
	{
		// 유저 정보
		yield return requestGetCoin((response) =>
		{
			if (response != null)
			{
				Debug.Log("## " + response.ToString());
				res_UserSessionID = response;
			}
		});
	}
	delegate void resCallback_GetCoin(Res_UserSessionID response);
	IEnumerator requestGetCoin(resCallback_GetCoin callback)
	{
		// get session id
		UnityWebRequest www = UnityWebRequest.Get($"https://dappx-api-sat.dappstore.me/users/{coin_name}/faucet?wallet={wallet_address}&amount={amount_in_integers}");
		ZeraBalance();
		yield return www.SendWebRequest();
		Debug.Log(www.downloadHandler.text);
	}

	//---------------
	// 베팅관련 셋팅 정보를 얻어오기
	public void Settings()
	{
		StartCoroutine(processRequestSettings());
	}
	IEnumerator processRequestSettings()
	{
		yield return requestSettings((response) =>
		{
			if (response != null)
			{
				Debug.Log("## Settings : " + response.ToString());
				res_BettingSetting = response;
				Debug.Log(res_BettingSetting);
			}
		});
	}
	delegate void resCallback_Settings(Res_BettingSetting response);
	IEnumerator requestSettings(resCallback_Settings callback)
	{
		string url = getBaseURL() + "/v1/betting/settings";


		UnityWebRequest www = UnityWebRequest.Get(url);
		www.SetRequestHeader("api-key", API_KEY);
		yield return www.SendWebRequest();
		Debug.Log(www.downloadHandler.text);
		txtInputField.text = www.downloadHandler.text;
		Res_BettingSetting res = JsonUtility.FromJson<Res_BettingSetting>(www.downloadHandler.text);
		callback(res);
		//UnityWebRequest www = new UnityWebRequest(URL);
	}

	//---------------
	// Zera 잔고 확인
	public void ZeraBalance()
	{
		StartCoroutine(processRequestZeraBalance());
	}
	IEnumerator processRequestZeraBalance()
	{
		yield return requestZeraBalance(res_UserSessionID.sessionId, (response) =>
		{
			if (response != null)
			{
				Debug.Log("## Response Zera Balance : " + response.ToString());
			}
		});
	}
	delegate void resCallback_BalanceInfo(Res_ZeraBalance response);
	IEnumerator requestZeraBalance(string sessionID, resCallback_BalanceInfo callback)
	{
		string url = getBaseURL() + ("/v1/betting/" + "zera" + "/balance/" + sessionID);

		UnityWebRequest www = UnityWebRequest.Get(url);
		www.SetRequestHeader("api-key", API_KEY);
		yield return www.SendWebRequest();
		Debug.Log(www.downloadHandler.text);
		//txtInputField.text = www.downloadHandler.text;

		Res_ZeraBalance res = JsonUtility.FromJson<Res_ZeraBalance>(www.downloadHandler.text);
		callback(res);

		string[] Value = res.ToString().Split("message : success Balance : ");
		balance = int.Parse(Value[1]);
		//UnityWebRequest www = new UnityWebRequest(URL);
	}

	//---------------
	// ZERA 베팅
	public void Betting_Zera()
	{
		StartCoroutine(processRequestBetting_Zera());
	}
	IEnumerator processRequestBetting_Zera()
	{
		Res_Initialize resBettingPlaceBet = null;
		Req_Initialize reqBettingPlaceBet = new Req_Initialize();
		reqBettingPlaceBet.players_session_id = new string[] { res_UserSessionID.sessionId };
		reqBettingPlaceBet.bet_id = selectedBettingID;// resSettigns.data.bets[0]._id;
		yield return requestCoinPlaceBet(reqBettingPlaceBet, (response) =>
		{
			if (response != null)
			{
				Debug.Log("## CoinPlaceBet : " + response.message);
				resBettingPlaceBet = response;
			}
		});
	}
	delegate void resCallback_BettingPlaceBet(Res_Initialize response);
	IEnumerator requestCoinPlaceBet(Req_Initialize req, resCallback_BettingPlaceBet callback)
	{
		string url = getBaseURL() + "/v1/betting/" + "zera" + "/place-bet";

		string reqJsonData = JsonUtility.ToJson(req);
		Debug.Log(reqJsonData);


		UnityWebRequest www = UnityWebRequest.Post(url, reqJsonData);
		byte[] buff = System.Text.Encoding.UTF8.GetBytes(reqJsonData);
		www.uploadHandler = new UploadHandlerRaw(buff);
		www.SetRequestHeader("api-key", API_KEY);
		www.SetRequestHeader("Content-Type", "application/json");
		yield return www.SendWebRequest();

		Debug.Log(www.downloadHandler.text);
		txtInputField.text = www.downloadHandler.text;
		Res_Initialize res = JsonUtility.FromJson<Res_Initialize>(www.downloadHandler.text);
		callback(res);
	}

	//---------------
	// ZERA 베팅-승자
	public void Betting_Zera_DeclareWinner()
	{
		StartCoroutine(processRequestBetting_Zera_DeclareWinner());
	}
	IEnumerator processRequestBetting_Zera_DeclareWinner()
	{
		Res_BettingWinner resBettingDeclareWinner = null;
		Req_BettingWinner reqBettingDeclareWinner = new Req_BettingWinner();
		reqBettingDeclareWinner.betting_id = selectedBettingID;// resSettigns.data.bets[0]._id;
		reqBettingDeclareWinner.winner_player_id = res_UserProfile.userProfile._id;
		yield return requestCoinDeclareWinner(reqBettingDeclareWinner, (response) =>
		{
			if (response != null)
			{
				Debug.Log("## CoinDeclareWinner : " + response.message);
				resBettingDeclareWinner = response;
			}
		});
	}
	delegate void resCallback_BettingDeclareWinner(Res_BettingWinner response);
	IEnumerator requestCoinDeclareWinner(Req_BettingWinner req, resCallback_BettingDeclareWinner callback)
	{
		string url = getBaseURL() + "/v1/betting/" + "zera" + "/declare-winner";

		string reqJsonData = JsonUtility.ToJson(req);
		Debug.Log(reqJsonData);


		UnityWebRequest www = UnityWebRequest.Post(url, reqJsonData);
		byte[] buff = System.Text.Encoding.UTF8.GetBytes(reqJsonData);
		www.uploadHandler = new UploadHandlerRaw(buff);
		www.SetRequestHeader("api-key", API_KEY);
		www.SetRequestHeader("Content-Type", "application/json");
		yield return www.SendWebRequest();

		Debug.Log(www.downloadHandler.text);
		txtInputField.text = www.downloadHandler.text;
		Res_BettingWinner res = JsonUtility.FromJson<Res_BettingWinner>(www.downloadHandler.text);
		callback(res);
	}

	//---------------
	// 베팅금액 반환
	public void Betting_Zera_Disconnect()
	{
		StartCoroutine(processRequestBetting_Zera_Disconnect());
	}
	IEnumerator processRequestBetting_Zera_Disconnect()
	{
		Res_BettingDisconnect resBettingDisconnect = null;
		Req_BettingDisconnect reqBettingDisconnect = new Req_BettingDisconnect();
		reqBettingDisconnect.betting_id = selectedBettingID;// resSettigns.data.bets[1]._id;
		yield return requestCoinDisconnect(reqBettingDisconnect, (response) =>
		{
			if (response != null)
			{
				Debug.Log("## CoinDisconnect : " + response.message);
				resBettingDisconnect = response;
			}
		});
	}
	delegate void resCallback_BettingDisconnect(Res_BettingDisconnect response);
	IEnumerator requestCoinDisconnect(Req_BettingDisconnect req, resCallback_BettingDisconnect callback)
	{
		string url = getBaseURL() + "/v1/betting/" + "zera" + "/disconnect";

		string reqJsonData = JsonUtility.ToJson(req);
		Debug.Log(reqJsonData);


		UnityWebRequest www = UnityWebRequest.Post(url, reqJsonData);
		byte[] buff = System.Text.Encoding.UTF8.GetBytes(reqJsonData);
		www.uploadHandler = new UploadHandlerRaw(buff);
		www.SetRequestHeader("api-key", API_KEY);
		www.SetRequestHeader("Content-Type", "application/json");
		yield return www.SendWebRequest();

		Debug.Log(www.downloadHandler.text);
		txtInputField.text = www.downloadHandler.text;
		Res_BettingDisconnect res = JsonUtility.FromJson<Res_BettingDisconnect>(www.downloadHandler.text);
		callback(res);
	}
}
#endregion