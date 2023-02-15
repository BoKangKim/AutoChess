using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//using Photon.Pun;
//using Photon.Realtime;

public class UIManager : MonoBehaviour
{
    // 참조 연결 -> 기능 돌아가는지 확인
    // 안쓰는 기능 제외
    // 

    #region Canvas
    [Header("[UnitState]")]
    [SerializeField] private GameObject UnitStatusContents;
    [SerializeField] private GameObject UnitStatusInfo;
    [SerializeField] private GameObject UnitStatusDetailInfo;
    [SerializeField] private GameObject UnitSkillInfo;
    //[SerializeField] private GameObject[] UnitSynergy; // 껏다 킬 유닛 시너지 창 //
    [SerializeField] private GameObject[] bettingUnitGrade;  // 베팅된 유닛 등급

    [Header("[Chatting]")]
    [SerializeField] private GameObject ChattingContents;
    [SerializeField] private GameObject ChattingContentsPopup;

    [Header("[Setting]")]
    [SerializeField] private GameObject SettingContents;
    [SerializeField] private GameObject SettingContentsPopup;

    [Header("[Player]")]
    [SerializeField] private TextMeshProUGUI playerExpensionLV;
    [SerializeField] private TextMeshProUGUI playerRankingnLV;
    [SerializeField] private TextMeshProUGUI playerGold; // 플레이어가 소지한 골드    
    //[SerializeField] private TextMeshProUGUI playerID;
    [SerializeField] private Image playerHPSlider;

    [Header("[Round]")]
    [SerializeField] private GameObject RoundContents;
    [SerializeField] private GameObject RoundEXContents;
    [SerializeField] private GameObject RoundBossContents; // 보스라운드
    [SerializeField] private Button[] SelectBossContents;
    [SerializeField] private GameObject RoundResultContents; // 승패 결과
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
    [SerializeField] private TextMeshProUGUI synergyContentsPopupName; // 시너지 팝업창 - 종족 이름
    [SerializeField] private TextMeshProUGUI[] synergyContentsPopupInfo; // 시너지 설명
    [SerializeField] private Image[] SynergyUnitIcon; // 시너지 3마리 들어갈 곳
    [SerializeField] private Image[] SnergyUnitPopupIcon; // 팝업 아이콘 5개
    [SerializeField] private TextMeshProUGUI SynergyPlus; // 보유한 유닛 수 비교 카운트 수 -> 배열 데이터로 받아오기 
    [SerializeField] private GameObject[] ClassSynergy, SpeciesSynergy; // 컨텐츠 오브젝트

    // 컨텐츠 박스안에 담길 정보 : 종족 이름, 종족 카운트 수, 설명글  

    [Header("[Ranking]")]
    [SerializeField] private GameObject rankingContents;
    [SerializeField] private TextMeshProUGUI rankingUserID; // 랭킹 UI에 뜨는 ID  - get data
    [SerializeField] private TextMeshProUGUI rankingUserLV;
    [SerializeField] private GameObject[] rankUserInfo;

    [Header("[Expansion]")]
    [SerializeField] private TextMeshProUGUI expansionUserID; // 확장 UI에 뜨는 ID  - get data
    [SerializeField] private TextMeshProUGUI expansionGold; // 확장 시 사용하는 골드
    [SerializeField] private TextMeshProUGUI expensionLV;
    [SerializeField] private Slider expansionEXPSlider;

