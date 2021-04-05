using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshakeController : MonoBehaviour
{
    public IEnumerator CameraShake (float ShakeTimeDuration, float ShakePower)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        

        while (elapsed < ShakeTimeDuration)
        {
            float xValue = Random.Range(0.05f, -0.05f) * ShakePower;
            float yValue = Random.Range(0.05f, -0.05f) * ShakePower;

            transform.localPosition = new Vector3(xValue, yValue, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
