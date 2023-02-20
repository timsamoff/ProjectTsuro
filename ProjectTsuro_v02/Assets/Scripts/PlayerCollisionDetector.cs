using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    [SerializeField] private float collisionMagnitudeThreshold = 5f;
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.2f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision.");
        if (other.relativeVelocity.magnitude > collisionMagnitudeThreshold)
        {
            Debug.Log("Collision over threshold.");
            ScreenShake.Instance.Shake(shakeDuration, shakeMagnitude);
        }
    }
}