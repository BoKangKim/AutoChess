using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Battle.Stage;
using Battle.AI.Effect;
// 진행중 - 시너지 팝업창 ON/OFF까지
// 

namespace UI
{
    public class UI_test : MonoBehaviour //,IPointerEnterHandler,IPointerExitHandler
    {


        #region SingleTon
        private UI_test() { }
        private static UI_test inst = null;
        public static UI_test Inst
        {
            get
            {
                if (inst == null)
                {
                    inst = FindObjectOfType<UI_test>();
                    if (inst == null)
                    {
                        inst = new GameObject("UIManager").AddComponent<UI_test>();
                    }
                }
                return inst;
            }
        }
        #endregion

        // 시너지 이름 받아옴 - 시너지 이름에 따라 적용될 수 있는 부분이 있나?
        /* public void SynergyInfo(string name) 
       {
           if (name == null)
           {
               synergyActivationName.text = "";
           }
           else
           {
               synergyActivationName.text += name + "\n";
           }
       }*/

        //--------------------------------------------------------------

        //Player        
        private int PlayerGoldValue = 100;
        private string PlayerID = "ID_123";
        private Image PlayerIcon = null;

        [Header("Contents")]
        public Canvas canvas;
        public Button UnitStateContents, ChattingContents, SettingContents, RoundContents = null;
        public Image SynergyContents, SynergyEXContents, RankingContents, ExpensionContents, GachaContents = null;
        // public RectTransform RouletteContents, AuctionContents = null;

        // Unit State Contents
        [Header("Unit State Contents")]
        public Image[] UnitStatusInfo = null; // UnitStatusInfo, UnitSkillInfo, Equipment
        public Button UnitStatusDetailInfo = null;
        public TextMeshProUGUI[] UnitStatusInfoTxt = null; // 유닛 레벨, HP, MP
        public TextMeshProUGUI[] UnitStatusDetailIInfoTxt = null; // 종족:클래스, HP, MP, 공격력, 주문력, 공격 속도
        public TextMeshProUGUI[] UnitSkillInfoTxt = null; // 스킬 이름, 효과 설명

        // Contents Popup
        [Header("Chatting Contents")]
        public Button ChattingContentsPopup = null;

        // Round Contents
        [Header("Round Contents")]
        public Button RoundEXContents = null;
        public Image RoundContentsBoss, RoundContentsResult = null;
        public TextMeshProUGUI RoundLimit = null;// 확장 레벨 / 배치된 유닛수
        public TextMeshProUGUI[] RoundNumber, RoundType = null; // 라운드 표시 텍스트

        // Setting Contents
        [Header("Setting Contents")]
        public Button SettingContentsPopup = null;
        public Button[] SettingBtn = null; // apply, quit?        

        // Synergy Contents
        [Header("Synergy Contents")]
        public Button[] ClassSynergy,SpeciesSynergy = null; // 클래스 : 어쌔신,매지션,딜러,탱커,워리어 / 종족 : 데몬,드워프,골렘,메카,오크
        // 시너지에 표시되는 3마리 아이콘, 시너지 설명에 들어갈 배치된 유닛 아이콘 - 들어온 순서대로 정렬 필요        
        public Image[] SynergyIcon, SynergyEXIcon = null; 
        public TextMeshProUGUI[] ClassSynergyEX, SpeciesSynergyEX = null; // 시너지 이름, 설명 - 클래스 : 2, 4 / 종족 : 3, 5
        public string[] SynergyEX = null;
        // 배치된 유닛의 해당하는 이미지, 이름, 종족, 클래스 정보를 읽어와서 넣어줘야 함. -> 어디서 받아올 수 있는지?

        // Ranking Contents
        [Header("Ranking Contents")]
        public Button[] Ranking= null; // 순위
        public TextMeshProUGUI[] RankingContentsInfo = null; // 플레이어 레벨, ID

        // Excepsion Contents
        [Header("Excepsion Contents")]
        public Button ExcepsionLevelUP = null;
        public TextMeshProUGUI[] ExcepsionContentsInfo = null; // 플레이어 레벨, ID, 구매비용, CurExp/MaxExp