    [Header("[Roulette]")]
    [SerializeField] private GameObject RouletteContents; // 룰렛
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
        Debug.Log("항복");
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
        SynergyEXContentsPopup.gameObject.SetActive(false); // 시너지 팝업창
    }

    // Deployed Unit Synergy - Popup
    #region Deployed Unit Synergy 
    private void DeployedUnitSynergy() // 배치된 유닛의 이미지 아이콘 활성화하기
    {
        // 각 시너지마다 해당하는 유닛인지 확인필요 - 임시 적용중
        switch (deployedUnit)
        {
            case 0:
                SynergyEXContents.gameObject.SetActive(false); // 스크롤 뷰에 있는 오브젝트
                SnergyUnitPopupIcon[0].gameObject.SetActive(false);
                for (int i = 0; i < SnergyUnitPopupIcon.Length; i++)
                {
                    OffSynergyPopupAlpha(i);
                }
                break;
            case 1:
                SynergyEXContents.gameObject.SetActive(true);
                SnergyUnitPopupIcon[0].gameObject.SetActive(true); // 시너지 컨텐츠 - 유닛 이미지
                SnergyUnitPopupIcon[1].gameObject.SetActive(false);
                OnSynergyPopupAlpha(0); // 시너지 팝업 컨텐츠 - 설명에 들어가는 유닛 이미지
                OffSynergyPopupAlpha(1);
                break;
            case 2:
                //IsMechaSynergy3 = false;
                SnergyUnitPopupIcon[1].gameObject.SetActive(true);
                SnergyUnitPopupIcon[2].gameObject.SetActive(false);
                ChangeTextColorInitiate(SynergyPlus);
                OnSynergyPopupAlpha(1);
                OffSynergyPopupAlpha(2);
                ChangeTextColorInitiate(synergyContentsPopupInfo[0]); // 설명 글 활성화
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
    // 시너지 팝업창 활성화 / 비활성화
    private void OnSynergyPopupAlpha(int num)
    {
        popupIconColor.a = 1.0f; // 알파값으로 활성화 비활성화 조절
        SnergyUnitPopupIcon[num].color = popupIconColor;
    }
    private void OffSynergyPopupAlpha(int num)
    {
        popupIconColor.a = 0.5f; // 알파값으로 활성화 비활성화 조절
        SnergyUnitPopupIcon[num].color = popupIconColor;
    }
    #endregion

    // Color
    private Color popupIconColor;

    [SerializeField] private string userName = "user1"; // 유저 닉네임 받아올 부분
    public string UserName { get { return userName; } set { userName = value; } }
    [SerializeField] private int deployedUnit; // 배치된 유닛 
    public int DeployedUnit { get { return deployedUnit; } set { deployedUnit = value; } } // 값은 여기로 들어옴

    #region Data 받아오기

    // 새로운 이름을 받을 때마다 콘텐츠가 추가 생성되는 방식으로 수정해야함
    private TextMeshProUGUI[] SpeciesSynergyName, ClassSynergyName;
    private string[] SpeciesNameList = new string[] { "Mecha", "Golem", "Orc", "Demon", "Dwarf" };
    private string[] ClassNameList = new string[] { "Warrior", "Assassin", "RangeDealer", "Tanker", "Magician" };
    private void Init() // 스크롤뷰 콘텐츠 자동 추가 test
    {
        ScrollRect scrollRect = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        Image contentsImage = scrollRect.content.GetChild(0).GetComponent<Image>();

        int Value = 0;
        for (int i = 0; i < 10; i++)
        {
            // item 초기화
            /*item.Item(GetStudyData().result[0].level,
                GetStudyData().result[0].unit,
                GetStudyData().result[0].text_title);*/
            // 위 코드 참고해서 가져올 데이터의 클래스, 종족이름 받아오면 됨.

            // Contents에 생성
            var index = Instantiate(contentsImage, new Vector3(0, 0, 0), Quaternion.identity);
            index.transform.SetParent(GameObject.Find("Content").transform);
            Value -= 200; // 간격 조정
            // Vertical Layout Group 추가 : item들의 위치 간격 자동으로 조정해주기 위함
            // Content Fitter : item 크기 자동으로 맞춰주기 위함
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
    // unit synergy data 받아오기    
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

    private float playerEXPValue, playerMaxHPValue, playerHPValue;
    private int playerGoldValue, gachaWeponGoldValue, gachaUnitGoldValue;
    private int expansionGoldValue, expansionMaxEXPValue, expensionLevelValue;
    private int roundTextColor; // 라운드마다 글씨 색상 바뀜

    public void UpdatePlayerInfo(int playerGold, float playerEXP, int expansionLV, string text, float hp, string nickname)
    {
        // data 받아올 것
        playerGoldValue = playerGold; //player.GetGold;
        playerEXPValue = playerEXP; //player.GetEXP; 0
        expensionLevelValue = expansionLV; //player.GetLevel; 1
        playerExpensionLV.text = text; // 레벨 텍스트
        playerRankingnLV.text = text;
        // player HP
        playerMaxHPValue = hp;
        playerHPValue = playerMaxHPValue;
        // player - data 받을 부분        
        rankingUserID.text = nickname;
        expansionUserID.text = nickname;
    }

    // 시너지 설명 데이터 관리  - 기획서 내용 참고
    private string InputInfo1, InputInfo2 = null;

    public void UpdateSynergyInfo(string info1, string info2) // 스크립터블 데이터로 만들어서 해당 불러오기 - 배열?
    {
        InputInfo1 = info1;
        InputInfo2 = info2;

        // 정보에 따라 다른 정보가 들어갈 수 있어야 함.
        synergyContentsPopupInfo[0].text = InputInfo1;
        synergyContentsPopupInfo[1].text = InputInfo2;
    }

    // Round Detail Info : 1-1 1-2 1-3 1-4 라운드 정보 색상 효과
    #region Round Detail Info

    private int roundStepNumber1, roundStepNumber2;

    public void UpdateRoundInfo(int col, int row) // 행렬 공부해
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

        // 이미지 색상 순서 수정 필요
        roundTextColor = 0;
        if (row == 1) roundTextColor = 1;
        else { ChangeTextColorInitiate(RoundDetailStepNum1); }

        if (row == 2) roundTextColor = 2;
        else { ChangeTextColorInitiate(RoundDetailStepNum2); }

        if (row == 3) roundTextColor = 3;
        else { ChangeTextColorInitiate(RoundDetailStepNum3); }

        if (row == 4) // 마지막 라운드 때마다 보스 라운드 활성화
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

    private int RoundRewardGold; // 한 라운드 돌아갈때마다 받을 골드 - 연승 연패 이자 있는지 확인하고 넣기
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
        //TryGetComponent<TimeManager>(out timeManager); // 작동이 안되서 일단 주석
        timeManager = FindObjectOfType<TimeManager>();
        // 고정 지불 비용 - ?
        gachaWeponGoldValue = 3;
        gachaUnitGoldValue = 4;
        gachaWeponGold.text = gachaWeponGoldValue.ToString();
        gachaUnitGold.text = gachaUnitGoldValue.ToString();

        IsDead = false;

        UpdatePlayerInfo(1000, 0, 1, ("Lv." + expensionLevelValue).ToString(), 100f, UserName);
        UpdateSynergyInfo("(3) 전투 시작 시 Orc의 함성소리와 함께 상대 유닛의 공격력과 체력을 5% 하락시킴", "(5) 전투 시작 시 Orc의 함성 소리와 함께 상대 유닛의 공격력과 체력을 15% 하락시킴");
        UpdateRoundInfo(1, 1);
        UpdateRoundReward(10, 4);

        expansionEXPSlider.value = playerEXPValue; // 슬라이더
        expansionMaxEXPValue = 32; // 임시 - 데이터 받아오기 - 수정필요
        expansionGoldValue = expansionMaxEXPValue;
        expansionGold.text = expansionGoldValue.ToString();

        deployedUnit = 0; // 임시 - 배치된 유닛 수

        //color
        popupIconColor = SnergyUnitPopupIcon[0].GetComponent<Image>().color;
        synergyContentsPopupName.text = Mecha; // 나중에 받아와서 업데이트

        RoundType(roundType);
    }
    public void UpdateExpansionInfo()
    {

    }

    void Update()
    {
        // 배치된 유닛에 따른 시너지 확인
        DeployedUnitSynergy();

        // 업데이트 문 줄여야 함
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
            //playerGoldValue += RoundRewardGold; // 한라운드당 골드 지급
        }
        UpdateRoundInfo(roundStepNumber1, roundStepNumber2); // 라운드정보 받아올 곳에서 씀               
        // 체력에 따른 랭킹 컨텐츠 위치 이동
        ChangeRankerPosition();

    }

    // 보스라운드 선택하면 선택한 보스 등장, 효과 적용
    // 선택한 번호에 따라 적용되는 효과, 등장하는 보스 다름

    public void SelectBoss_1()
    {
        // SelectBossContents[0].enabled = false;
        Debug.Log("1번 보스 선택 / 효과 적용 1 : ");
    }
    public void SelectBoss_2()
    {
        // SelectBossContents[1].enabled = false;
        Debug.Log("1번 보스 선택 / 효과 적용 2 : ");
    }
    public void SelectBoss_3()
    {
        // SelectBossContents[2].enabled = false;
        Debug.Log("1번 보스 선택 / 효과 적용 3 : ");
    }

    // test
    #region test
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
        if (deployedUnit >= 5) deployedUnit = 5;
    }
    public void UnitTest_Minus()
    {
        deployedUnit--;
        if (deployedUnit <= 0) deployedUnit = 0;
    }
    protected void ChangeRankerPosition()
    {
        // 다른 플레이어와 비교했을 때 가장 높은 HP일 경우 상위 노출             
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
            Debug.Log("죽음 - 비활성화 표시 필요");
        }
        // 죽은 플레이어 가장 아래로 내리고 비활성화 효과 주기
        // 죽은 플레이어가 여러명일 때 -> 가장 먼저 죽은 순서대로 밑에 차곡차곡 쌓임
        // 랭킹 컨텐츠 : HP 정보가 바뀌는 곳에서만 업데이트
        playerRankingnLV.text = ("Lv." + expensionLevelValue).ToString(); // 레벨 값 업데이트
        playerHPSlider.fillAmount = playerHPValue / playerMaxHPValue;
    }
    #endregion
    //

    // Text Color Change
    // 유닛 추가 -> 2마리째부터 참조를 못해옴 
    // 설명 글씨 색상 안바뀜
    private void ChangeTextColor(TextMeshProUGUI t) { t.color = Color.yellow; }
    private void ChangeTextColorInitiate(TextMeshProUGUI t) { t.color = Color.gray; }

    // Gacha Wepon UI    
    public void OnWeponGacha()
    {
        if (playerGoldValue <= gachaWeponGoldValue) return;
        playerGoldValue -= gachaWeponGoldValue;
        playerGold.text = string.Format("{0:#,###}", playerGoldValue); // 4자릿수 넘어가면 , 표시
    }
    // Gacha Unit UI    
    public void OnUnitGacha()
    {
        if (playerGoldValue <= gachaUnitGoldValue) return;
        playerGoldValue -= gachaUnitGoldValue;
        playerGold.text = string.Format("{0:#,###}", playerGoldValue); // 4자릿수 넘어가면 , 표시
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
        expansionGoldValue = Mathf.Abs(expansionMaxEXPValue - (int)playerEXPValue); // Abs : 절댓값
        playerGoldValue -= expansionGoldValue; // 남은 exp 에 따른 골드 차감 변경       
        expansionGold.text = expansionGoldValue.ToString();
        playerGold.text = string.Format("{0:#,###}", playerGoldValue); // 단위 끊기

        // Exp 슬라이더 값
        playerEXPValue += RoundRewardEXP; // 플레이어 EXP + 추가 EXP
        playerExpensionLV.text = ("Lv." + expensionLevelValue).ToString(); // 레벨 값 업데이트
        expansionEXPSlider.value = (playerEXPValue * 0.1f);
        expansionEXPSlider.maxValue = (expansionMaxEXPValue * 0.1f);
    }

    // Game Quit
    public void QuitGame()
    {
        Application.Quit();
    }

}