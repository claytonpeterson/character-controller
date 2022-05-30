using UnityEngine;

namespace CharacterMovement
{
    [System.Serializable]
    public class JumpMechanic
    {
        [SerializeField]
        private float duration;

        [SerializeField]
        private float jumpForce;
        
        [SerializeField]
        private float doubleJumpForce;

        [SerializeField]
        private int maxInAirJumps;

        private CombinedForce forces;
        private TimedForce jump;
        private int inAirJumps;

        public void Jump(CharacterController cc, CombinedForce forces, Vector3 velocity)
        {
            this.forces = forces;

            if(jump == null && cc.isGrounded)
            {
                StartJump(velocity);
            }
            else if (inAirJumps < maxInAirJumps-1)
            {
                EndJump();
                DoubleJump(velocity);
            }
        }

        private void StartJump(Vector3 velocity)
        {
            inAirJumps = 0;

            forces.AddForce(force: 
                new TimedForce(Vector3.up * jumpForce + velocity, duration));
        }

        private void DoubleJump(Vector3 velocity)
        {
            inAirJumps++;

            forces.AddForce(force:
                new TimedForce(Vector3.up * doubleJumpForce + velocity, duration));
        }

        private void EndJump()
        {
            forces.RemoveForce(jump);
            jump = null;
        }
    }
}