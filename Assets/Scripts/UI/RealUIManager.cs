using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using TMPro.Examples;

public class RealUIManager : MonoBehaviour
{
    [Header("[UnitState]")]
    [SerializeField] private GameObject UnitStatusContents;
    [SerializeField] private GameObject UnitStatusInfo;
    [SerializeField] private GameObject UnitStatusDetailInfo;
    [SerializeField] private GameObject UnitSkillInfo;

    [Header("[Setting]")]
    [SerializeField] private GameObject SettingContents;
    [SerializeField] private GameObject SettingContentsPopup;

    [Header("[Player]")]
    [SerializeField] private TextMeshProUGUI playerExpensionLV;
    [SerializeField] private TextMeshProUGUI playerRankingnLV;
    [SerializeField] private TextMeshProUGUI playerGold;
    [SerializeField] private Image playerHPSlider;

    [Header("[Round]")]
    [SerializeField] private GameObject RoundContents;
    [SerializeField] private GameObject RoundEXContents;
    [SerializeField] private GameObject RoundResultContents; // 승패 결과
    [SerializeField] private TextMeshProUGUI RoundInfoNum;
    [SerializeField] private TextMeshProUGUI RoundStageNum;
    [SerializeField] private TextMeshProUGUI RoundDetailNum;
    [SerializeField] private TextMeshProUGUI[] RoundDetailStepNum = new TextMeshProUGUI[4];

    [Header("[Synergy]")]
    [SerializeField] private GameObject SynergyContents;
    [SerializeField] private Image SynergyEXContents;
    [SerializeField] private Image SynergyEXContentsPopup;
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

    [Header("[Gacha]")]
    [SerializeField] private TextMeshProUGUI gachaWeponGold;
    [SerializeField] private TextMeshProUGUI gachaUnitGold;

    [Header("[Time]")]
    [SerializeField] TimeManager timeManager = null;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}
