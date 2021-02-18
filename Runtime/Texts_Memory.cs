using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;

public class Texts_Memory : MonoBehaviour
{
    private const float byte2KB = 1.0f / 1024.0f;
    private const float byte2MB = byte2KB / 1024.0f;

    public Text totalReservedMemory;
    public Text allocatedMemory;
    public Text reservedButNotAllocated;

    void Update()
    {
        totalReservedMemory.text = (Profiler.GetTotalReservedMemoryLong() * byte2MB) + " MB";
        allocatedMemory.text = (Profiler.GetTotalAllocatedMemoryLong() * byte2MB) + " MB";
        reservedButNotAllocated.text = (Profiler.GetTotalUnusedReservedMemoryLong() * byte2MB) + " MB";
    }
}