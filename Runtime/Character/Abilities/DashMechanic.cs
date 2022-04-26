using UnityEngine;

namespace CharacterMovement
{
    [System.Serializable]
    public class DashMechanic
    {
        [SerializeField]
        private float dashForce;
        
        [SerializeField]
        private float duration;

        public void Dash(CombinedForce forces, Vector3 direction)
        {
            forces.AddForce(force:
                new TimedForce(direction * dashForce, duration));
        }
    }
}