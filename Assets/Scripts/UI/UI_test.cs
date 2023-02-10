using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class UI_test : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {

        // activeSynergyList - 시너지 리스트에서 해당하는 이름으로 찾아와서 비교
        private int synergyActivation;
        // unitCount - 배치된 유닛 카운트
        private int deployedUnitCount;

        //TextMeshProUGUI Limit_Number;
        //ZoneSystem.MapController mapController;

        GameObject settingMenu;
        // 테스트 끝나면 SerializeField 삭제
        [SerializeField] Button ChattingButton;
        [SerializeField] Button SettingButton;
        [SerializeField] Button RoundButton;
        [SerializeField] Button RoundEXButton;
        [SerializeField] Button LevelUpButton;
        [SerializeField] Button WeaponDrawButton;
        [SerializeField] Button UnitDrawButton;


        [SerializeField] Image SpeciesSynergyInfo; // 마우스를 갖다댔을 때 화면이 보이게끔
        [SerializeField] Image ClassSynergyInfo;

        //test        
        GameObject touchObject; // 방금 닿은 오브젝트
        [SerializeField] Image testWindow;

        

        // 마우스 커서가 해당 오브젝트 위에 있을 때 발동
        public void OnPointerEnter(PointerEventData data) 
        {
            Debug.Log("오브젝트 위에 마우스 올림");
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // 마우스를 올린 위치가 synergy일때 synergy 창 띄우기
                /*if (gameObject.CompareTag(""))
                {

                }*/
                testWindow.gameObject.SetActive(true);
                
            }
        }
        public void OnPointerExit(PointerEventData data)
        {
            Debug.Log("오브젝트 위에 마우스 내림");
            testWindow.gameObject.SetActive(false);
        }
        public void Awake()
        {
            // touchObject = EventSystem.current.currentSelectedGameObject;
            // Debug.Log(touchObject.name +", "+ touchObject.GetComponentInChildren<TextMeshProUGUI>().text);

            // data
            //mapController = GetComponent<ZoneSystem.MapController>();
            //settingButton = GameObject.Find("Btn_Setting_menu").GetComponent<Button>();

            // 자주 활성화/비활성화하는 경우 부모오브젝트를 찾아두고 접근하게끔
            ChattingButton = transform.GetChild(0).GetComponent<Button>();
            SettingButton = transform.GetChild(1).GetComponent<Button>();
            RoundButton = transform.GetChild(3).GetComponent<Button>();
            RoundEXButton = transform.GetChild(3).GetChild(5).GetComponent<Button>();
            LevelUpButton = transform.GetChild(6).GetChild(0).GetChild(1).GetComponent<Button>();
            WeaponDrawButton = transform.GetChild(7).GetChild(1).GetComponent<Button>();
            UnitDrawButton = transform.GetChild(7).GetChild(2).GetComponent<Button>();

            testWindow = transform.GetChild(2).GetComponent<Image>();

            // ClassSynergyInfo = transform.GetChild(4).GetChild(3).GetComponent<Button>();
            // SpeciesSynergyInfo = transform.GetChild(4).GetChild(4).GetComponent<Button>();

        }
        
        public void Start()
        {
            IsSetting = false;
            ChattingButton.onClick.AddListener(OnChatting);
            SettingButton.onClick.AddListener(OnSetting);
            RoundButton.onClick.AddListener(OnRoundInfoChange);
            RoundEXButton.onClick.AddListener(OnRoundEXChange);
            LevelUpButton.onClick.AddListener(OnLevelUp);
            WeaponDrawButton.onClick.AddListener(OnWeaponDraw);
            UnitDrawButton.onClick.AddListener(OnUnitDraw);

            // ClassSynergyButton.onClick.AddListener(OnSynergy);
            // SpeciesSynergyButton.onClick.AddListener(OnSynergy);

            //hitObject = hit.transform.gameObject; // 레이가 충돌한 위치에 정보 출력
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        
        
        Ray ray;
        RaycastHit hit;
        GameObject hitObject ;
        // UI 레이어 
        void CheckRay() 
        {
         
            Physics.Raycast(ray, out hit);
            
        }

        private void Update()
        {
            Debug.DrawRay(this.transform.position, this.transform.forward, Color.red);
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
            }

            if (Input.GetKeyDown(KeyCode.Escape)) IsSetting = true;
            //ray = transform.position,transform.forward;
        }

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
        }
        public void OnRoundEXChange()
        {
            Debug.Log("ON RoundEXInfoChange");
        }
        public void OnSynergy()
        {
            Debug.Log("ON Synergy");
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


        // Synergy Contents
        public void SortUI()
        {
            // 종족이 들어왔을 경우 상위 노출되도록 정렬
        }
        public void SynergyInfo(string name, int num, Image[] icon)
        {

        }
        public void SynergyExplainPopup(string Ex3, string Ex5, Image[] icon)
        {
            // 들어오는 정보에 따라 표시되는 정보 text 다르게 표현

        }
        // 종족일 때
        // 클래스일 때
        // 타입을 받아올 수 있나?


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

