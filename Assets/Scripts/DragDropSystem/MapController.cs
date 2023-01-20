using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace ZoneSystem
{
    enum SpeciesType
    {
        Dwarf, Undead, Scorpion, Orc, Mecha
    }

    enum ClassType
    {
        Warrior, Tanker, Magician, RangeDealer, Assassin
    }
    public class MapController : MonoBehaviour
    {

        
        public GameObject[,] safetyObject;
        public GameObject[,] battleObject;


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

        public int BattlezoneChack()
        {
            for (int z = 0; z < 3; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (battleObject[z, x] != null)
                    {
                        ++battleUnitCount;
                        battleObject[z, x].GetComponent<Battle.AI.ParentBT>().enabled = true;

                        Debug.Log($"x {x} z {z} Pos{battleObject[z, x].transform.position}");
                    }
                }
            }
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
                        int PosX = (x * 3) + 1;
                        int PosZ = (z * 3) - 7;

                        safetyObject[z, x] = Instantiate(UnitPrefab, new Vector3(PosX, 0.25f, PosZ), Quaternion.identity);
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
                        int PosX = (x * 3) + 1;
                        int PosZ = (z * 3) - 7;

                        Debug.Log(RandomItem[Random.Range(0, 5)]);
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




    }
}
