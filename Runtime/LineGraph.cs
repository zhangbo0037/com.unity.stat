using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StatProject
{
    public class LineGraph : MonoBehaviour
    {
        private const float dx = 0.2f;
        private const float maxNumAxisX = 5.0f;

        private static float coordX = 0.0f;
        private const float offsetY = 20.0f;     
        private float maxCoordY;

        private const string str_totalMemory = "totalMemory";
        private const string str_totalGCAlloc = "totalGCAlloc";
        private const string str_gcAlloc = "gcAlloc";
        private const string str_Texture2D = "Texture2D";

        private const string str_text_totalMemory = "Total Reserved memory";
        private const string str_text_totalGCAlloc = "Total GC Allocated";
        private const string str_text_gcAlloc = "GC Allocated";
        private const string str_text_Texture2D = "Texture2D Memory";

        private static List<Point2D> list_totalMemory = new List<Point2D>();
        private static List<Point2D> list_totalGCAlloc = new List<Point2D>();
        private static List<Point2D> list_gcAlloc = new List<Point2D>();
        private static List<Point2D> list_Texture2D = new List<Point2D>();

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
            DrawGraph(list_totalMemory, str_totalMemory, color_totalMemory, Texts_Memory.gTotalReservedMemory, coordX, maxCoordY);

            // Draw Total GC Allocated Graph
            DrawGraph(list_totalGCAlloc, str_totalGCAlloc, color_totalGCAlloc, Texts_Memory.gTotalGCAlloc, coordX, maxCoordY);

            // Draw GC Allocated Graph
            DrawGraph(list_gcAlloc, str_gcAlloc, color_gcAlloc, Texts_Memory.gGCAlloc, coordX, maxCoordY);

            // Draw Texture 2D Graph
            DrawGraph(list_Texture2D, str_Texture2D, color_Texture2D, Texts_Memory.gTexture2DMemory, coordX, maxCoordY);

            //-------------------------------------
            // Draw Total Reserved Memory Text
            DrawText(str_text_totalMemory, color_totalMemory, Texts_Memory.gTotalReservedMemory, maxCoordY);

            // Draw Total GC Allocated Text
            DrawText(str_text_totalGCAlloc, color_totalGCAlloc, Texts_Memory.gTotalGCAlloc, maxCoordY - 300.0f);

            // Draw GC Allocated Text
            DrawText(str_text_gcAlloc, color_gcAlloc, Texts_Memory.gGCAlloc, maxCoordY);

            // Draw Texture2D Text
            DrawText(str_text_Texture2D, color_Texture2D, Texts_Memory.gTexture2DMemory, maxCoordY);

            //-------------------------------------
            // Update coordinate X
            coordX += dx;
            if (dx * maxNumAxisX - coordX <= 0.000001f)
                coordX = 0.0f;
        }

        public void DrawGraph(List<Point2D> list, string name, Color color, float input, float coordX, float maxCoordY)
        {
            DestroyGraph(list, name);
            list.Add(new Point2D(coordX, input / maxCoordY));
            DrawLine(list, name, color);
        }

        public void DrawLine(List<Point2D> pointList, string name, Color color)
        {
            for (int i = 0; i < (pointList.Count - 1); i++)
            {
                CreateLine(pointList[i], pointList[i + 1], i, name, color);
            }
        }

        public void CreateLine(Point2D pointA, Point2D pointB, int index, string name, Color color)
        {
            GameObject gameObject = new GameObject(name + index.ToString(), typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().color = color;

            Vector2 p1 = new Vector2(pointA.X, pointA.Y);
            Vector2 p2 = new Vector2(pointB.X, pointB.Y);
            Vector2 pCenter = (p1 + p2) * 0.5f;

            Vector2 dir = (p2 - p1).normalized;
            float dist = Vector2.Distance(p1, p2);

            RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
            rectTrans.pivot = new Vector2(0.5f, 0.5f);
            rectTrans.anchorMin = rectTrans.anchorMax = new Vector2(pCenter.x, pCenter.y);
            //rectTrans.localScale = new Vector3(0.2f / dist, 0.03f, 0.03f);
            rectTrans.localScale = new Vector3(0.5f, 0.03f, 0.03f);
            rectTrans.localEulerAngles = new Vector3(0.0f, 0.0f, Vector3.Angle(dir, new Vector2(1.0f, 0.0f)));
        }

        //public GameObject CreateCircle(Point2D point, int index, string name, Color color)
        //{
        //    GameObject gameObject = new GameObject(name + index.ToString(), typeof(Image));
        //    gameObject.transform.SetParent(graphContainer, false);
        //    gameObject.GetComponent<Image>().color = color;
        //    RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
        //    rectTrans.pivot = new Vector2(1.0f, 1.0f);
        //    rectTrans.anchorMin = new Vector2(point.X, point.Y);
        //    rectTrans.anchorMax = new Vector2(point.X, point.Y);
        //    rectTrans.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //    return gameObject;
        //}

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

        public void DestroyGraph(List<Point2D> list, string name)
        {
            if (list.Count == (int)maxNumAxisX)
            {
                list.RemoveAt(0);

                for (int i = 0; i < (int)maxNumAxisX; i++)
                {
                    Destroy(GameObject.Find(name + i.ToString()));
                }
            }
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
