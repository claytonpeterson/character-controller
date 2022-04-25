using UnityEngine;

namespace CharacterMovement
{
    public class Controller : MonoBehaviour
    {
        private Vector2 inputDirection;
        private Vector3 moveDirection;
        private Vector3 rotationInput;

        public void AddMoveInput(Vector2 moveInput)
        {
            inputDirection = moveInput;
        }

        public void AddRotationInput(Vector3 rotationInput)
        {
            this.rotationInput = rotationInput;
        }

        public void SetJump(bool jump)
        {

        }

        public void SetRunning(bool isRunning)
        {
            /*if (isRunning)
                speed.SetSpeed(1);
            else
                speed.SetSpeed(0);*/
        }
    }
}