        // Gacha Contents
        [Header("Gacha Contents")]
        public Button WeaponDrawContents, UnitDrawContents = null;
        public TextMeshProUGUI[] gachaContentsGold = null; // 플레이어, 무기뽑기, 유닛뽑기
        private int gachaContentsGoldValue = 5; // 임시

        // test
        [Header("test")]
        public Button unitBuyButton, unitSellButton, unitAttackButton, unitHealButton = null;
        
        public ScrollRect synergyScroll;

        // 시너지 발동 - 이름, 설명, 이미지 추가필요
        // 이미지 - 배치된 유닛에 해당하는 이름 정보를 받아와서 텍스트로 띄울 수 있도록
        // public TextMeshProUGUI synergyActivationName, synergyActivationCount3, synergyActivationCount5;

        // 배치된 유닛수 - 카운트 아직 안됨
        // public TextMeshProUGUI deployedActivation;
        // private int expensionLevelCount,deployedUnitCount;
       
        //--------------------------------------------------------------

        // Awake
        #region Awake
        public void Awake()
        {

            // 자주 활성화/비활성화하는 경우 부모오브젝트를 찾아두고 접근하게끔
            canvas = GetComponent<Canvas>();
            /*
            ChattingContents = canvas.transform.GetChild(2).GetComponent<Button>();

            SettingContents = canvas.transform.GetChild(3).GetComponent<Button>();
            // SettingPopupContents = canvas.transform.GetChild(4).GetComponent<Image>();
            // SettingQuitButton = SettingPopupContents.transform.GetChild(5).GetComponent<Button>();

            RoundContents = canvas.transform.GetChild(5).GetComponent<Button>();
            RoundEXContents = canvas.transform.GetChild(6).GetComponent<Button>();

            SynergyContents = canvas.transform.GetChild(7).GetComponent<Image>();
            // SynergyPopupContents = canvas.transform.GetChild(8).GetComponent<Image>();
            synergyScroll = SynergyContents.transform.Find("Scroll View").GetComponent<ScrollRect>();
            SynergyEXContents = synergyScroll.content.GetChild(0).GetComponent<Image>();

            RankingContents = canvas.transform.GetChild(9).GetComponent<Image>();

            ExpensionContents = canvas.transform.GetChild(10).GetComponent<Image>();

            GachaContents = canvas.transform.GetChild(11).GetComponent<Image>();
            WeaponDrawContents = GachaContents.transform.GetChild(1).GetComponent<Button>();
            UnitDrawContents = GachaContents.transform.GetChild(2).GetComponent<Button>();
            // 컨텐츠를 추가하는 것도 넣어야됨. - 배치된 유닛이 없으면 시너지 버튼도 필요없음
            // 배치된 유닛이 하나라도 있을 때 버튼이 활성화
            // */

        }
        #endregion

        public void Start()
        {
            /* ChattingContents.onClick.AddListener(OnChatting);
             SettingContents.onClick.AddListener(OnSetting);
             SettingQuitButton.onClick.AddListener(OffSetting);
             RoundContents.onClick.AddListener(OnRoundInfoChange);
             RoundEXContents.onClick.AddListener(OnRoundEXInfoChange);
             // ExpensionContents.onClick.AddListener(OnExpensionLevelUp);
             WeaponDrawContents.onClick.AddListener(OnWeaponDraw);
             UnitDrawContents.onClick.AddListener(OnUnitDraw);
             // SynergyContents.onClick.AddListener(OnSynergy);
             // SynergyEXContents.onClick.AddListener(OffSynergy);

             //hitObject = hit.transform.gameObject; // 레이가 충돌한 위치에 정보 출력
             //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             graphicRaycaster = GetComponent<GraphicRaycaster>();
             eventSystem = GetComponent<EventSystem>();     */
        }

