using UnityEngine;
using System;

namespace Battle.Location
{
    [System.Serializable]
    public struct LocationXY
    {
        public int x;
        public int y;

        public override string ToString()
        {
            return "( " + x + " , " + y + " )";
        }

        public bool CompareTo(LocationXY location)
        {
            if (x == location.x && y == location.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static class LocationControl
    {
        private const float tileDistanceX = 3f;
        private const float tileDistanceY = 2.5f;
        public static double radius 
        {
            get 
            {
                return Math.Abs(((tileDistanceX / 2) / Math.Sin((Math.PI / 180) * 60)) * 2);
                //return (2 * Math.Sqrt((4 / 3) * Math.Pow((tileDistanceX / 2), 2))) + 0.2f;
            }
        }


        public static LocationXY convertPositionToLocation(Vector3 unitPostion)
        {
            LocationXY location;
            location.x = (int)Math.Truncate((unitPostion.x / tileDistanceX));
            location.y = (int)Math.Truncate((unitPostion.z / tileDistanceY));
            
            return location;
        }

        public static Vector3 convertLocationToPosition(LocationXY location)
        {
            Vector3 position = Vector3.zero;

            position.x = location.x * tileDistanceX;
            position.z = location.y * tileDistanceY;

            if (location.y % 2 == 0)
            {
                position.x += 1.5f;
            }

            return position;
        }

        public static Vector3 convertMirrorMode(Vector3 position)
        {
            Vector3 pos;
            LocationXY location = convertPositionToLocation(position);
            location = convertMirrorMode(location);
            pos = convertLocationToPosition(location);

            return pos;
        }

        public static LocationXY convertMirrorMode(LocationXY location)
        {
            LocationXY mirror;

            mirror.x = 6 - location.x;
            mirror.y = 5 - location.y;

            return mirror;
        }

        public static Vector3 getFromLocationDirectionVector(LocationXY from, LocationXY to)
        {
            Vector3 fromPos = Vector3.zero;
            Vector3 toPos = Vector3.zero;

            fromPos = convertLocationToPosition(from);
            toPos = convertLocationToPosition(to);

            return (toPos - fromPos).normalized;
        }

        public static bool isEscapeLocation(LocationXY location,Vector3 unitPosition)
        {
            Vector3 convertResult = convertLocationToPosition(location);

            if (convertResult.x - unitPosition.x >= radius
                || convertResult.z - unitPosition.z >= radius)
            {
                return true;
            }

            return false;
        }

        public static float getDistance(LocationXY from,LocationXY to)
        {
            float distance = 0;

            distance = Mathf.Sqrt(Mathf.Pow(from.x - to.x,2) + Mathf.Pow(from.y - to.y,2));

            return distance;
        }
        
    }

}

