using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private static ScreenShake instance;

    public static ScreenShake Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScreenShake>();
                if (instance == null)
                {
                    instance = new GameObject("ScreenShake").AddComponent<ScreenShake>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private float shakeDuration = 0.1f;
    [SerializeField] private float shakeAmount = 0.1f;

    public void Shake(float duration, float amount)
    {
        StartCoroutine(DoShake(duration, amount));
    }

    private IEnumerator DoShake(float duration, float amount)
    {
        Vector3 originalPosition = transform.position;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            transform.position = originalPosition + Random.insideUnitSphere * amount;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }
}