using UnityEngine;

namespace CharacterMovement
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody platformRigidbody;

        [SerializeField]
        private float moveSpeed;

        private FirstPersonController controller;
        private ConstantForce force;

        private void OnTriggerEnter(Collider other)
        {
            controller = other.GetComponent<FirstPersonController>();
            
            if (controller != null)
            {
                force = new ConstantForce(platformRigidbody.velocity);
                controller.Motion.Forces.AddForce(force);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if(force != null)
            {
                force.ChangeVelocity(platformRigidbody.velocity);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            controller.Motion.Forces.RemoveForce(force);
            controller = null;
            force = null;
        }

        [SerializeField]
        private Vector3 endGoalOffset;

        private Vector3 startPosition;
        private Vector3 endPosition;

        private float lerp;

        private void Start()
        {
            startPosition = transform.position;    
        }

        private void Update()
        {
            lerp += Time.deltaTime / moveSpeed;
            platformRigidbody.MovePosition(Vector3.Lerp(startPosition, endPosition, Mathf.PingPong(lerp, 1)));
        }

        private void OnDrawGizmos()
        {
            Debug.DrawLine(startPosition, endPosition, Color.red);
        }
    }
}