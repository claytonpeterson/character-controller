using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterMovement
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private Movement movement;

        public Movement Movement { get => movement; set => movement = value; }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                Movement.Dash(
                    new Vector3(
                        x: ctx.ReadValue<Vector2>().x, 
                        y: 0, 
                        z: ctx.ReadValue<Vector2>().y), ctx.time);
            }
            Movement.SetMoveDirection(ctx.ReadValue<Vector2>());
        }

        public void OnRotate(InputAction.CallbackContext ctx)
        {
            Movement.SetRotation(ctx.ReadValue<Vector2>());
        }

        public void OnRun(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                Movement.SetRunning(true);
            }
            else if (ctx.canceled)
            {
                Movement.SetRunning(false);
            }
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                Movement.Jump();
            }
        }

        public void OnCrouch(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                Movement.SetCrouch(true);
            }
            else if (ctx.canceled)
            {
                Movement.SetCrouch(false);
            }
        }
    }
}