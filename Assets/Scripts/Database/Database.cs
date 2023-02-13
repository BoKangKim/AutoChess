using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using Newtonsoft.Json;


public class Database : MonoBehaviour
{
    private static Database _instance = null;
    public static Database Instance
    {
        get
        {
            if (_instance == null)
            {
                // find
                _instance = GameObject.FindObjectOfType<Database>();
                if (_instance == null)
                {
                    // Create
                    _instance = new GameObject("Database").GetComponent<Database>();
                }
            }
            return _instance;
        }

    // DataBase : Collection의 물리적 컨테이너
    // Collection : Document의 그룹, Document의 내부에 위치해 있음.
    // Document : 한개 이상의 Key-value 쌍으로 이루어진 구조 <BsonDocument>
    // Key / Field : 컬럼 명과 저장 값
    }

    #region Client 주소 설정
    MongoClient client = new MongoClient("mongodb+srv://mongo:mongo1234@autochesscluster.ogdgk4g.mongodb.net/?retryWrites=true&w=majority");
    #endregion

    //public TMP_InputField Input_PhoneNumber = null;
    //public TMP_InputField Input_NickName = null;
    //public TMP_InputField Input_Password = null;


    //public Image PhoneNumberPanel;
    //public Image NickNamePanel;

    public TextMeshProUGUI status = null;

    public IMongoDatabase database = null;
    public IMongoCollection<BsonDocument> collection;

    //Regex PhoneNumRegex;
    //Regex NickNameRegex;
    //Regex PasswordRegex;

    public UserInfo userInfo;

    [System.Serializable]
    public class UserInfo 
    {
        public string public_address;
        public string username;
        public int userIconIndex;
        public IDictionary<string, List<string>> unitInventory;

        public override string ToString()
        {
            return $"public_address : {public_address} username : {username} UserIconIndex : {userIconIndex} ";
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        //Input_PhoneNumber.characterLimit = 11;
        //Input_NickName.characterLimit = 12;
        //Input_PhoneNumber.contentType = TMP_InputField.ContentType.IntegerNumber;
        //Input_NickName.contentType = TMP_InputField.ContentType.Alphanumeric;
        //Input_Password.contentType = TMP_InputField.ContentType.Password;
        #region Database 연동
        // MongoDB database name
        database = client.GetDatabase("AutoChessDB");
        #endregion

        #region Collection 연동
        // 해당 Database에 있는 Collection 가져오기
        // MongoDB collection name
        collection = database.GetCollection<BsonDocument>("UserInfo");
        #endregion

        userInfo = new UserInfo();
        //
        //bsonarray에 콜렉션을 담아서 요소를 나눌수 있음.
        //BsonArray aa = DataFind("01013345678").GetValue("UnitInventory").AsBsonDocument.GetValue("Orc").AsBsonArray;

        //PhoneNumRegex = new Regex(@"010-[0-9]{4}-[0-9]{4}$");
        //PasswordRegex = new Regex(@"^(?=.*?[0-9]).{4,}$");

        //NickNameRegex = new Regex(@"^[0-9a-zA-Z]{2,12}$");


        //api에서 데이터를 받아왔을때 실행(일단은 db/ start에서 실행하게해놓음)
        GetUserInfo();
    }



