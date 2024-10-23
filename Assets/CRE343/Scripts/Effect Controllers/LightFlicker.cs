using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    // Variables for controlling the flicker
    public Light flickeringLight; // Reference to the Light component (Point or Spot)
    public float minIntensity = 0.5f; // Minimum light intensity
    public float maxIntensity = 2.0f; // Maximum light intensity
    public float flickerSpeed = 1f; // How quickly the flicker happens

    private float targetIntensity;
    private float currentIntensity;

    void Start()
    {
        // Ensure a light component is attached
        if (flickeringLight == null)
        {
            flickeringLight = GetComponent<Light>();
        }

        // Set the initial intensity
        targetIntensity = flickeringLight.intensity;
        currentIntensity = targetIntensity;
    }

    void Update()
    {
        // Randomly change the target intensity between the min and max
        targetIntensity = Random.Range(minIntensity, maxIntensity);

        // Smoothly interpolate to the new intensity
        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, flickerSpeed * Time.deltaTime);

        // Apply the flickering effect
        flickeringLight.intensity = currentIntensity;
    }
}