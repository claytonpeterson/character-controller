using UnityEngine;
using System.Collections;

namespace CharacterMovement
{
    public class Slide
    {
        private Transform body;

        public Slide(Transform body)
        {
            this.body = body;
        }

        public Vector3 FloorAngle()
        {
            if(Physics.Raycast(body.transform.position, Vector3.down * 2, out RaycastHit hit))
            {
                return hit.normal;
            }
            return Vector3.zero;
        }
    }
}