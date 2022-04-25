using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterMovement
{
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private FirstPersonController movement;

        private float moveStart = 0;

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if(ctx.started)
            {
                // Determine dash
                if(Time.time - moveStart < 0.3f)
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
            movement.SetRotation(ctx.ReadValue<Vector2>());
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