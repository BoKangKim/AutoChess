using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//using Photon.Pun;
//using Photon.Realtime;

public class UIManager : MonoBehaviour
{
    // �ó��� �з� 
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
    [Header("Canvas")]
    [SerializeField] private GameObject roundInfoUI;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject synergyContentsUI;
    [SerializeField] private GameObject rankingContentsUI;
    [SerializeField] private GameObject chattingUI;
    [SerializeField] private GameObject chattingModeUI; // ä�� ���
    [SerializeField] private GameObject synergyExplainUI;
    [SerializeField] private GameObject selectRoundUI;
    //
    [SerializeField] private GameObject AuctionLoadingUI; // ��� �ε�
    [SerializeField] private GameObject AuctionRoundUI; // ���
    [SerializeField] private GameObject AuctionResultUI; // ��� ���
    [SerializeField] private GameObject GameResultUI; // ���� ���
    [SerializeField] private GameObject RouletteUI; // �귿
    [SerializeField] private GameObject RouletteResultUI;
    [SerializeField] private GameObject UnitStatusUI;
    [SerializeField] private GameObject UnitStatusInfoUI;
    [SerializeField] private GameObject UnitSkillInfoUI;

    [SerializeField] private GameObject[] UnitSynergyUI; // ���� ų ���� �ó��� â
    [SerializeField] private GameObject[] bettingUnitGradeUI;  // ���õ� ���� ���

    //-----------------------------------------------------------------------------------------------------------

    // Rank Contents
    [SerializeField] private GameObject[] rankUserInfo;
    [SerializeField] private TextMeshProUGUI rankingUserID; // ��ŷ UI�� �ߴ� ID  - get data

    // Round
    [SerializeField] private TextMeshProUGUI RoundInfoNum;
    [SerializeField] private TextMeshProUGUI RoundStageNum;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum1;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum2;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum3;
    [SerializeField] private TextMeshProUGUI RoundDetailStepNum4;

    // Synergy Contents
    [SerializeField] private TextMeshProUGUI SnergyUnitPopup_Name; // �ó��� �˾�â - ���� �̸�
    [SerializeField] private TextMeshProUGUI[] SnergyUnitPopup_Info;
    [SerializeField] private Image[] SnergyUnitPopup_Icon;
    // ������ �ڽ��ȿ� ��� ���� : ���� �̸�, ���� ī��Ʈ ��, �����    
    [SerializeField] private Image[] SynergyIcon; // ù��° �ó��� â ������
    [SerializeField] private TextMeshProUGUI[] ClassSynergyName; // Ŭ���� �ó��� �̸�
    [SerializeField] private TextMeshProUGUI[] TribeSynergyName; // ���� �ó��� �̸�
    [SerializeField] private TextMeshProUGUI SynergyPlus; // ������ ���� �� �� ī��Ʈ �� -> �迭 �����ͷ� �޾ƿ��� 
    [SerializeField] private Image[] SynergyImage;
    //[SerializeField] private TextMeshProUGUI Mecha_synergyCountNumber;

    // Expansion Contents
    [SerializeField] private TextMeshProUGUI expansionUserID; // Ȯ�� UI�� �ߴ� ID  - get data
    [SerializeField] private TextMeshProUGUI expansionGold; // Ȯ�� �� ����ϴ� ���
    [SerializeField] private TextMeshProUGUI expensionLV;
    [SerializeField] private Slider expansionEXPSlider;
    // Player Info 
    [SerializeField] private TextMeshProUGUI playerExpensionLV;
    [SerializeField] private TextMeshProUGUI playerRankingnLV;
    [SerializeField] private TextMeshProUGUI playerGold; // �÷��̾ ������ ���    
    [SerializeField] private Image playerHPSlider;
    // Gacha
    [SerializeField] private TextMeshProUGUI gachaWeponGold;
    [SerializeField] private TextMeshProUGUI gachaUnitGold;
    // Betting
    [SerializeField] private TextMeshProUGUI prossessedUserGold; // ���� ������
    [SerializeField] private TextMeshProUGUI betOnUserGold; // ���õ� ���� �ݾ�
    [SerializeField] private TextMeshProUGUI betOnMaximumGold; // ���õ� �ִ� �ݾ�
    [SerializeField] private TextMeshProUGUI auctionRoundUnitinfo; // ���õ� ���� ����
    [SerializeField] private TextMeshProUGUI auctionRoundID; // ���� UI�� �ߴ� ID - get data
    // Button
    [SerializeField] private Button[] bettingButton;
    // Chatting button
    [SerializeField] private Button[] chattingMode;
    [SerializeField] private Button chattingSend;
    [SerializeField] private GameObject[] chattingModeBack;
    // Auction grade
    [SerializeField] private Image[] auctionGradeIcon;

