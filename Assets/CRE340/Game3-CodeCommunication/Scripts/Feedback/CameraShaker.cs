using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private float initialFrequency;
    private float initialAmplitude;
    private Coroutine shakeCoroutine;


    private void OnEnable(){
        FeedbackEventManager.ShakeCamera += ShakeCamera;
    }
    private void OnDisable(){
        FeedbackEventManager.ShakeCamera -= ShakeCamera;
    }

    private void Awake()
    {
        // Try to find the CinemachineVirtualCamera component in the scene
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        if (virtualCamera != null)
        {
            // Store the initial values of the noise parameters
            var noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            initialFrequency = noise.m_FrequencyGain;
            initialAmplitude = noise.m_AmplitudeGain;
        }
    }

    /// <summary>
    /// Starts a camera shake with target frequency, amplitude, and duration.
    /// </summary>
    /// <param name="targetFrequency">Peak frequency for the shake effect.</param>
    /// <param name="targetAmplitude">Peak amplitude for the shake effect.</param>
    /// <param name="duration">How long the shake effect lasts.</param>
    public void ShakeCamera(float targetFrequency, float targetAmplitude, float duration)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }
        shakeCoroutine = StartCoroutine(ShakeRoutine(targetFrequency, targetAmplitude, duration));
    }

    private IEnumerator ShakeRoutine(float targetFrequency, float targetAmplitude, float duration)
    {
        if (virtualCamera == null) yield break;

        var noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        float elapsed = 0f;

        // Increase frequency and amplitude to target values
        while (elapsed < duration / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (duration / 2);
            noise.m_FrequencyGain = Mathf.Lerp(initialFrequency, targetFrequency, t);
            noise.m_AmplitudeGain = Mathf.Lerp(initialAmplitude, targetAmplitude, t);
            yield return null;
        }

        // Reset elapsed time and lerp back down to initial values
        elapsed = 0f;

        while (elapsed < duration / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (duration / 2);
            noise.m_FrequencyGain = Mathf.Lerp(targetFrequency, initialFrequency, t);
            noise.m_AmplitudeGain = Mathf.Lerp(targetAmplitude, initialAmplitude, t);
            yield return null;
        }

        // Ensure the frequency and amplitude are reset to their initial values
        noise.m_FrequencyGain = initialFrequency;
        noise.m_AmplitudeGain = initialAmplitude;
    }
}
