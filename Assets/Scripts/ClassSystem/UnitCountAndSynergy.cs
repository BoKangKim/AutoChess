using UnityEngine;

namespace UnitClass
{
    public class Player
    {
        public GameObject[,] BattleZone = new GameObject[3, 7]; //전체 배틀존은 7X6이고 내가 배치가능한 배틀존은 7X3
        public GameObject[,] SafetyZone = new GameObject[2, 7]; //세이프티존은 7X2로 총 14칸
    }

    public static class GetBattleZoneUnitCount
    {
        private static Player[] players = new Player[4]; // 얘 불러다가 쓰려면 static void 에서 써야함
    }

    public static class GetSafetyZoneUnitCount
    {
        private static Player[] players = new Player[4];

        public static void InputPlayer(int zIndex, int xIndex, GameObject pfb) // 추후에 player num 받아서 서버작업때 변경
        {
            //players[0].BattleZone[zIndex, xIndex].gameObject
        }
    }


}



