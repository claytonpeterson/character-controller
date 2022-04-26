using UnityEngine;

[System.Serializable]
public class Rotator 
{
    public enum Axis
    {
        HORIZONTAL,
        VERTICAL
    }

    [SerializeField]
    private float rotationSpeed;
    
    [SerializeField]
    private float smoothing;

    private Vector3 rotation;
    private Axis rotationAxis;
    private Transform transform;
    private float smoothRotationInput;

    public void Setup(Axis rotationAxis, Transform transform)
    {
        this.rotationAxis = rotationAxis;
        this.transform = transform;
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
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles + rotation);
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
