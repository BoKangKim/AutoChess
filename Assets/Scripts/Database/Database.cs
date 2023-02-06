using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;

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
    }




    // DataBase : Collection�� ������ �����̳�
    // Collection : Document�� �׷�, Document�� ���ο� ��ġ�� ����.
    // Document : �Ѱ� �̻��� Key-value ������ �̷���� ���� <BsonDocument>
    // Key / Field : �÷� ��� ���� ��

    #region Client �ּ� ����
    MongoClient client = new MongoClient("mongodb+srv://mongo:mongo1234@autochesscluster.ogdgk4g.mongodb.net/?retryWrites=true&w=majority");
    #endregion

    public TMP_InputField Input_PhoneNumber = null;
    public TMP_InputField Input_NickName = null;

    public Image PhoneNumberPanel;
    public Image NickNamePanel;

    public TextMeshProUGUI status = null;

    public IMongoDatabase database = null;
    public IMongoCollection<BsonDocument> collection;



    public UserInfo userInfo;

    public class UserInfo
    {
        public string PhoneNumber { get; set; }
        public string NickName { get; set; }
        public int UserIconIndex { get; set; }
        public IDictionary<string, List<string>> UnitInventory { get; set; }
    }

    void Start()
    {
        Input_PhoneNumber.characterLimit = 11;
        Input_NickName.characterLimit = 8;
        Input_PhoneNumber.contentType = TMP_InputField.ContentType.IntegerNumber;
        Input_NickName.contentType = TMP_InputField.ContentType.Alphanumeric;
        #region Database ����
        // MongoDB database name
        database = client.GetDatabase("AutoChess");
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

    }


    public void InputPhoneNumber()
    {
        userInfo.PhoneNumber = Input_PhoneNumber.text;
        BsonDocument UserInfo = DataFind(userInfo.PhoneNumber);


        if (UserInfo == null)
        {
            DataInst(userInfo.PhoneNumber);
            //�г��� ��ǲ �ʵ尡 ��
            PhoneNumberPanel.gameObject.SetActive(false);
            NickNamePanel.gameObject.SetActive(true);
        }
        else
        {
            userInfo.PhoneNumber = (string)UserInfo.GetValue("PhoneNumber");
            userInfo.NickName = UserInfo.GetValue("NickName").ToString();
            userInfo.UserIconIndex = (int)UserInfo.GetValue("UserIconIndex");
           // userInfo.UnitInventory = (IDictionary<string, List<string>>)UserInfo.GetValue("UnitInventory");
        }
    }

    // �г����� ���� �г����� �����ϸ� ���ѹ������Ϳ� �г��� �߰�
    public void InputNickNameDataUpdate()
    {
        if (userInfo.NickName == null)
        {
            userInfo.NickName = Input_NickName.text;
            BsonDocument filter = new BsonDocument { { "PhoneNumber", userInfo.PhoneNumber } };
            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("NickName", userInfo.NickName);
            collection.FindOneAndUpdate(filter, update);
        }

        //�κ�� �̵�
    }

    //------------------------------------------------------------------------------------------------------------------
    //��ť��Ʈ ����
    void DataInst(string Num)
    {
        userInfo.PhoneNumber = Num;
        userInfo.NickName = null;
        userInfo.UserIconIndex = 1;
        userInfo.UnitInventory = new Dictionary<string, List<string>>();
        userInfo.UnitInventory.Add("Orc", new List<string>() { "OrcWarrior", "OrcAssassin", "OrcWizard", "OrcTanker", "OrcRangeDealer" });
        userInfo.UnitInventory.Add("Dwarf", new List<string>() { "DwarfWarrior", "DwarfAssassin", "DwarfWizard", "DwarfTanker", "DwarfRangeDealer" });
        userInfo.UnitInventory.Add("Golem", new List<string>() { "GolemWarrior", "GolemAssassin", "GolemWizard", "GolemTanker", "GolemRangeDealer" });
        userInfo.UnitInventory.Add("Mecha", new List<string>() { null, null, null, null, null });
        userInfo.UnitInventory.Add("Demon", new List<string>() { null, null, null, null, null });

        collection.InsertOne(userInfo.ToBsonDocument());
    }

    //------------------------------------------------------------------------------------------------------------------
    //Ư�� �ʵ�� ��ť��Ʈ ã��
    BsonDocument DataFind(string Num)
    {
        //FilterDefinition<BsonDocument> filter2 = Builders<BsonDocument>.Filter.Eq("id", findID);

        BsonDocument filter = new BsonDocument { { "PhoneNumber", Num } };

        BsonDocument targetData = collection.Find(filter).FirstOrDefault();

        //Debug.Log(targetData);

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
    void DetaFindFild()
    {
        var filter = Builders<BsonDocument>.Filter.Eq("id", "�Ƶ�");//ã�� ��ť��Ʈ�� Name�� �Ƶ��ΰ�
        var nullFilter = collection.Find(filter).FirstOrDefault();//if null �̸� ã�� ����
        if (nullFilter != null)
        {
            Debug.Log(nullFilter.GetValue("Name")); // Ư�� �ʵ带 ã��.
        }
    }
}