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
        gameObj = Instantiate(obCanvas) as GameObject;
    }

    // Client.Remove("com.unity.stat");

#endif
    }
}
