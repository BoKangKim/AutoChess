using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

namespace ZoneSystem
{
    public class DragAndDrop : MonoBehaviourPun
    {
        private PlayerData playerData;
        private MapController mapController;
        private GameObject selectedObject;
        private Camera cam;
        private int ObjectLayer;
        private int battleSpaceLayer;
        private int safetySpaceLayer;
        private int itemLayer;

        public GameObject safetyZoneTile;
        public GameObject battleZoneTile;
        private List<GameObject> dragObject;
        private Color tileColor;


        private Vector3 beforePos;

        Button posCheckButton = null;

        private void Awake()
        {
            mapController = GetComponent<MapController>();
            playerData = GetComponent<PlayerData>();
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

            if (photonView.IsMine)
            {
                safetyZoneTile = PlayerMapSpawner.Map.transform.Find("Tile").gameObject;
                safetyZoneTile = safetyZoneTile.transform.Find("SafetyZone").gameObject;

                battleZoneTile = PlayerMapSpawner.Map.transform.Find("Tile").gameObject;
                battleZoneTile = battleZoneTile.transform.Find("BattleZone").gameObject;
            }
        }
        private void Update()
        {
            if (!photonView.IsMine)
            {
                return;
            }


            #region PC��
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
                        //selectedObject.transform.parent = PlayerMapSpawner.Map.transform;



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
                    else if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<Equipment>() != null)
                    {
                        selectedObject = CastRay(ObjectLayer).collider.gameObject;
                        //selectedObject.transform.parent = PlayerMapSpawner.Map.transform;


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
                    sellObject();

                    if (EventSystem.current.IsPointerOverGameObject()) return;

                    Debug.Log(CastRay(safetySpaceLayer).collider);
                    Debug.Log(CastRay(battleSpaceLayer).collider);

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
        private RaycastHit CastRay(int Layer)
        {
            Vector3 screenMousePosFar = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));
            Vector3 screenMousePosNear = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

            Physics.Raycast(screenMousePosNear, screenMousePosFar - screenMousePosNear, out RaycastHit hit, Vector3.Magnitude(screenMousePosFar - screenMousePosNear), Layer);
            Debug.DrawRay(screenMousePosNear, screenMousePosFar - screenMousePosNear, Color.red);

            return hit;
        }
        #endregion

        #region outRange
        private void outRange()
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
                posCheckButton = null;

                Destroy(selectedObject);

                playerData.gold += 3;

                selectedObject = null;
                storeButtonChange();
                battleZoneTile.gameObject.SetActive(false);
                safetyZoneTile.gameObject.SetActive(false);


            }
            if (posCheckButton && selectedObject.GetComponent<Equipment>() != null)
            {
                posCheckButton = null;

                Destroy(selectedObject);
                playerData.gold += 3;

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
            if (UIManager.Inst.RaycastUI<Button>(1) != null && selectedObject != null)
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
        private void DropPosition(int Layer)
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
                    if ((int)this.beforePos.z < 0)
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
                            mapController.safetyObject[beforePos.z, beforePos.x].transform.localPosition = new Vector3(this.beforePos.x, 0.25f, this.beforePos.z);
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
                            mapController.battleObject[beforePos.z, beforePos.x].transform.localPosition = new Vector3(this.beforePos.x, 0.25f, this.beforePos.z);
                            mapController.safetyObject[safetyPos.z, safetyPos.x] = selectedObject;
                            selectedObject.transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }
                }
            }

            else if (Layer == battleSpaceLayer)
            {
                if (CastRay(ObjectLayer).collider != null && CastRay(ObjectLayer).collider.GetComponent<Equipment>() != null)
                {
                    selectedObject.transform.localPosition = new Vector3(this.beforePos.x, 0.25f, this.beforePos.z);
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
                    if ((int)this.beforePos.z < 0)
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
                            mapController.safetyObject[beforePos.z, beforePos.x].transform.localPosition = new Vector3(this.beforePos.x, 0.25f, this.beforePos.z);
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
                            mapController.battleObject[beforePos.z, beforePos.x].transform.localPosition = new Vector3(this.beforePos.x, 0.25f, this.beforePos.z);
                            mapController.battleObject[battlePos.z, battlePos.x] = selectedObject;

                            selectedObject.transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);

                        }
                    }

                }

            }
                selectedObject = null;
        }
        #endregion

        #region  타일색바뀜
        private void tileChangeColor()
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

        #region 버튼체인지
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

        #region 좌표변환
        int safetyInterval = 3;
        Vector3 battleInterval = new Vector3(1.5f, 0 , 2.5f);
        (int x , int z) battlePosToIndex(Vector3 Vec)
        {
            //Debug.Log($"pos + {Vec}");

            Vec.z = (Vec.z / battleInterval.z);

            if (Vec.z % 2 == 0) { Vec.x -= battleInterval.x; }

            else { }

            Vec.x /= 3f;
            return ((int)Vec.x,(int)Vec.z);
        }
        (int x, int z) safetyPosToIndex(Vector3 Vec)
        {
            Vec.x = (Vec.x - 1) / 3;
            Vec.z = (Vec.z + 7) / 3;


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
     
                return true;
            }
            return false;
        }
        #endregion
    }


}
