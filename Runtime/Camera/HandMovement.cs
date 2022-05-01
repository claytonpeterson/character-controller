using UnityEngine;

public class HandMovement : MonoBehaviour
{
    [SerializeField] private float maxSway = 0.2f;
    [SerializeField] private float maxSwaySpeed = 2;
    [SerializeField] private float returnSpeed = 10;

    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    [SerializeField] private CharacterController cc;
    [SerializeField] private float rotationMaxSpeed = 100f;

    [SerializeField] private AnimationCurve swaySpeedCurve;

    private Vector3 normalPosition;
    private Vector3 minPosition;
    private Vector2 maxPosition;

    private Vector2 input;

    void Start()
    {
        normalPosition = transform.localPosition;

        minPosition = normalPosition + new Vector3(-maxSway, 0, 0);
        maxPosition = normalPosition + new Vector3(maxSway, 0, 0);
    }

    void Update()
    {
        if (input.magnitude > 0 && CurrentSwaySpeed() > 0.25f)
        {
            if (input.x > 0)
                Sway(maxPosition, CurrentSwaySpeed());
            else
                Sway(minPosition, CurrentSwaySpeed());
        }
        else
        {
            ReturnToCenter();
        }
    }

    public void Move(Vector2 input)
    {
        this.input = input;
    }

    private void Sway(Vector3 targetPosition, float speed)
    {
        transform.localPosition = Vector3.Lerp(
            a: transform.localPosition,
            b: targetPosition,
            t: speed * Time.deltaTime);
    }

    private void ReturnToCenter()
    {
        transform.localPosition = Vector3.Lerp(
            a: transform.localPosition, 
            b: normalPosition, 
            t: returnSpeed * Time.deltaTime);
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
