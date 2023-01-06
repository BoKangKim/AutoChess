using UnityEngine;

namespace Battle.Location
{
    public struct LocationXY
    {
        public int x;
        public int y;

        public void init(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public static class LocationControl
    {
        private const float radius = 0.65f;
        private const float tileDistanceX = 1.3f;
        private const float tileDistanceY = 1.1f;

        public static LocationXY convertPositionToLocation(Vector3 unitPostion)
        {
            LocationXY location;

            location.x = (int)(unitPostion.x / tileDistanceX);
            location.y = (int)(unitPostion.y / tileDistanceY);

            return location;
        }

        public static Vector3 convertLocationToPosition(LocationXY location)
        {
            Vector3 position = Vector3.zero;

            position.x = location.x * tileDistanceX;
            position.y = location.y * tileDistanceY;

            return position;
        }

        public static Vector3 getFromLocationDirectionVector(LocationXY from, LocationXY to)
        {
            Vector3 fromPos = Vector3.zero;
            Vector3 toPos = Vector3.zero;

            fromPos = convertLocationToPosition(from);
            toPos = convertLocationToPosition(to);

            return (toPos - fromPos).normalized;
        }

        private static bool isEscapeLocation(LocationXY location,Vector3 unitPosition)
        {
            Vector3 convertResult = convertLocationToPosition(location);

            if (convertResult.x - unitPosition.x >= radius
                || convertResult.y - unitPosition.y >= radius)
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

