using UnityEngine;


namespace CharacterMovement
{
    public class DashMechanic
    {
        private readonly Transform transform;
        private readonly CombinedForce forces;
        private readonly float dashForce;
        private readonly float duration;

        private TimedForce dash;

        public DashMechanic(Transform transform, CombinedForce forces, float force, float duration)
        {
            this.transform = transform;
            this.forces = forces;
            this.dashForce = force;
            this.duration = duration;
        }

        public void Dash(Vector3 direction)
        {
            Debug.Log(direction);

            forces.AddForce(force:
                new TimedForce(direction * dashForce, duration));
        }
    }
}