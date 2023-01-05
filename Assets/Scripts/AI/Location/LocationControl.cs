using UnityEngine;

namespace Battle.AI
{
    public struct Location
    {
        public int x;
        public int y;
    }


    public class LocationControl
    {
        private const float radius = 0.65f;
        private const float tileDistanceX = 1.3f;
        private const float tileDistanceY = 1.1f;

        public Location convertPositionToLocation(Vector3 unitPostion)
        {
            Location location;

            location.x = (int)(unitPostion.x / tileDistanceX);
            location.y = (int)(unitPostion.y / tileDistanceY);

            return location;
        }

        public Vector3 convertLocationToPosition(Location location)
        {
            Vector3 position = Vector3.zero;

            position.x = location.x * tileDistanceX;
            position.y = location.y * tileDistanceY;

            return position;
        }

        public Vector3 getDirectionVector(Location from,Location to)
        {
            Vector3 fromPos = Vector3.zero;
            Vector3 toPos = Vector3.zero;

            fromPos = convertLocationToPosition(from);
            toPos = convertLocationToPosition(to);

            return (toPos - fromPos).normalized;
        }

        private bool isEscapeLocation(Location location,Vector3 unitPosition)
        {
            Vector3 convertResult = convertLocationToPosition(location);

            if (convertResult.x - unitPosition.x >= radius
                || convertResult.y - unitPosition.y >= radius)
            {
                return true;
            }

            return false;
        }
        
}

}