    //---------------
    // 유저 정보
    public void GetUserInfo()
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
				Debug.Log("## " + response.ToString());
            }
		});
	}
	delegate void Callback_GetUserInfo(UserInfo response);
	IEnumerator requestGetUserInfo(Callback_GetUserInfo callback)
	{
        // get user profile
        //yield return WaitUntil(()=> userInfo.public_address != null) ;
        yield return new WaitUntil(() => userInfo.public_address != null); 
        //txtInputField.text = www.downloadHandler.text;

        BsonDocument userInfos = DataFind(userInfo.public_address);

        Debug.Log(userInfos);

        if (userInfos == null)
        {
            DataInst(userInfo.public_address);
            Debug.Log("생성되니?");
        }
        else
        {
            List<string> unitRace = new List<string>();
            unitRace.Add("Orc");
            unitRace.Add("Dwarf");
            unitRace.Add("Golem");
            unitRace.Add("Mecha");
            unitRace.Add("Demon");


            userInfo.public_address = userInfos.GetValue("public_address").ToString();
            userInfo.username = userInfos.GetValue("username").ToString();
            userInfo.userIconIndex = (int)userInfos.GetValue("userIconIndex");

            BsonDocument UnitInventory = userInfos.GetValue("unitInventory").AsBsonDocument;
            userInfo.unitInventory = new Dictionary<string, List<string>>();

            for (int i = 0; i < unitRace.Count; i++)
            {
                List<string> list = new List<string>();
                BsonArray array = UnitInventory.GetValue(unitRace[i]).AsBsonArray;
                for (int j = 0; j < array.Count; j++)
                {
                    list.Add(array.AsBsonArray[0].ToString());

                }
                userInfo.unitInventory.Add(unitRace[i], list);
            }

        }
		callback(userInfo);
	}

	//---------------




    //public void Input_Login()
    //{

    //    Match M_PhoneNumRegex = PhoneNumRegex.Match(Input_PhoneNumber.text);
    //    Match M_Password = PasswordRegex.Match(Input_Password.text);

    //    if (!M_PhoneNumRegex.Success || !M_Password.Success)
    //    {
    //        Debug.Log("아이디나 비밀번호의 자리수가 맞지 않음.");
    //        return;
    //    }


    //    BsonDocument UserInfo = DataFind(userInfo.PhoneNumber);

    //    if (UserInfo == null)
    //    {
    //        DataInst(Input_PhoneNumber.text, Input_Password.text);
    //        //닉네임 인풋 필드가 뜸
    //        PhoneNumberPanel.gameObject.SetActive(false);
    //        NickNamePanel.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        if (Input_Password.text != DetaFindFild("PhoneNumber", Input_PhoneNumber.text).ToString())
    //        {
    //            Debug.Log("비밀번호가 틀렸습니다.");
    //        }
    //        else
    //        {
    //            userInfo.PhoneNumber = UserInfo.GetValue("PhoneNumber").ToString();
    //            userInfo.NickName = UserInfo.GetValue("NickName").ToString();
    //            userInfo.PassWord = UserInfo.GetValue("PassWord").ToString();
    //            userInfo.UserIconIndex = (int)UserInfo.GetValue("UserIconIndex");
    //            userInfo.UnitInventory = (IDictionary<string, List<string>>)UserInfo.GetValue("UnitInventory");
    //        }

    //    }
    //}

    // 닉네임을 적고 닉네임을 생성하면 폰넘버데이터에 닉네임 추가
    //public void InputNickNameDataUpdate()
    //{

    //    Match M_NickNameRegex = NickNameRegex.Match(Input_Password.text);

    //    if (!M_NickNameRegex.Success)
    //    {
    //        Debug.Log("올바른 닉네임이 아닙니다. 다시입력하세요.");
    //        return;
    //    }
    //    else if (userInfo.NickName == null)
    //    {
    //        userInfo.NickName = Input_NickName.text;
    //        BsonDocument filter = new BsonDocument { { "PhoneNumber", userInfo.PhoneNumber } };
    //        UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("NickName", userInfo.NickName);
    //        collection.FindOneAndUpdate(filter, update);
    //    }

    //    //로비로 이동
    //}





    //------------------------------------------------------------------------------------------------------------------
    //도큐먼트 생성
    void DataInst(string address)
    {
        userInfo.public_address = address;
        userInfo.userIconIndex = 1;
        userInfo.unitInventory = new Dictionary<string, List<string>>();
        userInfo.unitInventory.Add("Orc", new List<string>() { "OrcWarrior", "OrcAssassin", "OrcWizard", "OrcTanker", "OrcRangeDealer" });
        userInfo.unitInventory.Add("Dwarf", new List<string>() { "DwarfWarrior", "DwarfAssassin", "DwarfWizard", "DwarfTanker", "DwarfRangeDealer" });
        userInfo.unitInventory.Add("Golem", new List<string>() { "GolemWarrior", "GolemAssassin", "GolemWizard", "GolemTanker", "GolemRangeDealer" });
        userInfo.unitInventory.Add("Mecha", new List<string>() { null, null, null, null, null });
        userInfo.unitInventory.Add("Demon", new List<string>() { null, null, null, null, null });

        collection.InsertOne(userInfo.ToBsonDocument());
    }

    //------------------------------------------------------------------------------------------------------------------
    //특정 필드로 도큐먼트 찾기
    BsonDocument DataFind(string address)
    {
        //FilterDefinition<BsonDocument> filter2 = Builders<BsonDocument>.Filter.Eq("id", findID);

        BsonDocument filter = new BsonDocument { { "public_address", address } };

        BsonDocument targetData = collection.Find(filter).FirstOrDefault();


        return targetData;
    }



    //------------------------------------------------------------------------------------------------------------------
    //특정 도큐먼트의 특정 필드 수정
    void DataUpdate(string findID,string updataData)
    {
        BsonDocument filter = new BsonDocument { { "PhoneNumber", findID } };
        
        UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("NickName", updataData);
        collection.FindOneAndUpdate(filter, update);
        //collection.UpdateOne(filter,update);
    }

    //------------------------------------------------------------------------------------------------------------------
    //특정 도큐먼트 삭제
    void DataDelete(string findID)
    {
        BsonDocument filter = new BsonDocument { { "id", findID } };
        collection.FindOneAndDelete(filter);
    }

    //------------------------------------------------------------------------------------------------------------------
    //특정 도큐먼트를 찾은 뒤에 그 도큐먼트에 있는 특정 필드만 찾아옴.
    BsonValue DetaFindFild(string findKey, string findfild )
    {
        var filter = Builders<BsonDocument>.Filter.Eq(findKey, findfild);//찾을 도큐먼트의 Name이 아디인것
        var nullFilter = collection.Find(filter).FirstOrDefault();//if null 이면 찾지 못함
        if (nullFilter != null)
        {
            Debug.Log(nullFilter.GetValue("password")); // 특정 필드를 찾음.
        }
        return nullFilter.GetValue("password");
    }
}





