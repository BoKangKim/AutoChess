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
        public void SynergyInfo(string name, int num,Image[] icon) 
        { 
        
        }
        public void SynergyExplainPopup(string Ex3,string Ex5, Image[] icon) 
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

