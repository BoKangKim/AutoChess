using System.Collections.Generic;
using UnityEngine;
using Battle.Location;
using Battle.AI;

namespace Battle.RTASTAR 
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

        public LocationXY searchNextLocation(LocationXY unitLocation,LocationXY target)
        {
            initWeight();
            updateWeight();
            
            return calcLocationWeight(unitLocation, target);
        }

        private LocationXY calcLocationWeight(LocationXY unitLocation, LocationXY target)
        {
            List<LocationXY> nearLocation = new List<LocationXY>();

            for (int i = unitLocation.y - 1; i <= unitLocation.y + 1; i++)
            {
                for(int j = unitLocation.x - 1; j <= unitLocation.x + 1; j++)
                {
                    if (checkLocationArrange(j, i))
                    {
                        if (unitLocation.y % 2 == 0)
                        {
                            if (i == unitLocation.x - 1)
                            {
                                weight[i, j] += 100;
                            }
                        }
                        else
                        {
                            if (i == unitLocation.x + 1)
                            {
                                weight[i, j] += 100;
                            }
                        }

                        nearLocation.Add(new LocationXY());
                        nearLocation[nearLocation.Count - 1].init(j, i);
                    }

                }
            }

            float minWeight = 10000f;
            float temp = 0f;
            int index = 0;

            for(int i = 0; i < nearLocation.Count; i++)
            {
                weight[nearLocation[i].y, nearLocation[i].x] += LocationControl.getDistance(nearLocation[i],target);

                if(minWeight > (temp = weight[nearLocation[i].y, nearLocation[i].x]))
                {
                    minWeight = temp;
                    index = i;
                }
            }

            return nearLocation[index];
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

