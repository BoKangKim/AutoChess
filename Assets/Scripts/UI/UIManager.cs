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
    [SerializeField] GameObject chattingWindow;

    [SerializeField] TextMeshProUGUI RoundInfoNum;
    [SerializeField] TextMeshProUGUI RoundStageNum;
    [SerializeField] TextMeshProUGUI RoundDetailStepNum;
    [SerializeField] TextMeshProUGUI RoundDetailStepNum1;
    [SerializeField] TextMeshProUGUI RoundDetailStepNum2;
    [SerializeField] TextMeshProUGUI RoundDetailStepNum3;
    [SerializeField] TextMeshProUGUI RoundDetailStepNum4;
    [SerializeField] TextMeshProUGUI expansionGold; // Ȯ�� �� ����ϴ� ���
    [SerializeField] TextMeshProUGUI expensionLV; 
    [SerializeField] TextMeshProUGUI playerLV; 
    [SerializeField] TextMeshProUGUI playerGold; // �÷��̾ ������ ���
    [SerializeField] TextMeshProUGUI playerHP; 
    [SerializeField] TextMeshProUGUI gachaWeponGold;
    [SerializeField] TextMeshProUGUI gachaUnitGold;
    // ������ �ڽ��ȿ� ��� ���� : ���� �̸�, ���� ī��Ʈ ��
    [SerializeField] Image synergyIcon;
    [SerializeField] TextMeshProUGUI synergyName; 
    [SerializeField] TextMeshProUGUI synergyNum; 
    [SerializeField] Slider expansionEXPSlider;
    [SerializeField] Slider playerHPSlider;

    protected int playerGoldValue;
    protected float playerEXPValue;
    protected float playerHPValue;
    protected int playerMaxHPValue;

    protected int deployedUnit; // ��ġ�� ����
    protected int gachaWeponGoldValue;
    protected int gachaUnitGoldValue;
    protected int expansionGoldValue;
    protected int expansionMaxEXPValue;
    protected int expensionLevelValue;

    protected int roundStepNumber1 = 1;
    protected int roundStepNumber2 = 1;
    protected int roundTextColor;
    protected int RoundRewardGold = 10;
    protected float RoundRewardEXP = 4f;

    protected int synergyCount; // �ó��� ī��Ʈ
    
    protected bool IsESC { get; set; }
    protected bool IsSynergy { get; set; }
    protected bool IsRanking { get; set; }
    protected bool IsChatting { get; set; }
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
        //IsCurRound = true;

        // data �޾ƿ� ��
        playerGoldValue = 1000;//player.GetGold;
        expensionLevelValue = 1;//player.GetLevel;
        playerEXPValue = 0;//player.GetEXP;
        playerLV.text = expensionLevelValue.ToString(); // ���� �ؽ�Ʈ
        expansionEXPSlider.value = playerEXPValue; // �����̴�
        
        expansionMaxEXPValue = 32; // �ӽ� - ������ �޾ƿ���
        playerMaxHPValue = 1; 

        deployedUnit = 3; // �ӽ�
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
        RoundInfoNum.text = (expensionLevelValue + " / " + deployedUnit).ToString();
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
            // �� ���� ������ �ڵ� 1ȸ ����
            // ����
            //OnFactionExpansion();
        }
        RoundDetailInfo();
        playerHP.text = playerHPValue.ToString();
        playerHPSlider.value = (playerHPValue * 0.1f);
        playerHPSlider.value = (playerMaxHPValue * 0.1f);
        // �ٸ� �÷��̾�� ������ �� ���� ���� HP�� ��� ���� ����
        // gameobject (player ������ ��� ȭ��)
        // 1��.transform.position = -4, 175
        // 2��.transform.position = -4, 60
        // 3��.transform.position = -4, -55
        // 4��.transform.position = -4, -170
        
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
            synergyContents.transform.localPosition = Vector2.Lerp(synergyContents.transform.localPosition, new Vector2(synergyContents.transform.localPosition.x + 10f, synergyContents.transform.localPosition.y), Time.deltaTime * 100f);

        }
        else
        {
            if (synergyContents.transform.position.x <= -10.2f) return;
            synergyContents.transform.localPosition = Vector2.Lerp(synergyContents.transform.localPosition, new Vector2(synergyContents.transform.localPosition.x - 10f, synergyContents.transform.localPosition.y), Time.deltaTime * 100f);
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
            rankingContents.transform.localPosition = Vector2.Lerp(rankingContents.transform.localPosition, new Vector2(rankingContents.transform.localPosition.x - 10f, rankingContents.transform.localPosition.y), Time.deltaTime * 100f);
        }
        else
        {
            if (rankingContents.transform.position.x >= 10.2f) return;
            rankingContents.transform.localPosition = Vector2.Lerp(rankingContents.transform.localPosition, new Vector2(rankingContents.transform.localPosition.x + 10f, rankingContents.transform.localPosition.y), Time.deltaTime * 100f);
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
        chattingWindow.gameObject.SetActive(true);
    }
    public void OffChattingUI()
    {
        chattingWindow.gameObject.SetActive(false);
    }
    protected void Chatting()
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

    protected void WeponRandomCalculation()
    {
        // 25%
        // 1�� 4��
        // safety zone �� ��ġ
        // ����ó�� : ���� ���� ����
        // �� ��ũ��Ʈ ����
        // MixRandomUnit = Random.Range(0, 21); // ���� ������ Ȯ�� ����
    }
    protected void UnitRandomCalculation()
    {
        // ���� ������ �̱� Ȯ������ ����
        // 1�� �̰� ���� maxValue - 1
        // �Ǹ� �� ���� -> �̱� Ȯ���� �߰�
        // 2�� ��ũ �ϳ� �Ǹ� -> 1�� ��ũ 2�� ����
        // gachaUnit/maxUnit
        
        // MapController ���� ������ ����
    }
    // ���� �Ѿ�� �Ǵ� ���� ���� -> exp ��� 
    // Ÿ�̸� 0�϶� ���� ���� ����
    // ���� Exp �� max �� ���� level up
    // �� ���� �� 4�� ����
    // curexp/maxexp - exp ���׸�ŭ ��� ����
    // 4/12 - 8��� �Һ� - exp 4 ����
   
    // Game Quit
    public void QuitGame()
    {
        Application.Quit();
    }
}
