using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public float MaxXSway;
    public float XMovementSpeed;
    public float ReturnSpeed;

    public float SwaySpeed;

    private Vector3 normalPosition;
    private float xMin;
    private float xMax;

    [SerializeField]
    private GameObject leftHand;

    [SerializeField]
    private GameObject rightHand;

    [SerializeField]
    private CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        normalPosition = transform.localPosition;

        xMin = normalPosition.x - MaxXSway;
        xMax = normalPosition.x + MaxXSway;
    }

    bool isMoving;

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, normalPosition, ReturnSpeed * Time.deltaTime);
        }

        isMoving = false;
    }

    public void Move(Vector2 movement)
    {
        isMoving = true;

        // Left and right movement
        if (movement.x < 0)
        {
            if (transform.localPosition.x > xMin)
            {
                transform.localPosition += new Vector3(movement.x, 0, 0) * XMovementSpeed * Time.deltaTime;
            }
        }
        else if (movement.x > 0)
        {
            if (transform.localPosition.x < xMax)
            {
                transform.localPosition += new Vector3(movement.x, 0, 0) * XMovementSpeed * Time.deltaTime;
            }
        }
    }
}
