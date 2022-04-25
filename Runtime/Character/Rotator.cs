using UnityEngine;

public class Rotator 
{
    public enum Axis
    {
        HORIZONTAL,
        VERTICAL
    }

    private readonly Transform bodyTransform;
    private readonly float rotationSpeed;
    private readonly float smoothing;
    private readonly Axis rotationAxis;

    private float smoothRotationInput;
    private Vector3 rotation;

    public Rotator(Transform bodyTransform, float rotationSpeed, float smoothing, Axis rotationAxis)
    {
        this.bodyTransform = bodyTransform;
        this.rotationSpeed = rotationSpeed;
        this.smoothing = smoothing;
        this.rotationAxis = rotationAxis;
    }

    public void Rotate(float rotationInput)
    {
        rotationInput = rotationInput * rotationSpeed * Time.deltaTime;
        
        smoothRotationInput = Mathf.Lerp(
            smoothRotationInput, 
            rotationInput, 
            smoothing);

        rotation = GetEulerRotation(smoothRotationInput);

        if (rotation.magnitude > 0)
        {
            bodyTransform.rotation = Quaternion.Euler(bodyTransform.rotation.eulerAngles + rotation);
        }
    }

    private Vector3 GetEulerRotation(float rotationInput)
    {
        if (rotationAxis == Axis.HORIZONTAL)
        {
            return new Vector3(0f, rotationInput, 0f);
        }
        else if (rotationAxis == Axis.VERTICAL)
        {
            return new Vector3(rotationInput, 0, 0f);
        }
        return Vector3.zero;
    }
}
