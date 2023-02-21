
//������ ���� ��������
//sessionID�� ��ȿ���� ���θ� ����
//���� Odin�� ���������� Ȯ��, Osiris Multi Wallet�� ���� Odin�� �����ϵ��� ��
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


//������ sessionID �������� 
//����ڰ� Osiris Multi Wallet �α��� �ø��� �����Ǵ� �� ID�� �޾ƿ� 
//Osiris Multi Wallet �� ����Ǿ� �����Ƿ� /getsessionid API�� sessionId �޾ƿ�
//(b)
[System.Serializable]
public class Res_UserSessionID
{
    //���� ���
    public string Status;
    public string StatusCode;
    public string Message;
    //�޾ƿ� ������ session ID
    public string sessionId;

    public override string ToString()
    {
        return $"Status : {Status} StatusCode : {StatusCode} Message : {Message}";
    }
}
//���� �ܰ�
//http://localhost:8546/api/getsessionid API ������ ����Ͽ�  
//Osiris Multi Wallet�� ������ Zera�ڱ��� �� �� �ִ�.
//(c)
[System.Serializable]
public class Res_ZeraBalance
{
    //���� ��� 
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

//���� ����
//�� API�� ���� ���α׷��� ���� ���� ������ �����´�.
//���� ������ �迭�� ǥ�õǸ� �����ڴ� �ʿ��� ��ŭ ���� ������ ���� �� �ֽ��ϴ� 
//���� ���� API���� ���� Bet ID�� 
//place-bet API Response Body, {bet_id:< _id >}���� ����Ͽ� ���� ������ ������ �� �ֽ��ϴ�
//(g)
[System.Serializable]
public class Res_BettingSetting
{
    //����
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

//���� �ʱ�ȭ �ִ� 6�� (1:1:1~~)����
// /place-bet API��  Response Body�� ���⸦ �� �÷��̾��� ��� sessionId�� ��ġ�Ѵ�.
// http://localhost:8546/api/getsessionid �� ȣ�� �Ǵ� sessionID�� /place-bet API Response Body ��
// players_session_id: [sessionId�� �迭]�� ��ġ�մϴ�.
//(h)
//��û �ٵ�
public class Req_Initialize
{
    public string[] players_session_id;
    public string bet_id;
}
//����
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

//������ ���ڸ� �����Ϸ��� ���ڰ� �̹� ������ �� ��쿡�� ȣ�Ⱑ��  
//h.<Base URL>/v1/betting/<coin>/place-bet �� ȣ��Ǵ� betting ID��
// /declare-winner API Response Body �� betting_id: < betting_id >�� ��ġ�� �� �ִ�.
//(i)
//��û
public class Req_BettingWinner
{
    public string betting_id;
    public string winner_player_id;
    public object match_details;
}
//����
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

//���� ������ ���Ǵ� ��� �÷��̾��� ������ �ش� �������� ��ȯ�Ϸ��� 
//������ �̹�/place-bet API �� ����Ͽ� ��ġ�� ��쿡�� ȣ���� �� �ֽ��ϴ�.
// /declare-winner API �� ���� /disconnect API Response Body �� ���� betting_id �� ��ġ�� �� �ֽ��ϴ�.
//(j)
//��û
public class Req_BettingDisconnect
{
    public string betting_id;
}
//����
public class Res_BettingDisconnect
{
    public string message;
    public class Data
    {
    }
    public Data data;
}