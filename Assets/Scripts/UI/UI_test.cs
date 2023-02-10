using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class UI_test : MonoBehaviour
    {
        Button button;
        public void Awake()
        {
            
        }
        public void Start()
        {
            IsSetting = false;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) IsSetting = true;
        }

        private bool IsSetting { get; set; }
        private bool IsESC { get; set; }
        // On/Off UI
        public void SetActiveSelfUI() 
        { 
        }

        private void SetActiveUI(bool check, GameObject obj) 
        {
            if (!check)
            {
                check = true;
                obj.gameObject.SetActive(true);
            }
            else
            {
                check = false;
                obj.gameObject.SetActive(false);
            }
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
        public void SynergyInfo(string name, int num,Image[] icon) 
        { 
        
        }
        public void SynergyExplainPopup(string Ex3,string Ex5, Image[] icon) 
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

