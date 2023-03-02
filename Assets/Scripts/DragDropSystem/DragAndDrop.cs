using System.Collections.Generic;
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
            //if (!photonView.IsMine)
            //{
            //    return;
            //}
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
                    if (CastRay(ObjectLayer).collider == null) return;

                    if (CastRay(ObjectLayer).collider.GetComponent<UnitClass.Unit>() != null)
                    {
                        selectedObject = CastRay(ObjectLayer).collider.gameObject;

                        battleZoneTile.gameObject.SetActive(true);
                        safetyZoneTile.gameObject.SetActive(true);

                        if (CastRay(safetySpaceLayer).collider != null)
                        {
                            var vec = safetyPosToIndex(CastRay(safetySpaceLayer).collider.transform.localPosition);
                            mapController.safetyObject[vec.z, vec.x] = null;
                            beforePos = CastRay(safetySpaceLayer).collider.transform.localPosition;
                        }
                        else if (CastRay(battleSpaceLayer).collider != null)
                        {

                            var vec = battlePosToIndex(CastRay(battleSpaceLayer).collider.transform.localPosition);
                            mapController.battleObject[vec.z, vec.x] = null;
                            beforePos = CastRay(battleSpaceLayer).collider.transform.localPosition;
                        }

                        GameManager.Inst.soundOption.SFXPlay("SelectSFX");

                    }
                    else if (CastRay(ObjectLayer).collider.GetComponent<Equipment>() != null)
                    {
                        selectedObject = CastRay(ObjectLayer).collider.gameObject;


                        safetyZoneTile.gameObject.SetActive(true);


                        if (CastRay(safetySpaceLayer).collider != null)
                        {
                            var vec = safetyPosToIndex(selectedObject.transform.localPosition);
                            mapController.safetyObject[vec.z, vec.x] = null;
                            beforePos = CastRay(safetySpaceLayer).collider.transform.localPosition;
                        }
                        GameManager.Inst.soundOption.SFXPlay("SelectSFX");

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
                        GameManager.Inst.soundOption.SFXPlay("DropSFX");


                    }
                    else if (CastRay(battleSpaceLayer).collider != null)
                    {
                        DropPosition(battleSpaceLayer);
                        GameManager.Inst.soundOption.SFXPlay("DropSFX");


                    }
                    else
                    {
                        outRange();
                        GameManager.Inst.soundOption.SFXPlay("DropSFX");

                    }

                    battleZoneTile.gameObject.SetActive(false);
                    safetyZoneTile.gameObject.SetActive(false);
                }
                //storeButtonChange();

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
                        mapController.UnitOutItem(selectedObject.transform.Find("Equipment").GetChild(i).gameObject);
                        count--;
                        i--;
                    }
                }
                posCheckButton = null;

                PhotonNetwork.Destroy(selectedObject);
                GameManager.Inst.soundOption.bgmPlay("SellSFX");

                GameManager.Inst.GetPlayer().gold += 3;

                selectedObject = null;
                storeButtonChange();
                battleZoneTile.gameObject.SetActive(false);
                safetyZoneTile.gameObject.SetActive(false);

     

            }
            if (posCheckButton && selectedObject.GetComponent<Equipment>() != null)
            {
                posCheckButton = null;

                Destroy(selectedObject);
                GameManager.Inst.GetPlayer().gold += 3;

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
                            mapController.safetyObject[safetyPos.z, safetyPos.x].transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
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
                            mapController.safetyObject[safetyPos.z, safetyPos.x].transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }
                }
            }

            else if (Layer == battleSpaceLayer)
            {

                Debug.Log(battlePos);
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

                            mapController.battleObject[battlePos.z, battlePos.x].transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
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

                            mapController.battleObject[battlePos.z, battlePos.x].transform.localPosition = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                        }
                    }

                }
            }
            Debug.Log($"WP : { worldPosition}, SPINDEX :  {safetyPos} BPINDEX : {battlePos} BFINDEX {beforePos}");
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
            Vec.z = (Vec.z / battleInterval.z);

            if (Vec.z % 2 == 0) { Vec.x -= battleInterval.x; }

            else { }

            Vec.x /= 3f;

            Debug.Log($"{(int)Vec.x},{(int)Vec.z}");

            return ((int)Vec.x,(int)Vec.z);
        }
        (int x, int z) safetyPosToIndex(Vector3 Vec)
        {
            Vec.x = (Vec.x - 1) / 3;
            Vec.z = (Vec.z + 7) / 3;


            return ((int)Vec.x, (int)Vec.z);
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

            for (int i = 0; i < selected.Find("Equipment").childCount; i++)
            {
                selected.Find("Equipment").GetChild(i).GetComponent<Equipment>().SaveGrade();
                items.Add(selected.Find("Equipment").GetChild(i).transform);
            }

            for (int i = 0; i < stay.Find("Equipment").childCount; i++)
            {
                stay.Find("Equipment").GetChild(i).GetComponent<Equipment>().SaveGrade();
                items.Add(stay.Find("Equipment").GetChild(i).transform);
            }

            return EquipmentAutoMergeResult(items);
        }

        public void EquipmentAutoMerge(Transform selected, Transform stay)
        {
            List<Transform> items = new List<Transform>();

            for (int i = 0; i < selected.Find("Equipment").childCount; i++)
            {
                items.Add(selected.Find("Equipment").GetChild(i).transform);
            }

            for (int i = 0; i < stay.Find("Equipment").childCount; i++)
            {
                items.Add(stay.Find("Equipment").GetChild(i).transform);
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

            for (int i = 0; i < stay.Find("Equipment").childCount; i++)
            {
                items.Add(stay.Find("Equipment").GetChild(i).transform);
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
  

        #region 머지시스템
        public bool Merge(GameObject selectedObject, GameObject stayObject)
        {
       
            if (selectedObject == null || stayObject == null) return false;
            //유닛 끼리 머지
            if (selectedObject.GetComponent<UnitClass.Unit>() != null && stayObject.GetComponent<UnitClass.Unit>() != null)
            {
                if (selectedObject.name != stayObject.name) return false;
       
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
                            selectedUnit.transform.Find("Equipment").GetChild(i).GetComponent<Equipment>().LoadGrade();
                        }

                        for(int i = 0; i<stayEqCount; i++)
                        {
                            stayUnit.transform.Find("Equipment").GetChild(i).GetComponent<Equipment>().LoadGrade();
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
                                mapController.UnitOutItem(selectedUnit.transform.Find("Equipment").GetChild(i).gameObject);
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
     
                return true;
            }
            return false;
        }
        #endregion
    }


}
