using UnityEngine;
using System.Collections;

public class PerlinShake : MonoBehaviour
{

    public float duration = 0.6f;
    public float speed = 4.0f;
    public float magnitude = 0.3f;
    public float magnitudeMax = 1.0f;

    private float resetTime = 0;

    public void PlayShake()
    {
        if (Time.time < resetTime)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine("Shake");

        magnitude += 0.1f;

        if (magnitude > magnitudeMax)
            magnitude = magnitudeMax;

        resetTime = Time.time + duration;
    }

    IEnumerator Shake()
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            Camera.main.transform.position = originalCamPos + new Vector3(x, y, 0);

            yield return null;
        }

        Camera.main.transform.position = originalCamPos;
    }
}
