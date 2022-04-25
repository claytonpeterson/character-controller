using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterMovement
{
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private FirstPersonController movement;

        public void OnMove(InputAction.CallbackContext ctx)
        {
            Debug.Log(ctx.ReadValue<Vector2>());
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