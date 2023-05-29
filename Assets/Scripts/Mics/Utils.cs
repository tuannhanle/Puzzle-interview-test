
using System;
using UnityEngine;

namespace Mics
{
    public class Utils
    {
        public static bool ArePositionsClose(Transform position1, Transform position2, float deltaDistance = 1f)
        {
            float distance = Vector3.Distance(position1.position, position2.position);
            return distance <= deltaDistance;
        }
        
        public static bool ArePositionsClose(Vector3 position1, Vector3 position2, float deltaDistance = 1f)
        {
            float distance = Vector3.Distance(position1, position2);
            return distance <= deltaDistance;
        }

        public static bool ArePositionsClose(float pos1, float pos2, float deltaDistance = 1f)
        {
            float distance = Math.Abs(pos1 - pos2);
            return distance <= deltaDistance;
        }
        
        public static Vector2 Lerp(Vector2 a, Vector2 b, float interpolation)
        {
            var posX = Mathf.Lerp(a.x, b.x, interpolation);
            var posY = Mathf.Lerp(a.y, b.y, interpolation);
            return new Vector2(posX, posY);

        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float interpolation)
        {
            var posX = Mathf.Lerp(a.x, b.x, interpolation);
            var posY = Mathf.Lerp(a.y, b.y, interpolation);
            var posZ = Mathf.Lerp(a.z, b.z, interpolation);
            return new Vector3(posX, posY, posZ);
        }
    } 
}


