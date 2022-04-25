using UnityEngine;

namespace CharacterMovement
{
    public class JumpMechanic
    {
        private readonly CharacterController cc;
        private readonly CombinedForce forces;
        private readonly float jumpForce;
        private readonly float doubleJumpForce;
        private readonly int maxInAirJumps;

        private TimedForce jump;
        private int inAirJumps;

        public JumpMechanic(CharacterController cc, CombinedForce forces, float jumpForce, float doubleJumpForce, int maxInAirJumps)
        {
            this.cc = cc;
            this.forces = forces;
            this.jumpForce = jumpForce;
            this.doubleJumpForce = doubleJumpForce;
            this.maxInAirJumps = maxInAirJumps;
        }

        public void Jump(Vector3 velocity, float duration)
        {
            if(jump == null && cc.isGrounded)
            {
                StartJump(velocity, duration);
            }
            else if (inAirJumps < maxInAirJumps-1)
            {
                EndJump();
                DoubleJump(velocity, duration);
            }
        }

        private void StartJump(Vector3 velocity, float duration)
        {
            inAirJumps = 0;

            forces.AddForce(force: 
                new TimedForce(Vector3.up * jumpForce + velocity, duration));
        }

        private void DoubleJump(Vector3 velocity, float duration)
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