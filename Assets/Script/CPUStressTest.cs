using Unity.Profiling;
using UnityEngine;

public class CPUStressTest : MonoBehaviour
{
    [Range(100000, 500000)]
    public int iterations = 500000;

    public bool stressCPU = false;

    private float result;
    private static readonly ProfilerMarker cpuMarker = new ProfilerMarker("CPU Stress Down");
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stressCPU)
        {
            return;
        }

        using (cpuMarker.Auto())
        {
            float value = 0.0f;

            for (int i = 0; i < iterations; i++)
            {
                value += Mathf.Sqrt(i) * Mathf.Sin(i);
            }

            result = value;
        }
    }
}
