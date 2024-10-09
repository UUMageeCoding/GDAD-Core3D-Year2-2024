using System.Collections;
using UnityEngine;

public class EventReceiver : MonoBehaviour
{
    private Vector3 originalScale;

    private void OnEnable()
    {
        // Subscribe to the OnFire event
        EventSender.OnFire += HandleFireEvent;
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnFire event to avoid memory leaks
        EventSender.OnFire -= HandleFireEvent;
    }

    private void Start()
    {
        // Store the original scale of the object
        originalScale = transform.localScale;
    }

    // Method that handles the OnFire event
    private void HandleFireEvent(float scale, float speed)
    {
        // Start a coroutine to scale the object up and back down
        StartCoroutine(ScaleObject(scale, speed));
    }

    // Coroutine to scale the object using Lerp
    private IEnumerator ScaleObject(float targetScale, float speed)
    {
        Vector3 targetSize = originalScale * targetScale;
        float elapsedTime = 0f;
        float duration = 1f / speed; // Calculate duration based on speed

        // Scale up
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is exact
        transform.localScale = targetSize;

        // Reset elapsed time for scaling back
        elapsedTime = 0f;

        // Scale back down
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(targetSize, originalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the scale is reset to the original size
        transform.localScale = originalScale;
    }
}