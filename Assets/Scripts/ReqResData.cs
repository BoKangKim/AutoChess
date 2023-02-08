//--------------------------------------------------------
//
// ���� ���� ���� ������	//
//

[System.Serializable]
public class Res_GetUserProfile
{
	// ���� ���
	//
	public string Status;
	public int StatusCode;
	public string Message;



	// ���� ����
	[System.Serializable]
	public class UserProfile
	{
		public string referral_by;
		public string referral_code;
		public string username;
		public string email_id;
		public string public_address;
		public string _id;
		public string upline;
	}
	public UserProfile userProfile;


	public override string ToString()
	{
		return $"Status :{Status} StatusCode:{StatusCode} Message:{Message}";
	}
}

//--------------------------------------------------------
//
// ���� ������ ���� ID ���� ������
//
[System.Serializable]
public class Res_GetSessionID
{
	// ���� ���
	//
	public string Status;
	public int StatusCode;
	public string Message;

	// ������ Session ID
	public string sessionId;

	public override string ToString()
	{
		return $"Status :{Status} StatusCode:{StatusCode} Message:{Message}";
	}
}


//--------------------------------------------------------
//
// �� ���� ���ܰ� Ȯ��
[System.Serializable]
public class Res_BalanceInfo
{
	// ���� ���
	public string message;  // 
	[System.Serializable]
	public class Balance
	{
		public int balance;
	}
	public Balance data;

	public override string ToString()
	{
		return "ZERA : " + data.balance;
	}
}


//--------------------------------------------------------
//
// Settings
// ������� ���õ� ���� ����
[System.Serializable]
public class Res_Settings
{
	// ���� ���
	public string message;  // 
	[System.Serializable]
	public class Settings
	{
		public string _id;
		public string game_id;
		public bool betting;
		public bool maintenance;
		public string createdAt;
		public string updatedAt;
		public int __v;

		public override string ToString()
		{
			return $"_id: {_id} game_id: {game_id} betting: {betting} maintenance: {maintenance}  createdAt: {createdAt}  updatedAt: {updatedAt}  __v: {__v} ";
		}
	}

	[System.Serializable]
	public class BetInfo
	{
		public string _id;
		public string game_id;
		public int amount;
		public int platform_fee;
		public int developer_fee;
		public int win_reward;
		public int win_amount;
		public string createdAt;
		public string updatedAt;
		public int __v;
		public override string ToString()
		{
			return $"_id: {_id} game_id: {game_id} amount: {amount} platform_fee: {platform_fee}  developer_fee: {developer_fee}  win_reward: {win_reward}  win_amount: {win_amount} createdAt: {createdAt} updatedAt: {updatedAt} __v: {__v} ";
		}
	}

	[System.Serializable]
	public class Data
	{
		public Settings settings;
		public BetInfo[] bets;

		public override string ToString()
		{
			string retStr = settings.ToString() + "\n";
			for (int i = 0; i < bets.Length; ++i)
				retStr += bets[i].ToString() + "\n";

			return retStr;
		}
	}
	public Data data;

	public override string ToString()
	{
		return $"message:{message}\n data {data.ToString()}";
	}
}


//--------------------------------------------------------
//
// ���� �����ϱ�
//

//
//
// Request Place bet
public class ReqBettingPlaceBet
{
	public string[] players_session_id;
	public string bet_id;
}

// Response Place Bet
public class ResBettingPlaceBet
{
	public string message;

	[System.Serializable]
	public class Data
	{
		public string betting_id;
	}
	public Data data;
}


//--------------------------------------------------------
//
// �������� ȹ���ϰ� �� ����
//

//
//
// Request Declare Winner
public class ReqBettingDeclareWinner
{
	public string betting_id;
	public string winner_player_id;
	public object match_details;
}

// Response Declare Winner
[System.Serializable]
public class ResBettingDeclareWinner
{
	public string message;
	[System.Serializable]
	public class Data
	{
		public int amount_won;
	}
	public Data data;
}

//--------------------------------------------------------
//
// ���õ� ���� ��ȯ
//


// Request Disconnect
public class ReqBettingDisconnect
{
	public string betting_id;
}

// Response Disconnect
public class ResBettingDisconnect
{
	public string message;
	public class Data
	{
	}
	public Data data;

	public override string ToString()
	{
		return $"message:{message}\n data {data.ToString()}";
	}
}