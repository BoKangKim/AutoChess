using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace ZoneSystem
{

    public class MapController : MonoBehaviour
    {
        public GameObject[,] safetyObject;
        public GameObject[,] battleObject;
        public Dictionary<string, int> unitCount;
        public List<string> synergyList = new List<string>();
        public List<string> activeSynergyList;

        [Header("종족 시너지")] //개 무식한 방법으로 해놨는데 추후 수정해야함
        [SerializeField] private int orcSynergyCount;
        [SerializeField] private int dwarfSynergyCount;
        [SerializeField] private int golemSynergyCount;
        [SerializeField] private int mechaSynergyCount;
        [SerializeField] private int demonSynergyCount;

        [Header("직업 시너지")] //개 무식한 방법으로 해놨는데 추후 수정해야함
        [SerializeField] private int warriorSynergyCount;
        [SerializeField] private int tankerSynergyCount;
        [SerializeField] private int magicianSynergyCount;
        [SerializeField] private int assassinSynergyCount;
        [SerializeField] private int rangeDealerSynergyCount;

        public TextMeshProUGUI debug;

        //아이템 랜뽑 일단은 스트링값으로
        string[] RandomItem;

        public int battleUnitCount = 0;
        public int SafetyObjectCount = 0;

        [SerializeField] private GameObject UnitPrefab;
        [SerializeField] private GameObject ItemPrefab;
        [SerializeField] private GameObject battleZoneTile;

        Transform[] transforms;

        private void Awake()
        {
            transforms = new Transform[5];

            safetyObject = new GameObject[2, 7];
            battleObject = new GameObject[3, 7];
            RandomItem = new string[] { "sword", "cane", "dagger", "Armor", "robe" };
        }

        public int SafetyZoneCheck()
        {
            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (safetyObject[z,x]!=null)
                    {
                        SafetyObjectCount++;
                    }
                }
            }
            return SafetyObjectCount;
        }

        public int BattleZoneCheck() //배틀존 모든 드롭 시점관여
        {
            //Debug.Log("AA");
            unitCount = new Dictionary<string, int>();
            orcSynergyCount = 0;
            dwarfSynergyCount = 0;
            golemSynergyCount = 0;
            mechaSynergyCount = 0;
            demonSynergyCount = 0;

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
                                    case "Golem":
                                        golemSynergyCount++;
                                        break;
                                    case "Demon":
                                        demonSynergyCount++;
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

                            if (golemSynergyCount > 2 && golemSynergyCount < 5)
                            {
                                if (!activeSynergyList.Contains("Golem")) activeSynergyList.Add("Golem");
                            }
                            if (golemSynergyCount >= 5)
                            {
                                if (activeSynergyList.Contains("Golem"))
                                {
                                    activeSynergyList.Remove("Golem");
                                }

                                if (!activeSynergyList.Contains("Golem2"))
                                {
                                    activeSynergyList.Add("Golem2");
                                }
                            }

                            if (demonSynergyCount > 2 && demonSynergyCount < 5)
                            {
                                if (!activeSynergyList.Contains("Demon")) activeSynergyList.Add("Demon");
                            }
                            if (demonSynergyCount >= 5)
                            {
                                if (activeSynergyList.Contains("Demon"))
                                {
                                    activeSynergyList.Remove("Demon");
                                }

                                if (!activeSynergyList.Contains("Demon2"))
                                {
                                    activeSynergyList.Add("Demon2");
                                }
                            }
                        }
                        synergyList.Clear();
                    }
                }
            }

            //여기서 시너지를 뱉어줘야함 근데 실제 유닛 적용은 유닛 생성(Awake)에서 ㄱ

            //맵컨트롤이랑 드래그앤 드롭은 서로 연결 되어있다 보면됨?

            //시너지 ui표시
            UIManager.Inst.SynergyText(null);
            activeSynergyList.ForEach(str => UIManager.Inst.SynergyText(str));
            activeSynergyList.Clear();
            return battleUnitCount;
        }

        public void OnClick_UnitInst() //유닛 구매
        {
            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (safetyObject[z, x] == null)
                    {
                        int PosX = (x * 3) + 1;
                        int PosZ = (z * 3) - 7;

                        safetyObject[z, x] = Instantiate(UnitPrefab, new Vector3(PosX, 0.25f, PosZ), Quaternion.identity);
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
            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (safetyObject[z, x] == null)
                    {
                        int PosX = (x * 3) + 1;
                        int PosZ = (z * 3) - 7;

                        //Debug.Log(RandomItem[Random.Range(0, 5)]);
                        safetyObject[z, x] = getItem;
                        safetyObject[z, x].name = RandomItem[Random.Range(0, 5)];
                        safetyObject[z, x].transform.position = new Vector3(PosX, 0.25f, PosZ);
                        safetyObject[z, x].transform.rotation = Quaternion.identity;
                        safetyObject[z, x].layer = 31;
                        return;
                    }
                }
            }
            debug.text = "세이프티존이 꽉차서 아이템을 습득할 수 없습니다.";
        }

        public void UnitOutItem(GameObject Item)
        {
            Item.transform.parent = this.transform;
            Item.gameObject.SetActive(true);
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
