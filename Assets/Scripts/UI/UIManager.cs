using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    /*
    [Header("[UnitState]")]
    [SerializeField] private GameObject unitStatusContents;
    [SerializeField] private GameObject unitStatusInfo;
    [SerializeField] private GameObject unitStatusDetailInfo;
    [SerializeField] private GameObject unitSkillInfo;

    [Header("[Player]")]
    [SerializeField] private TextMeshProUGUI playerExpensionLv;
    [SerializeField] private TextMeshProUGUI playerRankingLv;
    [SerializeField] private TextMeshProUGUI playerGold;
    [SerializeField] private Image playerHpSlider;

    [Header("[Round]")]
    [SerializeField] private GameObject roundContents;
    [SerializeField] private GameObject roundExContents;
    [SerializeField] private GameObject roundResultContents; // ���� ���
    [SerializeField] private TextMeshProUGUI roundInfoNum;
    [SerializeField] private TextMeshProUGUI roundStageNum;
    [SerializeField] private TextMeshProUGUI roundDetailNum;
    [SerializeField] private TextMeshProUGUI[] roundDetailStepNum = new TextMeshProUGUI[4];

    [Header("[Synergy]")]
    [SerializeField] private GameObject synergyContents;
    [SerializeField] private Image synergyExContents;
    [SerializeField] private Image synergyExContentsPopup;
    [SerializeField] private TextMeshProUGUI synergyContentsPopupName; // �ó��� �˾�â - ���� �̸�
    [SerializeField] private TextMeshProUGUI[] synergyContentsPopupInfo; // �ó��� ����
    [SerializeField] private Image[] synergyUnitIcon; // �ó��� 3���� �� ��
    [SerializeField] private Image[] synergyUnitPopupIcon; // �˾� ������ 5��
    [SerializeField] private TextMeshProUGUI synergyPlus; // ������ ���� �� �� ī��Ʈ �� -> �迭 �����ͷ� �޾ƿ��� 
    [SerializeField] private GameObject[] classSynergy, speciesSynergy; // ������ ������Ʈ

    // ������ �ڽ��ȿ� ��� ���� : ���� �̸�, ���� ī��Ʈ ��, ������  

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

    
    */

    GraphicRaycaster graphicRaycaster = null;
    PointerEventData pointerEventData = null;
    List<RaycastResult> rrList = null;

    [Header("[Setting]")]
    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject settingContentsPopup;
    [SerializeField] private Button applyButton;

    [Header("Synergy")]
    [SerializeField] private ScrollRect synergyScroll = null;
    [SerializeField] private GameObject[] synergyList = null;

    [Header("Buy")]
    public PlayerData player = null;
    public TextMeshProUGUI playerGold = null;
    public Button buyItemButton = null;
    public Button buyUnitButton = null;
    public Button sellButton = null;

    public Action UnitInstButton;
    public Action ItemInstButton;
    public Action SellButton;


    [SerializeField] private Image player1HpBar = null;
    [SerializeField] private TextMeshProUGUI player1Lv = null;
    [SerializeField] private TextMeshProUGUI player1Name = null;
    [SerializeField] private Slider ExpSlider = null;

    [Header("Buy")]
    [SerializeField] private TextMeshProUGUI playerCurExp = null;
    [SerializeField] private TextMeshProUGUI limitUnitNum = null;

    [Header("Time")]
    [SerializeField] private TextMeshProUGUI timeText = null;

    public int buttlezoneUnitNum = 0;


    private void Awake()
    {
        //expansionUserID.text = Database.Instance.userInfo.username;
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(EventSystem.current);
        rrList = new List<RaycastResult>();
    }

    private void Start()
    {
        player = new PlayerData();
        GameManager.Inst.SetUIManager();
        PlayerInfoUpdate();
    }


    //public T RaycastUI<T>(int num) where T : Component
    //{
    //    rrList.Clear();

    //    Debug.Log(rrList);
    //    graphicRaycaster.Raycast(pointerEventData, rrList);
        

    //    if (rrList.Count == 0) return null;
    //    return rrList[num].gameObject.GetComponent<T>();
    //}

    public void unitInstButton() => UnitInstButton();
    public void itemInstButton() => ItemInstButton();
    public void sellDestroyButton() => SellButton();
    
    #region OnClick
    public void OnClickSettingButton() //���� ���� ��ư
    {
        settingContentsPopup.SetActive(true);
    }

    public void OnClickSettingApplyButton() //���� â ����
    {
        settingContentsPopup.SetActive(false);
    }

    public void SetTimeText(string time)
    {
        this.timeText.text = time;
    }

    public void OnClickBuyExp()
    {
        if (player.gold < 12)
        {
            Debug.Log("��尡 �����մϴ�.");
            return;
        }
        if (player.playerLevel > 6)
        {
            Debug.Log("�ִ뷹�� �Դϴ�.");
            return;
        }
        player.gold -= 12;
        player.CurExp += 4;
        
        if (player.CurExp >= player.MaxExp[player.playerLevel])
        {
            player.CurExp -= player.MaxExp[player.playerLevel];
            ++player.playerLevel;

        }
        //playerCurExp.text = player.CurExp.ToString() + "/" + player.MaxExp[player.playerLevel];
        if (player.playerLevel < 7)
        {
            PlayerInfoUpdate();
        }
        else if (player.playerLevel == 7)
        {
            PlayerInfoUpdate();
            playerCurExp.text = "MAX";
            ExpSlider.value = 1;

        }
    }


    public void OnClickDrawWeapon() // ��� ����
    {
        if (player.gold < 3)
        {
            Debug.Log("��尡 �����մϴ�.");
            return;
        }
    }

    public void OnClickDrawUnit() // ���� ����
    {
        if (player.gold < 3)
        {
            Debug.Log("��尡 �����մϴ�.");
            return;
        }
    }

    public void OnClickRanking1Player() // 1P����
    {

    }

    public void OnClickRanking2Player()
    {

    }

    public void OnClickRanking3Player()
    {

    }

    public void OnClickRanking4Player()
    {

    }


    #endregion

    public void SynergyScroll(List<string> activeSynergyList) //list�� �޾Ƽ� �ʱ�ȭ ���� �ϳ��� �ֱ�
    {
        RectTransform contentTransform = synergyScroll.content.GetComponentInChildren<RectTransform>();

        foreach (Transform child in contentTransform) // ������ ����
        {
           Destroy(child.gameObject);
        }

        for (int i = 0; i < activeSynergyList.Count; i++) //���� 2������ Ȯ��
        {
            for (int j = 0; j < synergyList.Length; j++)
            {
                if (activeSynergyList[i].Equals(synergyList[j].name))
                {
                    Instantiate(synergyList[j], synergyScroll.content.transform);
                }
            }

        }
    }

    public void PlayerHpRanking()
    {

    }

    public void PlayerInfoUpdate() // �ӽ÷� �÷��̾� �Ѹ� �� ��
    {
        player1HpBar.fillAmount = player.CurHP / 100;
        player1Lv.text = "LV : " + player.playerLevel.ToString();
        player1Name.text = player.playerName;
        playerGold.text = player.gold.ToString();
        playerCurExp.text = $"{player.CurExp} / {player.MaxExp[player.playerLevel]}";
        limitUnitNum.text = $"{buttlezoneUnitNum} / {player.playerLevel + 2}";
        ExpSlider.value = player.CurExp / player.MaxExp[player.playerLevel];
    }

    //스테이지 끝날때 
    //hp 마이너스
    //gold 획득




}
