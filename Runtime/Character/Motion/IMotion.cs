using UnityEngine;
using System.Collections;

namespace CharacterMovement
{
    public interface IMotion
    {
        Vector3 Velocity();
    }
}