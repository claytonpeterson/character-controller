using UnityEngine;

namespace CharacterMovement
{
    public interface IForce
    {
        Vector3 ForceVelocity();

        bool IsComplete();
    }
}