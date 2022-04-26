using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private Transform cam;

    private float lerp;
    private bool isShaking;
    private bool isTilting;

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
        if (isTilting)
            return;

        Vector3 eulerRotation = new Vector3(0, 0, tileAmount);;
       cam.transform.localRotation = Quaternion.Euler(eulerRotation);

        //StartCoroutine(TiltRoutine(Quaternion.Euler(eulerRotation), 0.5f));
    }

    private IEnumerator TiltRoutine(Quaternion endRotation, float duration)
    {
        isTilting = true;

        Quaternion startRotation = cam.transform.localRotation;

        lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime / duration;
            
            cam.transform.localRotation = Quaternion.Lerp(
                startRotation, 
                endRotation, 
                lerp);
            
            yield return null;
        }

        isTilting = false;
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
