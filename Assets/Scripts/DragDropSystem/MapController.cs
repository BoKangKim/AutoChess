using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ZoneSystem
{

    public class MapController : MonoBehaviour
    {
        public GameObject[,] safetyObject;
        public GameObject[,] battleObject;
        public Dictionary<string, int> unitCount;
        public List<string> synergyList = new List<string>();
        public List<string> activeSynergyList = new List<string>();

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
        string[] RandomItem;

        public int battleUnitCount = 0;

        [SerializeField]
        GameObject UnitPrefab;
        [SerializeField]
        GameObject battleZoneTile;

        Transform[] transforms;

        private void Awake()
        {
            transforms = new Transform[5];

            safetyObject = new GameObject[2, 7];
            battleObject = new GameObject[3, 7];
            RandomItem = new string[] { "sword", "cane", "dagger", "Armor", "robe" };
        }

        private void Start()
        {
            MapCreate();
        }

        public int BattleZoneCheck() //배틀존 모든 드롭 시점관여
        {
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
                            if (warriorSynergyCount == 2)
                            {
                                activeSynergyList.Add("Warrior");
                                Debug.Log("1단계 워리어 달성");
                            }
                            if (warriorSynergyCount == 4)
                            {
                                activeSynergyList.Remove("Warrior");
                                activeSynergyList.Add("Warrior2");
                                Debug.Log("2단계 워리어 달성");
                            }

                            if (tankerSynergyCount == 2) activeSynergyList.Add("Tanker");
                            if (warriorSynergyCount == 4)
                            {
                                activeSynergyList.Remove("Tanker");
                                activeSynergyList.Add("Tanker2");
                            }

                            if (assassinSynergyCount == 2) activeSynergyList.Add("Assassin");
                            if(assassinSynergyCount == 4)
                            {
                                activeSynergyList.Remove("Assassin");
                                activeSynergyList.Add("Assassin2");
                            }

                            if (magicianSynergyCount == 2) activeSynergyList.Add("Magician");
                            if (magicianSynergyCount == 4)
                            {
                                activeSynergyList.Remove("Magician");
                                activeSynergyList.Add("Magician2");
                            }

                            if (rangeDealerSynergyCount == 2) activeSynergyList.Add("RangeDealer");
                            if (rangeDealerSynergyCount == 4)
                            {
                                activeSynergyList.Remove("RangeDealer");
                                activeSynergyList.Add("RangeDealer2");
                            }

                            if (dwarfSynergyCount == 2) activeSynergyList.Add("Dwarf");
                            if (dwarfSynergyCount == 4)
                            {
                                activeSynergyList.Remove("Dwarf");
                                activeSynergyList.Add("Dwarf2");
                            }

                            if (orcSynergyCount == 2) activeSynergyList.Add("Orc");
                            if (orcSynergyCount == 4)
                            {
                                activeSynergyList.Remove("Orc");
                                activeSynergyList.Add("Orc2");
                            }

                            if (mechaSynergyCount == 2) activeSynergyList.Add("Mecha");
                            if (mechaSynergyCount == 4)
                            {
                                activeSynergyList.Remove("Mecha");
                                activeSynergyList.Add("Mecha2");
                            }

                            if (scorpionSynergyCount == 2) activeSynergyList.Add("Scorpion");
                            if (scorpionSynergyCount == 4)
                            {
                                activeSynergyList.Remove("Scorpion");
                                activeSynergyList.Add("Scorpion2");
                            }

                            if (undeadSynergyCount == 2) activeSynergyList.Add("Undead");
                            if (undeadSynergyCount == 4)
                            {
                                activeSynergyList.Remove("Undead");
                                activeSynergyList.Add("Undead2");
                            }




                        }
                        synergyList.Clear();
                    }
                }
            }

            //여기서 시너지를 뱉어줘야함 근데 실제 유닛 적용은 계속 하는게 아니라 특정 지점에만 해줘(라운드 시작 직전)
            for(int i = 0; i < activeSynergyList.Count; i++)
            {
                Debug.Log(activeSynergyList[i]);
            }

            activeSynergyList.Clear();
            return battleUnitCount;
        }

        public void OnClick_UnitInst()
        {

            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (safetyObject[z, x] == null)
                    {
                        safetyObject[z, x] = Instantiate(UnitPrefab, new Vector3(x, 0.25f, z - 2), Quaternion.identity);
                        return;
                    }
                }
            }
            debug.text = "세이프티존이 꽉차서 유닛을 소환할 수 없습니다.";
        }


        public void itemGain(GameObject getItem)
        {

            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (safetyObject[z, x] == null)
                    {
                        Debug.Log(RandomItem[Random.Range(0, 5)]);
                        safetyObject[z, x] = getItem;
                        safetyObject[z, x].name = RandomItem[Random.Range(0, 5)];
                        safetyObject[z, x].transform.position = new Vector3(x, 0.25f, z - 2);
                        safetyObject[z, x].transform.rotation = Quaternion.identity;
                        safetyObject[z, x].layer = 31;
                        return;
                    }
                }
            }
            debug.text = "세이프티존이 꽉차서 아이템을 습득할 수 없습니다.";
        }


        //맵생성
        public void MapCreate()
        {
            for (int z = 0; z < 3; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    float newPosX = (float)x * 1.5f;
                    float newPosZ = (float)(z * 1.3f);

                    if (z % 2 == 0) { }

                    else
                    {
                        newPosX += 0.65f;
                    }

                    Vector3 tilePos = new Vector3(newPosX, 0, newPosZ);
                    GameObject newTile = Instantiate(battleZoneTile, tilePos, Quaternion.identity);

                }
            }
        }

    }
}
