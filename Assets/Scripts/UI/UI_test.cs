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

        // activeSynergyList - �ó��� ����Ʈ���� �ش��ϴ� �̸����� ã�ƿͼ� ��
        private int synergyActivation;
        // unitCount - ��ġ�� ���� ī��Ʈ
        private int deployedUnitCount;

        //TextMeshProUGUI Limit_Number;
        //ZoneSystem.MapController mapController;

        GameObject settingMenu;
        // �׽�Ʈ ������ SerializeField ����
        [SerializeField] Button ChattingButton;
        [SerializeField] Button SettingButton;
        [SerializeField] Button RoundButton;
        [SerializeField] Button RoundEXButton;
        [SerializeField] Button LevelUpButton;
        [SerializeField] Button WeaponDrawButton;
        [SerializeField] Button UnitDrawButton;


        [SerializeField] Image SpeciesSynergyInfo; // ���콺�� ���ٴ��� �� ȭ���� ���̰Բ�
        [SerializeField] Image ClassSynergyInfo;

        //test        
        GameObject touchObject; // ��� ���� ������Ʈ
        [SerializeField] Image testWindow;

        

        // ���콺 Ŀ���� �ش� ������Ʈ ���� ���� �� �ߵ�
        public void OnPointerEnter(PointerEventData data) 
        {
            Debug.Log("������Ʈ ���� ���콺 �ø�");
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // ���콺�� �ø� ��ġ�� synergy�϶� synergy â ����
                /*if (gameObject.CompareTag(""))
                {

                }*/
                testWindow.gameObject.SetActive(true);
                
            }
        }
        public void OnPointerExit(PointerEventData data)
        {
            Debug.Log("������Ʈ ���� ���콺 ����");
            testWindow.gameObject.SetActive(false);
        }
        public void Awake()
        {
            // touchObject = EventSystem.current.currentSelectedGameObject;
            // Debug.Log(touchObject.name +", "+ touchObject.GetComponentInChildren<TextMeshProUGUI>().text);

            // data
            //mapController = GetComponent<ZoneSystem.MapController>();
            //settingButton = GameObject.Find("Btn_Setting_menu").GetComponent<Button>();

            // ���� Ȱ��ȭ/��Ȱ��ȭ�ϴ� ��� �θ������Ʈ�� ã�Ƶΰ� �����ϰԲ�
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

            //hitObject = hit.transform.gameObject; // ���̰� �浹�� ��ġ�� ���� ���
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        
        
        Ray ray;
        RaycastHit hit;
        GameObject hitObject ;
        // UI ���̾� 
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


        // Synergy Contents
        public void SortUI()
        {
            // ������ ������ ��� ���� ����ǵ��� ����
        }
        public void SynergyInfo(string name, int num, Image[] icon)
        {

        }
        public void SynergyExplainPopup(string Ex3, string Ex5, Image[] icon)
        {
            // ������ ������ ���� ǥ�õǴ� ���� text �ٸ��� ǥ��

        }
        // ������ ��
        // Ŭ������ ��
        // Ÿ���� �޾ƿ� �� �ֳ�?


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

