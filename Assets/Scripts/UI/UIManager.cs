using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//using Photon.Pun;
//using Photon.Realtime;

public class UIManager : MonoBehaviour
{
    // 시너지 분류 
    enum ClassSynergy
    {
        Warrior,
        Assassin,
        RangeDealer,
        Tanker,
        Magician
    }
    enum TypeSnergy
    {
        Mecha,
        Golem,
        Orc,
        Demon,
        Dwarf
    }

    // [SerializeField] private TextMeshProUGUI userID;
    // [SerializeField] private string playerID;

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

    [SerializeField] private GameObject[] UnitSynergyUI; // 껏다 킬 유닛 시너지 창

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
    [SerializeField] private TextMeshProUGUI SnergyUnitPopup_Name; // 시너지 팝업창 - 종족 이름
    [SerializeField] private TextMeshProUGUI[] SnergyUnitPopup_Info;
    [SerializeField] private Image[] SnergyUnitPopup_Icon;
    // 컨텐츠 박스안에 담길 정보 : 종족 이름, 종족 카운트 수, 설명글    
    [SerializeField] private Image[] SynergyIcon; // 첫번째 시너지 창 아이콘
    [SerializeField] private TextMeshProUGUI[] ClassSynergyName; // 클래스 시너지 이름
    [SerializeField] private TextMeshProUGUI[] TribeSynergyName; // 종족 시너지 이름
    [SerializeField] private TextMeshProUGUI SynergyPlus; // 보유한 유닛 수 비교 카운트 수 -> 배열 데이터로 받아오기 
    [SerializeField] private Image[] SynergyImage;
    //[SerializeField] private TextMeshProUGUI Mecha_synergyCountNumber;

    // Expansion Contents
    [SerializeField] private TextMeshProUGUI expansionGold; // 확장 시 사용하는 골드
    [SerializeField] private TextMeshProUGUI expensionLV;
    [SerializeField] private Slider expansionEXPSlider;
    // Player Info 
    [SerializeField] private TextMeshProUGUI playerExpensionLV;
    [SerializeField] private TextMeshProUGUI playerRankingnLV;
    [SerializeField] private TextMeshProUGUI playerGold; // 플레이어가 소지한 골드    
    [SerializeField] private Image playerHPSlider;
    // Gacha
    [SerializeField] private TextMeshProUGUI gachaWeponGold;
    [SerializeField] private TextMeshProUGUI gachaUnitGold;

    // Color
    private Color popupIconColor;

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
    // 
    private string InputInfo1 = "(3) 전투 시작 시 Orc의 함성소리와 함께 상대 유닛의 공격력과 체력을 5% 하락시킴";
    private string InputInfo2 = "(5) 전투 시작 시 Orc의 함성 소리와 함께 상대 유닛의 공격력과 체력을 15% 하락시킴";

    private int playerGoldValue;
    private float playerEXPValue;
    private float playerMaxHPValue;
    private float playerHPValue;

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

    private int TypeSynergyAllCount = 5; // 공통 전체 수
    // 종류 시너지 카운트
    private int mechaSynergyCount; // 보유 수
    public void TypeSynergyCount(int num)
    {
        // 배치된 유닛에 따른 타입 시너지 계산
    }
    private int SynergyCount3 = 3;
    private int SynergyCount5 = 5;
    // 클래스 시너지 카운트
    public void ClassSynergyCount(int num)
    {
        // 배치된 유닛에 따른 클래스 시너지 계산
    }
    private bool IsESC { get; set; }
    private bool IsSynergy { get; set; }
    private bool IsRanking { get; set; }
    private bool IsChatting { get; set; }
    private bool IsSynergyEX { get; set; }
    private bool IsDead { get; set; }

    // Specie Synergy bool
    #region Specie Synergy bool
    private bool IsSynergy3 { get; set; }
    private bool IsSynergy5 { get; set; }

    #endregion

    protected TimeManager timeManager; // 보강이가 작업중
    private void Awake()
    {
        // DontDestroyOnLoad(this);
    }

    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        // 고정 지불 비용
        gachaWeponGoldValue = 3;
        gachaUnitGoldValue = 4;
        gachaWeponGold.text = gachaWeponGoldValue.ToString();
        gachaUnitGold.text = gachaUnitGoldValue.ToString();

        IsESC = false;
        IsSynergy = false;
        IsSynergy3 = false;
        IsSynergy5 = false;
        IsRanking = false;
        IsChatting = false;
        IsSynergyEX = false;
        IsDead = false;

