using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Battle.AI;

namespace ZoneSystem
{

    public class MapController : MonoBehaviourPun
    {
        public GameObject[,] safetyObject;
        public GameObject[,] battleObject;
        public Dictionary<string, int> unitCount;
        public List<string> synergyList = new List<string>();
        public List<string> activeSynergyList;

        [Header("종족 시너지")] //개 무식한 방법으로 해놨는데 추후 수정해야함
        [SerializeField] private int orcSynergyCount;
        [SerializeField] private int dwarfSynergyCount;
        [SerializeField] private int scorpionSynergyCount;
        [SerializeField] private int mechaSynergyCount;
        [SerializeField] private int undeadSynergyCount;

        [Header("직업 시너지")] //개 무식한 방법으로 해놨는데 추후 수정해야함
        [SerializeField] private int warriorSynergyCount;
        [SerializeField] private int tankerSynergyCount;
        [SerializeField] private int magicianSynergyCount;
        [SerializeField] private int assassinSynergyCount;
        [SerializeField] private int rangeDealerSynergyCount;

        public TextMeshProUGUI debug;

        //아이템 랜뽑 일단은 스트링값으로
        private string[] RandomItem;

        public int battleUnitCount = 0;

        //유닛 랜덤뽑기
        private string[] units = new string[Database.Instance.userInfo.UserUnitCount];



        [SerializeField] private GameObject ItemPrefab;
        [SerializeField] private GameObject battleZoneTile;

        private void Awake()
        {
            //if (!photonView.IsMine) return;
      
            safetyObject = new GameObject[2, 7];
            battleObject = new GameObject[3, 7];
            RandomItem = new string[] { "sword", "cane", "dagger", "Armor", "robe" };

            UIManager.Inst.UnitInstButton = OnClick_UnitInst;

        }


