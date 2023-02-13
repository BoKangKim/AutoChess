using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ZoneSystem
{
    public class DragAndDrop : MonoBehaviour
    {

        // ��ǥ 
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

        Button posCheckButton = null;

        private void Awake()
        {
            mapController = GetComponent<MapController>();
        }
        private void Start()
        {
            cam = Camera.main;
            safetySpaceLayer = 1 << LayerMask.NameToLayer("SafetySpace"); //이거 비트연산자가 더 빠르지 않나?
            battleSpaceLayer = 1 << LayerMask.NameToLayer("BattleSpace");
            ObjectLayer = 1 << LayerMask.NameToLayer("Object");
            itemLayer = 1 << LayerMask.NameToLayer("Item");
            dragObject = new List<GameObject>();
            tileColor = new Color(51 / 255f, 83 / 255f, 113 / 255f, 1);
        }
        private void Update()
        {
            #region 모바일버전
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

            #region PC버전
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
                        Vector3 vec;

                        battleZoneTile.gameObject.SetActive(true);
                        safetyZoneTile.gameObject.SetActive(true);


                        if (CastRay(safetySpaceLayer).collider != null)
                        {
                            vec = safetyPosToIndex(selectedObject.transform.position);
                            mapController.safetyObject[(int)vec.z, (int)vec.x] = null;
                            beforePos = CastRay(safetySpaceLayer).collider.transform.position;
                        }
                        else if (CastRay(battleSpaceLayer).collider != null)
                        {
                            vec = battlePosToIndex(CastRay(battleSpaceLayer).collider.transform.position);
                            mapController.battleObject[(int)vec.z, (int)vec.x] = null;
                            beforePos = CastRay(battleSpaceLayer).collider.transform.position;
                        }
                    }
                    else if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<Equipment>() != null)
                    {
                        Vector3 vec;
                        selectedObject = CastRay(ObjectLayer).collider.gameObject;

                        safetyZoneTile.gameObject.SetActive(true);


                        if (CastRay(safetySpaceLayer).collider != null)
                        {
                            vec = safetyPosToIndex(selectedObject.transform.position);
                            mapController.safetyObject[(int)vec.z, (int)vec.x] = null;
                            beforePos = CastRay(safetySpaceLayer).collider.transform.position;
                        }
                    }
                }
                //Drop
                else
                {
                    // ���� �Ǹ�
                    sellObject();

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
                storeButtonChange();

            }
            //Drag
            if (selectedObject != null)
            {
                tileChangeColor();
                buttonPosCheck();
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
            selectedObject.transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
            selectedObject = null;
        }
        #endregion

        #region sellUnit
        void sellObject()
        {

            if (posCheckButton && selectedObject.GetComponent<UnitClass.Unit>() != null)
            {
                int count = selectedObject.GetComponent<UnitClass.Unit>().GetEquipmentCount;
                if (count != 0) // 판매시 장비 뱉는 로직
                {
                    for (int i = 0; i < count; i++)
                    {
                        mapController.UnitOutItem(selectedObject.transform.GetChild(i).gameObject);
                        count--;
                        i--;
                    }
                }
                posCheckButton = null;

                Destroy(selectedObject);
                selectedObject = null;
                storeButtonChange();
                battleZoneTile.gameObject.SetActive(false);
                safetyZoneTile.gameObject.SetActive(false);
            }
            if (posCheckButton && selectedObject.GetComponent<Equipment>() != null)
            {
                posCheckButton = null;

                Destroy(selectedObject);
                selectedObject = null;
                storeButtonChange();
                battleZoneTile.gameObject.SetActive(false);
                safetyZoneTile.gameObject.SetActive(false);


            }


        }
        #endregion

        #region buttonChange
        void buttonPosCheck()
        {

            if (UIManager.Inst.RaycastUI<Button>(1) != null && selectedObject.GetComponent<UnitClass.Unit>() != null)
            {
                posCheckButton = UIManager.Inst.RaycastUI<Button>(1);
                Debug.Log(UIManager.Inst.RaycastUI<Button>(0));
            }
            else
            {
                if (posCheckButton != null)
                {

                    posCheckButton = null;
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
            Vector3 worldPosition = CastRay(Layer).collider.transform.position;
            int worldPosX = (int)worldPosition.x;
            int worldPosZ = (int)worldPosition.z;

            int beforePosX = (int)beforePos.x;
            int beforePosZ = (int)beforePos.z;


            if (Layer == safetySpaceLayer)
            {
                if (mapController.safetyObject[(int)safetyPosToIndex(worldPosition).z, (int)safetyPosToIndex(worldPosition).x] == null)
                {
                    mapController.safetyObject[(int)safetyPosToIndex(worldPosition).z, (int)safetyPosToIndex(worldPosition).x] = selectedObject;
                    selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    mapController.BattleZoneCheck();
                    selectedObject = null;
                }
                else
                {
                    if ((int)beforePos.z < 0)
                    {

                        if (Merge(selectedObject, mapController.safetyObject[(int)safetyPosToIndex(worldPosition).z, (int)safetyPosToIndex(worldPosition).x]))
                        {
                            CastRay(Layer).collider.GetComponent<MeshRenderer>().material.color = tileColor;
                            mapController.safetyObject[(int)safetyPosToIndex(beforePos).z, (int)safetyPosToIndex(beforePos).x] = null;
                        }
                        else
                        {
                            mapController.safetyObject[(int)safetyPosToIndex(beforePos).z, (int)safetyPosToIndex(beforePos).x] = mapController.safetyObject[(int)safetyPosToIndex(worldPosition).z, (int)safetyPosToIndex(worldPosition).x];
                            mapController.safetyObject[(int)safetyPosToIndex(beforePos).z, (int)safetyPosToIndex(beforePos).x].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.safetyObject[(int)safetyPosToIndex(worldPosition).z, (int)safetyPosToIndex(worldPosition).x] = selectedObject;
                            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }
                    else
                    {
                        if (Merge(selectedObject, mapController.safetyObject[(int)safetyPosToIndex(worldPosition).z, (int)safetyPosToIndex(worldPosition).x]))
                        {
                            CastRay(Layer).collider.GetComponent<MeshRenderer>().material.color = tileColor;

                            mapController.battleObject[(int)battlePosToIndex(beforePos).z, (int)battlePosToIndex(beforePos).x] = null;
                        }
                        else
                        {
                            mapController.battleObject[(int)battlePosToIndex(beforePos).z, (int)battlePosToIndex(beforePos).x] = mapController.safetyObject[(int)safetyPosToIndex(worldPosition).z, (int)safetyPosToIndex(worldPosition).x];
                            mapController.battleObject[(int)battlePosToIndex(beforePos).z, (int)battlePosToIndex(beforePos).x].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.safetyObject[(int)safetyPosToIndex(worldPosition).z, (int)safetyPosToIndex(worldPosition).x] = selectedObject;
                            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }
                    selectedObject = null;
                }
            }

            else if (Layer == battleSpaceLayer)
            {
                if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<Equipment>() != null)
                {
                    selectedObject.transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                    selectedObject = null;
                    return;
                }

                Vector3 vec = battlePosToIndex(CastRay(Layer).collider.transform.position);

                if (mapController.battleObject[(int)vec.z, (int)vec.x] == null)
                {
                    mapController.battleObject[(int)vec.z, (int)vec.x] = selectedObject;
                    selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    mapController.BattleZoneCheck();
                    selectedObject = null;
                }
                else
                {
                    if ((int)beforePos.z < 0)
                    {
                        if (Merge(selectedObject, mapController.battleObject[(int)vec.z, (int)vec.x]))
                        {
                            CastRay(Layer).collider.GetComponent<MeshRenderer>().material.color = tileColor;

                            mapController.safetyObject[(int)safetyPosToIndex(beforePos).z, (int)safetyPosToIndex(beforePos).x] = null;
                        }
                        else
                        {
                            mapController.safetyObject[(int)safetyPosToIndex(beforePos).z, (int)safetyPosToIndex(beforePos).x] = mapController.battleObject[(int)vec.z, (int)vec.x];
                            mapController.safetyObject[(int)safetyPosToIndex(beforePos).z, (int)safetyPosToIndex(beforePos).x].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.battleObject[(int)vec.z, (int)vec.x] = selectedObject;

                            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }
                    else
                    {
                        if (Merge(selectedObject, mapController.battleObject[(int)vec.z, (int)vec.x]))
                        {
                            CastRay(Layer).collider.GetComponent<MeshRenderer>().material.color = tileColor;

                            mapController.battleObject[(int)battlePosToIndex(beforePos).z, (int)battlePosToIndex(beforePos).x] = null;

                        }
                        else
                        {
                            mapController.battleObject[(int)battlePosToIndex(beforePos).z, (int)battlePosToIndex(beforePos).x] = mapController.battleObject[(int)vec.z, (int)vec.x];
                            mapController.battleObject[(int)battlePosToIndex(beforePos).z, (int)battlePosToIndex(beforePos).x].transform.position = new Vector3(beforePos.x, 0.25f, beforePos.z);
                            mapController.battleObject[(int)vec.z, (int)vec.x] = selectedObject;

                            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);

                        }
                    }
                    selectedObject = null;

                }
            }
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
                    if (CastRay(safetySpaceLayer).transform.position != dragObject[0].transform.position)
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
                    if (CastRay(battleSpaceLayer).transform.position != dragObject[0].transform.position)
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

        #region ��ưü����
        void storeButtonChange()
        {
            if (selectedObject == null)
            {
                UIManager.Inst.unitBuyButton.gameObject.SetActive(true);
                UIManager.Inst.equipmentBuyButton.gameObject.SetActive(true);
                UIManager.Inst.sellButton.gameObject.SetActive(false);
            }
            else
            {
                UIManager.Inst.unitBuyButton.gameObject.SetActive(false);
                UIManager.Inst.equipmentBuyButton.gameObject.SetActive(false);
                UIManager.Inst.sellButton.gameObject.SetActive(true);
            }


        }
        #endregion

        #region ��ǥ�� �ε����� ��ȯ

        //��Ʋ�� ��ǥ�� �ε��� ������ ��ȯ
        Vector3 battlePosToIndex(Vector3 Vec)
        {
            //Debug.Log($"pos + {Vec}");

            Vec.z = (Vec.z / 2.5f);

            if (Vec.z % 2 == 0) { Vec.x -= 1.5f; }

            else { }

            Vec.x /= 3f;
            //Debug.Log($"index + {Vec}");
            return Vec;
        }
        Vector3 safetyPosToIndex(Vector3 Vec)
        {
            Vec.x = (Vec.x - 1) / 3;
            Vec.z = (Vec.z + 7) / 3;


            return Vec;
        }
        #endregion
        #region 장비 auto merge 관련
        public bool EquipmentAutoMergeResult(List<Transform> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = i + 1; j < items.Count; j++)
                {
                    Transform item1 = items[i];
                    Transform item2 = items[j];
                    if (item1.GetComponent<Equipment>().GetEquipmentName == item2.GetComponent<Equipment>().GetEquipmentName
                                && item1.GetComponent<Equipment>().GetEquipmentGrade == item2.GetComponent<Equipment>().GetEquipmentGrade
                                && item1.GetComponent<Equipment>().GetEquipmentGrade < 4)
                    {
                        item1.GetComponent<Equipment>().Upgrade();
                        items.Remove(item2);
                        if (EquipmentAutoMergeResult(items) == false) return false;
                    }
                }
            }
            return items.Count <= 3;
        }
        public bool MergeItemResult(Transform selected, Transform stay)
        {
            List<Transform> items = new List<Transform>();

            for (int i = 0; i < selected.childCount; i++)
            {
                selected.GetChild(i).GetComponent<Equipment>().SaveGrade();
                items.Add(selected.GetChild(i).transform);
            }

            for (int i = 0; i < stay.childCount; i++)
            {
                stay.GetChild(i).GetComponent<Equipment>().SaveGrade();
                items.Add(stay.GetChild(i).transform);
            }

            return EquipmentAutoMergeResult(items);
        }

        public void EquipmentAutoMerge(Transform selected, Transform stay)
        {
            List<Transform> items = new List<Transform>();

            for (int i = 0; i < selected.childCount; i++)
            {
                items.Add(selected.GetChild(i).transform);
            }

            for (int i = 0; i < stay.childCount; i++)
            {
                items.Add(stay.GetChild(i).transform);
            }

            bool merged = true;
            while (merged)
            {
                merged = false;
                for (int i = 0; i < items.Count; i++)
                {
                    for (int j = i + 1; j < items.Count; j++)
                    {
                        Transform item1 = items[i];
                        Transform item2 = items[j];
                        if (item1.GetComponent<Equipment>().GetEquipmentName == item2.GetComponent<Equipment>().GetEquipmentName && item1.GetComponent<Equipment>().GetEquipmentGrade == item2.GetComponent<Equipment>().GetEquipmentGrade && item1.GetComponent<Equipment>().GetEquipmentGrade < 4 && item2.GetComponent<Equipment>().GetEquipmentGrade < 4)
                        {
                            item1.GetComponent<Equipment>().Upgrade();
                            items.Remove(item2);
                            Destroy(item2.gameObject);
                            merged = true;
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                items[i].transform.parent = stay.transform;
            }

        }

        public bool EquipItemToUnitAutoMerge(Transform Item, Transform stay)
        {
            List<Transform> items = new List<Transform>();

            for (int i = 0; i < stay.childCount; i++)
            {
                items.Add(stay.GetChild(i).transform);
            }

            items.Add(Item);

            bool isMerged = false;
            do
            {
                isMerged = false;
                for (int i = 0; i < items.Count; i++)
                {
                    for (int j = 0; j < items.Count; j++)
                    {
                        if (i != j)
                        {
                            Transform item1 = items[i];
                            Transform item2 = items[j];
                            if (item1.GetComponent<Equipment>().GetEquipmentName == item2.GetComponent<Equipment>().GetEquipmentName
                                && item1.GetComponent<Equipment>().GetEquipmentGrade == item2.GetComponent<Equipment>().GetEquipmentGrade
                                && item1.GetComponent<Equipment>().GetEquipmentGrade < 4)
                            {
                                item1.GetComponent<Equipment>().Upgrade(); //여기가 문제임 여기서 다해
                                items.Remove(item2);
                                Destroy(item2.gameObject);
                                isMerged = true;
                            }
                        }
                    }
                }
            } while (isMerged);
            if (items.Count < 4) return true;
            else return false;
        }

        #endregion
        #region Merge

        public bool Merge(GameObject selectedObject, GameObject stayObject)
        {

            if (selectedObject == null || stayObject == null) return false;
            //유닛 끼리 머지
            if (selectedObject.GetComponent<UnitClass.Unit>() != null && stayObject.GetComponent<UnitClass.Unit>() != null)
            {
                UnitClass.Unit selectedUnit = selectedObject.GetComponent<UnitClass.Unit>();
                UnitClass.Unit stayUnit = stayObject.GetComponent<UnitClass.Unit>();

                selectedUnit.EquipCount();
                stayUnit.EquipCount();

                int selectedEqCount = selectedUnit.GetEquipmentCount;
                int stayEqCount = stayUnit.GetEquipmentCount;


                if (selectedUnit.GetGrade == stayUnit.GetGrade && selectedUnit.GetSynergyName == stayUnit.GetSynergyName&&stayUnit.GetGrade <4)
                {

                    if (stayEqCount + selectedEqCount < 4)
                    {
                        EquipmentAutoMerge(selectedObject.transform, stayObject.transform);
                        stayUnit.EquipItem();
                        Destroy(selectedUnit.gameObject);
                        stayUnit.Upgrade();
                    }

                    else if (stayEqCount + selectedEqCount > 3 && MergeItemResult(selectedObject.transform, stayObject.transform) == true)
                    {
                        for (int i = 0; i < selectedEqCount; i++)
                        {
                            selectedUnit.transform.GetChild(i).GetComponent<Equipment>().LoadGrade();
                        }

                        for(int i = 0; i<stayEqCount; i++)
                        {
                            stayUnit.transform.GetChild(i).GetComponent<Equipment>().LoadGrade();
                        }

                        EquipmentAutoMerge(selectedObject.transform, stayObject.transform);
                        stayUnit.EquipItem();
                        Destroy(selectedUnit.gameObject);
                        stayUnit.Upgrade();
                    }

                    else if (stayEqCount + selectedEqCount > 3 && MergeItemResult(selectedObject.transform, stayObject.transform) == false)
                    {
                        if (mapController.SafetyZoneCheck() + selectedEqCount > 14)//얘는 그냥 머지 X
                        {
                            return false;
                        }
                        else
                        {
                            for (int i = 0; i < selectedEqCount; i++)//얘는 장비 사출
                            {
                                mapController.UnitOutItem(selectedUnit.transform.GetChild(i).gameObject);
                                selectedEqCount--;
                                i--;
                            }
                            Destroy(selectedUnit.gameObject);
                            stayUnit.Upgrade();
                        }
                    }




                    return true;
                }
            }
            //장비 끼리 머지
            else if (selectedObject.GetComponent<Equipment>() != null && stayObject.GetComponent<Equipment>() != null)
            {
                Equipment selectedItem = selectedObject.GetComponent<Equipment>();
                Equipment stayItem = stayObject.GetComponent<Equipment>();

                if (stayItem.GetEquipmentGrade > 3) return false;

                if (selectedItem.GetEquipmentGrade == stayItem.GetEquipmentGrade && selectedItem.GetEquipmentName == stayItem.GetEquipmentName)
                {
                    Destroy(selectedItem.gameObject);
                    stayItem.Upgrade();


                    return true;
                }
            }
            //유닛에 장비 장착
            else if (selectedObject.GetComponent<Equipment>() != null && stayObject.GetComponent<UnitClass.Unit>() != null)
            {

                if (!EquipItemToUnitAutoMerge(selectedObject.transform, stayObject.transform)) //여기서 automerge result 받아서 false이면 return
                {
                    Debug.Log("오토머지 안되서 장비 장착 불가능");
                    return false;
                }
                else //여기서 automerge result 받아서 true이기때문에 갯수 상관없이 auto merge 실행
                {
                    Debug.Log("오토머지 가능해서 장비 장착 ");
                    selectedObject.transform.parent = stayObject.transform;
                    stayObject.GetComponent<UnitClass.Unit>().EquipItem();
                }
                selectedObject.SetActive(false);

                ///여기 같은 장비 들어가면 오토 머지 되어야함 유닛 안에서
                return true;
            }
            return false;
        }
        #endregion
    }


}
