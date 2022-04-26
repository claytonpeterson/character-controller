using UnityEngine;

namespace CharacterMovement
{
    public class WallrunMechanic
    {
        private readonly CharacterController characterController;
        private readonly Transform transform;
        private readonly Speed speed;
        private readonly float minSpeed;

        private float startTime;

        public WallrunMechanic(CharacterController characterController, Transform transform, Speed speed, float minSpeed)
        {
            this.characterController = characterController;
            this.transform = transform;
            this.speed = speed;
            this.minSpeed = minSpeed;
        }

        public void WallRide()
        {
            if(CanWallride() && startTime <= 0)
            {

            }
        }

        public bool CanWallride()
        {
            return 
                (Left() || Right()) && 
                MovingFastEnough() &&
                //WithinDuration() &&
                !characterController.isGrounded;
        }

        public bool Left()
        {
            return CheckDirection(transform.TransformDirection(Vector3.left));
        }

        public bool Right()
        {
            return CheckDirection(transform.TransformDirection(Vector3.right));
        }

        private bool CheckDirection(Vector3 direction)
        {
            return Physics.Raycast(transform.position, direction, 2);
        }

        private bool MovingFastEnough()
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
