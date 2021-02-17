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

    //private static GameObject canvasStats;

    //Object[] objs;

    void Awake()
    {
        if (Application.isMobilePlatform)
        {
            //QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;
        }


    }

    void OnEnable()
    {
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


}
