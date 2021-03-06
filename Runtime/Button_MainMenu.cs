﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatProject
{
    public class Button_MainMenu : MonoBehaviour
    {
        public GameObject buttonReturn;
        public GameObject panelMainMenu;
        public GameObject panelPerformance;
        public GameObject panelMemory;
        public GameObject panelMemoryGraph;
        public GameObject panelDepthTexture;
        public GameObject panelOpaqueTexture;
        public GameObject panelShadowMapTexture;

        public void OnButton_Performance()
        {
            SetUIStates(panelPerformance);
        }

        public void OnButton_Memory()
        {
            SetUIStates(panelMemory);
        }

        public void OnButton_MemoryGraph()
        {
            SetUIStates(panelMemory);
            SetUIStates(panelMemoryGraph);
        }

        public void OnButton_DepthTexture()
        {
            SetUIStates(panelDepthTexture);
        }

        public void OnButton_OpaqueTexture()
        {
            SetUIStates(panelOpaqueTexture);
        }

        public void OnButton_ShadowMapTexture()
        {
            SetUIStates(panelShadowMapTexture);
        }

        private void SetUIStates(GameObject inputGO)
        {
            panelMainMenu.SetActive(false);
            buttonReturn.SetActive(true);
            inputGO.SetActive(true);
        }
    }
}