        public int BattleZoneCheck() //배틀존 모든 드롭 시점관여
        {

            //Debug.Log("AA");
            unitCount = new Dictionary<string, int>();
            orcSynergyCount = 0;
            dwarfSynergyCount = 0;
            scorpionSynergyCount = 0;
            mechaSynergyCount = 0;
            undeadSynergyCount = 0;

            warriorSynergyCount = 0;
            tankerSynergyCount = 0;
            magicianSynergyCount = 0;
            assassinSynergyCount = 0;
            rangeDealerSynergyCount = 0;

            for (int z = 0; z < 3; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (battleObject[z, x] != null)
                    {


                        ++battleUnitCount;
                        //Debug.Log($"{z},{x}");

                        //battleObject[z, x].GetComponent<ParentBT>().enabled = true;

                        if (unitCount.ContainsKey(battleObject[z, x].GetComponent<UnitClass.Unit>().GetSynergyName))
                        {
                            //Debug.Log("중복되는것이 있음");
                        }
                        else
                        {
                            unitCount.Add(battleObject[z, x].GetComponent<UnitClass.Unit>().GetSynergyName, 1);
                            //Debug.Log("유닛이 없어서 추가했음");
                            string[] str = battleObject[z, x].GetComponent<UnitClass.Unit>().GetSynergyName.Split(' ');
                            synergyList.Add(str[0]);
                            synergyList.Add(str[1]);
                            for (int i = 0; i < synergyList.Count; i++)
                            {
                                switch (synergyList[i]) //이거 무조건 고칠예정
                                {
                                    case "Warrior":
                                        warriorSynergyCount++;
                                        break;
                                    case "Tanker":
                                        tankerSynergyCount++;
                                        break;
                                    case "Assassin":
                                        assassinSynergyCount++;
                                        break;
                                    case "Magician":
                                        magicianSynergyCount++;
                                        break;
                                    case "RangeDealer":
                                        rangeDealerSynergyCount++;
                                        break;
                                    case "Dwarf":
                                        dwarfSynergyCount++;
                                        break;
                                    case "Mecha":
                                        mechaSynergyCount++;
                                        break;
                                    case "Orc":
                                        orcSynergyCount++;
                                        break;
                                    case "Scorpion":
                                        scorpionSynergyCount++;
                                        break;
                                    case "Undead":
                                        undeadSynergyCount++;
                                        break;
                                }

                            }
                            //이안에서 시너지 계산 다끝내고 
                            //종족(species)은 3,5 직업(class)은 2,4마리에서 각각 1,2단계가 활성화됨

                            //이거 무조건 고쳐야함 뇌뽑고 구현만했는데 말안됨
                            if (warriorSynergyCount > 1 && warriorSynergyCount < 4)
                            {
                                if (!activeSynergyList.Contains("Warrior")) activeSynergyList.Add("Warrior");
                            }
                            if (warriorSynergyCount >= 4)
                            {
                                if (activeSynergyList.Contains("Warrior"))
                                {
                                    activeSynergyList.Remove("Warrior");
                                }

                                if (!activeSynergyList.Contains("warrior2"))
                                {
                                    activeSynergyList.Add("Warrior2");
                                }
                            }

                            if (tankerSynergyCount > 1 && tankerSynergyCount < 4)
                            {
                                if (!activeSynergyList.Contains("Tanker")) activeSynergyList.Add("Tanker");
                            }
                            if (tankerSynergyCount >= 4)
                            {
                                if (activeSynergyList.Contains("Tanker"))
                                {
                                    activeSynergyList.Remove("Tanker");
                                }

                                if (!activeSynergyList.Contains("Tanker2"))
                                {
                                    activeSynergyList.Add("Tanker2");
                                }
                            }

                            if (assassinSynergyCount > 1 && assassinSynergyCount < 4)
                            {
                                if (!activeSynergyList.Contains("Assassin")) activeSynergyList.Add("Assassin");
                            }
                            if (assassinSynergyCount >= 4)
                            {
                                if (activeSynergyList.Contains("Assassin"))
                                {
                                    activeSynergyList.Remove("Assassin");
                                }

                                if (!activeSynergyList.Contains("Assassin2"))
                                {
                                    activeSynergyList.Add("Assassin2");
                                }
                            }

                            if (magicianSynergyCount > 1 && magicianSynergyCount < 4)
                            {
                                if (!activeSynergyList.Contains("Magician")) activeSynergyList.Add("Magician");
                            }
                            if (magicianSynergyCount == 4)
                            {
                                if (activeSynergyList.Contains("Magician"))
                                {
                                    activeSynergyList.Remove("Magician");
                                }

                                if (!activeSynergyList.Contains("Magician2"))
                                {
                                    activeSynergyList.Add("Magician2");
                                }
                            }

                            if (rangeDealerSynergyCount > 1 && rangeDealerSynergyCount < 4)
                            {
                                if (!activeSynergyList.Contains("RangeDealer")) activeSynergyList.Add("RangeDealer");
                            }
                            if (rangeDealerSynergyCount >= 4)
                            {
                                if (activeSynergyList.Contains("RangeDealer"))
                                {
                                    activeSynergyList.Remove("RangeDealer");
                                }

                                if (!activeSynergyList.Contains("RangeDealer2"))
                                {
                                    activeSynergyList.Add("RangeDealer2");
                                }
                            }

                            if (dwarfSynergyCount > 2 && dwarfSynergyCount < 5)
                            {
                                if (!activeSynergyList.Contains("Dwarf")) activeSynergyList.Add("Dwarf");
                            }
                            if (dwarfSynergyCount >= 5)
                            {
                                if (activeSynergyList.Contains("Dwarf"))
                                {
                                    activeSynergyList.Remove("Dwarf");
                                }

                                if (!activeSynergyList.Contains("Dwarf2"))
                                {
                                    activeSynergyList.Add("Dwarf2");
                                }
                            }

                            if (orcSynergyCount > 2 && orcSynergyCount < 5)
                            {
                                if (!activeSynergyList.Contains("Orc")) activeSynergyList.Add("Orc");
                            }
                            if (orcSynergyCount >= 5)
                            {
                                if (activeSynergyList.Contains("Orc"))
                                {
                                    activeSynergyList.Remove("Orc");
                                }

                                if (!activeSynergyList.Contains("Orc2"))
                                {
                                    activeSynergyList.Add("Orc2");
                                }
                            }

                            if (mechaSynergyCount > 2 && mechaSynergyCount < 5)
                            {
                                if (!activeSynergyList.Contains("Mecha")) activeSynergyList.Add("Mecha");
                            }
                            if (mechaSynergyCount >= 5)
                            {
                                if (activeSynergyList.Contains("Mecha"))
                                {
                                    activeSynergyList.Remove("Mecha");
                                }

                                if (!activeSynergyList.Contains("Mecha2"))
                                {
                                    activeSynergyList.Add("Mecha2");
                                }
                            }

                            if (scorpionSynergyCount > 2 && scorpionSynergyCount < 5)
                            {
                                if (!activeSynergyList.Contains("Scorpion")) activeSynergyList.Add("Scorpion");
                            }
                            if (scorpionSynergyCount >= 5)
                            {
                                if (activeSynergyList.Contains("Scorpion"))
                                {
                                    activeSynergyList.Remove("Scorpion");
                                }

                                if (!activeSynergyList.Contains("Scorpion2"))
                                {
                                    activeSynergyList.Add("Scorpion2");
                                }
                            }

                            if (undeadSynergyCount > 2 && undeadSynergyCount < 5)
                            {
                                if (!activeSynergyList.Contains("Undead")) activeSynergyList.Add("Undead");
                            }
                            if (undeadSynergyCount >= 5)
                            {
                                if (activeSynergyList.Contains("Undead"))
                                {
                                    activeSynergyList.Remove("Undead");
                                }

                                if (!activeSynergyList.Contains("Undead2"))
                                {
                                    activeSynergyList.Add("Undead2");
                                }
                            }
                        }
                        synergyList.Clear();
                    }
                }
            }

