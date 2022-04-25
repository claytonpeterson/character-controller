using UnityEngine;

namespace CharacterMovement
{
    [System.Serializable]
    public class Body 
    {
        private readonly CharacterController characterController;
        private readonly Rotator characterRotation;

        public Body(CharacterController characterController, Rotator characterRotation)
        {
            this.characterController = characterController;
            this.characterRotation = characterRotation;
        }

        public void Move(Vector3 velocity)
        {
            characterController.Move(velocity * Time.deltaTime);
        }

        public void Rotate(Vector3 rotationInput)
        {
            characterRotation.Rotate(rotationInput.x);
        }
    }
}