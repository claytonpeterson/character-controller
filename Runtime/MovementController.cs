using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterMovement
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField]
        private Movement movement;

        [SerializeField]
        private float dashDoubleTapSpeed = 0.4f;

        private float moveStart = 0;

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if(ctx.started)
            {
                // Determine dash
                if(Time.time - moveStart < dashDoubleTapSpeed)
                {
                    var inputDirection = ctx.ReadValue<Vector2>();
                    movement.Dash(new Vector3(inputDirection.x, 0, inputDirection.y));
                }

                moveStart = Time.time;
            }

            movement.SetMoveDirection(ctx.ReadValue<Vector2>());
        }

        public void OnRotate(InputAction.CallbackContext ctx)
        {
            movement.SetRotation(ctx.ReadValue<Vector2>().normalized);
        }

        public void OnRun(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                movement.SetRunning(true);
            }
            else if (ctx.canceled)
            {
                movement.SetRunning(false);
            }
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                movement.Jump();
            }
        }

        public void OnCrouch(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                movement.SetCrouch(true);
            }
            else if (ctx.canceled)
            {
                movement.SetCrouch(false);
            }
        }
    }
}