using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private Transform cam;

    private float lerp;
    private bool isShaking;

    public void Shake(float maxShakeX, float maxShakeY, float duration)
    {
        if (isShaking == false)
        {
            StartCoroutine(ShakeRoutine(
                endRotation: CreateRotation(maxShakeX, maxShakeY), 
                duration: duration));
        }
    }

    private IEnumerator ShakeRoutine(Quaternion endRotation, float duration)
    {
        isShaking = true;
        Quaternion startRotation = cam.transform.localRotation;

        yield return ShakeAnimation(duration/2f, startRotation, endRotation);
        yield return ShakeAnimation(duration/2f, endRotation, startRotation);

        isShaking = false;
    }
    
    private IEnumerator ShakeAnimation(float duration, Quaternion start, Quaternion end)
    {
        lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime / duration;
            cam.transform.localRotation = Quaternion.Lerp(start, end, lerp);
            yield return null;
        }
    }

    private Quaternion CreateRotation(float xMax, float yMax)
    {
        Vector3 eulerRotation = new Vector3(xMax, yMax, 0);
        return Quaternion.Euler(eulerRotation) * cam.localRotation;
    }
}