        // data 받아올 것
        playerGoldValue = 333333;//player.GetGold;
        expensionLevelValue = 1;//player.GetLevel;
        playerEXPValue = 0;//player.GetEXP;
        playerExpensionLV.text = ("Lv." + expensionLevelValue).ToString(); // 레벨 텍스트
        playerRankingnLV.text = ("Lv." + expensionLevelValue).ToString();
        expansionEXPSlider.value = playerEXPValue; // 슬라이더

        expansionMaxEXPValue = 32; // 임시 - 데이터 받아오기 - 수정필요
        expansionGoldValue = expansionMaxEXPValue;
        expansionGold.text = expansionGoldValue.ToString();

        deployedUnit = 0; // 임시 - 배치된 유닛 수
        RoundStageNum.text = (roundStepNumber1 + "-" + roundStepNumber2).ToString();

        RoundDetailInfo();

        // 유닛 시너지 컨텐츠 UI         
        for (int i = 0; i < UnitSynergyUI.Length; i++)
        {
            UnitSynergyUI[i].gameObject.SetActive(false);
        }

        // 정보에 따라 다른 정보가 들어갈 수 있어야 함.
        SnergyUnitPopup_Info[0].text = InputInfo1;
        SnergyUnitPopup_Info[1].text = InputInfo2;

        //color
        popupIconColor = SnergyUnitPopup_Icon[0].GetComponent<Image>().color;

        SnergyUnitPopup_Name.text = Mecha; // 나중에 받아와서 업데이트

