using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Battle.Stage;
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

        //        
        GraphicRaycaster graphicRaycaster = null;
        PointerEventData pointerEventData = null;
        List<RaycastResult> rrList = null;
        [SerializeField] private TextMeshProUGUI synergyInfo = null;
        //


        //--------------------------------------------------------------

        public Button ChattingButton, SettingButton, SettingPopupButton, RoundButton, RoundEXButton, LevelUpButton, WeaponDrawButton, UnitDrawButton, RankingButton = null;
        public Button unitBuyButton, unitSellButton = null;
        public Button unitAttackButton, unitHealButton = null;

        public Button SynergyButton, SynergyEXButton;

        // �ó��� �ߵ� - �̸�, ����, �̹���
        // ��ġ�� ���ֿ� �ش��ϴ� �̸� ������ �޾ƿͼ� �ؽ�Ʈ�� ���� ��

        public TextMeshProUGUI synergyActivationName, synergyActivationInfo3, synergyActivationInfo5;

        // ��ġ�� ���ּ�
        public TextMeshProUGUI deployedActivation;
        private int deployedUnitCount;

        // Gacha
        public TextMeshProUGUI gachaUserGold, gachaWeponGold, gachaUnitGold;
        private int gachaWeponGoldValue, gachaUnitGoldValue = 3;
        private int gachaUserGoldValue = 100;

        //test                
        public Image testWindow;

        //--------------------------------------------------------------
        // Round
        public TextMeshProUGUI RoundInfoNum, RoundStageNum;
        public TextMeshProUGUI RoundDetailStepNum, RoundDetailStepNum1, RoundDetailStepNum2, RoundDetailStepNum3, RoundDetailStepNum4;


        // Synergy Contents
        public void SortUI()
        {
            // ������ ������ ��� ���� ����ǵ��� ����
        }
        public void SynergyInfo(string name) // �ó��� �̸� �޾ƿ�
        {
            if (name == null)
            {
                synergyActivationName.text = "";
            }
            else
            {
                synergyActivationName.text += name + "\n";
            }
        }
        public void SynergyExplainPopup(string Ex3, string Ex5, Image[] icon)
        {
            // ������ ������ ���� ǥ�õǴ� ���� text �ٸ��� ǥ��
            // �̹��� �����͸� ��� �޾ƿ;� ���� - �̹����� �����ϴ� �����ͺ��̽��� ���� �ִ���?
        }

        // Awake
        #region Awake
        public void Awake()
        {
            
            graphicRaycaster = GetComponent<GraphicRaycaster>();
            pointerEventData = new PointerEventData(EventSystem.current);
            rrList = new List<RaycastResult>();

            // touchObject = EventSystem.current.currentSelectedGameObject;
            // Debug.Log(touchObject.name +", "+ touchObject.GetComponentInChildren<TextMeshProUGUI>().text);

            // data
            //mapController = GetComponent<ZoneSystem.MapController>();
            //settingButton = GameObject.Find("Btn_Setting_menu").GetComponent<Button>();

            // ���� Ȱ��ȭ/��Ȱ��ȭ�ϴ� ��� �θ������Ʈ�� ã�Ƶΰ� �����ϰԲ�
            ChattingButton = GameObject.Find("Chatting_Contents").GetComponent<Button>();
            SettingButton = GameObject.Find("Setting_Contents").GetComponent<Button>();
            SettingPopupButton = GameObject.Find("Setting_Contents_Popup").GetComponent<Button>();
            RoundButton = GameObject.Find("Round_Contents").GetComponent<Button>();
            RoundEXButton = GameObject.Find("ChattingContents").GetComponent<Button>();
            RankingButton = GameObject.Find("Ranking_Contents").GetComponent<Button>();
            // �������� �߰��ϴ� �͵� �־�ߵ�. - ��ġ�� ������ ������ �ó��� ��ư�� �ʿ����
            // ��ġ�� ������ �ϳ��� ���� �� ��ư�� Ȱ��ȭ
            // 
            SynergyButton = GameObject.Find("Synergy_Contents").GetComponent<Button>();
            SynergyEXButton = transform.GetChild(4).GetChild(1).GetComponent<Button>();
            LevelUpButton = transform.GetChild(6).GetChild(0).GetChild(1).GetComponent<Button>();
            WeaponDrawButton = transform.GetChild(7).GetChild(1).GetComponent<Button>();
            UnitDrawButton = transform.GetChild(7).GetChild(2).GetComponent<Button>();

            testWindow = transform.GetChild(2).GetComponent<Image>();

        }
        #endregion

        public void Start()
        {
            IsSetting = false;
            ChattingButton.onClick.AddListener(OnChatting);
            SettingButton.onClick.AddListener(OnSetting);
            RoundButton.onClick.AddListener(OnRoundInfoChange);
            RoundEXButton.onClick.AddListener(OnRoundEXInfoChange);
            LevelUpButton.onClick.AddListener(OnLevelUp);
            WeaponDrawButton.onClick.AddListener(OnWeaponDraw);
            UnitDrawButton.onClick.AddListener(OnUnitDraw);
            SynergyButton.onClick.AddListener(OnSynergy);
            SynergyEXButton.onClick.AddListener(OffSynergy);

            //hitObject = hit.transform.gameObject; // ���̰� �浹�� ��ġ�� ���� ���
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        
        private void Update()
        {            
            if (Input.GetKeyDown(KeyCode.Escape)) IsSetting = true;            
        }

        //UI Raycast
        public T RaycastUI<T>(int num) where T : Component
        {
            rrList.Clear();
            graphicRaycaster.Raycast(pointerEventData, rrList);

            if (rrList.Count == 0)
                return null;



            return rrList[num].gameObject.GetComponent<T>();
        }

        /*public void SynergyText(string text)
        {
            if (text == null)
            {
                SynergyInfo.text = "";
            }
            else
            {
                SynergyInfo.text += text + "\n";
            }
        }*/

        public void OnSetting()
        {
            Debug.Log("ON Setting");
            //settingMenu.gameObject.SetActive(true);
        }
        public void OnWeaponDraw()
        {
            Debug.Log("ON Weapon Draw");
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
            RoundEXButton.gameObject.SetActive(true);
        }
        public void OnRoundEXInfoChange()
        {
            Debug.Log("ON RoundEXInfoChange");
            RoundEXButton.gameObject.SetActive(false);
        }
        public void OnSynergy()
        {
            Debug.Log("ON Synergy");
            SynergyEXButton.gameObject.SetActive(true);
        }
        public void OffSynergy()
        {
            Debug.Log("OFF Synergy");
            SynergyEXButton.gameObject.SetActive(false);
        }
        public void OnLevelUp()
        {
            Debug.Log("ON LevelUp");
        }

        private bool IsSetting { get; set; }
        private bool IsESC { get; set; }



        // On/Off UI
        public void SetActiveSelfUI()
        {
        }

        // Gold Expense
        private int goldExpense;
        private bool IsWeponDraw { get; set; }
        private bool IsUnitDraw { get; set; }
        private bool IsLevelUp { get; set; }
        public void GoldException(int gold)
        {
            if (gold < goldExpense) return;
            gold -= goldExpense;

            // ���� ��ư�� ���� �̱��� ��
            if (IsWeponDraw)
            {

            }
            // ���� ��ư�� ���� �̱��� ��
            if (IsUnitDraw)
            {

            }
            // ���� ��ư�� �������� ��
            if (IsLevelUp)
            {

            }

            // playerGold.text = string.Format("{0:#,###}", playerGoldValue); 
        }

        private Image EXP;
        private int expValue;
        public void Exp(int exp)
        {

        }



        // Round UI
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