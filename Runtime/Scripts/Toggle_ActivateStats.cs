using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Toggle_ActivateStats : MonoBehaviour
{
    public GameObject buttonReturn;
    public GameObject panelMainMenu;
    public GameObject panelPerformance;
    public GameObject panelMemory;

    //private GameObject canvasStats;

    void Awake()
    {
        if (Application.isMobilePlatform)
        {
            //QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;
        }

        // Load asset
        LoadAssetCanvas();

        // Initialization
        buttonReturn.SetActive(false);

        panelMainMenu.SetActive(false);
        panelPerformance.SetActive(false);
        panelMemory.SetActive(false);
    }

    public void OnTriggerEnter_ActivateStats(bool activated)
    {
        if(activated)
        {
            panelMainMenu.SetActive(true);
        }
        else
        {
            panelMainMenu.SetActive(false);
            panelPerformance.SetActive(false);
            panelMemory.SetActive(false);
            buttonReturn.SetActive(false);
        }
    }

    static void LoadAssetCanvas()
    {
        // https://docs.unity3d.com/ScriptReference/AssetDatabase.LoadAssetAtPath.html

        //Resources.Load("Packages/com.unity.stat/Runtime/Prefabs/Canvas_Stats.prefab") as GameObject;
        //Instantiate(Resources.Load("Packages/com.unity.stat/Runtime/Prefabs/Canvas_Stats.prefab")) as GameObject;
        //var canvasStats = (GameObject)AssetDatabase.LoadAssetAtPath("Packages/com.unity.stat/Runtime/Prefabs/Canvas_Stats.prefab", typeof(GameObject));
    }
}
