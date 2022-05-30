using UnityEngine;

namespace CharacterMovement
{
    [System.Serializable]
    public class WallrunMechanic
    {
        [SerializeField]
        private float minSpeed;

        [SerializeField]
        private LayerMask layer;

        private float startTime;

        public void WallRide(CharacterController characterController, Speed speed)
        {
            if(characterController != null && CanWallride(characterController, speed) && startTime <= 0)
            {

            }
        }

        public bool CanWallride(CharacterController characterController, Speed speed)
        {
            return 
                (Left(characterController.transform) || Right(characterController.transform)) && 
                MovingFastEnough(speed) &&
                //WithinDuration() &&
                !characterController.isGrounded;
        }

        public bool Left(Transform transform)
        {
            return CheckDirection(transform.TransformDirection(Vector3.left), transform);
        }

        public bool Right(Transform transform)
        {
            return CheckDirection(transform.TransformDirection(Vector3.right), transform);
        }

        private bool CheckDirection(Vector3 direction, Transform transform)
        {
            return Physics.Raycast(transform.position, direction, 2, layer);
        }

        private bool MovingFastEnough(Speed speed)
        {
            return speed.CurrentSpeed() >= minSpeed;
        }

        private bool WithinDuration()
        {
            if(startTime > 0)
            {
                if (Time.time - startTime <= 4)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
