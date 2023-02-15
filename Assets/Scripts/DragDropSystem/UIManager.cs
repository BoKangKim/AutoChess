using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

namespace ZoneSystem
{
    public class UIManager : MonoBehaviour
    {
        #region �̱���
        private UIManager() { }
        private static UIManager inst = null;
        public static UIManager Inst
        {
            get
            {
                if (inst == null)
                {
                    inst = FindObjectOfType<UIManager>();
                    if (inst == null)
                    {
                        inst = new GameObject("UIManager").AddComponent<UIManager>();
                    }
                }
                return inst;
            }
        }
        #endregion
        [SerializeField] private PlayerData playerData;
        public Button unitBuyButton, equipmentBuyButton, sellButton = null;
        GraphicRaycaster graphicRaycaster = null;
        PointerEventData pointerEventData = null;
        List<RaycastResult> rrList = null;
        [SerializeField] private TextMeshProUGUI SynergyInfo = null;
        public int PlayerGold = 500;


        public Action UnitInstButton;

        private void Awake()
        {
            graphicRaycaster = GetComponent<GraphicRaycaster>();
            pointerEventData = new PointerEventData(EventSystem.current);
            rrList = new List<RaycastResult>();

        }

        private void Update()
        {
            pointerEventData.position = Input.mousePosition;
        }


        //UI Raycast
        public T RaycastUI<T>(int num) where T : Component
        {
            rrList.Clear();
            graphicRaycaster.Raycast(pointerEventData, rrList);
            if (rrList.Count == 0) return null;
            return rrList[num].gameObject.GetComponent<T>();
        }

        public void unitInstButton() => UnitInstButton();

        public void OnClick_EXP_Buy()
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
            if(playerData.CurExp <= playerData.MaxExp[playerData.playerLevel])
            {
                playerData.CurExp -= playerData.MaxExp[playerData.playerLevel];
                ++playerData.playerLevel;
                if (playerData.playerLevel == 9)
                {
                    playerData.CurExp = 0;
                }

            }
        }

        public void SynergyText(string text)
        {
            if (text == null) 
            {
                SynergyInfo.text = "";
            }
            else
            {
                SynergyInfo.text += text + "\n";
            }
        }

    }

}