using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//using Photon.Pun;
//using Photon.Realtime;

public class UIManager : MonoBehaviour
{
    // ���� ���� -> ��� ���ư����� Ȯ��
    // �Ⱦ��� ��� ����
    // 

    #region Canvas
    [Header("[UnitState]")]
    [SerializeField] private GameObject UnitStatusContents;
    [SerializeField] private GameObject UnitStatusInfo;
    [SerializeField] private GameObject UnitStatusDetailInfo;
    [SerializeField] private GameObject UnitSkillInfo;
    //[SerializeField] private GameObject[] UnitSynergy; // ���� ų ���� �ó��� â //
    [SerializeField] private GameObject[] bettingUnitGrade;  // ���õ� ���� ���

    [Header("[Chatting]")]
    [SerializeField] private GameObject ChattingContents;
    [SerializeField] private GameObject ChattingContentsPopup;

    [Header("[Setting]")]
    [SerializeField] private GameObject SettingContents;
    [SerializeField] private GameObject SettingContentsPopup;

    [Header("[Player]")]
    [SerializeField] private TextMeshProUGUI playerExpensionLV;
    [SerializeField] private TextMeshProUGUI playerRankingnLV;
    [SerializeField] private TextMeshProUGUI playerGold; // �÷��̾ ������ ���    
    //[SerializeField] private TextMeshProUGUI playerID;
    [SerializeField] private Image playerHPSlider;

    [Header("[Round]")]
    [SerializeField] private GameObject RoundContents;
    [SerializeField] private GameObject RoundEXContents;
    [SerializeField] private GameObject RoundBossContents; // ��������
    [SerializeField] private Button[] SelectBossContents;
    [SerializeField] private GameObject RoundResultContents; // ���� ���
    [SerializeField] private TextMeshProUGUI RoundInfoNum;
    [SerializeField] private TextMeshProUGUI RoundStageNum;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum1;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum2;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum3;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum4;
    [SerializeField] private TextMeshProUGUI[] roundTypeText;
    private string[] roundType = { "PVP", "PVE", "PVP", "BOSS" };


    [Header("[Synergy]")]
    [SerializeField] private GameObject SynergyContents;
    //[SerializeField] private GameObject synergyContentsPopup;
    [SerializeField] private Image SynergyEXContents;  // test
    [SerializeField] private Image SynergyEXContentsPopup;  // test
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

    [Header("[Roulette]")]
    [SerializeField] private GameObject RouletteContents; // �귿
    [SerializeField] private GameObject RouletteResultUI;

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

    // Deployed Unit Synergy - Popup
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
    private void Init() // ��ũ�Ѻ� ������ �ڵ� �߰� test
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

    private float playerEXPValue, playerMaxHPValue, playerHPValue;
    private int playerGoldValue, gachaWeponGoldValue, gachaUnitGoldValue;
    private int expansionGoldValue, expansionMaxEXPValue, expensionLevelValue;
    private int roundTextColor; // ���帶�� �۾� ���� �ٲ�

