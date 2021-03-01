using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StatProject
{
    public class LineGraph : MonoBehaviour
    {
        private static int index = 0;

        private const float dx = 0.15f;
        private const float raised = 0.995f;
        private const int maxNumAxisX = 5;

        private const string str_totalReserved = "Total Reserved Memory";
        private const string str_totalAllocated = "Total Allocated Memory";
        private const string str_tmpAllocator = "Tmp Allocator Memory";
        private const string str_allocatedGfxDriver = "Allocated for GfxDriver";
        private const string str_texture2D = "Total Texture2D Memory";
        private const string str_mesh = "Total Mesh Memory";
        private const string str_totalGCAlloc = "Total GC Allocated";
        private const string str_gcAlloc = "GC Allocated";

        private static Queue<GameObject> queue_totalReserved = new Queue<GameObject>();
        private static Queue<GameObject> queue_totalAllocated = new Queue<GameObject>();
        private static Queue<GameObject> queue_tmpAllocator = new Queue<GameObject>();
        private static Queue<GameObject> queue_allocatedGfxDriver = new Queue<GameObject>();
        private static Queue<GameObject> queue_texture2D = new Queue<GameObject>();
        private static Queue<GameObject> queue_mesh = new Queue<GameObject>();
        private static Queue<GameObject> queue_totalGCAlloc = new Queue<GameObject>();
        private static Queue<GameObject> queue_gcAlloc = new Queue<GameObject>();

        private Color color_totalReserved = new Vector4(0.0f, 1.0f, 0.0f, 1.0f); // green
        private Color color_totalAllocated = new Vector4(0.482352f, 0.619607f, 0.019607f, 1.0f); // dark green
        private Color color_tmpAllocator = new Vector4(1.0f, 0.0f, 1.0f, 1.0f); // Purple
        private Color color_allocatedGfxDriver = new Vector4(0.0f, 1.0f, 1.0f, 1.0f); // yan
        private Color color_totalGCAlloc = new Vector4(1.0f, 1.0f, 0.0f, 1.0f); // Yello
        private Color color_gcAlloc = new Vector4(0.552941f, 0.470588f, 0.090196f, 1.0f); // dark Yellow
        private Color color_texture2D = new Vector4(0.203921f, 0.533333f, 0.654901f, 1.0f); // dark blue
        private Color color_mesh = new Vector4(1.0f, 0.549019f, 0.0f, 1.0f); // Orange

        private RectTransform graphContainer;

        private void Awake()
        {
            graphContainer = this.GetComponent<RectTransform>();
        }

        private void FixedUpdate()
        {
            // 1. Draw Total Reserved Memory Graph
            DrawGraph(queue_totalReserved, str_totalReserved, color_totalReserved, Texts_Memory.gTotalReservedMemory);
            DrawText(str_totalReserved, color_totalReserved, Texts_Memory.gTotalReservedMemory);

            // 2. Draw Total Allocated Memory Graph
            DrawGraph(queue_totalAllocated, str_totalAllocated, color_totalAllocated, Texts_Memory.gTotalAllocatedMemory);
            DrawText(str_totalAllocated, color_totalAllocated, Texts_Memory.gTotalAllocatedMemory);

            // 3. Draw Tmp Allocator Graph
            DrawGraph(queue_tmpAllocator, str_tmpAllocator, color_tmpAllocator, Texts_Memory.gtmpAllocator);
            DrawText(str_tmpAllocator, color_tmpAllocator, Texts_Memory.gtmpAllocator);

            // 4. Draw Allocated GfxDriver Graph
            DrawGraph(queue_allocatedGfxDriver, str_allocatedGfxDriver, color_allocatedGfxDriver, Texts_Memory.gAllocatedGfxDriver);
            DrawText(str_allocatedGfxDriver, color_allocatedGfxDriver, Texts_Memory.gAllocatedGfxDriver);

            // 5. Draw Texture 2D Graph
            DrawGraph(queue_texture2D, str_texture2D, color_texture2D, Texts_Memory.gTexture2DMemory);
            DrawText(str_texture2D, color_texture2D, Texts_Memory.gTexture2DMemory);

            // 6. Draw Mesh
            DrawGraph(queue_mesh, str_mesh, color_mesh, Texts_Memory.gMeshMemory);
            DrawText(str_mesh, color_mesh, Texts_Memory.gMeshMemory);

            // 7. Draw Total Allocated GC Graph
            DrawGraph(queue_totalGCAlloc, str_totalGCAlloc, color_totalGCAlloc, Texts_Memory.gTotalGCAlloc);
            DrawText(str_totalGCAlloc, color_totalGCAlloc, Texts_Memory.gTotalGCAlloc);

            // 8. Draw Allocated GC  Graph
            DrawGraph(queue_gcAlloc, str_gcAlloc, color_gcAlloc, Texts_Memory.gGCAlloc);
            DrawText(str_gcAlloc, color_gcAlloc, Texts_Memory.gGCAlloc);
        }

        //----------------------------------------
        // Methods
        //----------------------------------------
        public void DrawGraph(Queue<GameObject> queue, string name, Color color, float memorySize)
        {
            DestroyGraph(queue, name);
            MovingAllItems(queue);
            AddNewItem(queue, name, color, memorySize);
            index++;
        }

        public void DrawText(string name, Color color, float memorySize)
        {
            DestroyText(name);

            GameObject gameObject = new GameObject(name);
            gameObject.transform.SetParent(graphContainer, false);

            RectTransform rectTrans = gameObject.AddComponent<RectTransform>();
            rectTrans.pivot = new Vector2(0.5f, 0.5f);
            rectTrans.sizeDelta = new Vector2(200.0f, 100.0f);

            float axisY = GetAxisYCoord(memorySize);
            rectTrans.anchorMin = rectTrans.anchorMax = new Vector2(0.9f, axisY);

            Text mtext = gameObject.AddComponent<Text>();
            mtext.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            mtext.alignment = TextAnchor.MiddleCenter;
            mtext.color = color;
            mtext.text = name + ": \n" + memorySize + " MB";
        }

        public void AddNewItem(Queue<GameObject> queue, string name, Color color, float memorySize)
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

            // The new Item is still Draw in coordinate 0.0f in axis X.
            float axisY = GetAxisYCoord(memorySize);
            rt.anchorMin = rt.anchorMax = new Vector2(0.0f, axisY);

            Vector2 posCur = new Vector2(rt.anchorMin.x, rt.anchorMin.y);
            Vector2 dir = (posPre - posCur).normalized;
            float dist = Vector2.Distance(posPre, posCur);

            rt.localScale = new Vector3(0.525f / dist, 0.02f, 0.02f);
            rt.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Pow(Vector3.Angle(dir, new Vector2(1.0f, 0.0f)), 1.5f));

            queue.Enqueue(go); // Add this game object
        }

        public void MovingAllItems(Queue<GameObject> queue)
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

        public void DestroyGraph(Queue<GameObject> queue, string name)
        {
            if (queue.Count == (int)maxNumAxisX)
            {
                index = 0; // Reset index
                DestroyImmediate(GameObject.Find(queue.Dequeue().name)); // Dequeue first element and destroy the gameObject
            }
        }

        public void DestroyText(string name)
        {
            Destroy(GameObject.Find(name));
        }

        public float GetAxisYCoord(float power)
        {
            return 1.0f - Mathf.Pow(raised, power);
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

    class MyQueue<T>
    {
        private readonly Queue<T> _inner = new Queue<T>();

        public int Count
        {
            get { return _inner.Count; }
        }

        public T Last { get; private set; }

        public void Enqueue(T item)
        {
            _inner.Enqueue(item);
            Last = item; // < --- record the last item
        }

        public void Dequeue(T item)
        {
            _inner.Dequeue();
        }
    }
}
