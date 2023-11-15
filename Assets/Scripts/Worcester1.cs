using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Worcester1 : MonoBehaviour
{
    public GUIStyle pixelStyle;
    public GUIStyle btnStyle;

    private Vector2 XminYmin = new Vector2(0.0f, 0.0f);
    private Vector2 XmaxYmax = new Vector2(0.0f, 0.0f);
    private int rotationValue;
    private bool guiSwitch;
    private string packageName;
    private int objectCount = 0;  // 新增物體編號變數
    private string className = "bottle";  // 類別名稱
    private static string fileName = "BoundingBoxes.txt";
    //private List<Rect> boundingBoxes = new List<Rect>();

    private void Start()
    {
        XminYmin = new Vector2(0.0f, 0.0f);
        XmaxYmax = new Vector2(0.0f, 0.0f);
        rotationValue = 0;
        guiSwitch = true;
        packageName = "com.rvl224.ZZZ";
    }

    private void OnGUI()
    {
        if (guiSwitch)
        {
            GUI.Box(Bbox(), "");

            GUI.Label(new Rect(XminYmin.x, XminYmin.y - 60.0f, 100.0f, 60.0f), XminYmin.ToString("0"), pixelStyle);
            GUI.Label(new Rect(XmaxYmax.x - 100.0f, XmaxYmax.y, 100.0f, 60.0f), XmaxYmax.ToString("0"), pixelStyle);
            GUI.Label(new Rect(260.0f, 40.0f, 120.0f, 90.0f), rotationValue.ToString("0"), btnStyle);

            if (GUI.Button(new Rect(Screen.width - 200.0f, 200.0f, 180.0f, 120.0f), "Shot", btnStyle))
            {
                Shot();
            }

            if (GUI.Button(new Rect(Screen.width - 140.0f, 400.0f, 120.0f, 90.0f), "+90", btnStyle))
            {
                rotationValue += 90;
            }

            if (GUI.Button(new Rect(Screen.width - 140.0f, 530.0f, 120.0f, 90.0f), "-90", btnStyle))
            {
                rotationValue -= 90;
            }

            transform.rotation = Quaternion.Euler(0, rotationValue, 0);
        }
    }
    private Rect Bbox()
    {
        Vector3 lid_center = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.center;
        Vector3 lid_extents = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.extents;

        Vector3 bottle_center = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().bounds.center;
        Vector3 bottle_extents = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().bounds.extents;

        Vector3[] vertices3d = new Vector3[16]
        {
            new Vector3(lid_center.x + lid_extents.x, lid_center.y + lid_extents.y, lid_center.z + lid_extents.z),
            new Vector3(lid_center.x + lid_extents.x, lid_center.y + lid_extents.y, lid_center.z - lid_extents.z),
            new Vector3(lid_center.x + lid_extents.x, lid_center.y - lid_extents.y, lid_center.z + lid_extents.z),
            new Vector3(lid_center.x + lid_extents.x, lid_center.y - lid_extents.y, lid_center.z - lid_extents.z),
            new Vector3(lid_center.x - lid_extents.x, lid_center.y + lid_extents.y, lid_center.z + lid_extents.z),
            new Vector3(lid_center.x - lid_extents.x, lid_center.y + lid_extents.y, lid_center.z - lid_extents.z),
            new Vector3(lid_center.x - lid_extents.x, lid_center.y - lid_extents.y, lid_center.z + lid_extents.z),
            new Vector3(lid_center.x - lid_extents.x, lid_center.y - lid_extents.y, lid_center.z - lid_extents.z),

            new Vector3(bottle_center.x + bottle_extents.x, bottle_center.y + bottle_extents.y, bottle_center.z + bottle_extents.z),
            new Vector3(bottle_center.x + bottle_extents.x, bottle_center.y + bottle_extents.y, bottle_center.z - bottle_extents.z),
            new Vector3(bottle_center.x + bottle_extents.x, bottle_center.y - bottle_extents.y, bottle_center.z + bottle_extents.z),
            new Vector3(bottle_center.x + bottle_extents.x, bottle_center.y - bottle_extents.y, bottle_center.z - bottle_extents.z),
            new Vector3(bottle_center.x - bottle_extents.x, bottle_center.y + bottle_extents.y, bottle_center.z + bottle_extents.z),
            new Vector3(bottle_center.x - bottle_extents.x, bottle_center.y + bottle_extents.y, bottle_center.z - bottle_extents.z),
            new Vector3(bottle_center.x - bottle_extents.x, bottle_center.y - bottle_extents.y, bottle_center.z + bottle_extents.z),
            new Vector3(bottle_center.x - bottle_extents.x, bottle_center.y - bottle_extents.y, bottle_center.z - bottle_extents.z)
        };
        Vector2[] vertices2d = new Vector2[16];

        for (int i = 0; i < 16; i++)
        {
            vertices2d[i] = Camera.main.WorldToScreenPoint(vertices3d[i]);
            vertices2d[i].y = Screen.height - vertices2d[i].y;
        }

        Vector2 min = vertices2d[0];
        Vector2 max = vertices2d[0];

        foreach (Vector2 V2 in vertices2d)
        {
            min = Vector2.Min(V2, min);
            max = Vector2.Max(V2, max);
        }

        XminYmin = min;
        XmaxYmax = max;

        return new Rect(XminYmin.x, XminYmin.y, XmaxYmax.x - XminYmin.x, XmaxYmax.y - XminYmin.y);
    }
    private List<Rect> Bboxes()
    {
        List<Transform> children = new List<Transform>();
        List<Rect> boundingBoxes = new List<Rect>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 lid_center = transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().bounds.center;
            Vector3 lid_extents = transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().bounds.extents;
            i++;
            Vector3 bottle_center = transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().bounds.center;
            Vector3 bottle_extents = transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().bounds.extents;
            Vector3[] vertices3d = new Vector3[16]
       {
            new Vector3(lid_center.x + lid_extents.x, lid_center.y + lid_extents.y, lid_center.z + lid_extents.z),
            new Vector3(lid_center.x + lid_extents.x, lid_center.y + lid_extents.y, lid_center.z - lid_extents.z),
            new Vector3(lid_center.x + lid_extents.x, lid_center.y - lid_extents.y, lid_center.z + lid_extents.z),
            new Vector3(lid_center.x + lid_extents.x, lid_center.y - lid_extents.y, lid_center.z - lid_extents.z),
            new Vector3(lid_center.x - lid_extents.x, lid_center.y + lid_extents.y, lid_center.z + lid_extents.z),
            new Vector3(lid_center.x - lid_extents.x, lid_center.y + lid_extents.y, lid_center.z - lid_extents.z),
            new Vector3(lid_center.x - lid_extents.x, lid_center.y - lid_extents.y, lid_center.z + lid_extents.z),
            new Vector3(lid_center.x - lid_extents.x, lid_center.y - lid_extents.y, lid_center.z - lid_extents.z),

            new Vector3(bottle_center.x + bottle_extents.x, bottle_center.y + bottle_extents.y, bottle_center.z + bottle_extents.z),
            new Vector3(bottle_center.x + bottle_extents.x, bottle_center.y + bottle_extents.y, bottle_center.z - bottle_extents.z),
            new Vector3(bottle_center.x + bottle_extents.x, bottle_center.y - bottle_extents.y, bottle_center.z + bottle_extents.z),
            new Vector3(bottle_center.x + bottle_extents.x, bottle_center.y - bottle_extents.y, bottle_center.z - bottle_extents.z),
            new Vector3(bottle_center.x - bottle_extents.x, bottle_center.y + bottle_extents.y, bottle_center.z + bottle_extents.z),
            new Vector3(bottle_center.x - bottle_extents.x, bottle_center.y + bottle_extents.y, bottle_center.z - bottle_extents.z),
            new Vector3(bottle_center.x - bottle_extents.x, bottle_center.y - bottle_extents.y, bottle_center.z + bottle_extents.z),
            new Vector3(bottle_center.x - bottle_extents.x, bottle_center.y - bottle_extents.y, bottle_center.z - bottle_extents.z)
       };
            Vector2[] vertices2d = new Vector2[16];

            for (int j = 0; j < 16; j++)
            {
                vertices2d[j] = Camera.main.WorldToScreenPoint(vertices3d[j]);
                vertices2d[j].y = Screen.height - vertices2d[j].y;
            }

            Vector2 min = vertices2d[0];
            Vector2 max = vertices2d[0];

            foreach (Vector2 V2 in vertices2d)
            {
                min = Vector2.Min(V2, min);
                max = Vector2.Max(V2, max);
            }

            XminYmin = min;
            XmaxYmax = max;
            Rect temp = new Rect(XminYmin.x, XminYmin.y, XmaxYmax.x - XminYmin.x, XmaxYmax.y - XminYmin.y);
            boundingBoxes.Add(temp);
        }
        return boundingBoxes;
    }

    private void Shot()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        string fileTime = System.DateTime.Now.ToString("MMdd_HH:mm:ss");

        SnapshotGUI(fileTime);
        yield return new WaitForEndOfFrame();

        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        guiSwitch = false;
        yield return new WaitForEndOfFrame();
        List<Rect> boundingBoxes = Bboxes();
        //boundingBoxes.Add(boundingBox);
        SnapshotNoGUI(fileTime, boundingBoxes);
        yield return new WaitForEndOfFrame();
        //boundingBox = Camera.main.pixelRect;
        //SaveBoundingBoxesToFile(boundingBox);
        //yield return new WaitForEndOfFrame();
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
        guiSwitch = false;
        //yield return new WaitForEndOfFrame();
       /* if (boundingBoxes.Count > 1)
        {
            SaveBoundingBoxesToFile();
        }*/
    }
    private void SnapshotGUI(string _fileTime)
    {
        ScreenCapture.CaptureScreenshot($"{_fileTime}G.jpg");
    }

    private void SnapshotNoGUI(string _fileTime, List<Rect> boundingBoxes)
    {
        ScreenCapture.CaptureScreenshot($"{_fileTime}.jpg");

        float Xmin = XminYmin.x;
        float Ymin = XminYmin.y;
        float Xmax = XmaxYmax.x;
        float Ymax = XmaxYmax.y;

        /*float standardXmin = XminYmin.x / Screen.width;
        float standardYmin = XminYmin.y / Screen.height;
        float standardXmax = XmaxYmax.x / Screen.width;
        float standardYmax = XmaxYmax.y / Screen.height;*/
        
        using (StreamWriter writer = new StreamWriter($"/storage/emulated/0/Android/data/{packageName}/VOC_txt/result.txt", true))
        //using (StreamWriter writer = new StreamWriter($"/storage/emulated/0/Android/data/{packageName}/VOC_txt/{_fileTime}.txt", true))
        {
            writer.WriteLine($"{_fileTime}");
            foreach (Rect boundingBox in boundingBoxes)
            {
                //writer.WriteLine($"{_fileTime}");
                //writer.WriteLine($"{className} {Xmin.ToString("0")} {Ymin.ToString("0")} {Xmax.ToString("0")} {Ymax.ToString("0")}");
                // writer.WriteLine($"{className} {standardXmin.ToString("0.000000")} {standardYmin.ToString("0.000000")} {standardXmax.ToString("0.000000")} {standardYmax.ToString("0.000000")}");
                writer.WriteLine($"{className} {boundingBox.xMin.ToString("0")} {boundingBox.yMin.ToString("0")} {boundingBox.xMax.ToString("0")} {boundingBox.yMax.ToString("0")}");
            }
            
        }

        //objectCount++;
    }
    //private void SaveBoundingBoxesToFile(Rect box)
    //{
    //    using (StreamWriter writer = new StreamWriter($"/storage/emulated/0/Android/data/{packageName}/VOC_txt/{fileName}", true))
    //    {
    //       // foreach (Rect box in boundingBoxes)
    //        //{
    //            writer.WriteLine($"{box.xMin.ToString("0")} {box.yMin.ToString("0")} {box.xMax.ToString("0")} {box.yMax.ToString("0")}");
    //        //}
    //    }
    //    boundingBoxes.Clear();  // 清空列表
    //}
}
