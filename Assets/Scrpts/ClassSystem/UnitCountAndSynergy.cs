using UnityEngine;


namespace UnitClass
{
    public class Player
    {
        public GameObject[,] BattleZone = new GameObject[3, 7]; //��ü ��Ʋ���� 7X6�̰� ���� ��ġ������ ��Ʋ���� 7X3
        public GameObject[,] SafetyZone = new GameObject[2, 7]; //������Ƽ���� 7X2�� �� 14ĭ
    }

    public static class UnitCountAndSynergy
    {
        private static Player[] players = new Player[4]; // �� �ҷ��ٰ� ������ static void ���� �����
    }


}



