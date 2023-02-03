using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//using Photon.Pun;
//using Photon.Realtime;

public class UIManager : MonoBehaviour
{
    #region Singleton
    static public UIManager instance;
    static public UIManager INSTANCE
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
                }

            }
            return instance;
        }
    }
    #endregion

    //ScriptableUnit unitData;
    //Player_test player;


    [SerializeField] private TextMeshProUGUI userID;
    [SerializeField] private string playerID;

    [SerializeField] private GameObject roundInfoUI;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject synergyContentsUI;
    [SerializeField] private GameObject rankingContentsUI;
    [SerializeField] private GameObject chattingUI;
    [SerializeField] private GameObject synergyExplainUI;
    [SerializeField] private GameObject selectRoundUI;
    //
    [SerializeField] private GameObject AuctionLoadingUI; // ���
    [SerializeField] private GameObject AuctionRoundUI;
    [SerializeField] private GameObject AuctionResultUI;
    [SerializeField] private GameObject GameResultUI; // ���� ���
    [SerializeField] private GameObject RouletteUI; // �귿
    [SerializeField] private GameObject RouletteResultUI;
    [SerializeField] private GameObject UnitStatusUI;
    [SerializeField] private GameObject UnitStatusInfoUI;
    [SerializeField] private GameObject UnitSkillInfoUI;

    //-----------------------------------------------------------------------------------------------------------

    // Rank Contents
    [SerializeField] private GameObject[] rankUserInfo;

    // Round
    [SerializeField] private TextMeshProUGUI RoundInfoNum;
    [SerializeField] private TextMeshProUGUI RoundStageNum;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum1;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum2;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum3;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum4;

    // Synergy Contents
    [SerializeField] private TextMeshProUGUI SnergyUnitPopup_Name;
    [SerializeField] private TextMeshProUGUI[] SnergyUnitPopup_Info;
    [SerializeField] private Image[] SnergyUnitPopup_Icon;
    // ������ �ڽ��ȿ� ��� ���� : ���� �̸�, ���� ī��Ʈ ��, �����    
    [SerializeField] private Image[] SynergyIcon; // ù��° �ó��� â ������
    [SerializeField] private TextMeshProUGUI[] ClassSynergyName; // Ŭ���� �ó��� �̸�
    [SerializeField] private TextMeshProUGUI[] TribeSynergyName; // ���� �ó��� �̸�
    [SerializeField] private TextMeshProUGUI[] SynergyNum; // ������ ���� �� �� ī��Ʈ �� -> �迭 �����ͷ� �޾ƿ��� 
    [SerializeField] private TextMeshProUGUI Mecha_synergyCountNumber;

    // Expansion Contents
    [SerializeField] private TextMeshProUGUI expansionGold; // Ȯ�� �� ����ϴ� ���
    [SerializeField] private TextMeshProUGUI expensionLV;
    [SerializeField] private Slider expansionEXPSlider;
    // Player Info 
    [SerializeField] private TextMeshProUGUI playerLV;
    [SerializeField] private TextMeshProUGUI playerGold; // �÷��̾ ������ ���
    [SerializeField] private TextMeshProUGUI playerHP;
    [SerializeField] private Slider playerHPSlider;
    // Gacha
    [SerializeField] private TextMeshProUGUI gachaWeponGold;
    [SerializeField] private TextMeshProUGUI gachaUnitGold;

    #region Data �޾ƿ���
    protected void UpdateTribeName()
    {
        TribeSynergyName[0].text = Mecha;
        TribeSynergyName[1].text = Golem;
        TribeSynergyName[2].text = Orc;
        TribeSynergyName[3].text = Demon;
        TribeSynergyName[4].text = Dwarf;
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
    private string TribeName = null; // �޾ƿ� ������ �̸��� ���� ������ �ҷ��´�
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
    private string InputInfo1 = "(3) ���� ���� �� Orc�� �Լ��Ҹ��� �Բ� ��� ������ ���ݷ°� ü���� 5% �϶���Ŵ";
    private string InputInfo2 = "(5) ���� ���� �� Orc�� �Լ� �Ҹ��� �Բ� ��� ������ ���ݷ°� ü���� 15% �϶���Ŵ";

    private int playerGoldValue;
    private float playerEXPValue;
    [SerializeField] private float playerHPValue;
    private int playerMaxHPValue;

    [SerializeField] protected int deployedUnit; // ��ġ�� ����
    private int gachaWeponGoldValue;
    private int gachaUnitGoldValue;
    private int expansionGoldValue;
    private int expansionMaxEXPValue;
    private int expensionLevelValue;

    private int roundStepNumber1 = 1;
    private int roundStepNumber2 = 1;
    private int roundTextColor;
    private int RoundRewardGold = 10;
    private float RoundRewardEXP = 4f;

    private int synergyCount;

    private int TypeSynergyAllCount=5; // ���� ��ü ��
    // ���� �ó��� ī��Ʈ
    private int mechaSynergyCount; // ���� ��
    private int golemSynergyCount;
    private int orcSynergyCount;
    private int demonSynergyCount;
    private int dwarfSynergyCount;
    // Ŭ���� �ó��� ī��Ʈ
    private int warriorSynergyCount;
    private int assassinSynergyCount;
    private int rangedealerSynergyCount;
    private int tankerSynergyCount;
    private int magicianSynergyCount;
    private int SynergyCount3 = 3;
    private int SynergyCount5 = 5;
    
    private bool IsESC { get; set; }
    private bool IsSynergy { get; set; }
    private bool IsRanking { get; set; }
    private bool IsChatting { get; set; }
    private bool IsSynergyEX { get; set; }

    // Specie Synergy bool
    #region Specie Synergy bool
    private bool IsMechaSynergy3 { get; set; }
    private bool IsMechaSynergy5 { get; set; }
    private bool IsGolemSynergy3 { get; set; }
    private bool IsGolemSynergy5 { get; set; }
    private bool IsOrcSynergy3 { get; set; }
    private bool IsOrcSynergy5 { get; set; }
    private bool IsDemonSynergy3 { get; set; }
    private bool IsDemonSynergy5 { get; set; }
    private bool IsDwarfSynergy3 { get; set; }
    private bool IsDwarfSynergy5 { get; set; }
    #endregion
    // Class Synergy bool
    #region Class Synergy bool
    private bool IsWarriorSynergy3 { get; set; }
    private bool IsWarriorSynergy5 { get; set; }
    private bool IsAssassinSynergy3 { get; set; }
    private bool IsAssassinSynergy5 { get; set; }
    private bool IsRangeDealerSynergy3 { get; set; }
    private bool IsRangeDealerSynergy5 { get; set; }
    private bool IsTankerSynergy3 { get; set; }
    private bool IsTankerSynergy5 { get; set; }
    private bool IsMagicianSynergy3 { get; set; }
    private bool IsMagicianSynergy5 { get; set; }
    #endregion

    //protected bool IsCurRound { get; set; }

    // �����̰� �۾���
    protected TimeManager timeManager;
    protected void Awake()
    {
        DontDestroyOnLoad(this);
    }

    protected void Count()
    {
        if (TribeName == Mecha) // �޾ƿ� ������ ��ī�� ��� �ش� �����Ϳ� �´� ���� �ҷ�����
        {            
            SnergyUnitPopup_Name.text = Mecha;
            // �ó��� �۾� ���� �ٲ������ Ȯ��
            SnergyUnitPopup_Info[0].text = InputInfo1; // ���� - �����ֿ� ������ ���� �ʿ��� �����
            SnergyUnitPopup_Info[1].text = InputInfo2;
            if (IsMechaSynergy3)
            {
                ChangeTextColor(SnergyUnitPopup_Info[0]);
                ChangeTextColorInitiate(SnergyUnitPopup_Info[1]);
            }
            if (IsMechaSynergy5)
            {
                ChangeTextColor(SnergyUnitPopup_Info[1]);
                ChangeTextColorInitiate(SnergyUnitPopup_Info[0]);
            }
        }
        // ���� ��ī ���� ���� ���� ������Ʈ ��
        Mecha_synergyCountNumber.text = (mechaSynergyCount +" / "+ TypeSynergyAllCount).ToString();
    }
    protected void Start()
    {
        //unitData = FindObjectOfType<ScriptableUnit>();
        //player = FindObjectOfType<Player_test>();
        //userID.text = unitData.GetUnitName; // user data
        userID.text = playerID;
        playerID = userID.text; // save user id

        // ���� ���� ���
        expansionGoldValue = 10;
        gachaWeponGoldValue = 3;
        gachaUnitGoldValue = 4;
        gachaWeponGold.text = (gachaWeponGoldValue + " G").ToString();
        gachaUnitGold.text = (gachaUnitGoldValue + " G").ToString();
        expansionGold.text = (expansionGoldValue + " G").ToString();

        IsESC = false;
        IsSynergy = false;
        IsRanking = false;
        IsChatting = false;
        IsSynergyEX = false;

        //IsCurRound = true;

        // data �޾ƿ� ��
        playerGoldValue = 1000;//player.GetGold;
        expensionLevelValue = 1;//player.GetLevel;
        playerEXPValue = 0;//player.GetEXP;
        playerLV.text = expensionLevelValue.ToString(); // ���� �ؽ�Ʈ
        expansionEXPSlider.value = playerEXPValue; // �����̴�

        expansionMaxEXPValue = 32; // �ӽ� - ������ �޾ƿ���
        playerMaxHPValue = 1;

        deployedUnit = 3; // �ӽ� - ��ġ�� ���� ��
        RoundStageNum.text = (roundStepNumber1 + "-" + roundStepNumber2).ToString();
        timeManager = FindObjectOfType<TimeManager>();

        RoundDetailInfo();
    }


    protected void Update()
    {
        // �ó��� ī��Ʈ �� üũ
        // Debug.Log("rankingContents.transform.position.x : " + rankingContents.transform.position.x);
        if ((mechaSynergyCount > 2) && (mechaSynergyCount < 5)) { IsMechaSynergy3 = true; }
        if (mechaSynergyCount <= 5) { IsMechaSynergy5 = true; }
        // mechaSynergyCount -> ī��Ʈ ������ �޾ƿ�
        Count();

        if (playerGoldValue <= 0) playerGoldValue = 0;
        playerGold.text = (playerGoldValue + " G").ToString();
        // UpdatePlayerInfo();

        // Game Setting
        if (Input.GetKeyDown(KeyCode.Escape)) SettingMenuESC();

        SynergyContents();
        RankingContents();

        // Round Info
        RoundInfoNum.text = (expensionLevelValue + " / " + deployedUnit).ToString();
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
            // �� ���� ������ �ڵ� 1ȸ ����
            // ����
            //OnFactionExpansion();
        }
        RoundDetailInfo();
        playerHP.text = playerHPValue.ToString();
        playerHPSlider.value = (playerHPValue * 0.1f);
        playerHPSlider.value = (playerMaxHPValue * 0.1f);

        ChangeRankerPosition(rankUserInfo[0].gameObject.transform.position);
        ChangeRankerPosition(rankUserInfo[1].gameObject.transform.position);
        ChangeRankerPosition(rankUserInfo[2].gameObject.transform.position);
        ChangeRankerPosition(rankUserInfo[3].gameObject.transform.position);
    }

    // �׽�Ʈ�� 
    private Vector2 playerRank1 = new Vector2(-4, 175);
    private Vector2 playerRank2 = new Vector2(-4, 60);
    private Vector2 playerRank3 = new Vector2(-4, -55);
    private Vector2 playerRank4 = new Vector2(-4, -170);
    protected void ChangeRankerPosition(Vector2 vec2)
    {
        // �ٸ� �÷��̾�� ������ �� ���� ���� HP�� ��� ���� ����        
        if (playerHPValue >= 50)
        {
            rankUserInfo[0].gameObject.transform.position = playerRank1;
        }
    }
    // IsMine�� �� ���� �ش��ϴ� ��ũ ������Ʈ �̵�
    // �迭�� �÷��̾� ��� (�г���, HP) �޾ƿ���
    // ���� 2,3,4� �ִ� ������Ʈ�� 1�� �ڸ��� �̵��� ��
    // �ٸ� ������Ʈ���� �ϳ��� ������Ʈ �̵��� ���� ������
    // ���� ���ϰ� ������Ʈ �ǵ����� ��
    

    // Round Detail Info
    #region Round Detail Info
    protected void RoundDetailInfo()
    {
        RoundDetailStepNum.text = roundStepNumber1.ToString();
        RoundDetailStepNum1.text = (roundStepNumber1 + "-" + 1).ToString();
        RoundDetailStepNum2.text = (roundStepNumber1 + "-" + 2).ToString();
        RoundDetailStepNum3.text = (roundStepNumber1 + "-" + 3).ToString();
        RoundDetailStepNum4.text = (roundStepNumber1 + "-" + 4).ToString();

        roundTextColor = 0;
        if (roundStepNumber2 == 1) roundTextColor = 1;
        else { ChangeTextColorInitiate(RoundDetailStepNum1); }

        if (roundStepNumber2 == 2) roundTextColor = 2;
        else { ChangeTextColorInitiate(RoundDetailStepNum2); }

        if (roundStepNumber2 == 3) roundTextColor = 3;
        else { ChangeTextColorInitiate(RoundDetailStepNum3); }

        if (roundStepNumber2 == 4) roundTextColor = 4;
        else { ChangeTextColorInitiate(RoundDetailStepNum4); }

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
        //Debug.Log("text color : " + RoundDetailStepNum1.color);
    }
    #endregion

    // Text Color Change
    private void ChangeTextColor(TextMeshProUGUI t) { t.color = Color.yellow; }
    private void ChangeTextColorInitiate(TextMeshProUGUI t) { t.color = Color.gray; }

    // Setting Menu
    #region Setting Menu
    public void OnSettingMenu()
    {
        settingUI.gameObject.SetActive(true);
    }
    public void OffSettingMenu()
    {
        settingUI.gameObject.SetActive(false);
    }
    protected void SettingMenuESC()
    {
        if (!IsESC)
        {
            IsESC = true;
            OnSettingMenu();
        }
        else
        {
            IsESC = false;
            OffSettingMenu();
        }
    }
    public void Surrender()
    {
        Debug.Log("�׺�");
    }
    #endregion
    protected bool IsRound;
    // Round Info Menu
    #region Setting Menu
    public void OnRoundInfoMenu()
    {
        roundInfoUI.gameObject.SetActive(true);
    }
    public void OffRoundInfoMenu()
    {
        roundInfoUI.gameObject.SetActive(false);
    }
    private void Round()
    {
        if (!IsRound)
        {
            IsRound = true;
            OnRoundInfoMenu();
        }
        else
        {
            IsRound = false;
            OffRoundInfoMenu();
        }
    }
    #endregion

    // Synergy Contents UI - Slide
    #region Synergy Contents UI   
    private void SynergyContents()
    {
        if (IsSynergy)
        {
            if (synergyContentsUI.transform.position.x >= 190f) return;
            synergyContentsUI.transform.localPosition = Vector2.Lerp(synergyContentsUI.transform.localPosition, new Vector2(synergyContentsUI.transform.localPosition.x + 10f, synergyContentsUI.transform.localPosition.y), Time.deltaTime * 100f);

        }
        else
        {
            if (synergyContentsUI.transform.position.x <= -150f) return;
            synergyContentsUI.transform.localPosition = Vector2.Lerp(synergyContentsUI.transform.localPosition, new Vector2(synergyContentsUI.transform.localPosition.x - 10f, synergyContentsUI.transform.localPosition.y), Time.deltaTime * 100f);
        }
    }
    public void OnOffSynergyContents()
    {
        if (IsSynergy)
        {
            IsSynergy = false;
        }
        else
        {
            IsSynergy = true;
        }
    }
    #endregion

    // Synergy Explain UI
    #region Synergy Explain UI
    public void OnSynergyExplain()
    {
        synergyExplainUI.gameObject.SetActive(true);
        // ���콺 ��ġ ���� ��ǥ�� �޾Ƽ� ������Ʈ ��쵵��
        // ����Ų �ó��� ȿ�� ������Ʈ�� �ش��ϴ� ������ �ó����� ���� ��
    }
    public void OffSynergyExplain()
    {
        synergyExplainUI.gameObject.SetActive(false);
    }
    private void SynergyExplain()
    {
        if (!IsSynergyEX)
        {
            IsSynergyEX = true;
            OnSynergyExplain();
        }
        else
        {
            IsSynergyEX = false;
            OffSynergyExplain();
        }
    }
    #endregion

    // Ranking Contesnt UI - Slide
    #region Ranking Contesnt UI   

    // ��ġ ���� �ʿ�
    private void RankingContents()
    {
        if (IsRanking)
        {
            if (rankingContentsUI.transform.position.x <= 1750f) return;
            rankingContentsUI.transform.localPosition = Vector2.Lerp(rankingContentsUI.transform.localPosition, new Vector2(rankingContentsUI.transform.localPosition.x - 10f, rankingContentsUI.transform.localPosition.y), Time.deltaTime * 100f);
        }
        else
        {
            if (rankingContentsUI.transform.position.x >= 2060f) return;
            rankingContentsUI.transform.localPosition = Vector2.Lerp(rankingContentsUI.transform.localPosition, new Vector2(rankingContentsUI.transform.localPosition.x + 10f, rankingContentsUI.transform.localPosition.y), Time.deltaTime * 100f);
        }
    }

    public void OnOffRankingContents()
    {
        if (IsRanking)
        {
            IsRanking = false;
        }
        else
        {
            IsRanking = true;
        }
    }
    // ������ ������ �޾ƿͼ� ���� ������� ����
    // �̹��� fill mode
    #endregion

    // Gacha Wepon UI    
    public void OnWeponGacha()
    {
        if (playerGoldValue <= gachaWeponGoldValue) return;
        playerGoldValue -= gachaWeponGoldValue;
    }
    // Gacha Unit UI    
    public void OnUnitGacha()
    {
        if (playerGoldValue <= gachaUnitGoldValue) return;
        playerGoldValue -= gachaUnitGoldValue;
    }

    // Faction Expansion UI
    public void OnFactionExpansion()
    {
        if (playerGoldValue <= expansionGoldValue) return;
        if (playerEXPValue >= expansionMaxEXPValue)
        {
            expensionLevelValue += 1;
            playerLV.text = expensionLevelValue.ToString();
            playerEXPValue = playerEXPValue - expansionMaxEXPValue;
            // 
        }
        playerGoldValue -= expansionGoldValue;
        playerGoldValue += RoundRewardGold;
        playerEXPValue += RoundRewardEXP;

        playerGold.text = playerGoldValue.ToString();
        playerLV.text = expensionLevelValue.ToString(); //
        expansionEXPSlider.value = (playerEXPValue * 0.1f); // �����̴� �� ���� �ʿ�
        expansionEXPSlider.maxValue = (expansionMaxEXPValue * 0.1f);
        Debug.Log("playerEXPValue : " + playerEXPValue);
    }

    #region Setting Menu
    public void OnChattingUI()
    {
        chattingUI.gameObject.SetActive(true);
    }
    public void OffChattingUI()
    {
        chattingUI.gameObject.SetActive(false);
    }
    private void Chatting()
    {
        if (!IsChatting)
        {
            IsChatting = true;
            OnChattingUI();
        }
        else
        {
            IsChatting = false;
            OffChattingUI();
        }
    }
    #endregion


    // Game Quit
    public void QuitGame()
    {
        Application.Quit();
    }
    //
    private void ChangeColor()
    {
        //SynergyName[0].text = Mecha;
        //SynergyName[1].text = Golem;
        //SynergyName[2].text = Orc;
        //SynergyName[3].text = Demon;
        //SynergyName[4].text = Dwarf;
        //SynergyName[5].text = Warrior;
        //SynergyName[6].text = Assassin;
        //SynergyName[7].text = RangeDealer;
        //SynergyName[8].text = Tanker;
        //SynergyName[9].text = Magician;
        // ���� �ó��� üũ -> ��ġ�� ������ �̸��� �ʿ��� ī��Ʈ�ϸ� �޾ƿ� ����ŭ üũ
        // �ó��� 3���� �̻��� ��, 5���� �����϶�
        // �ش� ������ ���� �۾� ���� Ȱ��ȭ



    }
}
