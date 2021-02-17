using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RunInitCodes : MonoBehaviour
{
    static Object obCanvas;
    static GameObject gameObj;
    
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad() // Initial UI system
    {
        obCanvas = Resources.Load("Prefabs/Stats", typeof(GameObject));
        gameObj = Instantiate(obCanvas) as GameObject;
    }
}
