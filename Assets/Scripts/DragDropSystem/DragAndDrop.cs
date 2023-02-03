using System.Collections.Generic;
using TMPro;
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
            safetySpaceLayer = 1 << LayerMask.NameToLayer("SafetySpace");
            battleSpaceLayer = 1 << LayerMask.NameToLayer("BattleSpace");
            ObjectLayer = 1 << LayerMask.NameToLayer("Object");
            itemLayer = 1 << LayerMask.NameToLayer("Item");
            dragObject = new List<GameObject>();
            tileColor = new Color(51 / 255f, 83 / 255f, 113 / 255f, 1);
        }
        private void Update()
        {
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
                        mapController.SellUnitOutItem(selectedObject.transform.GetChild(i).gameObject);
                        selectedObject.transform.GetChild(i).gameObject.SetActive(true);
                        selectedObject.transform.GetChild(i).transform.parent = null;
                        count--;
                        i--;
                    }
                }
               
            }
            posCheckButton = null;

            Destroy(selectedObject);
            selectedObject = null;
            storeButtonChange();
            battleZoneTile.gameObject.SetActive(false);
            safetyZoneTile.gameObject.SetActive(false);
        }
        #endregion

        #region buttonChange
        void buttonPosCheck()
        {

            if (UIManager.Inst.RaycastUI<Button>(1) != null && selectedObject.GetComponent<UnitClass.Unit>() != null)
            {
                posCheckButton = UIManager.Inst.RaycastUI<Button>(1);

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

        #region ����
        //���� + ������ ����
        public bool Merge(GameObject selectedObject, GameObject stayObject)
        {

            if (selectedObject == null || stayObject == null) return false;
            //���� ����
            if (selectedObject.GetComponent<UnitClass.Unit>() != null && stayObject.GetComponent<UnitClass.Unit>() != null)
            {
                UnitClass.Unit selectedUnit = selectedObject.GetComponent<UnitClass.Unit>();
                UnitClass.Unit stayUnit = stayObject.GetComponent<UnitClass.Unit>();

                int stayEqCount = stayUnit.GetEquipmentCount;
                int selectEqCount = selectedUnit.GetEquipmentCount;
                if (stayUnit.GetGrade > 3) return false;

                if (selectedUnit.GetGrade == stayUnit.GetGrade && selectedUnit.GetSynergyName == stayUnit.GetSynergyName)
                {


                    if (stayEqCount + selectEqCount < 4) // 그냥 머지(한 캐릭에 몰아주기)
                    {
                        for (int i = selectEqCount; i < 0; i--)
                        {
                            selectedUnit.transform.GetChild(i).transform.parent = stayUnit.transform;
                        }
                        stayUnit.EquipItem(selectEqCount + stayEqCount);
                    }



                    else // 아이템이 4개 이상이라서 확인이 필요->
                    {

                    }
                    Destroy(selectedUnit.gameObject);
                    stayUnit.Upgrade(); //���׷��̵� ���� ��� ����(2023.01.18 15:08-�̿���)

                    // stayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    return true;
                }
            }
            //��� ����
            else if (selectedObject.GetComponent<Equipment>() != null && stayObject.GetComponent<Equipment>() != null)
            {
                Equipment selectedItem = selectedObject.GetComponent<Equipment>();
                Equipment stayItem = stayObject.GetComponent<Equipment>();

                if (stayItem.GetEquipmentGrade > 2) return false;

                if (selectedItem.GetEquipmentGrade == stayItem.GetEquipmentGrade && selectedItem.GetEquipmentName == stayItem.GetEquipmentName)
                {
                    Destroy(selectedItem.gameObject);
                    stayItem.Upgrade();

                    //stayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    return true;
                }
            }
            //��� ����
            else if (selectedObject.GetComponent<Equipment>() != null && stayObject.GetComponent<UnitClass.Unit>() != null)
            {
                int eqcount = stayObject.GetComponent<UnitClass.Unit>().GetEquipmentCount;
                if (eqcount > 2)
                {
                    return false;
                }
                else
                {
                    selectedObject.transform.parent = stayObject.transform;
                    stayObject.GetComponent<UnitClass.Unit>().EquipItem(eqcount);
                }
                selectedObject.SetActive(false);
                //stayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                //stayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                return true;
            }
            return false;
        }
        #endregion
    }


}
