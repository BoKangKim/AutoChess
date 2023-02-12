using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Battle.Stage;
using Battle.AI.Effect;
// ������ - �ó��� �˾�â ON/OFF����
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

        // �ó��� �̸� �޾ƿ� - �ó��� �̸��� ���� ����� �� �ִ� �κ��� �ֳ�?
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
        public TextMeshProUGUI[] UnitStatusInfoTxt = null; // ���� ����, HP, MP
        public TextMeshProUGUI[] UnitStatusDetailIInfoTxt = null; // ����:Ŭ����, HP, MP, ���ݷ�, �ֹ���, ���� �ӵ�
        public TextMeshProUGUI[] UnitSkillInfoTxt = null; // ��ų �̸�, ȿ�� ����

        // Contents Popup
        [Header("Chatting Contents")]
        public Button ChattingContentsPopup = null;

        // Round Contents
        [Header("Round Contents")]
        public Button RoundEXContents = null;
        public Image RoundContentsBoss, RoundContentsResult = null;
        public TextMeshProUGUI RoundLimit = null;// Ȯ�� ���� / ��ġ�� ���ּ�
        public TextMeshProUGUI[] RoundNumber, RoundType = null; // ���� ǥ�� �ؽ�Ʈ

        // Setting Contents
        [Header("Setting Contents")]
        public Button SettingContentsPopup = null;
        public Button[] SettingBtn = null; // apply, quit?        

        // Synergy Contents
        [Header("Synergy Contents")]
        public Button[] ClassSynergy,SpeciesSynergy = null; // Ŭ���� : ��ؽ�,������,����,��Ŀ,������ / ���� : ����,�����,��,��ī,��ũ
        // �ó����� ǥ�õǴ� 3���� ������, �ó��� ���� �� ��ġ�� ���� ������ - ���� ������� ���� �ʿ�        
        public Image[] SynergyIcon, SynergyEXIcon = null; 
        public TextMeshProUGUI[] ClassSynergyEX, SpeciesSynergyEX = null; // �ó��� �̸�, ���� - Ŭ���� : 2, 4 / ���� : 3, 5
        public string[] SynergyEX = null;
        // ��ġ�� ������ �ش��ϴ� �̹���, �̸�, ����, Ŭ���� ������ �о�ͼ� �־���� ��. -> ��� �޾ƿ� �� �ִ���?

        // Ranking Contents
        [Header("Ranking Contents")]
        public Button[] Ranking= null; // ����
        public TextMeshProUGUI[] RankingContentsInfo = null; // �÷��̾� ����, ID

        // Excepsion Contents
        [Header("Excepsion Contents")]
        public Button ExcepsionLevelUP = null;
        public TextMeshProUGUI[] ExcepsionContentsInfo = null; // �÷��̾� ����, ID, ���ź��, CurExp/MaxExp

        // Gacha Contents
        [Header("Gacha Contents")]
        public Button WeaponDrawContents, UnitDrawContents = null;
        public TextMeshProUGUI[] gachaContentsGold = null; // �÷��̾�, ����̱�, ���ֻ̱�
        private int gachaContentsGoldValue = 5; // �ӽ�

        // test
        [Header("test")]
        public Button unitBuyButton, unitSellButton, unitAttackButton, unitHealButton = null;
        
        public ScrollRect synergyScroll;

        // �ó��� �ߵ� - �̸�, ����, �̹��� �߰��ʿ�
        // �̹��� - ��ġ�� ���ֿ� �ش��ϴ� �̸� ������ �޾ƿͼ� �ؽ�Ʈ�� ��� �� �ֵ���
        // public TextMeshProUGUI synergyActivationName, synergyActivationCount3, synergyActivationCount5;

        // ��ġ�� ���ּ� - ī��Ʈ ���� �ȵ�
        // public TextMeshProUGUI deployedActivation;
        // private int expensionLevelCount,deployedUnitCount;
       
        //--------------------------------------------------------------

        // Awake
        #region Awake
        public void Awake()
        {

            // ���� Ȱ��ȭ/��Ȱ��ȭ�ϴ� ��� �θ������Ʈ�� ã�Ƶΰ� �����ϰԲ�
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
            // �������� �߰��ϴ� �͵� �־�ߵ�. - ��ġ�� ������ ������ �ó��� ��ư�� �ʿ����
            // ��ġ�� ������ �ϳ��� ���� �� ��ư�� Ȱ��ȭ
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

             //hitObject = hit.transform.gameObject; // ���̰� �浹�� ��ġ�� ���� ���
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
                  Debug.Log("UI Layer �Դϴ�.");
                  Debug.Log("Synergy UI �Դϴ�. : " + hitObj.gameObject.name);
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
            // ������ ������ ��� ���� ����ǵ��� ����
        }

        public void SynergyExplainPopup(string Ex3, string Ex5, Image[] icon)
        {
            // ������ ������ ���� ǥ�õǴ� ���� text �ٸ��� ǥ��
            // �̹��� �����͸� ��� �޾ƿ;� ���� - �̹����� �����ϴ� �����ͺ��̽��� ���� �ִ���?
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
                 Debug.Log("���� �̱�");
             }            
             if (IsUnitDraw)
             {
                 Debug.Log("���� �̱�");
             }            
             if (IsLevelUp)
             {
                 Debug.Log("������");
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