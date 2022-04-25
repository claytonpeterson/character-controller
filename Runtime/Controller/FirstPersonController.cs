using UnityEngine;

namespace CharacterMovement 
{
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField]
        private CharacterController characterController;

        [SerializeField]
        private Transform headCamera;

        [SerializeField]
        private float rotationSpeed;

        [SerializeField]
        private float rotationSmoothing;

        [SerializeField]
        private float gravityMultiplier;

        [SerializeField]
        private float jumpForce = 12;

        [SerializeField]
        private float jumpDuration = 0.5f;

        [SerializeField]
        private float doubleJumpHeightReduction = 2f;

        [SerializeField]
        private CameraShake camShake;

        private Rotator horizontalRotation;
        private Rotator verticalRotation;

        private Body body;

        private Vector2 inputDirection;
        private Vector3 moveDirection;
        private Vector3 rotationInput;

        [SerializeField]
        private Motion motion;

        [SerializeField]
        private Speed speed;

        [SerializeField]
        private float dashForce = 12;

        [SerializeField]
        private float dashDuration = 1;

        [SerializeField]
        private float arielDashForce = 20;

        [SerializeField]
        private float arielDashDuration = 1.5f;

        //private Slide slide;

        private JumpMechanic jump;

        private DashMechanic dash;
        private DashMechanic arielDash;

        public Motion Motion { get => motion; }

        float currentSpeed = 0;

        private void Start()
        {
            horizontalRotation = new Rotator(transform, rotationSpeed, rotationSmoothing, Rotator.Axis.HORIZONTAL);
            verticalRotation = new Rotator(headCamera, rotationSpeed, rotationSmoothing, Rotator.Axis.VERTICAL);

            motion = new Motion(transform, Gravity());
            body = new Body(characterController, horizontalRotation);

            //slide = new Slide(characterController.transform);

            dash = new DashMechanic(transform, motion.Forces, dashForce, dashDuration);
            arielDash = new DashMechanic(transform, motion.Forces, arielDashForce, arielDashDuration);

            jump = new JumpMechanic(
                characterController, 
                motion.Forces, 
                jumpForce, 
                jumpForce / doubleJumpHeightReduction, 4);
        }

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
            body.Rotate(rotationInput);
            body.Move(motion.Velocity(true));

            /*if (slideForce != null)
            {
                slideForce.ChangeVelocity(slide.FloorAngle() * 15);
                Debug.Log(slide.FloorAngle());
            }*/

            // Rotate camera
            verticalRotation.Rotate(-rotationInput.y);
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
            jump.Jump(motion.Velocity(false), jumpDuration);
        }

        public void Dash(Vector3 direction)
        {
            if(characterController.isGrounded)
            {
                dash.Dash(transform.TransformDirection(direction));
                camShake.Shake(-direction.z * 2, 0, direction.x * 2, 0.25f);
            }
            else
            {
                arielDash.Dash(transform.TransformDirection(direction));
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

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            Debug.DrawRay(transform.position, motion.Velocity(true), Color.green);
        }
    }
}
