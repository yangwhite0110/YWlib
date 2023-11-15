using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Bottle_0 : MonoBehaviour
{
    public GUIStyle ULStyle;
    public GUIStyle LRStyle;
    public GUIStyle btnShotStyle;
    public GUIStyle btnRotationInStyle;
    public GUIStyle btnRotationDeStyle;
    public GUIStyle rotationValueStyle;

    private GameObject rectInfo;

    private Vector2 upperLeft;
    private Vector2 lowerRight;

    private float rectWidth;
    private float rectHeight;
    private float rotationValue;

    private bool guiSwitch;

    private string packageName;

    private void Start()
    {
        rectInfo = GameObject.Find("Rect Info");
        upperLeft = new Vector2(0.0f, 0.0f);
        lowerRight = new Vector2(0.0f, 0.0f);
        rectWidth = 0.0f;
        rectHeight = 0.0f;
        rotationValue = 0.0f;
        guiSwitch = true;
        packageName = "com.Yuuu.bottleO";
    }

    private void OnGUI()
    {
        if (guiSwitch)
        {
            GUI.Box(MRRect(), "");
            GUI.Label(new Rect(upperLeft.x, upperLeft.y - 60.0f, 100.0f, 60.0f), upperLeft.ToString("0.00"), ULStyle);
            GUI.Label(new Rect(lowerRight.x - 100.0f, lowerRight.y, 100.0f, 60.0f), lowerRight.ToString("0.00"), LRStyle);

            if (GUI.Button(new Rect(Screen.width - 200.0f, 200.0f, 180.0f, 120.0f), "Shot", btnShotStyle))
            {
                Shot();
            }

            GUI.Label(new Rect(260.0f, 40.0f, 120.0f, 90.0f), rotationValue.ToString("0.0"), rotationValueStyle);

            if (GUI.Button(new Rect(Screen.width - 140.0f, 400.0f, 120.0f, 90.0f), "+45", btnRotationInStyle))
            {
                rotationValue += 45.0f;
            }
            if (GUI.Button(new Rect(Screen.width - 140.0f, 530.0f, 120.0f, 90.0f), "-45", btnRotationDeStyle))
            {
                rotationValue -= 45.0f;
            }
            transform.rotation = Quaternion.Euler(0, rotationValue, 0);

        }
    }

    private Rect MRRect()
    {
        // MeshFilter unvalid!

        Vector3 oliveoil_center  = GetComponent<MeshCollider>().bounds.center;
        Vector3 oliveoil_extents = GetComponent<MeshCollider>().bounds.extents;

        Vector3[] vertices3d = new Vector3[8]
        {
            new Vector3(oliveoil_center.x + oliveoil_extents.x, oliveoil_center.y + oliveoil_extents.y, oliveoil_center.z + oliveoil_extents.z),
            new Vector3(oliveoil_center.x + oliveoil_extents.x, oliveoil_center.y + oliveoil_extents.y, oliveoil_center.z - oliveoil_extents.z),
            new Vector3(oliveoil_center.x + oliveoil_extents.x, oliveoil_center.y - oliveoil_extents.y, oliveoil_center.z + oliveoil_extents.z),
            new Vector3(oliveoil_center.x + oliveoil_extents.x, oliveoil_center.y - oliveoil_extents.y, oliveoil_center.z - oliveoil_extents.z),
            new Vector3(oliveoil_center.x - oliveoil_extents.x, oliveoil_center.y + oliveoil_extents.y, oliveoil_center.z + oliveoil_extents.z),
            new Vector3(oliveoil_center.x - oliveoil_extents.x, oliveoil_center.y + oliveoil_extents.y, oliveoil_center.z - oliveoil_extents.z),
            new Vector3(oliveoil_center.x - oliveoil_extents.x, oliveoil_center.y - oliveoil_extents.y, oliveoil_center.z + oliveoil_extents.z),
            new Vector3(oliveoil_center.x - oliveoil_extents.x, oliveoil_center.y - oliveoil_extents.y, oliveoil_center.z - oliveoil_extents.z)
        };
        Vector2[] vertices2d = new Vector2[8];

        // Vector3 ---> Vector2 & fix y
        for (int i = 0; i < 8; i++)
        {
            vertices2d[i] = Camera.main.WorldToScreenPoint(vertices3d[i]);
            vertices2d[i].y = Screen.height - vertices2d[i].y;
        }

        // find the min & the max
        Vector2 min = vertices2d[0];
        Vector2 max = vertices2d[0];

        foreach (Vector2 V2 in vertices2d)
        {
            min = Vector2.Min(V2, min);
            max = Vector2.Max(V2, max);
        }

        // frame vertices
        upperLeft.x = min.x;
        upperLeft.y = min.y;

        lowerRight.x = max.x;
        lowerRight.y = max.y;
        // frame vertices done

        // the width and the height of the rect
        rectWidth = max.x - min.x;
        rectHeight = max.y - min.y;

        rectInfo.GetComponent<Text>().text = $"Rect Width: {rectWidth.ToString("0.00000")}, Rect Height: {rectHeight.ToString("0.00000")}";

        return new Rect(min.x, min.y, rectWidth, rectHeight);
    }

    private void Shot()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        string fileTime = System.DateTime.Now.ToString("MMdd_HH:mm:ss");

        // data with GUI
        SnapshotGUI(fileTime);
        yield return new WaitForEndOfFrame();

        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        guiSwitch = false;
        yield return new WaitForEndOfFrame();

        // data without GUI
        SnapshotNoGUI(fileTime);
        yield return new WaitForEndOfFrame();

        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
        guiSwitch = true;
        yield return new WaitForEndOfFrame();

    }

    private void SnapshotGUI(string _fileTime)
    {
        ScreenCapture.CaptureScreenshot($"{_fileTime}G.jpg");
    }

    private void SnapshotNoGUI(string _fileTime)
    {
        ScreenCapture.CaptureScreenshot($"{_fileTime}.jpg");

        float Xmin = upperLeft.x;
        float Ymin = upperLeft.y;
        float Xmax = lowerRight.x;
        float Ymax = lowerRight.y;
        float standardXmin = upperLeft.x / Screen.width;
        float standardYmin = upperLeft.y / Screen.height;
        float standardXmax = lowerRight.x / Screen.width;
        float standardYmax = lowerRight.y / Screen.height;

        /*
        0 : car
        1 : pottedplant
        2 : bottle
        */
        string className = "bottle";

        StreamWriter swA = new StreamWriter($"/storage/emulated/0/Android/data/{packageName}/A_txt/{_fileTime}.txt");
        swA.WriteLine($"2 {standardXmin.ToString("0.000000")} {standardYmin.ToString("0.000000")} {standardXmax.ToString("0.000000")} {standardYmax.ToString("0.000000")}");
        swA.Close();

        StreamWriter swB = new StreamWriter($"/storage/emulated/0/Android/data/{packageName}/B_txt/{_fileTime}.txt");
        swB.WriteLine($"2 {((standardXmin + standardXmax) / 2).ToString("0.000000")} {((standardYmin + standardYmax) / 2).ToString("0.000000")} {(standardXmax - standardXmin).ToString("0.000000")} {(standardYmax - standardYmin).ToString("0.000000")}");
        swB.Close();

        StreamWriter swC = new StreamWriter($"/storage/emulated/0/Android/data/{packageName}/VOC_txt/{_fileTime}.txt");
        swC.WriteLine($"{className} {Xmin.ToString("0")} {Ymin.ToString("0")} {Xmax.ToString("0")} {Ymax.ToString("0")}");
        swC.Close();
    }
}
