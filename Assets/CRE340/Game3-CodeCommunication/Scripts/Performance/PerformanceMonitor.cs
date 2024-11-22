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
                $"FPS: {fps:F2} \nAvg FPS: {avgFps:F2}\n" +
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
        UnityEngine.Debug.Log("Starting benchmark...");
        stopwatch.Reset();
        stopwatch.Start();

        PerformHeavyCalculation();

        stopwatch.Stop();
        UnityEngine.Debug.Log($"Benchmark completed. Execution Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    private void PerformHeavyCalculation()
    {
        int[] numbers = new int[10000000];
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = Random.Range(0, 10000); // Fill the array with random numbers

            // Log progress at every 100,000 iterations
            if (i % 100000 == 0)
            {
                UnityEngine.Debug.Log($"Processing... {i} iterations completed.");
            }
        }

        // Sort the array as part of the heavy calculation
        System.Array.Sort(numbers);
        UnityEngine.Debug.Log("Sorting completed.");
    }
}
