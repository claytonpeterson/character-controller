using UnityEngine;

namespace CharacterMovement
{
    public class WallrunMechanic
    {
        private readonly CharacterController characterController;
        private readonly Transform transform;
        private readonly Speed speed;
        private readonly float minSpeed;

        private bool isWallRunning;

        public bool IsWallRunning => isWallRunning;

        public WallrunMechanic(CharacterController characterController, Transform transform, Speed speed, float minSpeed)
        {
            this.characterController = characterController;
            this.transform = transform;
            this.speed = speed;
            this.minSpeed = minSpeed;
        }

        public void Update()
        {
            isWallRunning = characterController.isGrounded == false && CanWallride();
            Debug.Log(string.Format("left? {0} right? {1}", Left(), Right()));
        }

        public bool CanWallride()
        {
            return (Left() || Right()) && MovingFastEnough();
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
            return speed.CurrentSpeed() > minSpeed;
        }
    }
}
