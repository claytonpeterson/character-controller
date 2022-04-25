using UnityEngine;

namespace CharacterMovement
{
    public class Jump
    {
        private readonly CombinedForce forces;
        private readonly float jumpForce;

        private TimedForce jump;

        public Jump(CombinedForce forces, float jumpForce)
        {
            this.forces = forces;
            this.jumpForce = jumpForce;
        }

        public void StartJump()
        {
            jump = new TimedForce(Vector3.up * jumpForce, 1);
            forces.AddForce(jump);
        }

        public void EndJump()
        {
            forces.RemoveForce(jump);
            jump = null;
        }
    }
}