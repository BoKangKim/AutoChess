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

        public int BattlezoneChack() //��Ʋ�� ��� ��� ��������
        {
            for (int z = 0; z < 3; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (battleObject[z, x] != null)
                    {
                        ++battleUnitCount;
                        Debug.Log($"{z},{x}");

                        //�� ������ �ó��� ��� �ΰ� ��ó����
                        Class[z, x] = battleObject[z, x].GetComponent<UnitClass.Unit>().GetClassName;
                        species[z, x] = battleObject[z, x].GetComponent<UnitClass.Unit>().GetSpeciesName;

                        //���� �ó����� ���ֿ� getcomponent�� �������ֱ�

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
                        //���� ���� �̱�
                        //���� ���� ���������� ������(����)
                        return;
                    }
                }
            }
        }
    }
}
