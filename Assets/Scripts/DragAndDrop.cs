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

        Vector3 beforePos;
        Vector3 beforelocalPos;

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
                    //CastRay(unitLayer);
                    if (CastRay(unitLayer).collider != null)
                    {
                        selectedObject = CastRay(unitLayer).collider.gameObject;
                        Vector3 vec;

                        if (CastRay(safetySpaceLayer).collider != null)
                        {
                            vec = selectedObject.transform.position;
                            mapController.safetyObject[(int)vec.z, (int)vec.x] = null;
                            beforePos = CastRay(safetySpaceLayer).collider.transform.position;
                        }
                        else if (CastRay(battleSpaceLayer).collider != null)
                        {
                            vec = PosToIndex(CastRay(battleSpaceLayer).collider.transform.localPosition);

                            mapController.battleObject[(int)vec.z, (int)vec.x] = null;
                            beforePos = CastRay(battleSpaceLayer).collider.transform.position;

                            beforelocalPos = CastRay(battleSpaceLayer).collider.transform.localPosition;
                        }
                    }
                }
                //Drop
                else
                {
                    // 유닛 판매
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
                        if ((int)beforePos.z < 2)
                        {
                            mapController.safetyObject[(int)beforePos.z, (int)beforePos.x] = selectedObject;
                        }
                        else
                        {
                            mapController.battleObject[(int)PosToIndex(beforelocalPos).z, (int)PosToIndex(beforelocalPos).x] = selectedObject;
                        }


                        selectedObject.transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
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
            Vector3 worldPosition = CastRay(Layer).collider.transform.position;

            if (Layer == safetySpaceLayer)
            {
                if (mapController.safetyObject[(int)worldPosition.z, (int)worldPosition.x] == null)
                {
                    mapController.safetyObject[(int)worldPosition.z, (int)worldPosition.x] = selectedObject;
                    selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    mapController.BattlezoneChack();
                    selectedObject = null;
                }
                else
                {
                    if ((int)beforePos.z < 2)
                    {
                        mapController.safetyObject[(int)beforePos.z, (int)beforePos.x] = mapController.safetyObject[(int)worldPosition.z, (int)worldPosition.x];
                        mapController.safetyObject[(int)beforePos.z, (int)beforePos.x].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                    }
                    else
                    {
                        mapController.battleObject[(int)PosToIndex(beforelocalPos).z, (int)PosToIndex(beforelocalPos).x] = mapController.safetyObject[(int)worldPosition.z, (int)worldPosition.x];
                        mapController.battleObject[(int)PosToIndex(beforelocalPos).z, (int)PosToIndex(beforelocalPos).x].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                    }
                    mapController.safetyObject[(int)worldPosition.z, (int)worldPosition.x] = selectedObject;

                    selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    selectedObject = null;
                }
            }

            else if (Layer == battleSpaceLayer)
            {

                Vector3 vec = PosToIndex(CastRay(Layer).collider.transform.localPosition);
                if (mapController.battleObject[(int)vec.z, (int)vec.x] == null)
                {
                    mapController.battleObject[(int)vec.z, (int)vec.x] = selectedObject;
                    selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    mapController.BattlezoneChack();
                    selectedObject = null;
                }
                else
                {
                    Debug.Log(beforePos.z);
                    if ((int)beforePos.z < 2)
                    {
                        mapController.safetyObject[(int)beforePos.z, (int)beforePos.x] = mapController.battleObject[(int)vec.z, (int)vec.x];
                        mapController.safetyObject[(int)beforePos.z, (int)beforePos.x].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                    }
                    else
                    {
                        mapController.battleObject[(int)PosToIndex(beforelocalPos).z, (int)PosToIndex(beforelocalPos).x] = mapController.battleObject[(int)vec.z, (int)vec.x];
                        mapController.battleObject[(int)PosToIndex(beforelocalPos).z, (int)PosToIndex(beforelocalPos).x].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                    }

                    mapController.battleObject[(int)vec.z, (int)vec.x] = selectedObject;

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

        Vector3 PosToIndex(Vector3 localVec)
        {
            localVec.x /= 1.3f;
            localVec.z -= 2f;
            return localVec;
        }


    }
}
