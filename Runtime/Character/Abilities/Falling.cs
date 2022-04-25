using UnityEngine;

public class Falling : MonoBehaviour
{
    [SerializeField]
    private float safeFallHeight;

    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private CameraShake shake;

    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private AudioClip landingSound;

    private float highestHeight;
    private float currentHeight;
    private float floorHeight;
    private float fallDistance;

    private void Update()
    {
        if(!characterController.isGrounded)
        {
            currentHeight = transform.position.y;

            if (currentHeight > highestHeight)
            {
                highestHeight = currentHeight;
            }
        }
        else 
        {
            if(highestHeight > currentHeight)
            {
                floorHeight = characterController.transform.position.y;
                fallDistance = highestHeight - floorHeight;
                highestHeight = currentHeight;

                ApplyShake(fallDistance);

                audio.clip = landingSound;
                audio.Play();
            }
        }
    }

    private void ApplyShake(float distance)
    {
        shake.Shake(
            maxShakeX: 2.5f, 
            maxShakeY: Random.Range(-1f, 1f), 
            0,
            duration:  0.25f);
    }
}
