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

        public Dictionary<ScriptableUnitType, int> unitTypeCount; //스크립터블 딕셔너리로 
        

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

        public int BattlezoneChack() //배틀존 모든 드롭 시점관여
        {
            for (int z = 0; z < 3; z++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (battleObject[z, x] != null) //근데 무기일경우에는 null이 뜰거임
                    {
                        ++battleUnitCount; //이거 왜케 증가량이 많은거같지?
                        Debug.Log($"{z},{x}");

                        //이 시점에 시너지 계산 싸고 뒤처리중
                        //Class[z, x] = battleObject[z, x].GetComponent<UnitClass.Unit>().GetClassName;
                        //species[z, x] = battleObject[z, x].GetComponent<UnitClass.Unit>().GetSpeciesName; //클래스와 시너지 전부 받아왔음 

                        //시너지 계산
                        unitTypeCount = new Dictionary<ScriptableUnitType, int>();

                        UnitClass.Unit Test = battleObject[z, x].GetComponent<UnitClass.Unit>();

                        if (unitTypeCount.ContainsKey(Test.GetUnitType01))
                        {
                            int unitCount = 0;
                            unitTypeCount.TryGetValue(Test.GetUnitType01, out unitCount);

                            unitCount++;

                            unitTypeCount[Test.GetUnitType01] = unitCount;
                            Debug.Log(Test.GetUnitType01 + "이미 등록되어있습니다");

                        }
                        else
                        {
                            unitTypeCount.Add(Test.GetUnitType01, 1);
                            Debug.Log(Test.GetUnitType01 + "추가성공");
                        }

                        if(unitTypeCount.ContainsKey(Test.GetUnitType02))
                        {
                            int unitCount = 0;
                            unitTypeCount.TryGetValue(Test.GetUnitType02, out unitCount);

                            unitCount++;

                            unitTypeCount[Test.GetUnitType02] = unitCount;
                            Debug.Log(Test.GetUnitType02 + "등록되어있습니다");
                        }
                        else
                        {
                            unitTypeCount.Add(Test.GetUnitType02, 1);
                            Debug.Log(Test.GetUnitType02 + "추가성공");
                        }

                        
                        //이제 타입 검사해서 받아가지고 적용시키는 정적 클래스 짜야함

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
