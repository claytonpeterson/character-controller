using UnityEngine;

namespace CharacterMovement
{
    [System.Serializable]
    public class DashMechanic
    {
        [SerializeField]
        private float doubleTapActivationSpeed = 0.25f;

        [SerializeField]
        private float dashForce;
        
        [SerializeField]
        private float duration;

        private double lastInput;

        public bool Dash(CharacterControllerForces forces, Vector3 direction, double time)
        {
            var canDash = CanDash(DurationSinceLastTap(time));
            if (canDash)
            {
                forces.AddForce(new TimedForce(direction * dashForce, duration));
            }

            lastInput = time;
            return canDash;
        }

        public bool CanDash(double duration)
        {
            return duration <= doubleTapActivationSpeed;
        }

        private double DurationSinceLastTap(double currentTime)
        {
            return currentTime - lastInput;
        }
    }
}