using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ZoneSystem;
using TMPro;
namespace ZoneSystem
{
    public class DragAndDrop : MonoBehaviour
    {
        MapController mapController;
        public GameObject selectedObject { get; private set; }
        Camera cam;
        int unitLayer;
        int battleSpaceLayer;
        int safetySpaceLayer;
        Vector3 beforePoint;
        Button buySellButton = null;

        private void Awake()
        {
            mapController = GetComponent<MapController>();
        }
        private void Start()
        {

            cam = Camera.main;
            safetySpaceLayer = 1 << LayerMask.NameToLayer("SafetySpace");
            battleSpaceLayer = 1 << LayerMask.NameToLayer("BattleSpace");
            unitLayer = 1 << LayerMask.NameToLayer("Unit");
        }
        private void Update()
        {
            #region 모바일용
            //if (Input.touchCount == 1)
            //{

            //    if (Input.GetTouch(0).phase == TouchPhase.Moved)
            //    {

            //        if (selectedObject == null)
            //        {
            //            CastRay(unitLayer);
            //            if (CastRay(unitLayer).collider != null)
            //            {
            //                selectedObject = CastRay(unitLayer).collider.gameObject;


            //                int x = (int)selectedObject.transform.position.x;
            //                int z = (int)selectedObject.transform.position.z;

            //                mapController.safetyZoneObject[z, x] = null;


            //                if (CastRay(safetySpaceLayer).collider != null)
            //                {
            //                    beforePoint = CastRay(safetySpaceLayer).collider.gameObject.transform.position;
            //                }
            //                else if (CastRay(battleSpaceLayer).collider != null)
            //                {
            //                    beforePoint = CastRay(battleSpaceLayer).collider.gameObject.transform.position;

            //                }
            //            }
            //        }

            //        else
            //        {
            //            if (UIManager.Inst.RaycastUI<Button>(1) != null)
            //            {
            //                buySellButton = UIManager.Inst.RaycastUI<Button>(1);

            //                storeButtonChange(Color.white, Color.black, false, "유닛 판매");

            //            }
            //            else
            //            {
            //                if (buySellButton != null)
            //                {
            //                    storeButtonChange(Color.black, Color.white, true, "유닛 소환");

            //                    buySellButton = null;
            //                }
            //            }
            //            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(selectedObject.transform.position).z);
            //            Vector3 worldPosition = cam.ScreenToWorldPoint(position);

            //            selectedObject.transform.position = new Vector3(worldPosition.x, 1f, worldPosition.z);
            //        }
            //    }
            //    //Drop
            //    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            //    {

            //        if (buySellButton)
            //        {

            //            storeButtonChange(Color.black, Color.white, true, "유닛 소환");
            //            buySellButton = null;

            //            Destroy(selectedObject);
            //        }

            //        if (EventSystem.current.IsPointerOverGameObject()) return;

            //        UIManager.Inst.OnBuyButton();
            //        if (CastRay(safetySpaceLayer).collider != null)
            //        {
            //            Vector3 worldPosition = CastRay(safetySpaceLayer).collider.gameObject.transform.position;
            //            mapController.safetyZoneObject[(int)worldPosition.z, (int)worldPosition.x] = selectedObject;
            //            selectedObject.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);

            //            selectedObject = null;
            //        }
            //        else if (CastRay(battleSpaceLayer).collider != null)
            //        {
            //            Vector3 worldPosition = CastRay(battleSpaceLayer).collider.gameObject.transform.position;
            //            mapController.safetyZoneObject[(int)worldPosition.z, (int)worldPosition.x] = selectedObject;
            //            selectedObject.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);

            //            selectedObject = null;
            //        }
            //        else
            //        {
            //            selectedObject.transform.position = new Vector3(beforePoint.x, 0.5f, beforePoint.z);
            //            mapController.safetyZoneObject[(int)beforePoint.z, (int)beforePoint.x] = selectedObject;
            //            selectedObject = null;

            //        }
            //    }
            //}
            #endregion



            #region PC용
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedObject == null)
                {
                    CastRay(unitLayer);
                    if (CastRay(unitLayer).collider != null)
                    {
                        selectedObject = CastRay(unitLayer).collider.gameObject;

                        int x = (int)selectedObject.transform.position.x;
                        int z = (int)selectedObject.transform.position.z;


                        if (CastRay(safetySpaceLayer).collider != null)
                        {
                            mapController.safetyZoneObject[z, x] = null;
                            beforePoint = CastRay(safetySpaceLayer).collider.gameObject.transform.position;
                        }
                        else if (CastRay(battleSpaceLayer).collider != null)
                        {
                            mapController.battleZoneObject[z, x] = null;
                            beforePoint = CastRay(battleSpaceLayer).collider.gameObject.transform.position;

                        }
                    }
                }
                //Drop
                else
                {

                    if (buySellButton)
                    {

                        storeButtonChange(Color.black, Color.white, true, "유닛 소환");
                        buySellButton = null;

                        Destroy(selectedObject);
                    }


                    if (EventSystem.current.IsPointerOverGameObject()) return;



                    if (CastRay(safetySpaceLayer).collider != null)
                    {
                        DropPosition(safetySpaceLayer);
                    }
                    else if (CastRay(battleSpaceLayer).collider != null)
                    {
                        DropPosition(battleSpaceLayer);
                    }
                    else
                    {

                        mapController.safetyZoneObject[(int)beforePoint.z, (int)beforePoint.x] = selectedObject;

                        selectedObject.transform.position = new Vector3(beforePoint.x, 0.25f, beforePoint.z);
                        selectedObject = null;

                    }

                }
            }
            //Drag
            if (selectedObject != null)
            {

                if (UIManager.Inst.RaycastUI<Button>(1) != null)
                {
                    buySellButton = UIManager.Inst.RaycastUI<Button>(1);

                    storeButtonChange(Color.white, Color.black, false, "유닛 판매");

                }
                else
                {
                    if (buySellButton != null)
                    {
                        storeButtonChange(Color.black, Color.white, true, "유닛 소환");

                        buySellButton = null;
                    }
                }



                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = cam.ScreenToWorldPoint(position);

                selectedObject.transform.position = new Vector3(worldPosition.x, 1f, worldPosition.z);
            }
            #endregion
        }

        #region raycast
        RaycastHit CastRay(int Layer)
        {
            Vector3 screenMousePosFar = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));
            Vector3 screenMousePosNear = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

            Physics.Raycast(screenMousePosNear, screenMousePosFar - screenMousePosNear, out RaycastHit hit, Vector3.Magnitude(screenMousePosFar - screenMousePosNear), Layer);
            Debug.DrawRay(screenMousePosNear, screenMousePosFar - screenMousePosNear, Color.red);

            return hit;
        }
        #endregion


        #region DropPos
        void DropPosition(int Layer)
        {
            Vector3 worldPosition = CastRay(Layer).collider.gameObject.transform.position;

            if (Layer == safetySpaceLayer)
            {
                if (mapController.safetyZoneObject[(int)worldPosition.z, (int)worldPosition.x] == null)
                {

                    mapController.safetyZoneObject[(int)worldPosition.z, (int)worldPosition.x] = selectedObject;

                    selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    selectedObject = null;
                }
                else
                {
                    mapController.safetyZoneObject[(int)beforePoint.z, (int)beforePoint.x] = mapController.safetyZoneObject[(int)worldPosition.z, (int)worldPosition.x];

                    mapController.safetyZoneObject[(int)beforePoint.z, (int)beforePoint.x].transform.position = new Vector3(beforePoint.x, 0.25f, beforePoint.z);

                    mapController.safetyZoneObject[(int)worldPosition.z, (int)worldPosition.x] = selectedObject;

                    selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    selectedObject = null;
                }
            }

            else if (Layer == battleSpaceLayer)
            {

                if (mapController.battleZoneObject[(int)worldPosition.z, (int)worldPosition.x] == null)
                {

                    mapController.battleZoneObject[(int)worldPosition.z, (int)worldPosition.x] = selectedObject;

                    selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    selectedObject = null;
                }
                else
                {
                    mapController.battleZoneObject[(int)beforePoint.z, (int)beforePoint.x] = mapController.battleZoneObject[(int)worldPosition.z, (int)worldPosition.x];

                    mapController.battleZoneObject[(int)beforePoint.z, (int)beforePoint.x].transform.position = new Vector3(beforePoint.x, 0.25f, beforePoint.z);

                    mapController.battleZoneObject[(int)worldPosition.z, (int)worldPosition.x] = selectedObject;

                    selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    selectedObject = null;
                }


            }


        }
        #endregion

        #region 버튼체인지
        void storeButtonChange(Color text, Color button, bool enabled, string unitStatus)
        {

            buySellButton.image.color = button;
            TextMeshProUGUI buttonText = buySellButton.GetComponentInChildren<TextMeshProUGUI>();
            buySellButton.enabled = enabled;
            buttonText.text = unitStatus;
            buttonText.color = text;
        }
        #endregion



    }
}
