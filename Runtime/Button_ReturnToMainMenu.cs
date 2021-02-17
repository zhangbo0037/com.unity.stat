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

    public void OnButton_ReturnToMainMenu()
    {
        buttonReturn.SetActive(false);

        panelPerformance.SetActive(false);
        panelMemory.SetActive(false);
        panelMainMenu.SetActive(true);
    }
}
