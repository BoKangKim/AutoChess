
//유저의 정보 가져오기
//sessionID가 유효한지 여부를 인증
//현재 Odin이 실행중인지 확인, Osiris Multi Wallet을 통해 Odin에 연결하도록 함
//(a)

[System.Serializable]
public class Res_UserProfile
{
    public string Status;
    public string StatusCode;
    public string Message;
    [System.Serializable]
    public class UserProfie
    {
        public string referral_by;
        public string referral_code;
        public string username;
        public string email_id;
        public string public_address;
        public string _id;
        public string upline;
    }

    public UserProfie userProfile;

    public override string ToString()
    {
        return $"Status : {Status} StatusCode: {StatusCode} Message : {Message}";
    }
}

//----------------------------------------

[System.Serializable]
public class Res_dummyTournamentPool
{
    public string status;
    public string message;
    [System.Serializable]
    public class Data
    {
        public string redirectionURL;
        public Records[] records;

        public override string ToString()
        {
            string reString = null;
            for (int i = 0; i < records.Length; i++)
            {
                reString += records[i].ToString() + "\n";
            }

            return $"redirectionURL : { redirectionURL } records : {reString}";
        }
    }

    public Data data;

    public override string ToString()
    {
        return $"Status : {status} Message : {message} data : {data.ToString()}";
    }
}
//------------------------------------------------
[System.Serializable]
public class Records
{
    public string _id;
    public string title;
    public int amount;
    public int totalollectedAmount;
    public string startTime;
    public string endTime;
    public string createdAt;
    public string updatedAt;
    public int __v;

    public override string ToString()
    {
        return $"_id : {_id} title : {title} amount : {amount} totalollectedAmount : {totalollectedAmount} startTime : {startTime} endTime : {endTime} createdAt : {createdAt} updatedAt : {updatedAt} __v : {__v} ";
    }
}
//-------------------------------------------------


//유저의 sessionID 가져오기 
//사용자가 Osiris Multi Wallet 로그인 시마다 생성되는 새 ID를 받아옴 
//Osiris Multi Wallet 에 연결되어 있으므로 /getsessionid API로 sessionId 받아옴
//(b)
[System.Serializable]
public class Res_UserSessionID
{
    //응답 결과
    public string Status;
    public string StatusCode;
    public string Message;
    //받아온 유저의 session ID
    public string sessionId;

    public override string ToString()
    {
        return $"Status : {Status} StatusCode : {StatusCode} Message : {Message}";
    }
}
//제라 잔고
//http://localhost:8546/api/getsessionid API 응답을 사용하여  
//Osiris Multi Wallet과 연결해 Zera자금을 볼 수 있다.
//(c)
[System.Serializable]
public class Res_ZeraBalance
{
    //응답 결과 
    public string message;

    [System.Serializable]
    public class Data
    {
        public int balance;
    }
    public Data data;

    public override string ToString()
    {
        return $"message : {message} Balance : {data.balance}";
    }
}

//배팅 세팅
//이 API는 응용 프로그램의 현재 배팅 설정을 가져온다.
//배팅 설정은 배열에 표시되며 개발자는 필요한 만큼 베팅 설정을 가질 수 있습니다 
//배팅 설정 API에서 받은 Bet ID는 
//place-bet API Response Body, {bet_id:< _id >}에서 사용하여 베팅 조건을 생성할 수 있습니다
//(g)
[System.Serializable]
public class Res_BettingSetting
{
    //응답
    public string message;
    [System.Serializable]
    public class Setting
    {
        public string _id;
        public string game_id;
        public string betting;
        public string maintenance;
        public string createdAt;
        public string updatedAt;
        public int __v;

        public override string ToString()
        {
            return $"_id : {_id} game_id : {game_id} betting : {betting} maintenance : {maintenance} createdAt : {createdAt} updatedAt : {updatedAt} __v : {__v}";
        }
    }

    [System.Serializable]
    public class Bets
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
            return $"_id : {_id} game_id : {game_id} amount : {amount} platform_fee : {platform_fee} developer_fee : {developer_fee} win_reward : {win_reward} win_amount : {win_amount} createdAt : {createdAt} updatedAt : {updatedAt} __v : {__v}";
        }
    }

    [System.Serializable]
    public class Data
    {
        public Setting setting;
        public Bets[] bets;

        public override string ToString()
        {
            string reString = setting.ToString() + "\n";
            for (int i = 0; i < bets.Length; i++)
            {
                reString += bets[i].ToString() + "\n";
            }

            return reString;
        }
    }
    public Data data;
    public override string ToString()
    {
        return $"message : {message} \n data : {data.ToString()}";
    }
}

//베팅 초기화 최대 6인 (1:1:1~~)지원
// /place-bet API의  Response Body에 내기를 건 플레이어의 모든 sessionId를 배치한다.
// http://localhost:8546/api/getsessionid 로 호출 되는 sessionID를 /place-bet API Response Body 의
// players_session_id: [sessionId의 배열]에 배치합니다.
//(h)
//요청 바디
public class Req_Initialize
{
    public string[] players_session_id;
    public string bet_id;
}
//응답
public class Res_Initialize
{
    public string message;
    [System.Serializable]
    public class Data
    {
        public string betting_id;
    }
    public Data data;
}
public class Res_balance
{
    public string message;
}

//베팅의 승자를 선언하려면 승자가 이미 베팅을 한 경우에만 호출가능  
//h.<Base URL>/v1/betting/<coin>/place-bet 로 호출되는 betting ID를
// /declare-winner API Response Body 의 betting_id: < betting_id >에 배치할 수 있다.
//(i)
//요청
public class Req_BettingWinner
{
    public string betting_id;
    public string winner_player_id;
    public object match_details;
}
//응답
public class Res_BettingWinner
{
    public string message;
    [System.Serializable]
    public class Data
    {
        public int amount_won;
    }
    public Data data;
}

//연결 해제가 허용되는 경우 플레이어의 베팅을 해당 지갑으로 반환하려면 
//베팅이 이미/place-bet API 를 사용하여 배치된 경우에만 호출할 수 있습니다.
// /declare-winner API 와 같이 /disconnect API Response Body 에 같은 betting_id 을 배치할 수 있습니다.
//(j)
//요청
public class Req_BettingDisconnect
{
    public string betting_id;
}
//응답
public class Res_BettingDisconnect
{
    public string message;
    public class Data
    {
    }
    public Data data;
}