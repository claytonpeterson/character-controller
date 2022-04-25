using UnityEngine;

namespace CharacterMovement
{
    public class ConstantForce : IForce
    {
        private Vector3 velocity;

        public ConstantForce(Vector3 velocity)
        {
            this.velocity = velocity;
        }

        public bool IsComplete()
        {
            return false;
        }

        public void UpdateForce()
        {
            
        }

        public Vector3 ForceVelocity()
        {
            return velocity;
        }

        // TODO this shouldnt be here if it is "constant!"
        public void ChangeVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
        }
    }
}
