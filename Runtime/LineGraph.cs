using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StatProject
{
    public class LineGraph : MonoBehaviour
    {
        private const float dx = 0.15f;
        private const int maxNumAxisX = 5;

        private const float offsetY = 20.0f;
        private float maxCoordY;

        private static int index = 0;

        private const string str_totalMemory = "totalMemory";
        private const string str_totalGCAlloc = "totalGCAlloc";
        private const string str_gcAlloc = "gcAlloc";
        private const string str_Texture2D = "Texture2D";

        private const string str_text_totalMemory = "Total Reserved memory";
        private const string str_text_totalGCAlloc = "Total GC Allocated";
        private const string str_text_gcAlloc = "GC Allocated";
        private const string str_text_Texture2D = "Texture2D Memory";

        private static Queue<GameObject> Queue_totalMemory = new Queue<GameObject>();
        private static Queue<GameObject> Queue_totalGCAlloc = new Queue<GameObject>();
        private static Queue<GameObject> Queue_gcAlloc = new Queue<GameObject>();
        private static Queue<GameObject> Queue_Texture2D = new Queue<GameObject>();

        private Color color_totalMemory = new Vector4(0.48235294117f, 0.61960784313f, 0.01960784313f, 1.0f); // dark green
        private Color color_totalGCAlloc = new Vector4(0.55294117647f, 0.47058823529f, 0.09019607843f, 1.0f); // dark Yellow
        private Color color_Texture2D = new Vector4(0.20392156862f, 0.53333333333f, 0.65490196078f, 1.0f); // dark blue
        private Color color_gcAlloc = new Vector4(1, 0, 0, 1); // Red

        private RectTransform graphContainer;

        private void Awake()
        {
            graphContainer = this.GetComponent<RectTransform>();
        }

        private void FixedUpdate()
        {
            maxCoordY = Texts_Memory.gTotalReservedMemory + offsetY;

            // Draw Total Reserved Memory Graph
            DrawGraph(Queue_totalMemory, str_totalMemory, color_totalMemory, Texts_Memory.gTotalReservedMemory, maxCoordY);

            // Draw Total GC Allocated Graph
            DrawGraph(Queue_totalGCAlloc, str_totalGCAlloc, color_totalGCAlloc, Texts_Memory.gTotalGCAlloc, maxCoordY);

            // Draw GC Allocated Graph
            DrawGraph(Queue_gcAlloc, str_gcAlloc, color_gcAlloc, Texts_Memory.gGCAlloc, maxCoordY);

            // Draw Texture 2D Graph
            DrawGraph(Queue_Texture2D, str_Texture2D, color_Texture2D, Texts_Memory.gTexture2DMemory, maxCoordY);

            //-------------------------------------
            // Draw Total Reserved Memory Text
            DrawText(str_text_totalMemory, color_totalMemory, Texts_Memory.gTotalReservedMemory, maxCoordY);

            // Draw Total GC Allocated Text
            DrawText(str_text_totalGCAlloc, color_totalGCAlloc, Texts_Memory.gTotalGCAlloc, maxCoordY - 100.0f);

            // Draw GC Allocated Text
            DrawText(str_text_gcAlloc, color_gcAlloc, Texts_Memory.gGCAlloc, maxCoordY);

            // Draw Texture2D Text
            DrawText(str_text_Texture2D, color_Texture2D, Texts_Memory.gTexture2DMemory, maxCoordY);
        }

        public void DrawGraph(Queue<GameObject> queue, string name, Color color, float input, float maxCoordY)
        {
            DestroyGraph(queue, name);
            MovingAllElements(queue);
            AddNewElement(queue, name, color, input, maxCoordY);
            index++;
        }

        public void DestroyGraph(Queue<GameObject> queue, string name)
        {
            if (queue.Count == (int)maxNumAxisX)
            {
                index = 0; // Reset index
                DestroyImmediate(GameObject.Find(queue.Dequeue().name)); // Dequeue first element and destroy the gameObject
            }
        }

        public void MovingAllElements(Queue<GameObject> queue)
        {
            foreach (var each in queue)
            {
                if (each)
                {
                    // Moving(ReSet) Anchors
                    RectTransform rt = each.GetComponent<RectTransform>();

                    Vector2 tmp = rt.anchorMin;
                    tmp.x += dx;
                    rt.anchorMin = tmp;

                    tmp = rt.anchorMax;
                    tmp.x += dx;
                    rt.anchorMax = tmp;
                }
            }
        }

        public void AddNewElement(Queue<GameObject> queue, string name, Color color, float input, float maxCoordY)
        {
            GameObject go = new GameObject(name + index.ToString(), typeof(Image));
            go.transform.SetParent(graphContainer, false);
            go.GetComponent<Image>().color = color;

            RectTransform rtPre;
            Vector2 posPre = new Vector2(0.0f, 0.0f);

            if (queue.Count != 0)
            {
                rtPre = queue.Peek().GetComponent<RectTransform>();
                posPre = new Vector2(rtPre.anchorMin.x, rtPre.anchorMin.y);
            }

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.pivot = new Vector2(0.0f, 0.0f);
            rt.anchorMin = rt.anchorMax = new Vector2(0.0f, input / maxCoordY);
            
            Vector2 posCur = new Vector2(rt.anchorMin.x, rt.anchorMin.y);
            Vector2 dir = (posPre - posCur).normalized;
            float dist = Vector2.Distance(posPre, posCur);

            rt.localScale = new Vector3(0.6f / dist, 0.025f, 0.025f);
            rt.localEulerAngles = new Vector3(0.0f, 0.0f, Vector3.Angle(dir, new Vector2(1.0f, 0.0f)));

            queue.Enqueue(go); // Add this game object
        }

        public void DrawText(string name, Color color, float inputValue, float maxCoordY)
        {
            DestroyText(name);

            GameObject gameObject = new GameObject(name);
            gameObject.transform.SetParent(graphContainer, false);

            RectTransform rectTrans = gameObject.AddComponent<RectTransform>();
            rectTrans.pivot = new Vector2(0.5f, 0.5f);
            rectTrans.sizeDelta = new Vector2(200.0f, 100.0f);
            rectTrans.anchorMin = new Vector2(0.9f, inputValue / maxCoordY);
            rectTrans.anchorMax = new Vector2(0.9f, inputValue / maxCoordY);

            Text mtext = gameObject.AddComponent<Text>();
            mtext.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            mtext.alignment = TextAnchor.MiddleCenter;
            mtext.color = color;
            mtext.text = name + ": \n" + inputValue + " MB";
        }

        public void DestroyText(string name)
        {
            Destroy(GameObject.Find(name));
        }
    }

    public struct Point2D
    {
        private float x;
        private float y;

        public Point2D(float x_, float y_)
        {
            this.x = x_;
            this.y = y_;
        }

        public float X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public float Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
    }
}