    // �ش� ��޿� �°� ��� ǥ�� - ��ſ� �ߴ� ����� ���� ��
    #region Auction Grade
    public void AuctionGrade()
    {
        auctionGradeIcon[0].gameObject.SetActive(true);
        auctionGradeIcon[1].gameObject.SetActive(true);
        auctionGradeIcon[2].gameObject.SetActive(true);
        auctionGradeIcon[3].gameObject.SetActive(true);
    }
    #endregion

    delegate void MyDelegate();
    MyDelegate myDelegate;

    public void DelegateTest()
    {
        // delegate += �Լ�

    }
    // �� ��ư�� �������� ����� ���� ��


    // Color
    private Color popupIconColor;

    #region Data �޾ƿ���
    private string UserName = "user1"; // �޾ƿ���
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
    // �ó��� ���� ������ ���� ��� ���� 
    private string InputInfo1 = "(3) ���� ���� �� Orc�� �Լ��Ҹ��� �Բ� ��� ������ ���ݷ°� ü���� 5% �϶���Ŵ";
    private string InputInfo2 = "(5) ���� ���� �� Orc�� �Լ� �Ҹ��� �Բ� ��� ������ ���ݷ°� ü���� 15% �϶���Ŵ";

    private int playerGoldValue;
    private float playerEXPValue;
    private float playerMaxHPValue;
    private float playerHPValue;

    [SerializeField] protected int deployedUnit; // ��ġ�� ����
    private int gachaWeponGoldValue;
    private int gachaUnitGoldValue;
    private int expansionGoldValue;
    private int expansionMaxEXPValue;
    private int expensionLevelValue;

    private int roundStepNumber1 = 1;
    private int roundStepNumber2 = 1;
    private int roundTextColor;
    private int RoundRewardGold = 10; // �� ���� ���ư������� ���� ���
    private float RoundRewardEXP = 4f;

    // Betting Round
    private int prossessedUserGoldValue;
    private int betOnUserGoldValue;
    private int betOnMaximumGoldValue;

    private int TypeSynergyAllCount = 5; // ���� �ó��� ��ü ��
    // ���� �ó��� ī��Ʈ
    private int mechaSynergyCount; // ���� ��
    public void TypeSynergyCount(int num)
    {
        // ��ġ�� ���ֿ� ���� Ÿ�� �ó��� ���
    }
    private int SynergyCount3 = 3;
    private int SynergyCount5 = 5;
    // Ŭ���� �ó��� ī��Ʈ
    public void ClassSynergyCount(int num)
    {
        // ��ġ�� ���ֿ� ���� Ŭ���� �ó��� ���
    }
    private bool IsESC { get; set; }
    private bool IsSynergy { get; set; }
    private bool IsRanking { get; set; }
    private bool IsChatting { get; set; }
    private bool IsChattingMode { get; set; }
    private bool IsSynergyEX { get; set; }
    private bool IsDead { get; set; }

    // Specie Synergy bool
    #region Specie Synergy bool
    private bool IsSynergy3 { get; set; }
    private bool IsSynergy5 { get; set; }

    #endregion

    protected TimeManager timeManager; // �����̰� �۾���

