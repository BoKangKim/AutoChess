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
    [SerializeField] private GameObject AuctionLoadingUI; // 경매
    [SerializeField] private GameObject AuctionRoundUI;
    [SerializeField] private GameObject AuctionResultUI;
    [SerializeField] private GameObject GameResultUI; // 승패 결과
    [SerializeField] private GameObject RouletteUI; // 룰렛
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
    // 컨텐츠 박스안에 담길 정보 : 종족 이름, 종족 카운트 수, 설명글    
    [SerializeField] private Image[] SynergyIcon; // 첫번째 시너지 창 아이콘
    [SerializeField] private TextMeshProUGUI[] ClassSynergyName; // 클래스 시너지 이름
    [SerializeField] private TextMeshProUGUI[] TribeSynergyName; // 종족 시너지 이름
    [SerializeField] private TextMeshProUGUI[] SynergyNum; // 보유한 유닛 수 비교 카운트 수 -> 배열 데이터로 받아오기 
    [SerializeField] private TextMeshProUGUI Mecha_synergyCountNumber;

    // Expansion Contents
    [SerializeField] private TextMeshProUGUI expansionGold; // 확장 시 사용하는 골드
    [SerializeField] private TextMeshProUGUI expensionLV;
    [SerializeField] private Slider expansionEXPSlider;
    // Player Info 
    [SerializeField] private TextMeshProUGUI playerLV;
    [SerializeField] private TextMeshProUGUI playerGold; // 플레이어가 소지한 골드
    [SerializeField] private TextMeshProUGUI playerHP;
    [SerializeField] private Slider playerHPSlider;
    // Gacha
    [SerializeField] private TextMeshProUGUI gachaWeponGold;
    [SerializeField] private TextMeshProUGUI gachaUnitGold;

    #region Data 받아오기
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
    // unit synergy data 받아오기
    private string TribeName = null; // 받아온 정보의 이름이 같은 정보를 불러온다
    private string Mecha = "Mecha";
    private string Golem = "Golem";
    private string Orc = "Orc";
    private string Demon = "Demon";
    private string Dwarf = "Dwarf";
    // class synergy data 받아오기
    private string Warrior = "Warrior";
    private string Assassin = "Assassin";
    private string RangeDealer = "RangeDealer";
    private string Tanker = "Tanker";
    private string Magician = "Magician";
    #endregion
    private string InputInfo1 = "(3) 전투 시작 시 Orc의 함성소리와 함께 상대 유닛의 공격력과 체력을 5% 하락시킴";
    private string InputInfo2 = "(5) 전투 시작 시 Orc의 함성 소리와 함께 상대 유닛의 공격력과 체력을 15% 하락시킴";

    private int playerGoldValue;
    private float playerEXPValue;
    [SerializeField] private float playerHPValue;
    private int playerMaxHPValue;

    [SerializeField] protected int deployedUnit; // 배치된 유닛
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

    private int TypeSynergyAllCount=5; // 공통 전체 수
    // 종류 시너지 카운트
    private int mechaSynergyCount; // 보유 수
    private int golemSynergyCount;
    private int orcSynergyCount;
    private int demonSynergyCount;
    private int dwarfSynergyCount;
    // 클래스 시너지 카운트
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

    // 보강이가 작업중
    protected TimeManager timeManager;
    protected void Awake()
    {
        DontDestroyOnLoad(this);
    }

    protected void Count()
    {
        if (TribeName == Mecha) // 받아온 정보가 메카일 경우 해당 데이터에 맞는 정보 불러오기
        {            
            SnergyUnitPopup_Name.text = Mecha;
            // 시너지 글씨 색상 바뀌는지만 확인
            SnergyUnitPopup_Info[0].text = InputInfo1; // 내용 - 다음주에 데이터 관리 쪽에서 물어볼것
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
        // 현재 메카 보유 수에 따라 업데이트 됨
        Mecha_synergyCountNumber.text = (mechaSynergyCount +" / "+ TypeSynergyAllCount).ToString();
    }
    protected void Start()
    {
        //unitData = FindObjectOfType<ScriptableUnit>();
        //player = FindObjectOfType<Player_test>();
        //userID.text = unitData.GetUnitName; // user data
        userID.text = playerID;
        playerID = userID.text; // save user id

        // 고정 지불 비용
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

        // data 받아올 것
        playerGoldValue = 1000;//player.GetGold;
        expensionLevelValue = 1;//player.GetLevel;
        playerEXPValue = 0;//player.GetEXP;
        playerLV.text = expensionLevelValue.ToString(); // 레벨 텍스트
        expansionEXPSlider.value = playerEXPValue; // 슬라이더

        expansionMaxEXPValue = 32; // 임시 - 데이터 받아오기
        playerMaxHPValue = 1;

        deployedUnit = 3; // 임시 - 배치된 유닛 수
        RoundStageNum.text = (roundStepNumber1 + "-" + roundStepNumber2).ToString();
        timeManager = FindObjectOfType<TimeManager>();

        RoundDetailInfo();
    }


    protected void Update()
    {
        // 시너지 카운트 수 체크
        // Debug.Log("rankingContents.transform.position.x : " + rankingContents.transform.position.x);
        if ((mechaSynergyCount > 2) && (mechaSynergyCount < 5)) { IsMechaSynergy3 = true; }
        if (mechaSynergyCount <= 5) { IsMechaSynergy5 = true; }
        // mechaSynergyCount -> 카운트 갯수를 받아옴
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
            // 한 라운드 끝나면 자동 1회 지급
            // 조건
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

    // 테스트용 
    private Vector2 playerRank1 = new Vector2(-4, 175);
    private Vector2 playerRank2 = new Vector2(-4, 60);
    private Vector2 playerRank3 = new Vector2(-4, -55);
    private Vector2 playerRank4 = new Vector2(-4, -170);
    protected void ChangeRankerPosition(Vector2 vec2)
    {
        // 다른 플레이어와 비교했을 때 가장 높은 HP일 경우 상위 노출        
        if (playerHPValue >= 50)
        {
            rankUserInfo[0].gameObject.transform.position = playerRank1;
        }
    }
    // IsMine일 때 나에 해당하는 랭크 오브젝트 이동
    // 배열로 플레이어 목록 (닉네임, HP) 받아오기
    // 만약 2,3,4등에 있던 오브젝트가 1등 자리로 이동할 때
    // 다른 오브젝트들은 하나의 오브젝트 이동이 있을 때마다
    // 값을 비교하고 업데이트 되도록할 것
    

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
        Debug.Log("항복");
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
        // 마우스 위치 기준 좌표를 받아서 오브젝트 띄우도록
        // 가리킨 시너지 효과 오브젝트가 해당하는 종족의 시너지와 같을 것
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

    // 수치 조절 필요
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
    // 유저의 라이프 받아와서 높은 순서대로 정렬
    // 이미지 fill mode
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
        expansionEXPSlider.value = (playerEXPValue * 0.1f); // 슬라이더 값 조절 필요
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
        // 현재 시너지 체크 -> 배치된 유닛의 이름을 맵에서 카운트하면 받아온 수만큼 체크
        // 시너지 3마리 이상일 때, 5마리 이하일때
        // 해당 종류인 애의 글씨 설명 활성화



    }
}
