using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RealUIManager : MonoBehaviour
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



    [Header("[Setting]")]
    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject settingContentsPopup;
    [SerializeField] private Button applyButton;

    [Header("Synergy")]
    [SerializeField] private ScrollRect synergyScroll = null;
    [SerializeField] private GameObject[] synergyList = null;

    [Header("Buy")]
    private PlayerData player = null;
    [SerializeField] private TextMeshProUGUI gachaWeponGold = null;
    [SerializeField] private TextMeshProUGUI gachaUnitGold = null;
    public Button buyUnitButton = null;
    public Action UnitInstButton;
    [SerializeField] private Button buyItemButton = null;

    [SerializeField] private Image player1HpBar = null;
    [SerializeField] private TextMeshProUGUI player1Lv = null;
    [SerializeField] private TextMeshProUGUI player1Name = null;

    [Header("Buy")]
    [SerializeField] private TextMeshProUGUI playerCurExp = null;

    private void Awake()
    {
        //expansionUserID.text = Database.Instance.userInfo.username;
    }

    private void Start()
    {
        player = new PlayerData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            List<string> activeSynergyList = new List<string>();
            activeSynergyList.Add("Mecha");
            activeSynergyList.Add("Orc");
            activeSynergyList.Add("Demon");
            activeSynergyList.Add("Warrior");
            activeSynergyList.Add("Assassin");
            activeSynergyList.Add("Tanker");
            activeSynergyList.Add("RangeDealer");
            activeSynergyList.Add("Magician");

            SynergyScroll(activeSynergyList);
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            List<string> activeSynergyList = new List<string>();
            activeSynergyList.Clear();
            activeSynergyList.Add("Warrior");
            activeSynergyList.Add("Assassin");
            activeSynergyList.Add("Magician");
            activeSynergyList.Add("RangeDealer");
            activeSynergyList.Add("Orc");
            activeSynergyList.Add("Demon");
            activeSynergyList.Add("Tanker");
            activeSynergyList.Add("Mecha");

            SynergyScroll(activeSynergyList);
        }


    }

    public void unitInstButton() => UnitInstButton();

    #region OnClick
    public void OnClickSettingButton() //���� ���� ��ư
    {
        settingContentsPopup.SetActive(true);
    }

    public void OnClickSettingApplyButton() //���� â ����
    {
        settingContentsPopup.SetActive(false);
    }

    public void OnClickBuyExp()
    {
        if (player.gold < 4)
        {
            Debug.Log("��尡 �����մϴ�.");
            return;
        }
        if (player.playerLevel > 8)
        {
            Debug.Log("�ִ뷹�� �Դϴ�.");
            return;
        }
        player.CurExp += 4;
        //������
        if (player.CurExp <= player.MaxExp[player.playerLevel])
        {
            player.CurExp -= player.MaxExp[player.playerLevel];
            ++player.playerLevel;
            if (player.playerLevel == 9)
            {
                player.CurExp = 0;
            }
        }
        playerCurExp.text = player.CurExp.ToString() + "/" + player.MaxExp[player.playerLevel];
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
    }


}
