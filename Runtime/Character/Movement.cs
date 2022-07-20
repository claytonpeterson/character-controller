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

        private Vector2 inputDirection;
        private Vector3 rotationInput;

        bool cleanedup;

        CharacterControllerForces forces;
        ConstantForce slideForce = null;
        ControlledForce characterMovement;

        private void Start()
        {
            characterMovement = new ControlledForce(transform);
            forces = new CharacterControllerForces();

            // TODO REFACTOR THIS (it can probably be setup in the object?)
            verticalRotator.Setup(Rotator.Axis.VERTICAL, headCamera);
            horizontalRotator.Setup(Rotator.Axis.HORIZONTAL, transform);        
        }

        public void SetMoveDirection(Vector2 input)
        {
            inputDirection = input;
        }

        public void SetRotation(Vector3 rotation)
        {
            rotationInput = rotation;
        }

        public void SetRunning(bool isRunning)
        {
            if (isRunning)
                speed.SetSpeed(1);
            else
                speed.SetSpeed(0);
        }

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

        public void Jump()
        {
            jump.Jump(characterController, forces, Velocity(false));
        }

        public void Update()
        {
            speed.UpdateSpeed(isAccelerating: inputDirection.magnitude > 0);

            characterMovement.AddInput(GetMoveDirection(), GetCurrentSpeed());
            forces.RemoveCompleteForces();

            MoveBody();
            RotateBody();

            HandleWallRide();

            // TODO Can this be moved?
            handMovement.Move(rotationInput);

            /*if (slideForce != null)
            {
                slideForce.ChangeVelocity(slide.FloorAngle() * 15);
                Debug.Log(slide.FloorAngle());
            }*/
        }

        public Vector3 Velocity(bool useGravity)
        {
            var v = characterMovement.GetVelocity();

            if (useGravity)
                v = ApplyGravity(v);

            return v + forces.Velocity();
        }


        /// <summary>
        /// Gets the move direction that cooresponds with the input direction
        /// </summary>
        /// <returns></returns>
        private Vector3 GetMoveDirection()
        {
            if (inputDirection == Vector2.zero)
                return Vector3.zero;

            return new Vector3(inputDirection.x, 0, inputDirection.y);
        }

        /// <summary>
        /// Returns the current move speed of the character
        /// </summary>
        /// <returns></returns>
        private float GetCurrentSpeed()
        {
            if (speed.Percent() <= 0)
                return 0;
            else
                return speed.CurrentSpeed();
        }

        /// <summary>
        /// 
        /// </summary>
        private void MoveBody()
        {
            var vel = Velocity(useGravity: !wallrun.CanWallride(characterController, speed));
            characterController.Move(vel * Time.deltaTime);
        }

        /// <summary>
        /// 
        /// </summary>
        private void RotateBody()
        {
            horizontalRotator.Rotate(rotationInput.x);
            verticalRotator.Rotate(-rotationInput.y);
        }

        public void Dash(Vector3 direction, double time)
        {
            if(characterController.isGrounded)
            {
                if(dash.Dash(forces, transform.TransformDirection(direction), time))
                {
                    camShake.Shake(-direction.z * 2, 0, direction.x * 2, 0.25f);
                }
            }
            else
            {
                if(aerialDash.Dash(forces, transform.TransformDirection(direction), time))
                {
                    camShake.Shake(-direction.z * 3, 0, direction.x * 3, 0.25f);
                }
            }
        }

        private void HandleWallRide()
        {
            // Check for wall ride
            if (wallrun.CanWallride(characterController, speed))
            {
                cleanedup = false;
                camShake.Tilt(wallrun.Left(transform) ? -25 : 25);
            }
            else if (!cleanedup)
            {
                camShake.Tilt(0);
                cleanedup = true;
            }
        }

        private float Gravity()
        {
            return 9.81f * gravityMultiplier;
        }

        private Vector3 ApplyGravity(Vector3 velocity)
        {
            velocity.y -= 9.81f;
            return velocity;
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            Debug.DrawRay(transform.position,Velocity(true), Color.green);
        }
    }
}
