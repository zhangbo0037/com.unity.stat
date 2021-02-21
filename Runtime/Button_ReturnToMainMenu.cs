using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The icon image is from:
// https://www.freeiconspng.com/downloadimg/34573

public class Button_ReturnToMainMenu : MonoBehaviour
{
    public GameObject buttonReturn;
    public GameObject panelMainMenu;
    public GameObject panelPerformance;
    public GameObject panelMemory;
    public GameObject panelDepthTexture;
    public GameObject panelOpaqueTexture;
    public GameObject panelShadowMapTexture;

    public void OnButton_ReturnToMainMenu()
    {
        panelMainMenu.SetActive(true);
        SetUI2FalseExceptMainMenu();
    }

    private void SetUI2FalseExceptMainMenu()
    {
        buttonReturn.SetActive(false);
        panelPerformance.SetActive(false);
        panelMemory.SetActive(false);
        panelDepthTexture.SetActive(false);
        panelOpaqueTexture.SetActive(false);
        panelShadowMapTexture.SetActive(false);
    }
}
