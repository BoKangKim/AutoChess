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
        private string[,] Class = null;
        private string[,] species = null;

        public int battleUnitCount = 0;
        [SerializeField]
        GameObject UnitPrefab;


        void Start()
        {
            safetyObject = new GameObject[2, 7];
            battleObject = new GameObject[3, 7];
            Class = new string[3, 7];
            species = new string[3, 7];
        }

        public int BattlezoneChack() //배틀존 모든 드롭 시점관여
        {
            for (int z = 0; z < 3; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (battleObject[z, x] != null)
                    {
                        ++battleUnitCount;
                        Debug.Log($"{z},{x}");

                        //이 시점에 시너지 계산 싸고 뒤처리중
                        Class[z, x] = battleObject[z, x].GetComponent<UnitClass.Unit>().GetClassName;
                        species[z, x] = battleObject[z, x].GetComponent<UnitClass.Unit>().GetSpeciesName;

                        //계산된 시너지를 유닛에 getcomponent로 전달해주기

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
                        //랜덤 으로 뽑기
                        //뽑은 랜덤 마스터한테 보내기(이후)
                        return;
                    }
                }
            }
        }
    }
}