    // �ΰ��� ä�� ��� x
    // ä�� ��庰 ��ư - �ӼӸ� ��� ��ü
    // Chatting Menu
    #region Chatting Menu
    public void ChattingMode()
    {
        chattingModeUI.gameObject.SetActive(true);
    }
    public void OnChattingUI()
    {
        chattingUI.gameObject.SetActive(true);
        IsChattingMode = true;
    }
    public void OffChattingUI()
    {
        chattingUI.gameObject.SetActive(false);
        IsChattingMode = false;
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

    (string name, int age) TupleTest(string name, int age)
    {
        return (name, age);
    }
    private void Start()
    {
        (string name, int age) = TupleTest("����", 20);
        Debug.Log(name);
        Debug.Log(age);

        timeManager = FindObjectOfType<TimeManager>();
        // ���� ���� ���
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
        IsChattingMode = false;
        IsSynergyEX = false;
        IsDead = false;

        // data �޾ƿ� ��
        playerGoldValue = 100;//player.GetGold;
        expensionLevelValue = 1;//player.GetLevel;
        playerEXPValue = 0;//player.GetEXP;
        playerExpensionLV.text = ("Lv." + expensionLevelValue).ToString(); // ���� �ؽ�Ʈ
        playerRankingnLV.text = ("Lv." + expensionLevelValue).ToString();
        expansionEXPSlider.value = playerEXPValue; // �����̴�

        expansionMaxEXPValue = 32; // �ӽ� - ������ �޾ƿ��� - �����ʿ�
        expansionGoldValue = expansionMaxEXPValue;
        expansionGold.text = expansionGoldValue.ToString();

        deployedUnit = 0; // �ӽ� - ��ġ�� ���� ��
        RoundStageNum.text = (roundStepNumber1 + "-" + roundStepNumber2).ToString();

        RoundDetailInfo();

        // ���� �ó��� ������ UI         
        for (int i = 0; i < UnitSynergyUI.Length; i++)
        {
            UnitSynergyUI[i].gameObject.SetActive(false);
        }

        // ������ ���� �ٸ� ������ �� �� �־�� ��.
        SnergyUnitPopup_Info[0].text = InputInfo1;
        SnergyUnitPopup_Info[1].text = InputInfo2;

        //color
        popupIconColor = SnergyUnitPopup_Icon[0].GetComponent<Image>().color;

        SnergyUnitPopup_Name.text = Mecha; // ���߿� �޾ƿͼ� ������Ʈ

        // player HP
        playerMaxHPValue = 100f;
        playerHPValue = playerMaxHPValue;
        // player - data ���� �κ�
        auctionRoundID.text = UserName;
        rankingUserID.text = UserName;
    }

    protected void Update()
    {
        // ��ġ�� ���ֿ� ���� �ó��� Ȯ��
        DeployedUnitSynergy();

        if (playerGoldValue <= 0) playerGoldValue = 0;
        playerGold.text = string.Format("{0:#,###}", playerGoldValue); // 4�ڸ��� �Ѿ�� , ǥ��

        if (playerHPValue <= 0) IsDead = true;

        // Game Setting
        if (Input.GetKeyDown(KeyCode.Escape)) SettingMenuESC();

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
            //playerGoldValue += RoundRewardGold; // �Ѷ���� ��� ����
        }
        RoundDetailInfo();
        expansionUserID.text = UserName;
        expansionGold.text = expansionGoldValue.ToString();

        // ��ŷ ������
        playerRankingnLV.text = ("Lv." + expensionLevelValue).ToString(); // ���� �� ������Ʈ
        playerHPSlider.fillAmount = playerHPValue / playerMaxHPValue;
        // ü�¿� ���� ��ŷ ������ ��ġ �̵�
        ChangeRankerPosition();
        // ���� �ݾ�
        Auction_test();
        AuctionButton();

    }


