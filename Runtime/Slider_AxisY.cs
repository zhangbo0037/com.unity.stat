using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatProject
{
    public class Slider_AxisY : MonoBehaviour
    {
        public void Slider_AdjustAxisY(float input)
        {
            LineGraph.raised = input;
        }
    }
}
