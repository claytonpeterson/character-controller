using UnityEngine;

public class HandMovement : MonoBehaviour
{
    [SerializeField] private float maxSway = 0.2f;
    [SerializeField] private float minSwaySpeed = 0.25f;
    [SerializeField] private float maxSwaySpeed = 2;
    [SerializeField] private float returnSpeed = 10;

    [SerializeField] private CharacterController cc;
    [SerializeField] private float rotationMaxSpeed = 100f;

    [SerializeField] private AnimationCurve swaySpeedCurve;

    private Vector3 normalPosition;
    private Vector3 minPosition;
    private Vector3 maxPosition;

    private Vector2 input;

    void Start()
    {
        normalPosition = transform.localPosition;

        minPosition = normalPosition + new Vector3(-maxSway, 0, 0);
        maxPosition = normalPosition + new Vector3(maxSway, 0, 0);
    }

    void Update()
    {
        if (CanMove())
        {
            MoveTowardsPosition(
                targetPosition: input.x > 0 ? maxPosition : minPosition, 
                speed: CurrentSwaySpeed());
        }
        else
        {
            MoveTowardsPosition(
                targetPosition: normalPosition, 
                speed: returnSpeed);
        }
    }

    public void Move(Vector2 input)
    {
        this.input = input;
    }

    private bool CanMove()
    {
        return input.magnitude > 0 && CurrentSwaySpeed() >= minSwaySpeed;
    }

    private void MoveTowardsPosition(Vector3 targetPosition, float speed)
    {
        transform.localPosition = Vector3.Lerp(
            a: transform.localPosition,
            b: targetPosition,
            t: speed * Time.deltaTime);
    }

    private float CurrentSwaySpeed()
    {
        return maxSwaySpeed * swaySpeedCurve.Evaluate(RotationInputSpeed());
    }

    private float RotationInputSpeed()
    {
        return Mathf.Clamp(Mathf.Abs(input.x) / rotationMaxSpeed, 0f, 1f);
    }
}
