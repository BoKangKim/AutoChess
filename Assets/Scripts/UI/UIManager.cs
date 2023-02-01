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
    Player_test player;

    [SerializeField] TextMeshProUGUI userID;
    [SerializeField] string playerID;

    [SerializeField] GameObject roundInfoUI;
    [SerializeField] GameObject settingWindow;
    [SerializeField] GameObject synergyContents;
    [SerializeField] GameObject rankingContents;

    [SerializeField] TextMeshProUGUI RoundInfoNum;
    [SerializeField] TextMeshProUGUI RoundStageNum;
    [SerializeField] TextMeshProUGUI RoundDetailStepNum;
    [SerializeField] TextMeshProUGUI RoundDetailStepNum1;
    [SerializeField] TextMeshProUGUI RoundDetailStepNum2;
    [SerializeField] TextMeshProUGUI RoundDetailStepNum3;
    [SerializeField] TextMeshProUGUI RoundDetailStepNum4;
    [SerializeField] TextMeshProUGUI expansionGold; // 확장 시 사용하는 골드
    [SerializeField] TextMeshProUGUI playerLV; 
    [SerializeField] TextMeshProUGUI playerGold; // 플레이어가 소지한 골드
    [SerializeField] TextMeshProUGUI playerHP; 
    [SerializeField] TextMeshProUGUI gachaWeponGold;
    [SerializeField] TextMeshProUGUI gachaUnitGold;
    [SerializeField] Slider expansionEXPSlider; //
    [SerializeField] Slider playerHPSlider; //

    protected int playerGoldValue;
    protected int playerLevelValue;
    protected float playerEXPValue;
    protected float playerHPValue;
    protected int playerMaxHPValue;

    protected int deployedUnit; // 배치된 유닛
    protected int gachaWeponGoldValue;
    protected int gachaUnitGoldValue;
    protected int expansionGoldValue;
    protected int expansionMaxEXPValue;

    protected int roundStepNumber1 = 1;
    protected int roundStepNumber2 = 1;
    protected int roundTextColor;
    protected int RoundRewardGold = 10;
    protected float RoundRewardEXP = 4f;
    protected bool IsESC { get; set; }
    protected bool IsSynergy { get; set; }
    protected bool IsRanking { get; set; }
    //protected bool IsCurRound { get; set; }

    TimeManager timeManager;
    protected void Awake()
    {
        DontDestroyOnLoad(this);
    }
    protected void Start()
    {
        //unitData = FindObjectOfType<ScriptableUnit>();
        player = FindObjectOfType<Player_test>();
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
        //IsCurRound = true;

        // data 받아올 것
        playerGoldValue = 1000;//player.GetGold;
        playerLevelValue = 1;//player.GetLevel;
        playerEXPValue = 0;//player.GetEXP;
        playerLV.text = playerLevelValue.ToString(); // 레벨 텍스트
        expansionEXPSlider.value = playerEXPValue; // 슬라이더
        
        expansionMaxEXPValue = 32; // 임시 - 데이터 받아오기
        playerMaxHPValue = 1; 

        deployedUnit = 3; // 임시
        RoundStageNum.text = (roundStepNumber1 + "-" + roundStepNumber2).ToString();
        timeManager = FindObjectOfType<TimeManager>();

        RoundDetailInfo();
    }
    

    protected void Update()
    {
        if (playerGoldValue <= 0) playerGoldValue = 0;
        playerGold.text = (playerGoldValue + " G").ToString();
        // UpdatePlayerInfo();

        // Game Setting
        if (Input.GetKeyDown(KeyCode.Escape)) SettingMenuESC();

        SynergyContents();
        RankingContents();
       
        // Round Info
        RoundInfoNum.text = (playerLevelValue + " / " + deployedUnit).ToString();
        // timer = 0 -> Next Round
        if (timeManager.currentTime<=0f)
        {
            if ((roundStepNumber1 >= 9)&& (roundStepNumber2 >= 4))
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
        // 다른 플레이어와 비교했을 때 가장 높은 HP일 경우 상위 노출
        
    }
   
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
        else { ChangeRoundColorInitiate(RoundDetailStepNum1); }
        
        if (roundStepNumber2 == 2) roundTextColor = 2;
        else { ChangeRoundColorInitiate(RoundDetailStepNum2); }

        if (roundStepNumber2 == 3) roundTextColor = 3;
        else { ChangeRoundColorInitiate(RoundDetailStepNum3); }

        if (roundStepNumber2 == 4) roundTextColor = 4;
        else { ChangeRoundColorInitiate(RoundDetailStepNum4); }

        switch (roundTextColor) 
        {
            case 1:
                ChangeRoundColor(RoundDetailStepNum1);
                break;                
            case 2:
                ChangeRoundColor(RoundDetailStepNum2);
                break;
            case 3:
                ChangeRoundColor(RoundDetailStepNum3);
                break;
            case 4:
                ChangeRoundColor(RoundDetailStepNum4);
                break;
        }
        Debug.Log("text color : " + RoundDetailStepNum1.color);
    }
    #endregion

    // Text Color Change
    protected void ChangeRoundColor(TextMeshProUGUI t) { t.color = Color.yellow; }
    protected void ChangeRoundColorInitiate(TextMeshProUGUI t) { t.color = Color.gray; }
  
    // Setting Menu
    #region Setting Menu
    public void OnSettingMenu()
    {
        settingWindow.gameObject.SetActive(true);
    }
    public void OffSettingMenu()
    {
        settingWindow.gameObject.SetActive(false);
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
    protected void Round()
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

    // Synergy Contents UI
    #region Synergy Contents UI   
    protected void SynergyContents()
    {
        if (IsSynergy)
        {
            if (synergyContents.transform.position.x >= -7f) return;
            synergyContents.transform.localPosition = Vector2.Lerp(synergyContents.transform.localPosition, new Vector2(synergyContents.transform.localPosition.x + 10f, synergyContents.transform.localPosition.y), Time.deltaTime * 50f);

        }
        else
        {
            if (synergyContents.transform.position.x <= -10.2f) return;
            synergyContents.transform.localPosition = Vector2.Lerp(synergyContents.transform.localPosition, new Vector2(synergyContents.transform.localPosition.x - 10f, synergyContents.transform.localPosition.y), Time.deltaTime * 50f);
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

    // Ranking Contesnt UI
    #region Ranking Contesnt UI   

    protected void RankingContents()
    {
        if (IsRanking)
        {
            if (rankingContents.transform.position.x <= 7f) return;
            rankingContents.transform.localPosition = Vector2.Lerp(rankingContents.transform.localPosition, new Vector2(rankingContents.transform.localPosition.x - 10f, rankingContents.transform.localPosition.y), Time.deltaTime * 50f);
        }
        else
        {
            if (rankingContents.transform.position.x >= 10.2f) return;
            rankingContents.transform.localPosition = Vector2.Lerp(rankingContents.transform.localPosition, new Vector2(rankingContents.transform.localPosition.x + 10f, rankingContents.transform.localPosition.y), Time.deltaTime * 50f);
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
    protected int MixRandomWepon;
    public void OnWeponGacha()
    {
        if (playerGoldValue <= gachaWeponGoldValue) return;        
        playerGoldValue -= gachaWeponGoldValue;
    }
    // Gacha Unit UI
    protected int MixRandomUnit;
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
            playerLevelValue += 1;
            playerLV.text = playerLevelValue.ToString();
            playerEXPValue = playerEXPValue - expansionMaxEXPValue;
            // 
        }        
            playerGoldValue -= expansionGoldValue;
            playerGoldValue += RoundRewardGold;
            playerEXPValue += RoundRewardEXP;
       
        playerGold.text = playerGoldValue.ToString();
        playerLV.text = playerLevelValue.ToString(); //
        expansionEXPSlider.value = (playerEXPValue * 0.1f); // 슬라이더 값 조절 필요
        expansionEXPSlider.maxValue = (expansionMaxEXPValue * 0.1f);
        Debug.Log("playerEXPValue : " + playerEXPValue);        
    }

    protected void WeponRandomCalculation()
    {
        // 25%
        // 1성 4종
        // safety zone 에 배치
        // 예외처리 : 여유 공간 없음
        // 맵 스크립트 참고
        // MixRandomUnit = Random.Range(0, 21); // 뽑을 때마다 확률 랜덤
    }
    protected void UnitRandomCalculation()
    {
        // 뽑은 유닛은 뽑기 확률에서 제외
        // 1번 뽑고 나면 maxValue - 1
        // 판매 된 유닛 -> 뽑기 확률에 추가
        // 2성 오크 하나 판매 -> 1성 오크 2개 증가
        // gachaUnit/maxUnit
        
        // MapController 에서 데이터 참고
    }
    // 라운드 넘어가는 판단 먼저 진행 -> exp 계산 
    // 타이머 0일때 다음 라운드 진행
    // 진영 Exp 가 max 면 진영 level up
    // 한 라운드 당 4씩 오름
    // curexp/maxexp - exp 차액만큼 골드 지불
    // 4/12 - 8골드 소비 - exp 4 증가
   
    // Game Quit
    public void QuitGame()
    {
        Application.Quit();
    }
}
