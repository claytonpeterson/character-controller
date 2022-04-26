using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private Transform cam;

    private float lerp;
    private bool isShaking;

    public void Shake(float maxShakeX, float maxShakeY, float maxShakeZ, float duration)
    {
        if (isShaking == false)
        {
            StartCoroutine(ShakeRoutine(
                endRotation: CreateRotation(maxShakeX, maxShakeY, maxShakeZ), 
                duration: duration));
        }
    }

    public void Tilt(float tileAmount)
    {
        Vector3 eulerRotation = new Vector3(0, 0, tileAmount);;
        cam.transform.localRotation = Quaternion.Euler(eulerRotation);
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

    private Quaternion CreateRotation(float xMax, float yMax, float zMax)
    {
        Vector3 eulerRotation = new Vector3(xMax, yMax, zMax);
        return Quaternion.Euler(eulerRotation) * cam.localRotation;
    }
}
