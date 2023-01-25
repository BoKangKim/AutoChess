using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ZoneSystem
{
    public class UIManager : MonoBehaviour
    {
        #region ╫л╠шео
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

        [SerializeField]
        Button buyButton, sellButton = null;
        GraphicRaycaster graphicRaycaster = null;
        PointerEventData pointerEventData = null;
        List<RaycastResult> rrList = null;
        [SerializeField] private TextMeshProUGUI SynergyInfo = null;



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

            if (rrList.Count == 0)
                return null;



            return rrList[num].gameObject.GetComponent<T>();
        }

        public void SynergyText(string text)
        {
            Debug.Log(text);
            if (text == null)
            {
                //SynergyInfo.text = "";
            }
            else
            {
                SynergyInfo.text += "\n" + text;
            }
        }

    }

}