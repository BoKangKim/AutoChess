using UnityEngine;
using System.Collections.Generic;

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
        public GameObject[,] safetyZoneObject;
        public GameObject[,] battleZoneObject;

        [SerializeField]
        public GameObject UnitPrefab;

        [Header("ScriptableObject")]
        [SerializeField] ScriptableObject Unit = null;

        [SerializeField] private List<ScriptableObject> SpeciesType = null;

        [SerializeField] private List<ScriptableObject> ClassType = null;

        void Start()
        {
            safetyZoneObject = new GameObject[2, 7];
            battleZoneObject = new GameObject[3, 7];
        }

        public void OnClick_UnitInst()
        {
            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 7; x++)
                { 
                    if (safetyZoneObject[z, x] == null)
                    {
                        safetyZoneObject[z, x] = Instantiate(UnitPrefab, new Vector3(x, 0.25f, z), Quaternion.identity);
                        //safetyZoneObject[z, x].
                        //InputPlayer(z, x);
                        return;
                    }
                }
            }
        }

        public static void SendData()
        {
            
        }

        

    }
}
