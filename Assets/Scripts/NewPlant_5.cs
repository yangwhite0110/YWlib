using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class NewPlant_5 : MonoBehaviour
{
    public GUIStyle pixelStyle;
    public GUIStyle btnStyle;

    private GameObject rectInfo;
    private Vector2 XminYmin;
    private Vector2 XmaxYmax;

    private int rotationValue;
    private bool guiSwitch;
    private string packageName;

    private void Start()
    {
        rectInfo = GameObject.Find("Rect Info");
        XminYmin = new Vector2(0.0f, 0.0f);
        XmaxYmax = new Vector2(0.0f, 0.0f);
        rotationValue = 0;
        guiSwitch = true;
        packageName = "com.Yuuu.Plant5";
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
        // MeshFilter unvalid!

        Vector3 plant_center  = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.center;
        Vector3 plant_extents = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.extents;

        Vector3 pot_center  = transform.GetChild(3).GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.center;
        Vector3 pot_extents = transform.GetChild(3).GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.extents;

        

        Vector3[] vertices3d = new Vector3[16]
        {
            new Vector3(plant_center.x + plant_extents.x, plant_center.y + plant_extents.y, plant_center.z + plant_extents.z),
            new Vector3(plant_center.x + plant_extents.x, plant_center.y + plant_extents.y, plant_center.z - plant_extents.z),
            new Vector3(plant_center.x + plant_extents.x, plant_center.y - plant_extents.y, plant_center.z + plant_extents.z),
            new Vector3(plant_center.x + plant_extents.x, plant_center.y - plant_extents.y, plant_center.z - plant_extents.z),
            new Vector3(plant_center.x - plant_extents.x, plant_center.y + plant_extents.y, plant_center.z + plant_extents.z),
            new Vector3(plant_center.x - plant_extents.x, plant_center.y + plant_extents.y, plant_center.z - plant_extents.z),
            new Vector3(plant_center.x - plant_extents.x, plant_center.y - plant_extents.y, plant_center.z + plant_extents.z),
            new Vector3(plant_center.x - plant_extents.x, plant_center.y - plant_extents.y, plant_center.z - plant_extents.z),

            new Vector3(pot_center.x + pot_extents.x, pot_center.y + pot_extents.y, pot_center.z + pot_extents.z),
            new Vector3(pot_center.x + pot_extents.x, pot_center.y + pot_extents.y, pot_center.z - pot_extents.z),
            new Vector3(pot_center.x + pot_extents.x, pot_center.y - pot_extents.y, pot_center.z + pot_extents.z),
            new Vector3(pot_center.x + pot_extents.x, pot_center.y - pot_extents.y, pot_center.z - pot_extents.z),
            new Vector3(pot_center.x - pot_extents.x, pot_center.y + pot_extents.y, pot_center.z + pot_extents.z),
            new Vector3(pot_center.x - pot_extents.x, pot_center.y + pot_extents.y, pot_center.z - pot_extents.z),
            new Vector3(pot_center.x - pot_extents.x, pot_center.y - pot_extents.y, pot_center.z + pot_extents.z),
            new Vector3(pot_center.x - pot_extents.x, pot_center.y - pot_extents.y, pot_center.z - pot_extents.z)
        };
        Vector2[] vertices2d = new Vector2[16];

        // Vector3 ---> Vector2 & fix y
        for (int i = 0; i < 16; i++)
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

        if (rotationValue == 0 || rotationValue == 180)
        {
            min.x += 25.0f;
            max.x -= 25.0f;
        }
        else if (rotationValue == 90 || rotationValue == 270)
        {
            min.x += 30.0f;
            max.x -= 30.0f;
        }


        XminYmin = min;
        XmaxYmax = max;

        rectInfo.GetComponent<Text>().text = $"Rect Width: {(XmaxYmax.x - XminYmin.x).ToString("0")}, Rect Height: {(XmaxYmax.y - XminYmin.y).ToString("0")}";

        return new Rect(XminYmin.x, XminYmin.y, XmaxYmax.x - XminYmin.x, XmaxYmax.y - XminYmin.y);
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

        float Xmin = XminYmin.x;
        float Ymin = XminYmin.y;
        float Xmax = XmaxYmax.x;
        float Ymax = XmaxYmax.y;
        float standardXmin = XminYmin.x / Screen.width;
        float standardYmin = XminYmin.y / Screen.height;
        float standardXmax = XmaxYmax.x / Screen.width;
        float standardYmax = XmaxYmax.y / Screen.height;

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
