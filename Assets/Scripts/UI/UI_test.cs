using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Battle.Stage;
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

        // 시너지 발동 - 이름, 설명, 이미지
        // 배치된 유닛에 해당하는 이름 정보를 받아와서 텍스트로 띄우면 됨

        public TextMeshProUGUI synergyActivationName, synergyActivationInfo3, synergyActivationInfo5;

        // 배치된 유닛수
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
            // 종족이 들어왔을 경우 상위 노출되도록 정렬
        }
        public void SynergyInfo(string name) // 시너지 이름 받아옴
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
            // 들어오는 정보에 따라 표시되는 정보 text 다르게 표현
            // 이미지 데이터를 어떻게 받아와야 할지 - 이미지를 관리하는 데이터베이스가 따로 있는지?
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

            // 자주 활성화/비활성화하는 경우 부모오브젝트를 찾아두고 접근하게끔
            ChattingButton = GameObject.Find("Chatting_Contents").GetComponent<Button>();
            SettingButton = GameObject.Find("Setting_Contents").GetComponent<Button>();
            SettingPopupButton = GameObject.Find("Setting_Contents_Popup").GetComponent<Button>();
            RoundButton = GameObject.Find("Round_Contents").GetComponent<Button>();
            RoundEXButton = GameObject.Find("ChattingContents").GetComponent<Button>();
            RankingButton = GameObject.Find("Ranking_Contents").GetComponent<Button>();
            // 컨텐츠를 추가하는 것도 넣어야됨. - 배치된 유닛이 없으면 시너지 버튼도 필요없음
            // 배치된 유닛이 하나라도 있을 때 버튼이 활성화
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

            //hitObject = hit.transform.gameObject; // 레이가 충돌한 위치에 정보 출력
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

            // 누른 버튼이 무기 뽑기일 때
            if (IsWeponDraw)
            {

            }
            // 누른 버튼이 유닛 뽑기일 때
            if (IsUnitDraw)
            {

            }
            // 누른 버튼이 레벨업일 때
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