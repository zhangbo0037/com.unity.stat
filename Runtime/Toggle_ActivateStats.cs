using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_ActivateStats : MonoBehaviour
{
    public GameObject buttonReturn;
    public GameObject panelMainMenu;

    public GameObject panelPerformance;
    public GameObject panelMemory;
    public GameObject panelDepthTexture;
    public GameObject panelOpaqueTexture;

    void Awake()
    {
        if (Application.isMobilePlatform)
        {
            //QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;
        }

        //if (Camera.main.GetComponent<Camera>().depthTextureMode == DepthTextureMode.None)
        //{
        //    Camera.main.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
        //}
    }

    void OnEnable()
    {   
        SetAllUI2False(); // Initialization
    }

    public void OnTriggerEnter_ActivateStats(bool activated)
    {
        if(activated)
        {
            panelMainMenu.SetActive(true);
        }
        else
        {
            SetAllUI2False();
        }
    }

    private void SetAllUI2False()
    {
        buttonReturn.SetActive(false);
        panelMainMenu.SetActive(false);
        panelPerformance.SetActive(false);
        panelMemory.SetActive(false);
        panelDepthTexture.SetActive(false);
        panelOpaqueTexture.SetActive(false);
    }
}