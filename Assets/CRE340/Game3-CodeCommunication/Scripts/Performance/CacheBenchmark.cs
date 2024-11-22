using UnityEngine;
using System.Diagnostics;

public class CacheBenchmark : MonoBehaviour
{
    private Renderer cachedRenderer; // Cached reference for the Renderer

    void Start()
    {
        // Cache the Renderer component once for the cached loop
        cachedRenderer = GetComponent<Renderer>();

        // Run the benchmarks
        BenchmarkNonCachedLoop();
        BenchmarkCachedLoop();
    }

    public void RunBenchmarks()
    {
        BenchmarkNonCachedLoop();
        BenchmarkCachedLoop();
    }
    
    private void BenchmarkNonCachedLoop()
    {
        // Stopwatch to measure execution time
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < 100000; i++) // Increase the iterations for meaningful results
        {
            // Non-cached loop: repeatedly call GetComponent
            GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        }

        stopwatch.Stop();
        UnityEngine.Debug.Log($"Non-Cached Loop Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    private void BenchmarkCachedLoop()
    {
        // Stopwatch to measure execution time
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < 100000; i++) // Same number of iterations as non-cached
        {
            // Cached loop: use the pre-cached reference
            cachedRenderer.material.color = new Color(Random.value, Random.value, Random.value);
        }

        stopwatch.Stop();
        UnityEngine.Debug.Log($"Cached Loop Time: {stopwatch.ElapsedMilliseconds} ms");
    }
}