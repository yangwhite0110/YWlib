using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Plant_2 : MonoBehaviour
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
        packageName = "com.Yuuu.Plant2";
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

            if (GUI.Button(new Rect(Screen.width - 140.0f, 400.0f, 120.0f, 90.0f), "+60", btnRotationInStyle))
            {
                rotationValue += 60.0f;
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

        Vector3 _0child_center  = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.center;
        Vector3 _0child_extents = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.extents;

        Vector3 _1child_center  = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().bounds.center;
        Vector3 _1child_extents = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().bounds.extents;

        Vector3 _2child_center  = transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().bounds.center;
        Vector3 _2child_extents = transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().bounds.extents;

        Vector3 _3child_center  = transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().bounds.center;
        Vector3 _3child_extents = transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().bounds.extents;

        Vector3 _4child_center  = transform.GetChild(4).gameObject.GetComponent<MeshRenderer>().bounds.center;
        Vector3 _4child_extents = transform.GetChild(4).gameObject.GetComponent<MeshRenderer>().bounds.extents;

        Vector3[] vertices3d = new Vector3[40]
        {
            new Vector3(_0child_center.x + _0child_extents.x, _0child_center.y + _0child_extents.y, _0child_center.z + _0child_extents.z),
            new Vector3(_0child_center.x + _0child_extents.x, _0child_center.y + _0child_extents.y, _0child_center.z - _0child_extents.z),
            new Vector3(_0child_center.x + _0child_extents.x, _0child_center.y - _0child_extents.y, _0child_center.z + _0child_extents.z),
            new Vector3(_0child_center.x + _0child_extents.x, _0child_center.y - _0child_extents.y, _0child_center.z - _0child_extents.z),
            new Vector3(_0child_center.x - _0child_extents.x, _0child_center.y + _0child_extents.y, _0child_center.z + _0child_extents.z),
            new Vector3(_0child_center.x - _0child_extents.x, _0child_center.y + _0child_extents.y, _0child_center.z - _0child_extents.z),
            new Vector3(_0child_center.x - _0child_extents.x, _0child_center.y - _0child_extents.y, _0child_center.z + _0child_extents.z),
            new Vector3(_0child_center.x - _0child_extents.x, _0child_center.y - _0child_extents.y, _0child_center.z - _0child_extents.z),

            new Vector3(_1child_center.x + _1child_extents.x, _1child_center.y + _1child_extents.y, _1child_center.z + _1child_extents.z),
            new Vector3(_1child_center.x + _1child_extents.x, _1child_center.y + _1child_extents.y, _1child_center.z - _1child_extents.z),
            new Vector3(_1child_center.x + _1child_extents.x, _1child_center.y - _1child_extents.y, _1child_center.z + _1child_extents.z),
            new Vector3(_1child_center.x + _1child_extents.x, _1child_center.y - _1child_extents.y, _1child_center.z - _1child_extents.z),
            new Vector3(_1child_center.x - _1child_extents.x, _1child_center.y + _1child_extents.y, _1child_center.z + _1child_extents.z),
            new Vector3(_1child_center.x - _1child_extents.x, _1child_center.y + _1child_extents.y, _1child_center.z - _1child_extents.z),
            new Vector3(_1child_center.x - _1child_extents.x, _1child_center.y - _1child_extents.y, _1child_center.z + _1child_extents.z),
            new Vector3(_1child_center.x - _1child_extents.x, _1child_center.y - _1child_extents.y, _1child_center.z - _1child_extents.z),

            new Vector3(_2child_center.x + _2child_extents.x, _2child_center.y + _2child_extents.y, _2child_center.z + _2child_extents.z),
            new Vector3(_2child_center.x + _2child_extents.x, _2child_center.y + _2child_extents.y, _2child_center.z - _2child_extents.z),
            new Vector3(_2child_center.x + _2child_extents.x, _2child_center.y - _2child_extents.y, _2child_center.z + _2child_extents.z),
            new Vector3(_2child_center.x + _2child_extents.x, _2child_center.y - _2child_extents.y, _2child_center.z - _2child_extents.z),
            new Vector3(_2child_center.x - _2child_extents.x, _2child_center.y + _2child_extents.y, _2child_center.z + _2child_extents.z),
            new Vector3(_2child_center.x - _2child_extents.x, _2child_center.y + _2child_extents.y, _2child_center.z - _2child_extents.z),
            new Vector3(_2child_center.x - _2child_extents.x, _2child_center.y - _2child_extents.y, _2child_center.z + _2child_extents.z),
            new Vector3(_2child_center.x - _2child_extents.x, _2child_center.y - _2child_extents.y, _2child_center.z - _2child_extents.z),

            new Vector3(_3child_center.x + _3child_extents.x, _3child_center.y + _3child_extents.y, _3child_center.z + _3child_extents.z),
            new Vector3(_3child_center.x + _3child_extents.x, _3child_center.y + _3child_extents.y, _3child_center.z - _3child_extents.z),
            new Vector3(_3child_center.x + _3child_extents.x, _3child_center.y - _3child_extents.y, _3child_center.z + _3child_extents.z),
            new Vector3(_3child_center.x + _3child_extents.x, _3child_center.y - _3child_extents.y, _3child_center.z - _3child_extents.z),
            new Vector3(_3child_center.x - _3child_extents.x, _3child_center.y + _3child_extents.y, _3child_center.z + _3child_extents.z),
            new Vector3(_3child_center.x - _3child_extents.x, _3child_center.y + _3child_extents.y, _3child_center.z - _3child_extents.z),
            new Vector3(_3child_center.x - _3child_extents.x, _3child_center.y - _3child_extents.y, _3child_center.z + _3child_extents.z),
            new Vector3(_3child_center.x - _3child_extents.x, _3child_center.y - _3child_extents.y, _3child_center.z - _3child_extents.z),

            new Vector3(_4child_center.x + _4child_extents.x, _4child_center.y + _4child_extents.y, _4child_center.z + _4child_extents.z),
            new Vector3(_4child_center.x + _4child_extents.x, _4child_center.y + _4child_extents.y, _4child_center.z - _4child_extents.z),
            new Vector3(_4child_center.x + _4child_extents.x, _4child_center.y - _4child_extents.y, _4child_center.z + _4child_extents.z),
            new Vector3(_4child_center.x + _4child_extents.x, _4child_center.y - _4child_extents.y, _4child_center.z - _4child_extents.z),
            new Vector3(_4child_center.x - _4child_extents.x, _4child_center.y + _4child_extents.y, _4child_center.z + _4child_extents.z),
            new Vector3(_4child_center.x - _4child_extents.x, _4child_center.y + _4child_extents.y, _4child_center.z - _4child_extents.z),
            new Vector3(_4child_center.x - _4child_extents.x, _4child_center.y - _4child_extents.y, _4child_center.z + _4child_extents.z),
            new Vector3(_4child_center.x - _4child_extents.x, _4child_center.y - _4child_extents.y, _4child_center.z - _4child_extents.z)
        };
        Vector2[] vertices2d = new Vector2[40];

        // Vector3 ---> Vector2 & fix y
        for (int i = 0; i < 40; i++)
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

        // fix the error
        float errorU = 0.0f;
        float errorD = 0.0f;
        float errorL = 0.0f;
        float errorR = 0.0f;
        
        if (rotationValue == 0.0f)
        {
            errorU = 70.0f;     //65l
            errorD = -65.0f;    //
            errorL = 120.0f;
            errorR = -140.0f;   //
        }
        else if (rotationValue == 45.0f)
        {
            errorU = 65.0f;      //60l
            errorD = -155.0f;    //-160m
            errorL = 435.0f;     //425l
            errorR = -350.0f;    //-340l
        }
        else if (rotationValue == 90.0f)
        {
            errorU = 25.0f;      //
            errorD = -20.0f;     //
            errorL = 110.0f;     //
            errorR = -90.0f;     //
        }
        else if (rotationValue == 135.0f)
        {
            errorU = 30.0f;      //
            errorD = -95.0f;     //
            errorL = 460.0f;     //450l
            errorR = -320.0f;    //
        }
        else if (rotationValue == 180.0f)
        {
            errorU = 15.0f;  //
            errorD = -10.0f; //
            errorL = 75.0f;  //
            errorR = -85.0f; //
        }
        else if (rotationValue == 225.0f)
        {
            errorU = 25.0f;     //
            errorD = -95.0f;    //
            errorL = 360.0f;    //350l
            errorR = -420.0f;   //-410l
        }
        else if (rotationValue == 270.0f)
        {
            errorU = 25.0f;     //
            errorD = -25.0f;    //
            errorL = 70.0f;     //
            errorR = -95.0f;    //
        }
        else if (rotationValue == 315.0f)
        {
            errorU = 70.0f;      //65l
            errorD = -160.0f;    //
            errorL = 415.0f;     //405l
            errorR = -425.0f;    //
        }
        
        min.x += errorL;
        min.y += errorU;

        max.x += errorR;
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
        string className = "pottedplant";

        StreamWriter swA = new StreamWriter($"/storage/emulated/0/Android/data/{packageName}/A_txt/{_fileTime}.txt");
        swA.WriteLine($"1 {standardXmin.ToString("0.000000")} {standardYmin.ToString("0.000000")} {standardXmax.ToString("0.000000")} {standardYmax.ToString("0.000000")}");
        swA.Close();
        StreamWriter swB = new StreamWriter($"/storage/emulated/0/Android/data/{packageName}/B_txt/{_fileTime}.txt");
        swB.WriteLine($"1 {((standardXmin + standardXmax) / 2).ToString("0.000000")} {((standardYmin + standardYmax) / 2).ToString("0.000000")} {(standardXmax - standardXmin).ToString("0.000000")} {(standardYmax - standardYmin).ToString("0.000000")}");
        swB.Close();

        StreamWriter swC = new StreamWriter($"/storage/emulated/0/Android/data/{packageName}/VOC_txt/{_fileTime}.txt");
        swC.WriteLine($"{className} {Xmin.ToString("0")} {Ymin.ToString("0")} {Xmax.ToString("0")} {Ymax.ToString("0")}");
        swC.Close();
    }
}
