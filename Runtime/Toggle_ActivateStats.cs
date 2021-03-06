﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatProject
{
    public class Toggle_ActivateStats : MonoBehaviour
    {
        public GameObject buttonReturn;
        public GameObject panelMainMenu;
        public GameObject panelPerformance;
        public GameObject panelMemory;
        public GameObject panelMemoryGraph;
        public GameObject panelDepthTexture;
        public GameObject panelOpaqueTexture;
        public GameObject panelShadowMapTexture;

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
            if (activated)
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
            panelMemoryGraph.SetActive(false);
            panelDepthTexture.SetActive(false);
            panelOpaqueTexture.SetActive(false);
            panelShadowMapTexture.SetActive(false);
        }
    }
}