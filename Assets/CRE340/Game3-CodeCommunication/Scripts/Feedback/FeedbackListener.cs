using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackListener : MonoBehaviour, IFeedback
{
    public void TriggerFeedback(GameObject source)
    {
        Debug.Log("Triggered feedback on " + source.name);
        shakeIntensity = 0.1f;
        shakeDuration = 0.5f;
        shakeTimer = 0.5f;
    }

    public void TriggerFeedback(GameObject source, float intensity)
    {
        Debug.Log("Triggered feedback on " + source.name + " with intensity " + intensity);
    }

    public void TriggerFeedback(GameObject source, float intensity, float duration)
    {
        Debug.Log("Triggered feedback on " + source.name + " with intensity " + intensity + " and duration " + duration);
    }

    public void TriggerFeedback(GameObject source, float intensity, float duration, float cooldown)
    {
        Debug.Log("Triggered feedback on " + source.name + " with intensity " + intensity + " and duration " + duration + " and cooldown " + cooldown);
    }

    public void TriggerFeedback(GameObject source, float intensity, float duration, float cooldown, bool isPermanent)
    {
        Debug.Log("Triggered feedback on " + source.name + " with intensity " + intensity + " and duration " + duration + " and cooldown " + cooldown + " and isPermanent " + isPermanent);
    }


    public Transform cameraTransform;
    private Vector3 originalPosition;
    public float shakeIntensity;
    public float shakeDuration;
    private float shakeTimer;

    private void Awake()
    {
        // Cache the camera's transform and original position
        cameraTransform = Camera.main.transform;
        originalPosition = cameraTransform.position;
    }
    //
    // public void TriggerFeedback(GameObject source, float intensity, float duration)
    // {
    //     shakeIntensity = intensity;
    //     shakeDuration = duration;
    //     shakeTimer = duration;
    // }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            // Apply random shake effect to the camera's position
            cameraTransform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;

            // Decrease shake timer
            shakeTimer -= Time.deltaTime;

            // Reset the camera position once shaking is done
            if (shakeTimer <= 0)
            {
                cameraTransform.position = originalPosition;
            }
        }
    }
}
