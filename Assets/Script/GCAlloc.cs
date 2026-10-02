using NUnit.Framework;
using Unity.Profiling;
using UnityEngine;

public class GCAlloc : MonoBehaviour
{
    public int arraySize = 100000;

    public bool allocateMemory = false;

    private byte[] temporaryData;

    private static readonly ProfilerMarker gcMarker = new ProfilerMarker("GC Allocation Demo");


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!allocateMemory)
        {
            return;
        }

        using (gcMarker.Auto()) 
        {
            temporaryData = new byte[arraySize];
            temporaryData[0] = 1;
        }
    }
}