        // player HP
        playerMaxHPValue = 100f;
        playerHPValue = playerMaxHPValue;
    }



    protected void Update()
    {
        // 배치된 유닛에 따른 시너지 확인
        DeployedUnitSynergy();

        if (IsSynergy3) Debug.Log("시너지 3 적용");
        else Debug.Log("시너지 3 적용 취소");

        if (IsSynergy5) Debug.Log("시너지 5 적용");
        else Debug.Log("시너지 5 적용 취소");


        if (playerGoldValue <= 0) playerGoldValue = 0;
        playerGold.text = string.Format("{0:#,###}", playerGoldValue); // 4자릿수 넘어가면 , 표시

        if (playerHPValue <= 0) IsDead=true;

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
            //playerGoldValue += RoundRewardGold; // 한라운드당 골드 지급
        }
        RoundDetailInfo();

        expansionGold.text = expansionGoldValue.ToString();

        // 랭킹 컨텐츠
        playerRankingnLV.text = ("Lv." + expensionLevelValue).ToString(); // 레벨 값 업데이트
        playerHPSlider.fillAmount = playerHPValue / playerMaxHPValue;
        // 체력에 따른 랭킹 컨텐츠 위치 이동
        ChangeRankerPosition();
    }

    private float attackPower = 10f; // 데이터 들어올 부분
    private float healPower = 10f;
    public void AttackTest() // test 중
    {
        if (playerHPValue <= 0) playerHPValue = 0; // 죽음
        playerHPValue -= attackPower;
    }
    public void HealTest() // test 중
    {
        Debug.Log("힐");
        if (playerHPValue >= playerMaxHPValue) playerHPValue = playerMaxHPValue;
        playerHPValue += healPower;
    }
    public void UnitTest_Plus() // test 중
    {
        deployedUnit++;
        if (deployedUnit >= 5)
        {
            deployedUnit = 5;
        }
    }
    public void UnitTest_Minus()
    {
        deployedUnit--;
        if (deployedUnit <= 0)
        {
            deployedUnit = 0;
        }
    }

    Color rendererColor;

    protected void ChangeRankerPosition()
    {
        rendererColor = rankUserInfo[0].gameObject.GetComponent<Image>().color;
        // 다른 플레이어와 비교했을 때 가장 높은 HP일 경우 상위 노출        
        rendererColor.a = 1.0f;
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
            rendererColor.a = 0.5f;
        }

        // 죽은 플레이어 가장 아래로 내리고 비활성화 효과 주기
        // 죽은 플레이어가 여러명일 때 -> 가장 먼저 죽은 순서대로 밑에 차곡차곡 쌓임

         /*if ((playerHPValue >= 50f) ||(playerHPValue < 70f))
         {
             rankUserInfo[0].transform.localPosition = new Vector2(125, 40);
         }
         if ((playerHPValue >= 30f) || (playerHPValue < 50f))
         {
             rankUserInfo[0].transform.localPosition = new Vector2(125, -80);
         }
         if ((playerHPValue >= 0f) || (playerHPValue < 30f))
         {
             rankUserInfo[0].transform.localPosition = new Vector2(125, -200);
         }*/
       

    }
    // IsMine일 때 나에 해당하는 랭크 오브젝트 이동
    // 배열로 플레이어 목록 (닉네임, HP) 받아오기
    // 만약 2,3,4등에 있던 오브젝트가 1등 자리로 이동할 때
    // 다른 오브젝트들은 하나의 오브젝트 이동이 있을 때마다
    // 값을 비교하고 업데이트 되도록할 것


    // Round Detail Info : 1-1 1-2 1-3 1-4 라운드 정보 색상 효과
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
            if (synergyContentsUI.transform.position.x >= 270f) return;
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

    // Deployed Unit Synergy - Popup
    #region Deployed Unit Synergy 
    private void DeployedUnitSynergy()
    {
        // 각 시너지마다 해당하는 유닛인지 확인필요 - 임시 적용중
        switch (deployedUnit)
        {
            case 0:
                UnitSynergyUI[0].gameObject.SetActive(false);
                SynergyImage[0].gameObject.SetActive(false);
                for (int i = 0; i < SnergyUnitPopup_Icon.Length; i++)
                {
                    OffSynergyPopupAlpha(i);
                }
                break;
            case 1:
                UnitSynergyUI[0].gameObject.SetActive(true);
                SynergyImage[0].gameObject.SetActive(true); // 시너지 컨텐츠 - 유닛 이미지
                SynergyImage[1].gameObject.SetActive(false);
                OnSynergyPopupAlpha(0); // 시너지 팝업 컨텐츠 - 설명에 들어가는 유닛 이미지
                OffSynergyPopupAlpha(1);
                break;
            case 2:
                //IsMechaSynergy3 = false;
                SynergyImage[1].gameObject.SetActive(true);
                SynergyImage[2].gameObject.SetActive(false);
                ChangeTextColorInitiate(SynergyPlus);
                OnSynergyPopupAlpha(1);
                OffSynergyPopupAlpha(2);
                ChangeTextColorInitiate(SnergyUnitPopup_Info[0]); // 설명 글 활성화
                break;
            case 3:
                //IsMechaSynergy3 = true;
                SynergyImage[2].gameObject.SetActive(true);
                ChangeTextColor(SynergyPlus);
                OnSynergyPopupAlpha(2);
                OffSynergyPopupAlpha(3);
                ChangeTextColor(SnergyUnitPopup_Info[0]);
                break;
            case 4:
                //IsMechaSynergy5 = false;
                OnSynergyPopupAlpha(3);
                OffSynergyPopupAlpha(4);
                ChangeTextColorInitiate(SnergyUnitPopup_Info[1]);
                break;
            case 5:
                //IsMechaSynergy5 = true;
                OnSynergyPopupAlpha(4);
                ChangeTextColor(SnergyUnitPopup_Info[1]);
                ChangeTextColorInitiate(SnergyUnitPopup_Info[0]);
                break;
        }
    }
    // 시너지 팝업창 활성화 / 비활성화
    private void OnSynergyPopupAlpha(int num)
    {
        popupIconColor.a = 1.0f; // 알파값으로 활성화 비활성화 조절
        SnergyUnitPopup_Icon[num].color = popupIconColor;
    }
    private void OffSynergyPopupAlpha(int num)
    {
        popupIconColor.a = 0.5f; // 알파값으로 활성화 비활성화 조절
        SnergyUnitPopup_Icon[num].color = popupIconColor;
    }
    #endregion

    // Ranking Contesnt UI - Slide
    #region Ranking Contesnt UI  
    private void RankingContents()
    {
        if (IsRanking)
        {
            if (rankingContentsUI.transform.position.x <= 1650f) return;
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
            playerEXPValue = playerEXPValue - expansionMaxEXPValue;
        }
        expansionGoldValue = Mathf.Abs(expansionMaxEXPValue - (int)playerEXPValue);
        playerGoldValue -= expansionGoldValue; // 남은 exp 에 따른 골드 차감 변경       
        playerEXPValue += RoundRewardEXP;
        playerGold.text = string.Format("{0:#,###}", playerGoldValue); // 단위 끊기
        playerExpensionLV.text = ("Lv." + expensionLevelValue).ToString(); // 레벨 값 업데이트
        expansionEXPSlider.value = (playerEXPValue * 0.1f); // 슬라이더 값 조절 필요
        expansionEXPSlider.maxValue = (expansionMaxEXPValue * 0.1f);

    }

    // Setting Menu
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
