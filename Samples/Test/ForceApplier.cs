using UnityEngine;
using CharacterMovement;

public class ForceApplier : MonoBehaviour
{
    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    private float duration;

    private void OnTriggerEnter(Collider other)
    {
        var force = new TimedForce(velocity, duration);
        other.GetComponent<FirstPersonController>().Motion.Forces.AddForce(force);
    }
}