    public void UpdatePlayerInfo(int playerGold, float playerEXP, int expansionLV, string text, float hp, string nickname)
    {
        // data �޾ƿ� ��
        playerGoldValue = playerGold; //player.GetGold;
        playerEXPValue = playerEXP; //player.GetEXP; 0
        expensionLevelValue = expansionLV; //player.GetLevel; 1
        playerExpensionLV.text = text; // ���� �ؽ�Ʈ
        playerRankingnLV.text = text;
        // player HP
        playerMaxHPValue = hp;
        playerHPValue = playerMaxHPValue;
        // player - data ���� �κ�        
        rankingUserID.text = nickname;
        expansionUserID.text = nickname;
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

    private int roundStepNumber1, roundStepNumber2;

    public void UpdateRoundInfo(int col, int row) // ��� ������
    {
        roundStepNumber1 = col;
        roundStepNumber2 = row;

        RoundStageNum.text = (col + "-" + row).ToString();
        //RoundStageNum.text = (roundStepNumber1 + "-" + roundStepNumber2).ToString();

        RoundDetailStepNum.text = col.ToString();
        RoundDetailStepNum1.text = (col + "-" + 1).ToString();
        RoundDetailStepNum2.text = (col + "-" + 2).ToString();
        RoundDetailStepNum3.text = (col + "-" + 3).ToString();
        RoundDetailStepNum4.text = (col + "-" + 4).ToString();

        // �̹��� ���� ���� ���� �ʿ�
        roundTextColor = 0;
        if (row == 1) roundTextColor = 1;
        else { ChangeTextColorInitiate(RoundDetailStepNum1); }

        if (row == 2) roundTextColor = 2;
        else { ChangeTextColorInitiate(RoundDetailStepNum2); }

        if (row == 3) roundTextColor = 3;
        else { ChangeTextColorInitiate(RoundDetailStepNum3); }

        if (row == 4) // ������ ���� ������ ���� ���� Ȱ��ȭ
        {
            roundTextColor = 4;
            RoundBossContents.gameObject.SetActive(true);
        }
        else
        {
            ChangeTextColorInitiate(RoundDetailStepNum4);
            RoundBossContents.gameObject.SetActive(false);
        }

        switch (roundTextColor)
        {
            case 1:
                ChangeTextColor(RoundDetailStepNum1);
                break;
            case 2:
                ChangeTextColor(RoundDetailStepNum2);
                break;
            case 3:
                ChangeTextColor(RoundDetailStepNum3);
                break;
            case 4:
                ChangeTextColor(RoundDetailStepNum4);
                break;
        }
        RoundInfoNum.text = (expensionLevelValue + " / " + deployedUnit).ToString();
    }

    public void RoundType(string[] type)
    {
        for (int i = 0; i < roundTypeText.Length; i++)
        {
            roundTypeText[i].text = type[i];
        }
    }
    #endregion

    private int RoundRewardGold; // �� ���� ���ư������� ���� ��� - ���� ���� ���� �ִ��� Ȯ���ϰ� �ֱ�
    private float RoundRewardEXP;
    public void UpdateRoundReward(int rewardGold, float rewardEXP)
    {
        RoundRewardGold = rewardGold;
        RoundRewardEXP = rewardEXP;
    }

    // Betting Round
    // private int prossessedUserGoldValue;
    // private int betOnUserGoldValue;
    // private int betOnMaximumGoldValue;
    private bool IsDead { get; set; }
    void Awake()
    {
        // SynergyUnitIcon = SynergyEXContents.transform.GetChild();
        //TryGetComponent<TimeManager>(out timeManager); // �۵��� �ȵǼ� �ϴ� �ּ�
        timeManager = FindObjectOfType<TimeManager>();
        // ���� ���� ��� - ?
        gachaWeponGoldValue = 3;
        gachaUnitGoldValue = 4;
        gachaWeponGold.text = gachaWeponGoldValue.ToString();
        gachaUnitGold.text = gachaUnitGoldValue.ToString();

        IsDead = false;

        UpdatePlayerInfo(1000, 0, 1, ("Lv." + expensionLevelValue).ToString(), 100f, UserName);
        UpdateSynergyInfo("(3) ���� ���� �� Orc�� �Լ��Ҹ��� �Բ� ��� ������ ���ݷ°� ü���� 5% �϶���Ŵ", "(5) ���� ���� �� Orc�� �Լ� �Ҹ��� �Բ� ��� ������ ���ݷ°� ü���� 15% �϶���Ŵ");
        UpdateRoundInfo(1, 1);
        UpdateRoundReward(10, 4);

        expansionEXPSlider.value = playerEXPValue; // �����̴�
        expansionMaxEXPValue = 32; // �ӽ� - ������ �޾ƿ��� - �����ʿ�
        expansionGoldValue = expansionMaxEXPValue;
        expansionGold.text = expansionGoldValue.ToString();

        deployedUnit = 0; // �ӽ� - ��ġ�� ���� ��

        //color
        popupIconColor = SnergyUnitPopupIcon[0].GetComponent<Image>().color;
        synergyContentsPopupName.text = Mecha; // ���߿� �޾ƿͼ� ������Ʈ

        RoundType(roundType);
    }
    public void UpdateExpansionInfo()
    {

    }

    void Update()
    {
        // ��ġ�� ���ֿ� ���� �ó��� Ȯ��
        DeployedUnitSynergy();

        // ������Ʈ �� �ٿ��� ��
        if (playerHPValue <= 0) IsDead = true;
        if (playerGoldValue <= 0) playerGoldValue = 0;

        // Round Info
        RoundType(roundType);
        //RoundInfoNum.text = (expensionLevelValue + " / " + deployedUnit).ToString();
        // timer = 0 -> Next Round      
        if (timeManager.currentTime <= 0f)
        {
            if ((roundStepNumber1 >= 9) && (roundStepNumber2 >= 4))
            {
                roundStepNumber1 = 9;
                roundStepNumber2 = 4;
                timeManager.IsTimeOver = true;
                return;
            }
            roundStepNumber2++;
            if (roundStepNumber2 >= 5)
            {
                roundStepNumber1 += 1;
                roundStepNumber2 = 1;
            }
            RoundStageNum.text = (roundStepNumber1 + "-" + roundStepNumber2).ToString();
            timeManager.IsNextRound = true;
            //playerGoldValue += RoundRewardGold; // �Ѷ���� ��� ����
        }
        UpdateRoundInfo(roundStepNumber1, roundStepNumber2); // �������� �޾ƿ� ������ ��               
        // ü�¿� ���� ��ŷ ������ ��ġ �̵�
        ChangeRankerPosition();

    }

    // �������� �����ϸ� ������ ���� ����, ȿ�� ����
    // ������ ��ȣ�� ���� ����Ǵ� ȿ��, �����ϴ� ���� �ٸ�

    public void SelectBoss_1()
    {
        // SelectBossContents[0].enabled = false;
        Debug.Log("1�� ���� ���� / ȿ�� ���� 1 : ");
    }
    public void SelectBoss_2()
    {
        // SelectBossContents[1].enabled = false;
        Debug.Log("1�� ���� ���� / ȿ�� ���� 2 : ");
    }
    public void SelectBoss_3()
    {
        // SelectBossContents[2].enabled = false;
        Debug.Log("1�� ���� ���� / ȿ�� ���� 3 : ");
    }

    // test
    #region test
    private float attackPower = 10f; // ������ ���� �κ�
    private float healPower = 10f;
    public void AttackTest() // test ��
    {
        if (playerHPValue <= 0) playerHPValue = 0; // ����
        playerHPValue -= attackPower;
    }
    public void HealTest() // test ��
    {
        Debug.Log("��");
        if (playerHPValue >= playerMaxHPValue) playerHPValue = playerMaxHPValue;
        playerHPValue += healPower;
    }
    public void UnitTest_Plus() // test ��
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
            rankUserInfo[0].transform.localPosition = new Vector2(125, -200);
        }
        if ((playerHPValue >= 50f) && (playerHPValue < 70f))
        {
            rankUserInfo[0].transform.localPosition = new Vector2(125, -80);
        }
        if ((playerHPValue >= 30f) && (playerHPValue < 50f))
        {
            rankUserInfo[0].transform.localPosition = new Vector2(125, 40);
        }
        if ((playerHPValue >= 0f) && (playerHPValue < 30f))
        {
            rankUserInfo[0].transform.localPosition = new Vector2(125, 160);
        }
        if (IsDead)
        {
            //rankUserInfo[0].interactable = false;
            Debug.Log("���� - ��Ȱ��ȭ ǥ�� �ʿ�");
        }
        // ���� �÷��̾� ���� �Ʒ��� ������ ��Ȱ��ȭ ȿ�� �ֱ�
        // ���� �÷��̾ �������� �� -> ���� ���� ���� ������� �ؿ� �������� ����
        // ��ŷ ������ : HP ������ �ٲ�� �������� ������Ʈ
        playerRankingnLV.text = ("Lv." + expensionLevelValue).ToString(); // ���� �� ������Ʈ
        playerHPSlider.fillAmount = playerHPValue / playerMaxHPValue;
    }
    #endregion
    //

    // Text Color Change
    // ���� �߰� -> 2����°���� ������ ���ؿ� 
    // ���� �۾� ���� �ȹٲ�
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
        expansionGoldValue = Mathf.Abs(expansionMaxEXPValue - (int)playerEXPValue); // Abs : ����
        playerGoldValue -= expansionGoldValue; // ���� exp �� ���� ��� ���� ����       
        expansionGold.text = expansionGoldValue.ToString();
        playerGold.text = string.Format("{0:#,###}", playerGoldValue); // ���� ����

        // Exp �����̴� ��
        playerEXPValue += RoundRewardEXP; // �÷��̾� EXP + �߰� EXP
        playerExpensionLV.text = ("Lv." + expensionLevelValue).ToString(); // ���� �� ������Ʈ
        expansionEXPSlider.value = (playerEXPValue * 0.1f);
        expansionEXPSlider.maxValue = (expansionMaxEXPValue * 0.1f);
    }

    // Game Quit
    public void QuitGame()
    {
        Application.Quit();
    }

}