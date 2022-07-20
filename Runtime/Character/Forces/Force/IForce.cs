using UnityEngine;

namespace CharacterMovement
{
    public interface IForce
    {
        Vector3 GetVelocity();

        bool IsComplete();
    }
}