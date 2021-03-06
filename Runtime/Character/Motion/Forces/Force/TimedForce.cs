using UnityEngine;

namespace CharacterMovement
{
    public class TimedForce : IForce
    {
        private Vector3 initialVelocity;

        private readonly float startTime;
        private readonly float endTime;
        private readonly float duration;

        public TimedForce(Vector3 initialVelocity, float duration)
        {
            this.initialVelocity = initialVelocity;
            this.duration = duration;

            startTime = Time.time;
            endTime = startTime + duration;
        }

        public bool IsComplete()
        {
            return Time.time >= endTime;
        }

        public Vector3 ForceVelocity()
        {
            var lerp = (Time.time - startTime) / duration;

            return Vector3.Lerp(initialVelocity, Vector3.zero, lerp);
        }
    }
}