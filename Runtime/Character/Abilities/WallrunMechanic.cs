using UnityEngine;

public class WallrunMechanic
{
    private readonly CharacterController characterController;
    private readonly Transform transform;

    private bool isWallRunning;

    public bool IsWallRunning => isWallRunning; 

    public WallrunMechanic(CharacterController characterController, Transform transform)
    {
        this.characterController = characterController;
        this.transform = transform;
    }

    public void Update()
    {
        isWallRunning = characterController.isGrounded == false && CanWallride();
    }

    private bool CanWallride()
    {
        return CheckDirection(Left()) || CheckDirection(Right());
    }

    private bool CheckDirection(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, 2);
    }

    private Vector3 Left()
    {
        return transform.TransformDirection(Vector3.left);
    }

    private Vector3 Right()
    {
        return transform.TransformDirection(Vector3.right);
    }
}
