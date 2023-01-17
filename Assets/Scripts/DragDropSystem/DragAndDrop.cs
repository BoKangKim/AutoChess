using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
namespace ZoneSystem
{
    public class DragAndDrop : MonoBehaviour
    {
        MapController mapController;
        GameObject selectedObject;
        Camera cam;
        int ObjectLayer;
        int battleSpaceLayer;
        int safetySpaceLayer;
        int itemLayer;

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
            ObjectLayer = 1 << LayerMask.NameToLayer("Object");
            itemLayer = 1 << LayerMask.NameToLayer("Item");
        }
        private void Update()
        {
            #region 모바일용
            //if (Input.touchCount == 1)
            //{
            //    if (Input.GetTouch(0).phase == TouchPhase.Began)
            //    {
            //        if (CastRay(itemLayer).collider != null)
            //        {
            //            mapController.itemGain(CastRay(itemLayer).collider.gameObject);
            //            return;
            //        }

            //        if (selectedObject == null)
            //        {
            //            if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<testscript>() != null)
            //            {
            //                selectedObject = CastRay(ObjectLayer).collider.gameObject;
            //                Vector3 vec;

            //                if (CastRay(safetySpaceLayer).collider != null)
            //                {
            //                    vec = selectedObject.transform.position;
            //                    mapController.safetyObject[(int)vec.z, (int)vec.x] = null;
            //                    beforePos = CastRay(safetySpaceLayer).collider.transform.position;
            //                }
            //                else if (CastRay(battleSpaceLayer).collider != null)
            //                {
            //                    vec = PosToIndex(CastRay(battleSpaceLayer).collider.transform.localPosition);
            //                    mapController.battleObject[(int)vec.z, (int)vec.x] = null;

            //                    beforePos = CastRay(battleSpaceLayer).collider.transform.position;
            //                    beforelocalPos = CastRay(battleSpaceLayer).collider.transform.localPosition;
            //                }
            //            }
            //            else if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<testItem>() != null)
            //            {
            //                Vector3 vec;
            //                selectedObject = CastRay(ObjectLayer).collider.gameObject;

            //                if (CastRay(safetySpaceLayer).collider != null)
            //                {
            //                    vec = selectedObject.transform.position;
            //                    mapController.safetyObject[(int)vec.z, (int)vec.x] = null;
            //                    beforePos = CastRay(safetySpaceLayer).collider.transform.position;
            //                }
            //            }
            //        }
            //    }
            //    //Drag

            //    else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            //    {
            //        //Drag
            //        if(selectedObject != null)
            //        {
            //            buttonChange();
            //            Drag();
            //        }
            //    }

            //    //Drop
            //    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            //    {
            //        if (selectedObject == null) return;
            //        //유닛 판매
            //        sellUnit();

            //        if (EventSystem.current.IsPointerOverGameObject()) return;

            //        if (CastRay(safetySpaceLayer).collider != null)
            //        {
            //            DropPosition(safetySpaceLayer);
            //        }
            //        else if (CastRay(battleSpaceLayer).collider != null)
            //        {
            //            DropPosition(battleSpaceLayer);
            //        }
            //        else
            //        {
            //            outRange();
            //        }
            //    }
            //}
            #endregion

            #region PC용
            if (Input.GetMouseButtonDown(0))
            {
                if (CastRay(itemLayer).collider != null)
                {
                    mapController.itemGain(CastRay(itemLayer).collider.gameObject);
                    return;
                }

                if (selectedObject == null)
                {
                    if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<UnitClass.Unit>() != null)
                    {
                        selectedObject = CastRay(ObjectLayer).collider.gameObject;
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
                    else if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<testItem>() != null)
                    {
                        Vector3 vec;
                        selectedObject = CastRay(ObjectLayer).collider.gameObject;

                        if (CastRay(safetySpaceLayer).collider != null)
                        {
                            vec = selectedObject.transform.position;
                            mapController.safetyObject[(int)vec.z, (int)vec.x] = null;
                            beforePos = CastRay(safetySpaceLayer).collider.transform.position;
                        }
                    }
                }
                //Drop
                else
                {
                    // 유닛 판매
                    sellUnit();

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
                        outRange();
                    }
                }
            }
            //Drag
            if (selectedObject != null)
            {
                buttonChange();
                Drag();
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



        void outRange()
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

        void sellUnit()
        {
            if (buySellButton && selectedObject.GetComponent<UnitClass.Unit>() != null)
            {
                storeButtonChange(Color.black, Color.white, true, "유닛 소환");
                buySellButton = null;

                Destroy(selectedObject);
            }
        }
        void buttonChange()
        {

            if (UIManager.Inst.RaycastUI<Button>(1) != null && selectedObject.GetComponent<UnitClass.Unit>() != null)
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
        }
        private void Drag()
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = cam.ScreenToWorldPoint(position);

            selectedObject.transform.position = new Vector3(worldPosition.x, 1f, worldPosition.z);
        }


        #region DropPos
        void DropPosition(int Layer)
        {
            Vector3 worldPosition = CastRay(Layer).collider.transform.position;
            int worldPosX = (int)worldPosition.x;
            int worldPosZ = (int)worldPosition.z;
            int beforePosX = (int)beforePos.x;
            int beforePosZ = (int)beforePos.z;


            if (Layer == safetySpaceLayer)
            {
                if (mapController.safetyObject[worldPosZ, worldPosX] == null)
                {
                    mapController.safetyObject[worldPosZ, worldPosX] = selectedObject;
                    selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    mapController.BattlezoneChack();
                    selectedObject = null;
                }
                else
                {
                    if ((int)beforePos.z < 2)
                    {

                        if (Merge(selectedObject, mapController.safetyObject[worldPosZ, worldPosX]))
                        {
                            mapController.safetyObject[beforePosZ, beforePosX] = null;
                        }

                        else
                        {
                            mapController.safetyObject[beforePosZ, beforePosX] = mapController.safetyObject[worldPosZ, worldPosX];
                            mapController.safetyObject[beforePosZ, beforePosX].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.safetyObject[worldPosZ, worldPosX] = selectedObject;
                            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }
                    else
                    {
                        if (Merge(selectedObject, mapController.safetyObject[worldPosZ, worldPosX]))
                        {
                            mapController.battleObject[(int)PosToIndex(beforelocalPos).z, (int)PosToIndex(beforelocalPos).x] = null;
                        }
                        else
                        {
                            mapController.battleObject[(int)PosToIndex(beforelocalPos).z, (int)PosToIndex(beforelocalPos).x] = mapController.safetyObject[worldPosZ, worldPosX];
                            mapController.battleObject[(int)PosToIndex(beforelocalPos).z, (int)PosToIndex(beforelocalPos).x].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.safetyObject[worldPosZ, worldPosX] = selectedObject;
                            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }

                    selectedObject = null;
                }
            }

            else if (Layer == battleSpaceLayer)
            {
                if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<testItem>() != null)
                {
                    selectedObject.transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                    selectedObject = null;
                    return;
                }

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
                    if ((int)beforePos.z < 2)
                    {
                        if (Merge(selectedObject, mapController.battleObject[(int)vec.z, (int)vec.x]))
                        {
                            mapController.safetyObject[beforePosZ, beforePosX] = null;
                        }
                        else
                        {
                            mapController.safetyObject[beforePosZ, beforePosX] = mapController.battleObject[(int)vec.z, (int)vec.x];
                            mapController.safetyObject[beforePosZ, beforePosX].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.battleObject[(int)vec.z, (int)vec.x] = selectedObject;

                            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }
                    else
                    {
                        if (Merge(selectedObject, mapController.battleObject[(int)vec.z, (int)vec.x]))
                        {
                            mapController.battleObject[(int)PosToIndex(beforelocalPos).z, (int)PosToIndex(beforelocalPos).x] = null;

                        }
                        else
                        {
                            mapController.battleObject[(int)PosToIndex(beforelocalPos).z, (int)PosToIndex(beforelocalPos).x] = mapController.battleObject[(int)vec.z, (int)vec.x];
                            mapController.battleObject[(int)PosToIndex(beforelocalPos).z, (int)PosToIndex(beforelocalPos).x].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.battleObject[(int)vec.z, (int)vec.x] = selectedObject;

                            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);

                        }
                    }
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

        //배틀존 좌표를 인덱스 값으로 변환
        Vector3 PosToIndex(Vector3 localVec)
        {
            localVec.x /= 1.3f;
            localVec.z -= 2f;
            return localVec;
        }

        //유닛 + 아이템 머지
        public bool Merge(GameObject selectedObject, GameObject stayObject)
        {
            if (selectedObject == null || stayObject == null) return false;
            //유닛 머지
            if (selectedObject.GetComponent<UnitClass.Unit>() != null && stayObject.GetComponent<UnitClass.Unit>() != null)
            {
                UnitClass.Unit selectedUnit = selectedObject.GetComponent<UnitClass.Unit>();
                UnitClass.Unit stayUnit = stayObject.GetComponent<UnitClass.Unit>();

                if (stayUnit.grade > 2) return false;

                if (selectedUnit.grade == stayUnit.grade)
                {
                    Destroy(selectedUnit.gameObject);
                    ++stayUnit.grade;

                    stayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    return true;
                }
            }
            //장비 머지
            else if(selectedObject.GetComponent<testItem>() != null && stayObject.GetComponent<testItem>() != null)
            {
                testItem selectedItem = selectedObject.GetComponent<testItem>();
                testItem stayItem = stayObject.GetComponent<testItem>();

                if (stayItem.itemNum > 2) return false;

                if (selectedItem.itemNum == stayItem.itemNum)
                {
                    Destroy(selectedItem.gameObject);
                    ++stayItem.itemNum;

                    stayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    return true;
                }
            }
            //장비 장착
            else if(selectedObject.GetComponent<testItem>() != null && stayObject.GetComponent<UnitClass.Unit>() != null)
            {
                
                    Destroy(selectedObject.gameObject);
                    stayObject.gameObject.name = "Item_Equip";

                    stayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    return true;
                
            }
            return false;
        }
    }

    
}
