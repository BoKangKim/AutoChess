using System.Collections.Generic;
using UnityEngine;
using Battle.Location;
using Battle.AI;

namespace Battle.RTAstar 
{
    public class RTAstar
    {

        private float[,] weight = new float[6, 7];
        public Queue<LocationXY> routeHistory = null;
        private ParentBT target = null;

        public RTAstar()
        {
            routeHistory = new Queue<LocationXY>();
        }

        private void initWeight()
        {
            for(int i = 0; i < weight.GetLength(0); i++)
            {
                for(int j = 0; j < weight.GetLength(1); j++)
                {
                    weight[i, j] = 0f;
                }
            }
        }

        private void updateWeight()
        {
            ParentBT[] allUnits = MonoBehaviour.FindObjectsOfType<ParentBT>();

            for(int i = 0; i < allUnits.Length; i++)
            {
                weight[allUnits[i].getMyLocation().y, allUnits[i].getMyLocation().y] += 1000;
            }
        }

        public LocationXY searchNextLocation(LocationXY unitLocation)
        {
            initWeight();
            updateWeight();

            List<LocationXY> nearLocations = new List<LocationXY>();
            // Â¦ ¿ì»ó ¿ìÇÏ
            // È¦ ÁÂ»ó ÁÂÇÏ
            

            LocationXY result = unitLocation;
            return result;
        }

        private void calcLocationWeight(LocationXY unitLocation)
        {
            if (unitLocation.y % 2 == 0)
            {

            }
            else
            {

            }
        }

        private bool checkLocationArrange(int x, int y)
        {
            if ((x - 1 >= 0 && x + 1 < weight.GetLength(1))
                    || (y - 1 >= 0 && y + 1 < weight.GetLength(0)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

