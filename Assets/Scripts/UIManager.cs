using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace ZoneSystem
{
    public class UIManager : MonoBehaviour
    {
        #region �̱���
        private UIManager() { }
        private static UIManager inst = null;

        PointerEventData pe = new PointerEventData(EventSystem.current);

        public static UIManager Inst
        {
            get
            {
                if (inst == null)
                {
                    inst = GameObject.FindObjectOfType<UIManager>();
                    if (inst == null)
                    {
                        inst = new GameObject("UIManager").AddComponent<UIManager>();
                    }
                }
                return inst;
            }
        }
        #endregion

        public Button unitInstButton;

        void Start()
        {
            



        }



        void Update()
        {

        }
    }
}
