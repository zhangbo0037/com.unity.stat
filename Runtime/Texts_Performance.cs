using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texts_Performance : MonoBehaviour
{
    public Text fpsText;
    public Text msText;

    private float deltaTime = 0.0f;
    private float currentFpsTime = 0.0f;
    private float fpsShowPeriod = 0.25f;

    void Update()
    {
        currentFpsTime += Time.deltaTime;

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        float ms = 1000.0f / fps;

        if (currentFpsTime > fpsShowPeriod)
        {
            //fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
            fpsText.text = fps.ToString() + " FPS";
            msText.text = ms.ToString() + " ms";
            currentFpsTime = 0.0f;
        }
    }
}
