using System.Collections.Generic;
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

        public Dictionary<ScriptableUnitType, int> unitTypeCount; //��ũ���ͺ� ��ųʸ��� 
        

        public int battleUnitCount;
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
                    if (battleObject[z, x] != null) //�ٵ� �����ϰ�쿡�� null�� �����
                    {
                        ++battleUnitCount; //�̰� ���� �������� �����Ű���?
                        Debug.Log($"{z},{x}");

                        //�� ������ �ó��� ��� �ΰ� ��ó����
                        //Class[z, x] = battleObject[z, x].GetComponent<UnitClass.Unit>().GetClassName;
                        //species[z, x] = battleObject[z, x].GetComponent<UnitClass.Unit>().GetSpeciesName; //Ŭ������ �ó��� ���� �޾ƿ��� 

                        //�ó��� ���
                        unitTypeCount = new Dictionary<ScriptableUnitType, int>();

                        UnitClass.Unit Test = battleObject[z, x].GetComponent<UnitClass.Unit>();

                        if (unitTypeCount.ContainsKey(Test.GetUnitType01))
                        {
                            int unitCount = 0;
                            unitTypeCount.TryGetValue(Test.GetUnitType01, out unitCount);

                            unitCount++;

                            unitTypeCount[Test.GetUnitType01] = unitCount;
                            Debug.Log(Test.GetUnitType01 + "�̹� ��ϵǾ��ֽ��ϴ�");

                        }
                        else
                        {
                            unitTypeCount.Add(Test.GetUnitType01, 1);
                            Debug.Log(Test.GetUnitType01 + "�߰�����");
                        }

                        if(unitTypeCount.ContainsKey(Test.GetUnitType02))
                        {
                            int unitCount = 0;
                            unitTypeCount.TryGetValue(Test.GetUnitType02, out unitCount);

                            unitCount++;

                            unitTypeCount[Test.GetUnitType02] = unitCount;
                            Debug.Log(Test.GetUnitType02 + "��ϵǾ��ֽ��ϴ�");
                        }
                        else
                        {
                            unitTypeCount.Add(Test.GetUnitType02, 1);
                            Debug.Log(Test.GetUnitType02 + "�߰�����");
                        }

                        
                        //���� Ÿ�� �˻��ؼ� �޾ư����� �����Ű�� ���� Ŭ���� ¥����

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