            //여기서 시너지를 뱉어줘야함 근데 실제 유닛 적용은 계속 하는게 아니라 특정 지점에만 해줘(라운드 시작 직전)
            //뱉는 시점은 여기인데 각각 객체 접근하려면 또 이중 for문 돌려야 하는데 그러긴 싫고
            //스크립트에 접근하자니 추후 포톤뷰 체크도 해야하고
            //나중에 거울모드 뽑을때 데이터 가져가는거 생각까지 해야해서 생각보다 어려움

            //시너지 ui표시
            UIManager.Inst.SynergyText(null);
            activeSynergyList.ForEach(str => UIManager.Inst.SynergyText(str));
            activeSynergyList.Clear();

            return battleUnitCount;
        }

        public void OnClick_UnitInst() //유닛 구매
        {
            //if (!photonView.IsMine) return;

            if (UIManager.Inst.PlayerGold < 5)
            {
                debug.text = "골드가 부족합니다.";
                return;
            }

            //랜덤생성로직
            string UnitPrefab = units[Random.Range(0, Database.Instance.userInfo.UserUnitCount)];
            // 유닛의 최대 수는 15개
            
            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (safetyObject[z, x] == null)
                    {
                        int PosX = (x * 3) + 1;
                        int PosZ = (z * 3) - 7;

                        safetyObject[z, x] = PhotonNetwork.Instantiate(UnitPrefab,Vector3.zero,Quaternion.identity);

                        if (PlayerMapSpawner.Map != null)
                        {
                        safetyObject[z, x].transform.parent = PlayerMapSpawner.Map.transform;

                        }
                        safetyObject[z, x].transform.localPosition = new Vector3(PosX,0.25f,PosZ);
                        return;
                    }
                }
            }
            debug.text = "세이프티존이 꽉차서 유닛을 소환할 수 없습니다.";
        }

        public void OnClick_ItemInst()
        {
            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (safetyObject[z, x] == null)
                    {
                        int PosX = (x * 3) + 1;
                        int PosZ = (z * 3) - 7;

                        safetyObject[z, x] = Instantiate(ItemPrefab, new Vector3(PosX, 0.25f, PosZ), Quaternion.identity);
                        safetyObject[z, x].layer = 31;

                        return;
                    }
                }
            }
            debug.text = "세이프티존이 꽉차서 장비를 소환할 수 없습니다.";
        }


        public void itemGain(GameObject getItem)
        {
            //if (!photonView.IsMine) return;

            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (safetyObject[z, x] == null)
                    {
                        int PosX = (x * 3) + 1;
                        int PosZ = (z * 3) - 7;

                        Debug.Log(RandomItem[Random.Range(0, 5)]);
                        safetyObject[z, x] = getItem;
                        safetyObject[z, x].name = RandomItem[Random.Range(0, 5)];
        
                        if (PlayerMapSpawner.Map != null)
                        {
                            safetyObject[z, x].transform.parent = PlayerMapSpawner.Map.transform;

                        }
                        safetyObject[z, x].transform.localPosition = new Vector3(PosX, 0.25f, PosZ);
                        safetyObject[z, x].transform.rotation = Quaternion.identity;
                        safetyObject[z, x].layer = 31;
                        return;
                    }
                }
            }
            debug.text = "세이프티존이 꽉차서 아이템을 습득할 수 없습니다.";
        }

        public void SellUnitOutItem(GameObject Item)
        {
            
            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (safetyObject[z, x] == null)
                    {
                        int PosX = (x * 3) + 1;
                        int PosZ = (z * 3) - 7;
                        safetyObject[z, x] = Item;
                        safetyObject[z, x].transform.position = new Vector3(PosX, 0.25f, PosZ);
                        safetyObject[z, x].transform.rotation = Quaternion.identity;
                        safetyObject[z, x].layer = 31;
                        return;
                    }
                }
            }
        }


        


    }
}
