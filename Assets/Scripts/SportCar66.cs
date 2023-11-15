using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SportCar66 : MonoBehaviour
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

    private void Start()
    {
        rectInfo = GameObject.Find("Rect Info");
        upperLeft = new Vector2(0.0f, 0.0f);
        lowerRight = new Vector2(0.0f, 0.0f);
        rectWidth = 0.0f;
        rectHeight = 0.0f;
        rotationValue = 0.0f;
        guiSwitch = true;
    }

    private void OnGUI()
    {
        if (guiSwitch)
        {
            GUI.Box(MRRect(), "");
            GUI.Label(new Rect(upperLeft.x, upperLeft.y - 60.0f, 100.0f, 60.0f), upperLeft.ToString("0.00"), ULStyle);
            GUI.Label(new Rect(lowerRight.x - 100.0f, lowerRight.y, 100.0f, 60.0f), lowerRight.ToString("0.00"), LRStyle);

            if (GUI.Button(new Rect(30.0f, 30.0f, 200.0f, 120.0f), "Shot", btnShotStyle))
            {
                Shot();
            }

            GUI.Label(new Rect(230.0f, 30.0f, 180.0f, 90.0f), rotationValue.ToString("0.0"), rotationValueStyle);

            if (GUI.Button(new Rect(30.0f, 180.0f, 120.0f, 90.0f), "+15", btnRotationInStyle))
            {
                rotationValue += 15.0f;
            }
            if (GUI.Button(new Rect(200.0f, 180.0f, 120.0f, 90.0f), "-15", btnRotationDeStyle))
            {
                rotationValue -= 15.0f;
            }
            transform.parent.rotation = Quaternion.Euler(0, rotationValue, 0);
            /*
            sliderValue = GUI.VerticalSlider(new Rect(100.0f, 100.0f, 100.0f, 300.0f), sliderValue, 45.0f, 0.0f);
            */
        }
    }

    private Rect MRRect()
    {
        // MeshFilter unvalid!

        Vector3 center = transform.GetChild(0).gameObject.GetComponent<MeshCollider>().bounds.center;
        Vector3 extents = transform.GetChild(0).gameObject.GetComponent<MeshCollider>().bounds.extents;
        Vector3[] vertices3d = new Vector3[8]
        {
            new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z),
            new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z),
            new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z),
            new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z),

            new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z),
            new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z),
            new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z),
            new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z)
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

        // fix the min.x
        float widthErrorL = 0.0f;
        float widthErrorR = 0.0f;
        float errorD = 0.0f;
        
        if (rotationValue == 0.0f || rotationValue == 180.0f)
        {
            widthErrorL = 40.0f;
            widthErrorR = 40.0f;
            errorD = 5.0f;
        }
        else if (rotationValue == 15.0f || rotationValue == 195.0f)
        {
            widthErrorL = 20.0f;    //50 30 25
            widthErrorR = 110.0f;   //175 120
            errorD = 5.0f;
        }
        else if (rotationValue == 30.0f || rotationValue == 210.0f)
        {
            widthErrorL = 35.0f;    //65 45 40
            widthErrorR = 120.0f;   //200 150
            errorD = 15.0f; //10
        }
        else if (rotationValue == 45.0f || rotationValue == 225.0f)
        {
            widthErrorL = 45.0f;    //90 50
            widthErrorR = 120.0f;   //195
            errorD = 20.0f;
        }
        else if (rotationValue == 60.0f || rotationValue == 240.0f)
        {
            widthErrorL = 55.0f;    //90 60
            widthErrorR = 110.0f;   //175 120
            errorD = 25.0f; //30
        }
        else if (rotationValue == 75.0f || rotationValue == 255.0f)
        {
            widthErrorL = 45.0f;    //75 50
            widthErrorR = 80.0f;   //125 100 90 85
            errorD = 25.0f; //30
        }
        else if (rotationValue == 90.0f || rotationValue == 270.0f)
        {
            widthErrorL = 40.0f;    //65 45
            widthErrorR = 45.0f;    //70 50
            errorD = 30.0f; //35
        }
        /*
        else if (rotationValue == 105.0f || rotationValue == 285.0f)
        {
            widthErrorL = 110.0f;   //120
            widthErrorR = 95.0f;   //100
            errorD = 35.0f;
        }
        else if (rotationValue == 120.0f || rotationValue == 300.0f)
        {
            widthErrorL = 175.0f;   //200 180
            widthErrorR = 100.0f;   //140 120 110
            errorD = 25.0f;
        }
        else if (rotationValue == 135.0f || rotationValue == 315.0f)
        {
            widthErrorL = 205.0f;   //220 200
            widthErrorR = 95.0f;   //150 120 105
            errorD = 15.0f;
        }
        else if (rotationValue == 150.0f || rotationValue == 330.0f)
        {
            widthErrorL = 200.0f;   //200
            widthErrorR = 80.0f;   //140 120 100
            //errorD = 5.0f;
        }
        else if (rotationValue == 165.0f || rotationValue == 345.0f)
        {
            widthErrorL = 175.0f;   //180
            widthErrorR = 60.0f;   //80 70
            errorD = -15.0f;
        }
        */

        min.x += widthErrorL;
        max.x -= widthErrorR;
        max.y += errorD;

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
        /*
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        guiSwitch = false;
        yield return new WaitForEndOfFrame();

        // data without GUI
        SnapshotNoGUI(fileTime);
        yield return new WaitForEndOfFrame();

        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
        guiSwitch = true;
        yield return new WaitForEndOfFrame();
        */
    }

    private void SnapshotGUI(string _fileTime)
    {
        ScreenCapture.CaptureScreenshot($"{_fileTime}G.jpg");
    }

    private void SnapshotNoGUI(string _fileTime)
    {
        ScreenCapture.CaptureScreenshot($"{_fileTime}.jpg");


        float standardXmin = upperLeft.x / Screen.width;
        float standardYmin = upperLeft.y / Screen.height;
        float standardXmax = lowerRight.x / Screen.width;
        float standardYmax = lowerRight.y / Screen.height;

        StreamWriter swA = new StreamWriter($"/storage/emulated/0/Android/data/com.Yuuu.SC66/A_txt/{_fileTime}.txt");
        swA.WriteLine($"0 {standardXmin.ToString("0.000000")} {standardYmin.ToString("0.000000")} {standardXmax.ToString("0.000000")} {standardYmax.ToString("0.000000")}");
        swA.Close();
        StreamWriter swB = new StreamWriter($"/storage/emulated/0/Android/data/com.Yuuu.SC66/B_txt/{_fileTime}.txt");
        swB.WriteLine($"0 {((standardXmin + standardXmax) / 2).ToString("0.000000")} {((standardYmin + standardYmax) / 2).ToString("0.000000")} {(standardXmax - standardXmin).ToString("0.000000")} {(standardYmax - standardYmin).ToString("0.000000")}");
        swB.Close();

    }
}
