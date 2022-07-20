using UnityEngine;

namespace CharacterMovement 
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform headCamera;
        [SerializeField] private float gravityMultiplier;

        [SerializeField] private CameraShake camShake;
        [SerializeField] private HandMovement handMovement;

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
        [SerializeField] private Slide slide;

        public Motion Motion { get => motion; }

        private Vector2 inputDirection;
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
            speed.UpdateSpeed(isAccelerating: inputDirection.magnitude > 0);

            motion.Update(
                moveDirection: GetMoveDirection(), 
                moveSpeed: GetCurrentSpeed());

            var vel = motion.Velocity(useGravity: !wallrun.CanWallride(characterController, speed));

            // Move and rotate
            horizontalRotator.Rotate(rotationInput.x);
            verticalRotator.Rotate(-rotationInput.y);

            characterController.Move(vel * Time.deltaTime);

            // Check for wall ride
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

            handMovement.Move(rotationInput);

            /*if (slideForce != null)
            {
                slideForce.ChangeVelocity(slide.FloorAngle() * 15);
                Debug.Log(slide.FloorAngle());
            }*/
        }

        /// <summary>
        /// Gets the move direction that cooresponds with the input direction
        /// </summary>
        /// <returns></returns>
        public Vector3 GetMoveDirection()
        {
            if (inputDirection == Vector2.zero)
                return Vector3.zero;

            return new Vector3(inputDirection.x, 0, inputDirection.y);
        }

        /// <summary>
        /// Returns the current move speed of the character
        /// </summary>
        /// <returns></returns>
        public float GetCurrentSpeed()
        {
            if (speed.Percent() <= 0)
                return 0;
            else
                return speed.CurrentSpeed();
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

        public void Dash(Vector3 direction, double time)
        {
            if(characterController.isGrounded)
            {
                if(dash.Dash(motion.Forces, transform.TransformDirection(direction), time))
                {
                    camShake.Shake(-direction.z * 2, 0, direction.x * 2, 0.25f);
                }
            }
            else
            {
                if(aerialDash.Dash(motion.Forces, transform.TransformDirection(direction), time))
                {
                    camShake.Shake(-direction.z * 3, 0, direction.x * 3, 0.25f);
                }
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
            Debug.Log(slide.FloorAngle(transform.position));
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
        
        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            Debug.DrawRay(transform.position, motion.Velocity(true), Color.green);
        }
    }
}