//{
//    "status":"success",
//"message":"Fetchedsuccessfully!",
//"data":
//    {
//        "redirectionURL":null,
//        "records":
//            [
//                {
//                    "_id":"63e3115a32e6b608299fe87c",
//                  "title":"Tournament Prize Pool 1",
//                  "amount":20,
//                  "totalCollectedAmount":1960,
//                  "startTime":"2023-02-08T03:04:58.749Z",
//                  "endTime":"2023-02-15T03:04:58.750Z",
//                  "createdAt":"2023-02-08T03:04:58.765Z",
//                  "updatedAt":"2023-02-08T03:04:58.765Z",
//                  "__v":0
//                },
//                {
//                    "_id":"63e3115a32e6b608299fe87d",
//                  "title":"Tournament Prize Pool 2",
//                  "amount":15,
//                  "totalCollectedAmount":1600,
//                  "startTime":"2023-02-08T03:04:58.752Z",
//                  "endTime":"2023-02-16T03:04:58.752Z",
//                  "createdAt":"2023-02-08T03:04:58.765Z",
//                  "updatedAt":"2023-02-08T03:04:58.765Z",
//                  "__v":0
//                },
//                {
//                    "_id":"63e3115a32e6b608299fe87e",
//                  "title":"Tournament Prize Pool 3",
//                  "amount":50,
//                  "totalCollectedAmount":350,
//                  "startTime":"2023-02-08T03:04:58.753Z",
//                  "endTime":"2023-02-23T03:04:58.753Z",
//                  "createdAt":"2023-02-08T03:04:58.766Z",
//                  "updatedAt":"2023-02-08T03:04:58.766Z",
//                  "__v":0
//                }
//    ]
//    }
//}