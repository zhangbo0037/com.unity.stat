using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStats : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject panelPerformance;
    public GameObject panelMemory;
    public GameObject buttonReturn;

    void Awake()
    {
        if (Application.isMobilePlatform)
        {
            //QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;
        }

        // Initialization
        panelMainMenu.SetActive(false);
        panelPerformance.SetActive(false);
        panelMemory.SetActive(false);
        buttonReturn.SetActive(false);
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
