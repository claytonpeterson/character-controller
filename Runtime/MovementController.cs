using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterMovement
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField]
        private Movement movement;

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                movement.Dash(
                    new Vector3(
                        x: ctx.ReadValue<Vector2>().x, 
                        y: 0, 
                        z: ctx.ReadValue<Vector2>().y), ctx.time);
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