using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.PackageManager;

namespace StatProject
{
    class RunInitCodes : MonoBehaviour
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD

    static Object obCanvas;
    static GameObject gameObj;

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad() // Initial UI system
    {
        obCanvas = Resources.Load("Prefabs/Stats", typeof(GameObject));
        if(obCanvas)
        {
            gameObj = Instantiate(obCanvas) as GameObject;
        }
        else
        {
            Debug.LogError("Can not load UI Prefabs! Please check Script file 'RunInitCodes.cs' to fix it");
        }
    }

    // Client.Remove("com.unity.stat");

#endif
    }
}
