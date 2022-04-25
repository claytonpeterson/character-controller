using UnityEngine;

namespace CharacterMovement
{
    public class ControlledForce : IForce
    {
        private Vector3 moveDirection;
        private float moveSpeed;

        private readonly Transform body;

        public ControlledForce(Transform body)
        {
            this.body = body;
        }

        public void AddInput(Vector3 moveDirection, float moveSpeed)
        {
            this.moveDirection = moveDirection;
            this.moveSpeed = moveSpeed;
        }

        public Vector3 ForceVelocity()
        {
            var forward = body.TransformDirection(moveDirection);
            return forward * moveSpeed;
        }

        public void UpdateForce()
        {
            throw new System.NotImplementedException();
        }

        public bool IsComplete()
        {
            throw new System.NotImplementedException();
        }
    }
}