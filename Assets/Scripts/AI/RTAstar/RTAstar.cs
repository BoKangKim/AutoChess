using System.Collections.Generic;
using UnityEngine;
using Battle.Location;
using Battle.AI;

namespace Battle.RTASTAR 
{
    public class RTAstar
    {
        private float[,] weight = new float[6, 8];
        private ParentBT target = null;
        private ParentBT[] allUnits = null;
        private List<LocationXY> closeList = new List<LocationXY>();
        private LocationXY startLocation;
        private LocationXY preLocation;

        private string myNickName = "";

        public RTAstar(LocationXY startLocation, string myNickName)
        {
            this.startLocation = startLocation;
            preLocation = startLocation;
            this.myNickName = myNickName;
        }

        private void initWeight() // ����ġ x, y ��ǥ���� ����
        {
            for(int i = 0; i < weight.GetLength(0); i++)
            {
                for(int j = 0; j < weight.GetLength(1); j++)
                {
                    weight[i, j] = 0f;
                }
            }
        }

        // �� ��ũ��Ʈ�� �ް��ִ� �ֵ� �迭�� ���
        private void updateWeight()
        {
            for(int i = 0; i < allUnits.Length; i++)
            {
                LocationXY unitLocation = LocationControl.convertPositionToLocation(allUnits[i].gameObject.transform.position);
                weight[unitLocation.y, unitLocation.x] += 5000;
            }
        }

        public void initAllUnits(ParentBT[] allUnits)
        {
            this.allUnits = allUnits;
        }

        public LocationXY searchNextLocation(LocationXY unitLocation, LocationXY target)
        {
            initWeight();
            updateWeight();

            return calcLocationWeight(unitLocation, target);
        }

        private LocationXY calcLocationWeight(LocationXY unitLocation, LocationXY target)
        {
            List<LocationXY> nearLocation = new List<LocationXY>();
            nearLocation.Clear();

            if (closeList.Count == 0)
            {
                closeList.Add(unitLocation);
            }

            for (int i = unitLocation.y - 1; i <= unitLocation.y + 1; i++)
            {
                for(int j = unitLocation.x - 1; j <= unitLocation.x + 1; j++)
                {
                    if (checkLocationArrange(j, i))
                    {
                        LocationXY locationXY = new LocationXY();
                        locationXY.x = j;
                        locationXY.y = i;
                        nearLocation.Add(locationXY);
                    }

                }
            }

            float minWeight = 100000f;
            float temp = 0f;
            int index = -1;

            for (int i = 0; i < nearLocation.Count; i++)
            {
                if (isVisitedNode(nearLocation[i]))
                {
                    continue;
                }

                weight[nearLocation[i].y, nearLocation[i].x] += Vector3.Distance(LocationControl.convertLocationToPosition(nearLocation[i]), LocationControl.convertLocationToPosition(target));
                weight[nearLocation[i].y, nearLocation[i].x] += (Mathf.Abs(target.x - unitLocation.x) + Mathf.Abs(target.y - unitLocation.y));

                if (minWeight >= (temp = weight[nearLocation[i].y, nearLocation[i].x]))
                {
                    minWeight = temp;
                    index = i;
                }
            }

            if (index == -1)
            {
                closeList.Clear();
                return unitLocation;
            }

            if (nearLocation.Count != 0 &&
                closeList.Contains(nearLocation[index]) == false)
            {
                closeList.Add(nearLocation[index]);
            }

            return nearLocation[index];
        }

        private bool checkLocationArrange(int x, int y)
        {
            if ((x >= 0 && x < weight.GetLength(1))
                    && (y >= 0 && y < weight.GetLength(0)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool isVisitedNode(LocationXY location)
        {
            for(int i = 0; i < closeList.Count; i++)
            {
                if(location.x == closeList[i].x
                    && location.y == closeList[i].y)
                {
                    return true;
                }
            }

            return false;
        }

        public void initCloseList()
        {
            this.closeList.Clear();
        }

        public bool calcWeightLcoation(LocationXY location)
        {
            for(int i = 0; i < allUnits.Length; i++)
            {
                if (location.CompareTo(allUnits[i].getMyLocation()) == true)
                {
                    closeList.Clear();
                    return true;
                }
            }

            return false;
        }
    }
}

