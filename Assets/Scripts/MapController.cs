using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

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

        public int battleUnitCount = 0;

        [SerializeField]
        GameObject UnitPrefab;
        private void Awake()
        {
            safetyObject = new GameObject[2, 7];
            battleObject = new GameObject[3, 7];
        }


        private void Update()
        {
 

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
                        safetyObject[z, x] = Instantiate(UnitPrefab, new Vector3(x, 0.25f, z), Quaternion.identity);
                        return;
                    }
                }
            }
        }

        //������ ȹ��
        public void itemGain(GameObject getItem)
        {
            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (safetyObject[z, x] == null)
                    {
                        safetyObject[z, x] = getItem;
                        safetyObject[z, x].transform.position = new Vector3(x, 0.25f, z);
                        safetyObject[z, x].transform.rotation = Quaternion.identity;
                        safetyObject[z, x].layer = 31;
                        return;
                    }
                }
            }
        }


    }
}
