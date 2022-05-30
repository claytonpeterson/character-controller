using UnityEngine;
using System.Collections;

namespace CharacterMovement
{
    [System.Serializable]
    public class Slide
    {
        public float FloorAngle(Vector3 bodyPosition)
        {
            return Vector3.Angle(FloorDirection(bodyPosition), Vector3.up);
        }

        public Vector3 FloorDirection(Vector3 bodyPosition)
        {
            if(Physics.Raycast(bodyPosition, Vector3.down, out RaycastHit hit, 2))
            {
                return hit.normal;
            }
            return Vector3.zero;
        }
    }
}