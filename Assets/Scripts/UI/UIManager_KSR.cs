using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using TMPro.Examples;

//using Photon.Pun;
//using Photon.Realtime;

// ��ġ�� ���� ī��Ʈ, ���� Ŭ���� ���� ������ �ʿ�
// �ó��� ���� ���� ������� ����
// ����ĳ��Ʈ ���� �� �������� Ȯ���ϰ� UI ���°�
// �Ϻ� ��ư �ڵ�� �޾ƿ���

public class UIManager_KSR : MonoBehaviour
{

    #region ��� �̱��� �س�����
    private UIManager_KSR() { }
    private static UIManager_KSR inst = null;
    public static UIManager_KSR Inst
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType<UIManager_KSR>();
                if (inst == null)
                {
                    inst = new GameObject("UIManager").AddComponent<UIManager_KSR>();
                }
            }
            return inst;
        }
    }
    #endregion

    #region Canvas
    [Header("[UnitState]")]
    [SerializeField] private GameObject UnitStatusContents;
    [SerializeField] private GameObject UnitStatusInfo;
    [SerializeField] private GameObject UnitStatusDetailInfo;
    [SerializeField] private GameObject UnitSkillInfo;

    [Header("[Setting]")]
    [SerializeField] private GameObject SettingContents;
    [SerializeField] private GameObject SettingContentsPopup;

    [Header("[Player]")]
    [SerializeField] private TextMeshProUGUI playerExpensionLV;
    [SerializeField] private TextMeshProUGUI playerRankingnLV;
    [SerializeField] private TextMeshProUGUI playerGold; 
    [SerializeField] private Image playerHPSlider;

    [Header("[Round]")]
    [SerializeField] private GameObject RoundContents;
    [SerializeField] private GameObject RoundEXContents;
    [SerializeField] private GameObject RoundResultContents; // ���� ���
    [SerializeField] private TextMeshProUGUI RoundInfoNum;
    [SerializeField] private TextMeshProUGUI RoundStageNum;
    [SerializeField] private TextMeshProUGUI RoundDetailNum;
    [SerializeField] private TextMeshProUGUI[] RoundDetailStepNum=new TextMeshProUGUI[4];

    [Header("[Synergy]")]
    [SerializeField] private GameObject SynergyContents;
    [SerializeField] private Image SynergyEXContents;  
    [SerializeField] private Image SynergyEXContentsPopup;  
    [SerializeField] private TextMeshProUGUI synergyContentsPopupName; // �ó��� �˾�â - ���� �̸�
    [SerializeField] private TextMeshProUGUI[] synergyContentsPopupInfo; // �ó��� ����
    [SerializeField] private Image[] SynergyUnitIcon; // �ó��� 3���� �� ��
    [SerializeField] private Image[] SnergyUnitPopupIcon; // �˾� ������ 5��
    [SerializeField] private TextMeshProUGUI SynergyPlus; // ������ ���� �� �� ī��Ʈ �� -> �迭 �����ͷ� �޾ƿ��� 
    [SerializeField] private GameObject[] ClassSynergy, SpeciesSynergy; // ������ ������Ʈ

    // ������ �ڽ��ȿ� ��� ���� : ���� �̸�, ���� ī��Ʈ ��, �����  

    [Header("[Ranking]")]
    [SerializeField] private GameObject rankingContents;
    [SerializeField] private TextMeshProUGUI rankingUserID; // ��ŷ UI�� �ߴ� ID  - get data
    [SerializeField] private TextMeshProUGUI rankingUserLV;
    [SerializeField] private GameObject[] rankUserInfo;

    [Header("[Expansion]")]
    [SerializeField] private TextMeshProUGUI expansionUserID; // Ȯ�� UI�� �ߴ� ID  - get data
    [SerializeField] private TextMeshProUGUI expansionGold; // Ȯ�� �� ����ϴ� ���
    [SerializeField] private TextMeshProUGUI expensionLV;
    [SerializeField] private Slider expansionEXPSlider;

    [Header("[Gacha]")]
    [SerializeField] private TextMeshProUGUI gachaWeponGold;
    [SerializeField] private TextMeshProUGUI gachaUnitGold;

    [Header("[Time]")]
    [SerializeField] TimeManager timeManager = null;
    #endregion

    //-----------------------------------------------------------------------------------------------------------


    // Setting Menu
    #region Setting Menu

    public void OnSetting()
    {
        Debug.Log("ON Setting");
        SettingContentsPopup.gameObject.SetActive(true);
    }
    public void OffSetting()
    {
        Debug.Log("OFF Setting");
        SettingContentsPopup.gameObject.SetActive(false);
    }
    public void OnSurrender()
    {
        Debug.Log("�׺�");
    }
    #endregion

    public void OffRoundEXChange()
    {
        Debug.Log("OFF RoundEXChange");
        RoundEXContents.gameObject.SetActive(false);
    }
    public void OnRoundEXChange()
    {
        Debug.Log("ON RoundEXChange");
        RoundEXContents.gameObject.SetActive(true);
    }
    public void OnSynergy()
    {
        Debug.Log("ON Synergy");
        SynergyEXContentsPopup.gameObject.SetActive(true);
    }
    public void OffSynergy()
    {
        Debug.Log("OFF Synergy");
        SynergyEXContentsPopup.gameObject.SetActive(false); // �ó��� �˾�â
    }

    // Deployed Unit Synergy - Popup / ��ġ�� ���� ī��Ʈ test
    #region Deployed Unit Synergy 
    private void DeployedUnitSynergy() // ��ġ�� ������ �̹��� ������ Ȱ��ȭ�ϱ�
    {
        // �� �ó������� �ش��ϴ� �������� Ȯ���ʿ� - �ӽ� ������
        switch (deployedUnit)
        {
            case 0:
                SynergyEXContents.gameObject.SetActive(false); // ��ũ�� �信 �ִ� ������Ʈ
                SnergyUnitPopupIcon[0].gameObject.SetActive(false);
                for (int i = 0; i < SnergyUnitPopupIcon.Length; i++)
                {
                    OffSynergyPopupAlpha(i);
                }
                break;
            case 1:
                SynergyEXContents.gameObject.SetActive(true);
                SnergyUnitPopupIcon[0].gameObject.SetActive(true); // �ó��� ������ - ���� �̹���
                SnergyUnitPopupIcon[1].gameObject.SetActive(false);
                OnSynergyPopupAlpha(0); // �ó��� �˾� ������ - ���� ���� ���� �̹���
                OffSynergyPopupAlpha(1);
                break;
            case 2:
                //IsMechaSynergy3 = false;
                SnergyUnitPopupIcon[1].gameObject.SetActive(true);
                SnergyUnitPopupIcon[2].gameObject.SetActive(false);
                ChangeTextColorInitiate(SynergyPlus);
                OnSynergyPopupAlpha(1);
                OffSynergyPopupAlpha(2);
                ChangeTextColorInitiate(synergyContentsPopupInfo[0]); // ���� �� Ȱ��ȭ
                break;
            case 3:
                //IsMechaSynergy3 = true;
                SnergyUnitPopupIcon[2].gameObject.SetActive(true);
                ChangeTextColor(SynergyPlus);
                OnSynergyPopupAlpha(2);
                OffSynergyPopupAlpha(3);
                ChangeTextColor(synergyContentsPopupInfo[0]);
                break;
            case 4:
                //IsMechaSynergy5 = false;
                OnSynergyPopupAlpha(3);
                OffSynergyPopupAlpha(4);
                ChangeTextColorInitiate(synergyContentsPopupInfo[1]);
                break;
            case 5:
                //IsMechaSynergy5 = true;
                OnSynergyPopupAlpha(4);
                ChangeTextColor(synergyContentsPopupInfo[1]);
                ChangeTextColorInitiate(synergyContentsPopupInfo[0]);
                break;
        }
    }
    // �ó��� �˾�â Ȱ��ȭ / ��Ȱ��ȭ
    private void OnSynergyPopupAlpha(int num)
    {
        popupIconColor.a = 1.0f; // ���İ����� Ȱ��ȭ ��Ȱ��ȭ ����
        SnergyUnitPopupIcon[num].color = popupIconColor;
    }
    private void OffSynergyPopupAlpha(int num)
    {
        popupIconColor.a = 0.5f; // ���İ����� Ȱ��ȭ ��Ȱ��ȭ ����
        SnergyUnitPopupIcon[num].color = popupIconColor;
    }
    #endregion

    // Color
    private Color popupIconColor;

    [SerializeField] private string userName = "user1"; // ���� �г��� �޾ƿ� �κ�
    public string UserName { get { return userName; } set { userName = value; } }
    [SerializeField] private int deployedUnit; // ��ġ�� ���� 
    public int DeployedUnit { get { return deployedUnit; } set { deployedUnit = value; } } // ���� ����� ����

    #region Data �޾ƿ���

    // ���ο� �̸��� ���� ������ �������� �߰� �����Ǵ� ������� �����ؾ���
    private TextMeshProUGUI[] SpeciesSynergyName, ClassSynergyName;
    private string[] SpeciesNameList = new string[] { "Mecha", "Golem", "Orc", "Demon", "Dwarf" };
    private string[] ClassNameList = new string[] { "Warrior", "Assassin", "RangeDealer", "Tanker", "Magician" };

    private void Init() // ��ũ�Ѻ� ������ �ڵ� �߰� - test ��
    {
        ScrollRect scrollRect = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        Image contentsImage = scrollRect.content.GetChild(0).GetComponent<Image>();

        int Value = 0;
        for (int i = 0; i < 10; i++)
        {
            // item �ʱ�ȭ
            /*item.Item(GetStudyData().result[0].level,
                GetStudyData().result[0].unit,
                GetStudyData().result[0].text_title);*/
            // �� �ڵ� �����ؼ� ������ �������� Ŭ����, �����̸� �޾ƿ��� ��.

            // Contents�� ����
            var index = Instantiate(contentsImage, new Vector3(0, 0, 0), Quaternion.identity);
            index.transform.SetParent(GameObject.Find("Content").transform);
            Value -= 200; // ���� ����
            // Vertical Layout Group �߰� : item���� ��ġ ���� �ڵ����� �������ֱ� ����
            // Content Fitter : item ũ�� �ڵ����� �����ֱ� ����
        }
    }
    protected void UpdateSpeciesName()
    {
        SpeciesSynergyName[0].text = Mecha;
        SpeciesSynergyName[1].text = Golem;
        SpeciesSynergyName[2].text = Orc;
        SpeciesSynergyName[3].text = Demon;
        SpeciesSynergyName[4].text = Dwarf;
    }
    protected void UpdateClassName()
    {
        ClassSynergyName[0].text = Warrior;
        ClassSynergyName[1].text = Assassin;
        ClassSynergyName[2].text = RangeDealer;
        ClassSynergyName[3].text = Tanker;
        ClassSynergyName[4].text = Magician;
    }
    // unit synergy data �޾ƿ���    
    private string Mecha = "Mecha";
    private string Golem = "Golem";
    private string Orc = "Orc";
    private string Demon = "Demon";
    private string Dwarf = "Dwarf";
    // class synergy data �޾ƿ���
    private string Warrior = "Warrior";
    private string Assassin = "Assassin";
    private string RangeDealer = "RangeDealer";
    private string Tanker = "Tanker";
    private string Magician = "Magician";
    #endregion

    
    private int  gachaWeponGoldValue, gachaUnitGoldValue;
    private float expansionMaxEXPValue;
    private int expansionGoldValue, expensionLevelValue;
    private int roundTextColor; // ���帶�� �۾� ���� �ٲ�
    [SerializeField] private int playerGoldValue;
    [SerializeField] private float playerEXPValue;
    [SerializeField] private float playerMaxEXPValue;
    [SerializeField] private int playerHPValue;
    [SerializeField] private int playerMaxHPValue;
    public int GetPlayerGold { get { return playerGoldValue; } }
    public float GetPlayerEXP { get { return playerEXPValue; } }
    public float GetPlayerMaxEXP { get { return playerMaxEXPValue; } }
    public int GetPlayerHP { get { return playerHPValue; } }
    public int GetPlayerMaxHP { get { return playerMaxHPValue; } }

    private int attackPower = 10; // ���� ���ݷ� ����� �κ�
    private int healPower = 10;

    // �ѹ��� ������Ʈ �� �÷��̾� ����
    public void UpdatePlayerName(string nickname)
    {  
        rankingUserID.text = nickname;
        expansionUserID.text = nickname;
    }
    // �ֱ������� ������Ʈ�� �ʿ��� �÷��̾� ����
    public void UpdatePlayerInfo(int playerGold, float playerEXP, int playerLV, int hp) 
    {
        // data �޾ƿ� ��
        playerGoldValue = playerGold;
        playerEXPValue = playerEXP;
        expensionLevelValue = playerLV;
        playerExpensionLV.text = "Lv." + playerLV.ToString();
        playerRankingnLV.text = "Lv." + playerLV.ToString();
        // player HP
        playerMaxHPValue = hp;
        playerHPValue = playerMaxHPValue;
    }
    // �ó��� ���� ������ ����  - ��ȹ�� ���� ����
    private string InputInfo1, InputInfo2 = null;

    public void UpdateSynergyInfo(string info1, string info2) // ��ũ���ͺ� �����ͷ� ���� �ش� �ҷ����� - �迭?
    {
        InputInfo1 = info1;
        InputInfo2 = info2;

        // ������ ���� �ٸ� ������ �� �� �־�� ��.
        synergyContentsPopupInfo[0].text = InputInfo1;
        synergyContentsPopupInfo[1].text = InputInfo2;
    }

    // Round Detail Info : 1-1 1-2 1-3 1-4 ���� ���� ���� ȿ��
    #region Round Detail Info
    
    public void UpdateRoundInfo(int row,int col)
    {
        RoundStageNum.text = (row+ col).ToString();
        RoundDetailNum.text = (row + col).ToString();

        roundTextColor = 0;
        for (int i = 0; i < RoundDetailStepNum.Length; i++)
        {
            roundTextColor = i;
            RoundDetailStepNum[i].text = (row + col).ToString();
            if (row == i) ChangeTextColor(RoundDetailStepNum[i]);
            else { ChangeTextColorInitiate(RoundDetailStepNum[i]); }
        }
        RoundInfoNum.text = (expensionLevelValue + " / " + deployedUnit).ToString();
    }

    #endregion

    private int RoundRewardGold; 
    private float RoundRewardEXP;
    public void UpdateRoundReward(int rewardGold, float rewardEXP) // �ӽ� - ���� ����
    {
        RoundRewardGold = rewardGold;
        RoundRewardEXP = rewardEXP;
    }
    private bool IsDead { get; set; }

    public Button unitBuyButton, equipmentBuyButton, sellButton = null;
    GraphicRaycaster graphicRaycaster = null;
    PointerEventData pointerEventData = null;
    List<RaycastResult> rrList = null;
    [SerializeField] private TextMeshProUGUI SynergyInfo = null;

    void Awake()
    {
        IsDead = false;
        //TryGetComponent<TimeManager>(out timeManager); // �۵��� �ȵǼ� �ϴ� �ּ�
        timeManager = FindObjectOfType<TimeManager>();
        
        gachaWeponGoldValue = 3;
        gachaUnitGoldValue = 4;
        gachaWeponGold.text = gachaWeponGoldValue.ToString();
        gachaUnitGold.text = gachaUnitGoldValue.ToString();

        //UpdatePlayerInfo(1000, 0, 1, ("Lv." + expensionLevelValue).ToString(), 100f, UserName);
        UpdatePlayerName("nick");        
        UpdateSynergyInfo("(3) ���� ���� �� Orc�� �Լ��Ҹ��� �Բ� ��� ������ ���ݷ°� ü���� 5% �϶���Ŵ", "(5) ���� ���� �� Orc�� �Լ� �Ҹ��� �Բ� ��� ������ ���ݷ°� ü���� 15% �϶���Ŵ");        
        UpdateRoundReward(10, 4);

        expansionEXPSlider.value = playerEXPValue; // �����̴�
        expansionMaxEXPValue = playerMaxEXPValue; // �ӽ� - ������ �޾ƿ��� - �����ʿ�
        expansionGoldValue = (int)expansionMaxEXPValue;
        expansionGold.text = ("Buy "+expansionGoldValue+" EXP").ToString();

        deployedUnit = 0; // �ӽ� - ��ġ�� ���� ��

        //color
        popupIconColor = SnergyUnitPopupIcon[0].GetComponent<Image>().color;
        synergyContentsPopupName.text = Mecha; // ���߿� �޾ƿͼ� ������Ʈ

        graphicRaycaster = GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(EventSystem.current);
        rrList = new List<RaycastResult>();
    }
    void Update()
    {

        pointerEventData.position = Input.mousePosition;
        // ��ġ�� ���ֿ� ���� �ó��� Ȯ��
        DeployedUnitSynergy();

        // ������Ʈ �� �ٿ��� ��
        if (playerHPValue <= 0) IsDead = true;
        if (playerGoldValue <= 0) playerGoldValue = 0;
        
        // ü�¿� ���� ��ŷ ������ ��ġ �̵�
        ChangeRankerPosition();

    }

    // test
    #region test
   
    public void AttackTest()
    {
        if (playerHPValue <= 0) playerHPValue = 0; // ����
        playerHPValue -= attackPower;
    }
    public void HealTest()
    {
        if (playerHPValue >= playerMaxHPValue) playerHPValue = playerMaxHPValue;
        playerHPValue += healPower;
    }
    public void UnitTest_Plus()
    {
        deployedUnit++;
        if (deployedUnit >= 5) deployedUnit = 5;
    }
    public void UnitTest_Minus()
    {
        deployedUnit--;
        if (deployedUnit <= 0) deployedUnit = 0;
    }
    protected void ChangeRankerPosition()
    {
        // �ٸ� �÷��̾�� ������ �� ���� ���� HP�� ��� ���� ����             
        if (playerHPValue >= 70f)
        {
            rankUserInfo[0].transform.localPosition = new Vector2(125, 160);
        }
        if ((playerHPValue >= 50f) && (playerHPValue < 70f))
        {
            rankUserInfo[0].transform.localPosition = new Vector2(125, 40);
        }
        if ((playerHPValue >= 30f) && (playerHPValue < 50f))
        {
            rankUserInfo[0].transform.localPosition = new Vector2(125, -80);
        }
        if ((playerHPValue >= 0f) && (playerHPValue < 30f))
        {
            rankUserInfo[0].transform.localPosition = new Vector2(125, -200);
        }
        if (IsDead)
        {            
            Debug.Log("���� - ��Ȱ��ȭ ǥ�� �ʿ�");
        }        
        // ���� �÷��̾ �������� �� -> ���� ���� ���� ������� �ؿ� �������� ���̴°� �ʿ�
        // �ٸ� �÷��̾�� HP�� ���� ��� ��ġ �����ϴ� �� �ʿ�        
        playerRankingnLV.text = ("Lv." + expensionLevelValue).ToString(); // ���� �� ������Ʈ
        playerHPSlider.fillAmount = playerHPValue / playerMaxHPValue;
    }
    #endregion
    //

    // Text Color Change
    private void ChangeTextColor(TextMeshProUGUI t) { t.color = Color.yellow; }
    private void ChangeTextColorInitiate(TextMeshProUGUI t) { t.color = Color.gray; }

    // Gacha Wepon UI    
    public void OnWeponGacha()
    {
        if (playerGoldValue <= gachaWeponGoldValue) return;
        playerGoldValue -= gachaWeponGoldValue;
        playerGold.text = string.Format("{0:#,###}", playerGoldValue); // 4�ڸ��� �Ѿ�� , ǥ��
    }
    // Gacha Unit UI    
    public void OnUnitGacha()
    {
        if (playerGoldValue <= gachaUnitGoldValue) return;

        unitInstButton();
        playerGoldValue -= gachaUnitGoldValue;
        playerGold.text = string.Format("{0:#,###}", playerGoldValue); // 4�ڸ��� �Ѿ�� , ǥ��
    }

    // Faction Expansion UI
    public void OnFactionExpansion()
    {
        if (playerGoldValue <= expansionGoldValue) return;
        if (playerEXPValue >= expansionMaxEXPValue)
        {
            expensionLevelValue += 1;
            playerEXPValue = playerEXPValue - expansionMaxEXPValue;
        }
        // Gold
        expansionGoldValue = Mathf.Abs((int)expansionMaxEXPValue - (int)playerEXPValue); // Abs : ����
        playerGoldValue -= expansionGoldValue; // ���� exp �� ���� ��� ���� ����       
        expansionGold.text = expansionGoldValue.ToString();
        playerGold.text = string.Format("{0:#,###}", playerGoldValue); // ���� ����

        // Exp �����̴� ��
        playerEXPValue += RoundRewardEXP; // �÷��̾� EXP + �߰� EXP
        playerExpensionLV.text = ("Lv." + expensionLevelValue).ToString(); // ���� �� ������Ʈ
        expansionEXPSlider.value = (playerEXPValue * 0.1f);
        expansionEXPSlider.maxValue = (expansionMaxEXPValue * 0.1f);
    }


    public System.Action UnitInstButton;

    //UI Raycast
    public T RaycastUI<T>(int num) where T : Component
    {
        rrList.Clear();
        graphicRaycaster.Raycast(pointerEventData, rrList);
        if (rrList.Count == 0) return null;
        return rrList[num].gameObject.GetComponent<T>();
    }

    public void unitInstButton() => UnitInstButton();

    public void SynergyText(string text)
    {
        if (text == null)
        {
            SynergyInfo.text = "";
        }
        else
        {
            SynergyInfo.text += text + "\n";
        }
    }

}