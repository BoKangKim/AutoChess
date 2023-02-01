using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

namespace ZoneSystem
{
    public class DragAndDrop : MonoBehaviourPun
    {

        MapController mapController;
        GameObject selectedObject;
        Camera cam;
        int ObjectLayer;
        int battleSpaceLayer;
        int safetySpaceLayer;
        int itemLayer;

        public GameObject safetyZoneTile;
        public GameObject battleZoneTile;
        List<GameObject> dragObject;
        Color tileColor;



        Vector3 beforePos;

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
            dragObject = new List<GameObject>();
            tileColor = new Color(51 / 255f, 83 / 255f, 113 / 255f, 1);
        }
        private void Update()
        {
            if (!photonView.IsMine)
            {
                return;
            }
            #region ����Ͽ�
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
            //            if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<UnitClass.Unit>() != null)
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
            //                    vec = PosToIndex(CastRay(battleSpaceLayer).collider.transform.position);
            //                    mapController.battleObject[(int)vec.z, (int)vec.x] = null;

            //                    beforePos = CastRay(battleSpaceLayer).collider.transform.position;
            //                    beforePos = CastRay(battleSpaceLayer).collider.transform.position;
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
            //        if (selectedObject != null)
            //        {
            //            buttonChange();
            //            Drag();
            //        }
            //    }

            //    //Drop
            //    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            //    {
            //        if (selectedObject == null) return;
            //        //���� �Ǹ�
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

            #region PC��
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log(CastRay(battleSpaceLayer).transform);
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
                        selectedObject.transform.parent = PlayerMapSpawner.Map.transform;



                        battleZoneTile.gameObject.SetActive(true);
                        safetyZoneTile.gameObject.SetActive(true);


                        if (CastRay(safetySpaceLayer).collider != null)
                        {
                            var vec = safetyPosToIndex(selectedObject.transform.localPosition);
                            Debug.Log(vec);
                            mapController.safetyObject[vec.z, vec.x] = null;
                            beforePos = CastRay(safetySpaceLayer).collider.transform.localPosition;
                        }
                        else if (CastRay(battleSpaceLayer).collider != null)
                        {
                            var vec = battlePosToIndex(CastRay(battleSpaceLayer).collider.transform.localPosition);
                            mapController.battleObject[(int)vec.z, (int)vec.x] = null;

                            beforePos = CastRay(battleSpaceLayer).collider.transform.localPosition;

                        }
                    }
                    else if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<testItem>() != null)
                    {
                        selectedObject = CastRay(ObjectLayer).collider.gameObject;
                        selectedObject.transform.parent = PlayerMapSpawner.Map.transform;


                        safetyZoneTile.gameObject.SetActive(true);


                        if (CastRay(safetySpaceLayer).collider != null)
                        {
                            var vec = safetyPosToIndex(selectedObject.transform.localPosition);
                            mapController.safetyObject[vec.z, vec.x] = null;
                            beforePos = CastRay(safetySpaceLayer).collider.transform.localPosition;
                        }
                    }
                }
                //Drop
                else
                {
                    // ���� �Ǹ�
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

                    battleZoneTile.gameObject.SetActive(false);
                    safetyZoneTile.gameObject.SetActive(false);
                }
            }
            //Drag
            if (selectedObject != null)
            {
                tileChangeColor();
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

        #region outRange
        void outRange()
        {
            if ((int)beforePos.z < 0)
            {
                mapController.safetyObject[(int)safetyPosToIndex(beforePos).z, (int)safetyPosToIndex(beforePos).x] = selectedObject;
            }
            else
            {
                mapController.battleObject[(int)battlePosToIndex(beforePos).z, (int)battlePosToIndex(beforePos).x] = selectedObject;
            }
            selectedObject.transform.localPosition = new Vector3(beforePos.x, 0.25f, beforePos.z);
            selectedObject = null;
        }
        #endregion

        #region sellUnit
        void sellUnit()
        {
            if (buySellButton && selectedObject.GetComponent<UnitClass.Unit>() != null)
            {
                storeButtonChange(Color.black, Color.white, true, "유닛 구매");
                buySellButton = null;

                Destroy(selectedObject);
            }
        }
        #endregion

        #region buttonChange
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
                    storeButtonChange(Color.black, Color.white, true, "유닛 구매");

                    buySellButton = null;
                }
            }
        }
        #endregion
        #region Drag
        private void Drag()
        {

            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = cam.ScreenToWorldPoint(position);

            selectedObject.transform.position = new Vector3(worldPosition.x, 1f, worldPosition.z);
        }
        #endregion

        #region DropPos
        void DropPosition(int Layer)
        {
            Vector3 worldPosition = CastRay(Layer).collider.transform.localPosition;
            var safetyPos = safetyPosToIndex(worldPosition);
            var battlePos = battlePosToIndex(worldPosition);
            var beforePos = safetyPosToIndex(this.beforePos);



            if (Layer == safetySpaceLayer)
            {
                if (mapController.safetyObject[safetyPos.z, safetyPos.x] == null)
                {
                    mapController.safetyObject[safetyPos.z, safetyPos.x] = selectedObject;
                    selectedObject.transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    mapController.BattleZoneCheck();
                }
                else
                {
                    if ((int)beforePos.z < 0)
                    {
                        beforePos = safetyPosToIndex(this.beforePos);
                        if (Merge(selectedObject, mapController.safetyObject[safetyPos.z, safetyPos.x]))
                        {
                            CastRay(Layer).collider.GetComponent<MeshRenderer>().material.color = tileColor;
                            mapController.safetyObject[beforePos.z, beforePos.x] = null;
                        }
                        else
                        {
                            mapController.safetyObject[beforePos.z, beforePos.x] = mapController.safetyObject[safetyPos.z, safetyPos.x];
                            mapController.safetyObject[beforePos.z, beforePos.x].transform.localPosition = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.safetyObject[safetyPos.z, safetyPos.x] = selectedObject;
                            selectedObject.transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }
                    else
                    {
                        beforePos = battlePosToIndex(this.beforePos);
                        if (Merge(selectedObject, mapController.safetyObject[safetyPos.z, safetyPos.x]))
                        {
                            CastRay(Layer).collider.GetComponent<MeshRenderer>().material.color = tileColor;

                            mapController.battleObject[beforePos.z, beforePos.x] = null;
                        }
                        else
                        {
                            mapController.battleObject[beforePos.z, beforePos.x] = mapController.safetyObject[safetyPos.z, safetyPos.x];
                            mapController.battleObject[beforePos.z, beforePos.x].transform.localPosition = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.safetyObject[safetyPos.z, safetyPos.x] = selectedObject;
                            selectedObject.transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }
                }
            }

            else if (Layer == battleSpaceLayer)
            {
                if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<testItem>() != null)
                {
                    selectedObject.transform.localPosition = new Vector3(beforePos.x, 0.25f, beforePos.z);
                    selectedObject = null;
                    return;
                }


                if (mapController.battleObject[battlePos.z, battlePos.x] == null)
                {
                    mapController.battleObject[battlePos.z, battlePos.x] = selectedObject;
                    selectedObject.transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    mapController.BattleZoneCheck();
                }
                else
                {
                    if ((int)beforePos.z < 0)
                    {
                        beforePos = safetyPosToIndex(this.beforePos);
                        if (Merge(selectedObject, mapController.battleObject[battlePos.z, battlePos.x]))
                        {
                            CastRay(Layer).collider.GetComponent<MeshRenderer>().material.color = tileColor;

                            mapController.safetyObject[beforePos.z, beforePos.x] = null;
                        }
                        else
                        {
                            mapController.safetyObject[beforePos.z, beforePos.x] = mapController.battleObject[battlePos.z, battlePos.x];
                            mapController.safetyObject[beforePos.z, beforePos.x].transform.localPosition = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.battleObject[battlePos.z, battlePos.x] = selectedObject;

                            selectedObject.transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }
                    else
                    {
                        beforePos = battlePosToIndex(this.beforePos);
                        if (Merge(selectedObject, mapController.battleObject[battlePos.z, battlePos.x]))
                        {
                            CastRay(Layer).collider.GetComponent<MeshRenderer>().material.color = tileColor;

                            mapController.battleObject[beforePos.z, beforePos.x] = null;

                        }
                        else
                        {

                            mapController.battleObject[beforePos.z, beforePos.x] = mapController.battleObject[battlePos.z, battlePos.x];
                            mapController.battleObject[beforePos.z, beforePos.x].transform.localPosition = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.battleObject[battlePos.z, battlePos.x] = selectedObject;

                            selectedObject.transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);

                        }
                    }

                }

            }
                selectedObject = null;
        }
        #endregion

        #region �巡�׽� Ÿ�� ���� ����
        void tileChangeColor()
        {
            if (CastRay(safetySpaceLayer).collider != null)
            {
                CastRay(safetySpaceLayer).collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
                if (dragObject.Count != 0)
                {
                    if (CastRay(safetySpaceLayer).transform.localPosition != dragObject[0].transform.localPosition)
                    {
                        dragObject.Add(CastRay(safetySpaceLayer).collider.gameObject);
                    }
                }
                else
                {

                    dragObject.Add(CastRay(safetySpaceLayer).collider.gameObject);
                }
            }

            else if (CastRay(battleSpaceLayer).collider != null)
            {
                CastRay(battleSpaceLayer).collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
                if (dragObject.Count != 0)
                {
                    if (CastRay(battleSpaceLayer).transform.localPosition != dragObject[0].transform.localPosition)
                    {
                        dragObject.Add(CastRay(battleSpaceLayer).collider.gameObject);
                    }

                }
                else
                {
                    dragObject.Add(CastRay(battleSpaceLayer).collider.gameObject);
                }
            }

            if (dragObject.Count > 1)
            {
                dragObject[0].GetComponent<MeshRenderer>().material.color = tileColor;
                dragObject.RemoveAt(0);
            }

        }
        #endregion

        #region 스토어 버튼체인지
        void storeButtonChange(Color text, Color button, bool enabled, string unitStatus)
        {

            buySellButton.image.color = button;
            TextMeshProUGUI buttonText = buySellButton.GetComponentInChildren<TextMeshProUGUI>();
            buySellButton.enabled = enabled;
            buttonText.text = unitStatus;
            buttonText.color = text;
        }
        #endregion

        #region 좌표변환
        int safetyInterval = 3;
        Vector3 battleInterval = new Vector3(1.5f, 0 , 2.5f);
        (int x , int z) battlePosToIndex(Vector3 Vec)
        {

            Vec.z = (Vec.z / battleInterval.z);

            if (Vec.z % 2 == 0) { Vec.x -= battleInterval.x; }

            else{}

            Vec.x /= 3f;
            return ((int)Vec.x,(int)Vec.z);
        }
        (int x, int z) safetyPosToIndex(Vector3 Vec)
        {
            Vec.x = (Vec.x - 1) / safetyInterval;
            Vec.z = (Vec.z + 7) / safetyInterval;
       

            return ((int)Vec.x, (int)Vec.z);
        }
        #endregion

        #region 머지시스템
        public bool Merge(GameObject selectedObject, GameObject stayObject)
        {

            if (selectedObject == null || stayObject == null) return false;
            //���� ����
            if (selectedObject.GetComponent<UnitClass.Unit>() != null && stayObject.GetComponent<UnitClass.Unit>() != null)
            {
                UnitClass.Unit selectedUnit = selectedObject.GetComponent<UnitClass.Unit>();
                UnitClass.Unit stayUnit = stayObject.GetComponent<UnitClass.Unit>();

                if (stayUnit.GetGrade > 3) return false;

                if (selectedUnit.GetGrade == stayUnit.GetGrade)
                {

                    Destroy(selectedUnit.gameObject);
                    stayUnit.Upgrade(); //���׷��̵� ���� ��� ����(2023.01.18 15:08-�̿���)

                   // stayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    return true;
                }
            }
            //��� ����
            else if(selectedObject.GetComponent<testItem>() != null && stayObject.GetComponent<testItem>() != null)
            {
                testItem selectedItem = selectedObject.GetComponent<testItem>();
                testItem stayItem = stayObject.GetComponent<testItem>();

                if (stayItem.itemNum > 2) return false;

                if (selectedItem.itemNum == stayItem.itemNum)
                {
                    Destroy(selectedItem.gameObject);
                    ++stayItem.itemNum;

                    //stayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    return true;
                }
            }
            //��� ����
            else if(selectedObject.GetComponent<testItem>() != null && stayObject.GetComponent<UnitClass.Unit>() != null)
            {
                    Destroy(selectedObject.gameObject);
                    stayObject.gameObject.name = "Item_Equip";

                    //stayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    //stayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    return true;
            }
            return false;
        }
        #endregion
    }


}