    private void AuctionButton()
    {
        if (playerGoldValue < bettingGold1)
        {
            bettingButton[0].interactable = false;
        }
        else
        {
            bettingButton[0].interactable = true;
        }
        if (playerGoldValue < bettingGold2)
        {
            bettingButton[1].interactable = false;
        }
        else
        {
            bettingButton[1].interactable = true;
        }
        if (playerGoldValue < bettingGold3)
        {
            bettingButton[2].interactable = false;
        }
        else
        {
            bettingButton[2].interactable = true;
        }
        if (playerGoldValue < bettingGold4)
        {
            bettingButton[3].interactable = false;
        }
        else
        {
            bettingButton[3].interactable = true;
        }
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

    // Auction Round State
    #region Auction Round State
    enum AuctionRoundState
    {
        None,
        Loading,
        Auction,
        Result
    }
    Coroutine curCoroutine = null;
    AuctionRoundState curState = AuctionRoundState.None;

    private void nextState(AuctionRoundState newState)
    {
        if (newState == curState) return;
        if (curCoroutine != null) StopCoroutine(curCoroutine);
        curState = newState;
        curCoroutine = StartCoroutine(newState.ToString() + "State");
    }
    IEnumerator LoadingState()
    {
        AuctionLoadingUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        AuctionLoadingUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(AuctionState());
    }
    IEnumerator AuctionState()
    {
        AuctionRoundUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(15f); // Ÿ�̸� 0 �Ǹ� ����
        AuctionRoundUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(ResultState());
    }
    IEnumerator ResultState() // ������ �̹����� �Բ� ������
    {
        AuctionResultUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        AuctionResultUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
    #endregion

    public void Auction_test_On()
    {
        StartCoroutine(LoadingState());
    }

    private void Auction_test()
    {
        // 1P : �÷��̾� ����
        auctionRoundID.text = ("1P : " + UserName);
        // ���� ������
        prossessedUserGold.text = string.Format("{0:#,###}", playerGoldValue);
        // ������ �ݾ� ����
        betOnUserGold.text = (betOnUserGoldValue + "G").ToString();
        // �ְ� �ݾ� ���� - �񱳽� �ʿ�
        betOnMaximumGold.text = (betOnUserGoldValue + "G").ToString(); // �ְ�ݾ� ��� �ʿ�        


        // ���� : Ŭ����
        auctionRoundUnitinfo.text = (Mecha + " : " + Warrior);
        // Ÿ�̸� -> ��ũ��Ʈ ���� �����
        // ���

    }
    // ������ �ݾ� ��ư - ���� �����ݺ��� ���� ��� ����ó��
    private int bettingGold1 = 1;
    private int bettingGold2 = 5;
    private int bettingGold3 = 10;
    private int bettingGold4 = 20;

    public void BetOn1()
    {
        playerGoldValue -= 1;
        betOnUserGoldValue += 1;
    }
    public void BetOn2()
    {
        playerGoldValue -= 5;
        betOnUserGoldValue += 5;
    }
    public void BetOn3()
    {
        playerGoldValue -= 10;
        betOnUserGoldValue += 10;
    }
    public void BetOn4()
    {
        playerGoldValue -= 20;
        betOnUserGoldValue += 20;
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
        // ���� �÷��̾� ���� �Ʒ��� ������ ��Ȱ��ȭ ȿ�� �ֱ�
        // ���� �÷��̾ �������� �� -> ���� ���� ���� ������� �ؿ� �������� ����

    }
    #endregion
    //

    // Round Detail Info : 1-1 1-2 1-3 1-4 ���� ���� ���� ȿ��
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


    // Deployed Unit Synergy - Popup
    #region Deployed Unit Synergy 
    private void DeployedUnitSynergy()
    {
        // �� �ó������� �ش��ϴ� �������� Ȯ���ʿ� - �ӽ� ������
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
                SynergyImage[0].gameObject.SetActive(true); // �ó��� ������ - ���� �̹���
                SynergyImage[1].gameObject.SetActive(false);
                OnSynergyPopupAlpha(0); // �ó��� �˾� ������ - ���� ���� ���� �̹���
                OffSynergyPopupAlpha(1);
                break;
            case 2:
                //IsMechaSynergy3 = false;
                SynergyImage[1].gameObject.SetActive(true);
                SynergyImage[2].gameObject.SetActive(false);
                ChangeTextColorInitiate(SynergyPlus);
                OnSynergyPopupAlpha(1);
                OffSynergyPopupAlpha(2);
                ChangeTextColorInitiate(SnergyUnitPopup_Info[0]); // ���� �� Ȱ��ȭ
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
    // �ó��� �˾�â Ȱ��ȭ / ��Ȱ��ȭ
    private void OnSynergyPopupAlpha(int num)
    {
        popupIconColor.a = 1.0f; // ���İ����� Ȱ��ȭ ��Ȱ��ȭ ����
        SnergyUnitPopup_Icon[num].color = popupIconColor;
    }
    private void OffSynergyPopupAlpha(int num)
    {
        popupIconColor.a = 0.5f; // ���İ����� Ȱ��ȭ ��Ȱ��ȭ ����
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
            playerEXPValue = playerEXPValue - expansionMaxEXPValue;
        }
        expansionGoldValue = Mathf.Abs(expansionMaxEXPValue - (int)playerEXPValue);
        playerGoldValue -= expansionGoldValue; // ���� exp �� ���� ��� ���� ����       
        playerEXPValue += RoundRewardEXP;
        playerGold.text = string.Format("{0:#,###}", playerGoldValue); // ���� ����
        playerExpensionLV.text = ("Lv." + expensionLevelValue).ToString(); // ���� �� ������Ʈ
        expansionEXPSlider.value = (playerEXPValue * 0.1f); // �����̴� �� ���� �ʿ�
        expansionEXPSlider.maxValue = (expansionMaxEXPValue * 0.1f);

    }


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

    
    // Chatting Mode
    #region Chatting Mode
    // ���õ� ��� ��� �ѱ� / ����ȣ�� �Ű������� �޾Ƽ� ����
    // �⺻������ ��ü ä�� ��忡 ǥ��
    public void OnOffChattingList()
    {
        if (!IsChattingMode)
        {
            OnChattingList();
        }
        else
        {
            OffChattingList();
        }
    }
    private void OnChattingList() // ����Ʈ ǥ��
    {
        chattingMode[0].gameObject.SetActive(true);
        IsChattingMode = true;
    }
    private void OffChattingList()
    {
        chattingMode[0].gameObject.SetActive(false);
        IsChattingMode = false;
    }
    public void OnAllChatting() // chattingMode[1] ��ü
    {
        chattingMode[1].interactable = true;
        chattingModeBack[1].gameObject.SetActive(true);
    }
    public void OnGuildChatting() // chattingMode[2] ���
    {
        chattingMode[2].interactable = true;
        chattingModeBack[2].gameObject.SetActive(true);
    }
    public void OnWhisperChatting() // chattingMode[3] �ӼӸ�
    {
        chattingMode[3].interactable = true;
        chattingModeBack[3].gameObject.SetActive(true);
    }
    #endregion


    // Game Quit
    public void QuitGame()
    {
        Application.Quit();
    }

}