        /*  GraphicRaycaster graphicRaycaster = null;
          PointerEventData pointerEventData = null;
          EventSystem eventSystem = EventSystem.current;
          List<RaycastResult> resultsList = null;

          void touchRay() 
          {
              pointerEventData = new PointerEventData(eventSystem);
              pointerEventData.position = Input.mousePosition;
              resultsList = new List<RaycastResult>();
              graphicRaycaster.Raycast(pointerEventData, resultsList);

              GameObject hitObj = resultsList[0].gameObject;


              //if (hitObj.CompareTag("Demon")) { Debug.Log("Demon"); }
              if (hitObj.layer==5) 
              {
                  Debug.Log("UI Layer 입니다.");
                  Debug.Log("Synergy UI 입니다. : " + hitObj.gameObject.name);
                  *//*if (hitObj.gameObject.name=="Synergy")
                  {

                  }*//*
              }

          }*/
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OffSetting(); //IsSetting = true;            

            //touchRay();

            /*if (Input.GetMouseButtonDown(0))
            {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray,out hit,100,5))
                {
                    Debug.Log("name : " + hit.transform.gameObject);
                }
            }*/
        }


        public void OnSetting()
        {
            Debug.Log("ON Setting");
            //SettingPopupContents.gameObject.SetActive(true);
        }
        public void OffSetting()
        {
            Debug.Log("OFF Setting");
            //SettingPopupContents.gameObject.SetActive(false);
        }
        public void OnWeaponDraw() //int gold
        {
            Debug.Log("ON Weapon Draw");
            //if (gold < goldExpense) return;
            //gold -= goldExpense;
            //gachaUserGold.text = string.Format("{0:#,###}", PlayerGoldValue);

        }
        public void OnUnitDraw()
        {
            Debug.Log("ON Unit Draw");
        }
        public void OnChatting()
        {
            Debug.Log("ON Chatting");
        }
        public void OnRoundInfoChange()
        {
            Debug.Log("ON RoundInfoChange");
            RoundEXContents.gameObject.SetActive(true);
        }
        public void OnRoundEXInfoChange()
        {
            Debug.Log("ON RoundEXInfoChange");
            RoundEXContents.gameObject.SetActive(false);
        }

        public void OnSynergy() //PointerEventData data
        {
            /*EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>(); ;
            EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
            entry_PointerDown.eventID = EventTriggerType.PointerEnter;*/
            Debug.Log("ON Synergy");
            //SynergyPopupContents.gameObject.SetActive(true);

        }
        public void OffSynergy()
        {
            Debug.Log("OFF Synergy");
            //SynergyPopupContents.gameObject.SetActive(false);
        }
        public void OnExpensionLevelUp()
        {
            Debug.Log("ON LevelUp");
        }

        private bool IsSetting { get; set; }
        private bool IsESC { get; set; }



        // On/Off UI
        public void SetActiveSelfUI()
        {
        }

        // Synergy Contents
        public void SortUI()
        {
            // 종족이 들어왔을 경우 상위 노출되도록 정렬
        }

        public void SynergyExplainPopup(string Ex3, string Ex5, Image[] icon)
        {
            // 들어오는 정보에 따라 표시되는 정보 text 다르게 표현
            // 이미지 데이터를 어떻게 받아와야 할지 - 이미지를 관리하는 데이터베이스가 따로 있는지?
        }

        // Gold Expense
        private int goldExpense;
        private bool IsWeponDraw { get; set; }
        private bool IsUnitDraw { get; set; }
        private bool IsLevelUp { get; set; }
        /* public void GoldException(int gold, int goldExpense)
         {
             if (gold < goldExpense) return;
             gold -= goldExpense;
             gachaUserGold.text = string.Format("{0:#,###}", PlayerGoldValue);
             *//*if (IsWeponDraw)
             {
                 Debug.Log("무기 뽑기");
             }            
             if (IsUnitDraw)
             {
                 Debug.Log("유닛 뽑기");
             }            
             if (IsLevelUp)
             {
                 Debug.Log("레벨업");
             }  *//*
             // playerGold.text = string.Format("{0:#,###}", playerGoldValue); 
         }*/

        // Text Color Change
        private void ChangeTextColor(TextMeshProUGUI t) { t.color = Color.yellow; }
        private void ChangeTextColorInitiate(TextMeshProUGUI t) { t.color = Color.gray; }

        // Game Quit
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}