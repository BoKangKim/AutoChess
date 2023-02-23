using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using TMPro.Examples;
using System.Collections;

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

    [Header("[Gacha]")]
    [SerializeField] private TextMeshProUGUI gachaWeponGold;
    [SerializeField] private TextMeshProUGUI gachaUnitGold;
    */



    [Header("[Setting]")]
    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject settingContentsPopup;
    [SerializeField] private Button applyButton;

    [Header("Synergy")]
    [SerializeField] private ScrollRect synergyScroll = null;
    [SerializeField] private GameObject[] synergyList = null;

    [Header("Buy")]
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
            Debug.Log("생성");
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
        if (playerData.gold < 4)
        {
            Debug.Log("골드가 부족합니다.");
            return;
        }
        if (playerData.playerLevel > 8)
        {
            Debug.Log("최대레벨 입니다.");
            return;
        }
        playerData.CurExp += 4;
        //레벨업
        if (playerData.CurExp <= playerData.MaxExp[playerData.playerLevel])
        {
            playerData.CurExp -= playerData.MaxExp[playerData.playerLevel];
            ++playerData.playerLevel;
            if (playerData.playerLevel == 9)
            {
                playerData.CurExp = 0;
            }

        }
    }


    public void OnClickDrawWeapon() // 장비 구매
    {

    }

    public void OnClickDrawUnit() // 유닛 구매
    {

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

    public void SynergyScroll(string prefabName) // string name 넘겨받으면 알아서 들어감
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
