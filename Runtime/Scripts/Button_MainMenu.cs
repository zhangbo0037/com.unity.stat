using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_MainMenu : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject panelPerformance;
    public GameObject panelMemory;
    public GameObject buttonReturn;

    public void OnButton_Performance()
    {
        panelMainMenu.SetActive(false);
        panelPerformance.SetActive(true);
        buttonReturn.SetActive(true);
    }

    public void OnButton_Memory()
    {
        panelMainMenu.SetActive(false);
        panelMemory.SetActive(true);
        buttonReturn.SetActive(true);
    }
}
