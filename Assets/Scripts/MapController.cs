using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZoneSystem
{
    public class MapController : MonoBehaviour
    {
        public GameObject[,] safetyObject;
        public GameObject[,] battleObject;

        public int battleUnitCount = 0;
        [SerializeField]
        GameObject UnitPrefab;


        void Start()
        {
            safetyObject = new GameObject[2, 7];
            battleObject = new GameObject[3, 7];
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
                        Debug.Log($"{z},{x}");
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
    }
}
