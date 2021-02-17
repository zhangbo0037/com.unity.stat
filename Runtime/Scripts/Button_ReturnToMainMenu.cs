using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
