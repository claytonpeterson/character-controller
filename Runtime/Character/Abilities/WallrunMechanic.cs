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

        Debug.Log(string.Format("left? {0} right? {1}", Left(), Right()));
    }

    private bool CanWallride()
    {
        return Left() || Right();
    }

    public bool Left()
    {
        return CheckDirection(transform.TransformDirection(Vector3.left));
    }

    public bool Right()
    {
        return CheckDirection(transform.TransformDirection(Vector3.right));
    }

    private bool CheckDirection(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, 2);
    }
}
