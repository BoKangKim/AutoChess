using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using TMPro.Examples;

public class RealUIManager : MonoBehaviour
{
    [Header("[UnitState]")]
    [SerializeField] private GameObject unitStatusContents;
    [SerializeField] private GameObject unitStatusInfo;
    [SerializeField] private GameObject unitStatusDetailInfo;
    [SerializeField] private GameObject unitSkillInfo;

    [Header("[Setting]")]
    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject settingContentsPopup;
    [SerializeField] private Button applyButton;

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

    [Header("[Gacha]")]
    [SerializeField] private TextMeshProUGUI gachaWeponGold;
    [SerializeField] private TextMeshProUGUI gachaUnitGold;


    [SerializeField] private ScrollRect synergyScroll = null;
    [SerializeField] private GameObject[] synergyList = null;

    [SerializeField] private PlayerData playerData = null;

    private void Awake()
    {
        //expansionUserID.text = Database.Instance.userInfo.username;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("����");
            SynergyScroll("Mecha2");
            SynergyScroll("Orc");
            SynergyScroll("Demon");
            SynergyScroll("Assassin2");
            SynergyScroll("Tanker2");
            SynergyScroll("Warrior");
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            //expansionUserID.text = Database.Instance.userInfo.username;
        }
    }

    #region OnClick
    public void OnClickSettingButton() //���� ���� ��ư
    {
        settingContentsPopup.SetActive(true);
    }

    public void OnClickSettingApplyButton() //���� â ����
    {
        settingContentsPopup.SetActive(false);
    }

    public void OnClickBuyExp() // ����ġ ����
    {
        
    }


    public void OnClickDrawWeapon() // ��� ����
    {

    }

    public void OnClickDrawUnit() // ���� ����
    {

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

    public void SynergyScroll(string prefabName) // string name �Ѱܹ����� �˾Ƽ� ��
    {
        for(int i = 0;i<synergyList.Length;i++)
        {
            if (prefabName.Equals(synergyList[i].name))
            {
                Instantiate(synergyList[i],synergyScroll.content.transform);
            }
        }
    }
}
