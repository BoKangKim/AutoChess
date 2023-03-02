using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using Newtonsoft.Json;
using System.Collections;

public class Database : MonoBehaviour
{


    #region Client �ּ� ����
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
        #region Database ����
        // MongoDB database name
        database = client.GetDatabase("AutoChessDB");
        #endregion

        #region Collection ����
        // �ش� Database�� �ִ� Collection ��������
        // MongoDB collection name
        collection = database.GetCollection<BsonDocument>("UserInfo");
        #endregion

        userInfo = new UserInfo();
        //
        //bsonarray�� �ݷ����� ��Ƽ� ��Ҹ� ������ ����.
        //BsonArray aa = DataFind("01013345678").GetValue("UnitInventory").AsBsonDocument.GetValue("Orc").AsBsonArray;

        //PhoneNumRegex = new Regex(@"010-[0-9]{4}-[0-9]{4}$");
        //PasswordRegex = new Regex(@"^(?=.*?[0-9]).{4,}$");

        //NickNameRegex = new Regex(@"^[0-9a-zA-Z]{2,12}$");


        //api���� �����͸� �޾ƿ����� ����(�ϴ��� db/ start���� �����ϰ��س���)
    }



    public void GetUserInfo()
	{
		StartCoroutine(processRequestGetUserInfo());
	}
	IEnumerator processRequestGetUserInfo()
	{
		// ���� ����
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
        yield return new WaitUntil(() => userInfo.public_address != null); 

        BsonDocument userInfos = DataFind(userInfo.public_address);

        Debug.Log(userInfos);

        if (userInfos == null)
        {
            DataInst(userInfo.public_address);
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
    //        Debug.Log("���̵� ��й�ȣ�� �ڸ����� ���� ����.");
    //        return;
    //    }


    //    BsonDocument UserInfo = DataFind(userInfo.PhoneNumber);

    //    if (UserInfo == null)
    //    {
    //        DataInst(Input_PhoneNumber.text, Input_Password.text);
    //        //�г��� ��ǲ �ʵ尡 ��
    //        PhoneNumberPanel.gameObject.SetActive(false);
    //        NickNamePanel.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        if (Input_Password.text != DetaFindFild("PhoneNumber", Input_PhoneNumber.text).ToString())
    //        {
    //            Debug.Log("��й�ȣ�� Ʋ�Ƚ��ϴ�.");
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

    // �г����� ���� �г����� �����ϸ� ���ѹ������Ϳ� �г��� �߰�
    //public void InputNickNameDataUpdate()
    //{

    //    Match M_NickNameRegex = NickNameRegex.Match(Input_Password.text);

    //    if (!M_NickNameRegex.Success)
    //    {
    //        Debug.Log("�ùٸ� �г����� �ƴմϴ�. �ٽ��Է��ϼ���.");
    //        return;
    //    }
    //    else if (userInfo.NickName == null)
    //    {
    //        userInfo.NickName = Input_NickName.text;
    //        BsonDocument filter = new BsonDocument { { "PhoneNumber", userInfo.PhoneNumber } };
    //        UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("NickName", userInfo.NickName);
    //        collection.FindOneAndUpdate(filter, update);
    //    }

    //    //�κ�� �̵�
    //}





    //------------------------------------------------------------------------------------------------------------------
    //��ť��Ʈ ����
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
    //Ư�� �ʵ�� ��ť��Ʈ ã��
    BsonDocument DataFind(string address)
    {
        //FilterDefinition<BsonDocument> filter2 = Builders<BsonDocument>.Filter.Eq("id", findID);

        BsonDocument filter = new BsonDocument { { "public_address", address } };

        BsonDocument targetData = collection.Find(filter).FirstOrDefault();


        return targetData;
    }



    //------------------------------------------------------------------------------------------------------------------
    //Ư�� ��ť��Ʈ�� Ư�� �ʵ� ����
    void DataUpdate(string findID, string updataData)
    {
        BsonDocument filter = new BsonDocument { { "PhoneNumber", findID } };

        UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("NickName", updataData);
        collection.FindOneAndUpdate(filter, update);
        //collection.UpdateOne(filter,update);
    }

    //------------------------------------------------------------------------------------------------------------------
    //Ư�� ��ť��Ʈ ����
    void DataDelete(string findID)
    {
        BsonDocument filter = new BsonDocument { { "id", findID } };
        collection.FindOneAndDelete(filter);
    }

    //------------------------------------------------------------------------------------------------------------------
    //Ư�� ��ť��Ʈ�� ã�� �ڿ� �� ��ť��Ʈ�� �ִ� Ư�� �ʵ常 ã�ƿ�.
    BsonValue DetaFindFild(string findKey, string findfild )
    {
        var filter = Builders<BsonDocument>.Filter.Eq(findKey, findfild);//ã�� ��ť��Ʈ�� Name�� �Ƶ��ΰ�
        var nullFilter = collection.Find(filter).FirstOrDefault();//if null �̸� ã�� ����
        if (nullFilter != null)
        {
            Debug.Log(nullFilter.GetValue("password")); // Ư�� �ʵ带 ã��.
        }
        return nullFilter.GetValue("password");
    }
}



