using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;

public class Texts_Memory : MonoBehaviour
{
    public Text totalReservedMemory;
    public Text allocatedMemory;
    public Text reservedButNotAllocated;

    public Text tmpAllocator;

    public Text reservedManagedMemory;
    public Text allocatedManagedMemory;

    public Text totalTextureMemory;
    public Text totalMeshMemory;
    public Text totalMaterialMemory;
    public Text totalObjectMemory;

    //public Text allocatedMemoryGraphicsDriver;

    private long sum;
    private Object[] objs;

    private const float byte2KB = 1.0f / 1024.0f;
    private const float byte2MB = byte2KB / 1024.0f;

    void Update()
    {
        // Profiler Class:
        // https://docs.unity3d.com/ScriptReference/Profiling.Profiler.html

        // 1. The total memory Unity has reserved.
        totalReservedMemory.text = (Profiler.GetTotalReservedMemoryLong() * byte2MB) + " MB";

        // 2. The total memory allocated by the internal allocators in Unity.
        // Unity reserves large pools of memory from the system.
        // This function returns the amount of used memory in those pools.
        allocatedMemory.text = (Profiler.GetTotalAllocatedMemoryLong() * byte2MB) + " MB";

        // 3. Unity allocates memory in pools for usage when unity needs to allocate memory.
        // This function returns the amount of unused memory in these pools.
        reservedButNotAllocated.text = (Profiler.GetTotalUnusedReservedMemoryLong() * byte2MB) + " MB";

        // 4. Returns the size of the reserved space for managed-memory.
        reservedManagedMemory.text = (Profiler.GetMonoHeapSizeLong() * byte2MB) + " MB";

        // 5. The allocated managed-memory for live objects and non-collected objects.
        allocatedManagedMemory.text = (Profiler.GetMonoUsedSizeLong() * byte2MB) + " MB";

        // 6. Returns the size of the temp allocator.
        tmpAllocator.text = (Profiler.GetTempAllocatorSize() * byte2MB) + " MB";

        // 7. Gathers the native-memory used by a Unity object.
        sum = 0; // Texture
        objs = Resources.FindObjectsOfTypeAll(typeof(Texture));
        foreach (var tex in objs)
            sum += Profiler.GetRuntimeMemorySizeLong((Texture)tex);
        totalTextureMemory.text = (sum * byte2MB) + " MB";

        sum = 0; // Mesh
        objs = Resources.FindObjectsOfTypeAll(typeof(Mesh));
        foreach (var mes in objs)
            sum += Profiler.GetRuntimeMemorySizeLong((Mesh)mes);
        totalMeshMemory.text = (sum * byte2MB) + " MB";

        sum = 0; // Material
        objs = Resources.FindObjectsOfTypeAll(typeof(Material));
        foreach (var mat in objs)
            sum += Profiler.GetRuntimeMemorySizeLong((Material)mat);
        totalMaterialMemory.text = (sum * byte2MB) + " MB";

        sum = 0; // Object
        objs = Resources.FindObjectsOfTypeAll(typeof(Object));
        foreach (var obj in objs)
            sum += Profiler.GetRuntimeMemorySizeLong((Object)obj);
        totalObjectMemory.text = (sum * byte2MB) + " MB";

        // X. Returns the amount of allocated memory for the graphics driver, in bytes.
        // Only available in development players and editor.
        //allocatedMemoryGraphicsDriver.text = (Profiler.GetAllocatedMemoryForGraphicsDriver() * byte2MB) + " MB";
    }

    //private long GetTotalRuntimeMemorySizeByType<T>() where T : MonoBehaviour
    //{
    //    long sum = 0;
    //  var objs = Resources.FindObjectsOfTypeAll(typeof(T));
    //
    //    foreach (var obj in objs)
    //        sum += Profiler.GetRuntimeMemorySizeLong(GetValue<T>(obj)); // <---- Error ???
    //
    //    return sum;
    //}

    //public static T GetValue<T>(object value)
    //{
    //    return (T)Convert.ChangeType(value, typeof(T));
    //}

    //public static GameObject[] FindAll<T>() where T : MonoBehaviour
    //{
    //    MonoBehaviour[] objectT = ((MonoBehaviour[])UnityEngine.Object.FindObjectsOfType(typeof(T)));
    //    GameObject[] allObjectsT = new GameObject[objectT.Length];
    //    for (int i = 0; i < objectT.Length; i++)
    //    {
    //        allObjectsT[i] = objectT[i].gameObject;
    //   }
    //    return allObjectsT;
    //}
}