using System.Collections.Generic;
using UnityEngine;

namespace CharacterMovement
{
    [System.Serializable]
    public class CombinedForce : IMotion
    {
        [SerializeField]
        private List<IForce> activeForces = new List<IForce>();

        public Vector3 Velocity()
        {
            Vector3 velocity = Vector3.zero;
            for (int i = 0; i < activeForces.Count; i++)
            {
                velocity += activeForces[i].ForceVelocity();
            }
            return velocity;
        }

        public IForce AddForce(IForce force)
        {
            activeForces.Add(force);
            return force;
        }

        public void RemoveForce(IForce force)
        {
            activeForces.Remove(force);
        }

        // TODO rename this "update forces"
        public void RemoveCompleteForces()
        {
            for (int i = 0; i < activeForces.Count; i++)
            {
                if (activeForces[i].IsComplete())
                {
                    activeForces.Remove(activeForces[i]);
                }
            }
        }

        public bool IsComplete()
        {
            throw new System.NotImplementedException();
        }
    }
}
