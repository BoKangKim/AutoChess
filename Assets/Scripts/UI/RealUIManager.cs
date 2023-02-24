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
    [SerializeField] private GameObject roundResultContents; // 승패 결과
    [SerializeField] private TextMeshProUGUI roundInfoNum;
    [SerializeField] private TextMeshProUGUI roundStageNum;
    [SerializeField] private TextMeshProUGUI roundDetailNum;
    [SerializeField] private TextMeshProUGUI[] roundDetailStepNum = new TextMeshProUGUI[4];

    [Header("[Synergy]")]
    [SerializeField] private GameObject synergyContents;
    [SerializeField] private Image synergyExContents;
    [SerializeField] private Image synergyExContentsPopup;
    [SerializeField] private TextMeshProUGUI synergyContentsPopupName; // 시너지 팝업창 - 종족 이름
    [SerializeField] private TextMeshProUGUI[] synergyContentsPopupInfo; // 시너지 설명
    [SerializeField] private Image[] synergyUnitIcon; // 시너지 3마리 들어갈 곳
    [SerializeField] private Image[] synergyUnitPopupIcon; // 팝업 아이콘 5개
    [SerializeField] private TextMeshProUGUI synergyPlus; // 보유한 유닛 수 비교 카운트 수 -> 배열 데이터로 받아오기 
    [SerializeField] private GameObject[] classSynergy, speciesSynergy; // 컨텐츠 오브젝트

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
    public void OnClickSettingButton() //세팅 띄우는 버튼
    {
        settingContentsPopup.SetActive(true);
    }

    public void OnClickSettingApplyButton() //세팅 창 적용
    {
        settingContentsPopup.SetActive(false);
    }

    public void OnClickBuyExp()
    {
        if (player.gold < 4)
        {
            Debug.Log("골드가 부족합니다.");
            return;
        }
        if (player.playerLevel > 8)
        {
            Debug.Log("최대레벨 입니다.");
            return;
        }
        player.CurExp += 4;
        //레벨업
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


    public void OnClickDrawWeapon() // 장비 구매
    {
        if (player.gold < 3)
        {
            Debug.Log("골드가 부족합니다.");
            return;
        }
    }

    public void OnClickDrawUnit() // 유닛 구매
    {
        if (player.gold < 3)
        {
            Debug.Log("골드가 부족합니다.");
            return;
        }
    }

    public void OnClickRanking1Player() // 1P보기
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

    public void SynergyScroll(List<string> activeSynergyList) //list를 받아서 초기화 이후 하나씩 넣기
    {
        RectTransform contentTransform = synergyScroll.content.GetComponentInChildren<RectTransform>();

        foreach (Transform child in contentTransform) // 기존거 삭제
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < activeSynergyList.Count; i++) //여기 2중포문 확인
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

    public void PlayerInfoUpdate() // 임시로 플레이어 한명 피 깎
    {
        player1HpBar.fillAmount = player.CurHP / 100;
        player1Lv.text = "LV : " + player.playerLevel.ToString();
        player1Name.text = player.playerName;
    }


}
