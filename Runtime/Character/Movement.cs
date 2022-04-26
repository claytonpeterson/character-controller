using UnityEngine;

namespace CharacterMovement 
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform headCamera;
        [SerializeField] private float gravityMultiplier;
        [SerializeField] private CameraShake camShake;

        [Header("Movement")]
        [SerializeField] private Motion motion;
        [SerializeField] private Speed speed;

        [Header("Rotation")]
        [SerializeField] private Rotator horizontalRotator;
        [SerializeField] private Rotator verticalRotator;

        [Header("Abilities")]
        [SerializeField] private JumpMechanic jump;
        [SerializeField] private DashMechanic dash;
        [SerializeField] private DashMechanic aerialDash;
        [SerializeField] private WallrunMechanic wallrun;

        public Motion Motion { get => motion; }

        float currentSpeed = 0;

        private Vector2 inputDirection;
        private Vector3 moveDirection;
        private Vector3 rotationInput;

        private void Start()
        {
            motion = new Motion(transform, Gravity());

            verticalRotator.Setup(Rotator.Axis.VERTICAL, headCamera);
            horizontalRotator.Setup(Rotator.Axis.HORIZONTAL, transform);        
        }

        bool cleanedup;

        public void Update()
        {
            if (inputDirection != Vector2.zero)
            {
                moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
            }

            speed.UpdateSpeed(isAccelerating: inputDirection.magnitude > 0);

            if(speed.Percent() <= 0)
            {
                currentSpeed = 0;
            }
            else
            {
                currentSpeed = speed.CurrentSpeed();
            }

            motion.Update(moveDirection, currentSpeed);
            Rotate(rotationInput);
            Move(motion.Velocity(useGravity: !wallrun.CanWallride(characterController, speed)));

            if(wallrun.CanWallride(characterController, speed))
            {
                cleanedup = false;
                camShake.Tilt(wallrun.Left(transform) ? -25 : 25);
            }
            else if (!cleanedup)
            {
                camShake.Tilt(0);
                cleanedup = true;
            }

            /*if (slideForce != null)
            {
                slideForce.ChangeVelocity(slide.FloorAngle() * 15);
                Debug.Log(slide.FloorAngle());
            }*/

            // Rotate camera
            verticalRotator.Rotate(-rotationInput.y);
        }

        public void SetRotation(Vector3 rotation)
        {
            rotationInput = rotation;
        }

        public void SetMoveDirection(Vector2 input)
        {
            inputDirection = input;
        }

        public void Jump()
        {
            jump.Jump(characterController, motion.Forces, motion.Velocity(false));
        }

        public void Dash(Vector3 direction)
        {
            if(characterController.isGrounded)
            {
                dash.Dash(motion.Forces, transform.TransformDirection(direction));
                camShake.Shake(-direction.z * 2, 0, direction.x * 2, 0.25f);
            }
            else
            {
                aerialDash.Dash(motion.Forces, transform.TransformDirection(direction));
                camShake.Shake(-direction.z * 3, 0, direction.x * 3, 0.25f);
            }
        }

        public void SetRunning(bool isRunning)
        {
            if (isRunning)
                speed.SetSpeed(1);
            else
                speed.SetSpeed(0);
        }

        ConstantForce slideForce = null;

        public void SetCrouch(bool isCrouching)
        {
            /*if (isCrouching)
            {
                slideForce = new ConstantForce(slide.FloorAngle().normalized);
                motion.Forces.AddForce(slideForce);
            }
            else
            {
                slideForce = null;
                motion.Forces.RemoveForce(slideForce);
                Debug.Log("done sliding");
            }*/
        }

        private float Gravity()
        {
            return 9.81f * gravityMultiplier;
        }
        private void Move(Vector3 velocity)
        {
            characterController.Move(velocity * Time.deltaTime);
        }

        private void Rotate(Vector3 rotationInput)
        {
            horizontalRotator.Rotate(rotationInput.x);
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            Debug.DrawRay(transform.position, motion.Velocity(true), Color.green);
        }
    }
}
