using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeManager : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 originalPosition;
    private float shakeIntensity;
    private float shakeDuration;
    private float shakeTimer;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        originalPosition = cameraTransform.position;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            ShakeCamera();
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            cameraTransform.position = originalPosition;
        }
    }

    public void ShakeCamera(float intensity, float duration)
    {
        shakeIntensity = intensity;
        shakeDuration = duration;
        shakeTimer = duration;
    }

    private void ShakeCamera()
    {
        cameraTransform.position = originalPosition + new Vector3(Random.Range(-shakeIntensity, shakeIntensity), Random.Range(-shakeIntensity, shakeIntensity), 0f);
    }
}
