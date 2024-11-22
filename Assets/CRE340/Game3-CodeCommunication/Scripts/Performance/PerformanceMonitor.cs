using UnityEngine;
using TMPro;
using System.Diagnostics;

public class PerformanceMonitor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI performanceText; // Assign a TextMeshProUGUI component in the Inspector

    private float deltaTime = 0.0f;
    private int frameCount = 0;
    private float timeElapsed = 0.0f;
    private float totalTime = 0.0f;
    private int totalFrames = 0;

    private Stopwatch stopwatch;

    void Start()
    {
        stopwatch = new Stopwatch();
    }

    void Update()
    {
        // FPS Calculation
        deltaTime += Time.unscaledDeltaTime;
        timeElapsed += Time.unscaledDeltaTime;
        frameCount++;
        totalFrames++;

        if (deltaTime >= 1.0f)
        {
            float fps = frameCount / deltaTime;
            float avgFps = totalFrames / totalTime;

            // Memory Usage
            long memoryUsed = System.GC.GetTotalMemory(false); // In bytes

            // Frame Times
            float cpuFrameTime = Time.deltaTime * 1000f; // CPU frame time in milliseconds
            float gpuFrameTime = Time.unscaledDeltaTime * 1000f; // Estimated GPU frame time

            // Display Metrics
            performanceText.text =
                $"FPS: {fps:F2} | Avg FPS: {avgFps:F2}\n" +
                $"CPU Frame Time: {cpuFrameTime:F2} ms \nGPU Frame Time: {gpuFrameTime:F2} ms\n" +
                $"Memory: {memoryUsed / (1024f * 1024f):F2} MB";

            // Reset for the next interval
            deltaTime = 0.0f;
            frameCount = 0;
        }

        totalTime += Time.unscaledDeltaTime;
    }

    public void BenchmarkMethod()
    {
        stopwatch.Reset();
        stopwatch.Start();

        // Example code to benchmark
        PerformHeavyCalculation();

        stopwatch.Stop();
        UnityEngine.Debug.Log($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    private void PerformHeavyCalculation()
    {
        // Example of heavy computation
        for (int i = 0; i < 1000000; i++)
        {
            Mathf.Sqrt(i);
        }
    }
}
