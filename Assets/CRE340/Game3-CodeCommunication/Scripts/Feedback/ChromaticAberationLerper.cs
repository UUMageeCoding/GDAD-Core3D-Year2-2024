using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChromaticAberrationEffect : MonoBehaviour
{
    private ChromaticAberration chromaticAberration;
    private float initialAberration;
    private Coroutine aberrationCoroutine;

    private void OnEnable(){
        FeedbackEventManager.ChromaticAberrationLerp += LerpAberrationEffect;
    }
    private void OnDisable(){
        FeedbackEventManager.ChromaticAberrationLerp -= LerpAberrationEffect;
    }

    private void Awake()
    {
        // Access the Volume component attached to this GameObject
        Volume volume = GetComponent<Volume>();

        // Try to get Chromatic Aberration from the Volume Profile
        if (volume.profile.TryGet(out chromaticAberration))
        {
            initialAberration = chromaticAberration.intensity.value; // Store the initial value of the effect
        }
        else
        {
            Debug.LogWarning("No Chromatic Aberration effect found in the Volume Profile.");
        }
    }

    /// <summary>
    /// Starts the chromatic aberration effect with a target intensity and duration.
    /// </summary>
    /// <param name="targetIntensity">The peak intensity for the chromatic aberration effect.</param>
    /// <param name="duration">How long the effect lasts.</param>
    public void LerpAberrationEffect(float targetIntensity, float duration)
    {
        if (aberrationCoroutine != null)
        {
            StopCoroutine(aberrationCoroutine);
        }
        aberrationCoroutine = StartCoroutine(AberrationRoutine(targetIntensity, duration));
    }

    private IEnumerator AberrationRoutine(float targetIntensity, float duration)
    {
        if (chromaticAberration == null) yield break;

        float elapsed = 0f;

        // Increase intensity to target value
        while (elapsed < duration / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (duration / 2);
            chromaticAberration.intensity.value = Mathf.Lerp(initialAberration, targetIntensity, t);
            yield return null;
        }

        // Reset elapsed time and lerp back down to initial value
        elapsed = 0f;

        while (elapsed < duration / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (duration / 2);
            chromaticAberration.intensity.value = Mathf.Lerp(targetIntensity, initialAberration, t);
            yield return null;
        }

        // Ensure the chromatic aberration is reset to its initial value
        chromaticAberration.intensity.value = initialAberration;
    }
}
