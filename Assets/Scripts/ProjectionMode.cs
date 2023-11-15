using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectionMode : MonoBehaviour
{
    private GameObject goProj;

    void Start()
    {
        goProj = GameObject.Find("Projection Info");

        goProj.GetComponent<Text>().text = Camera.main.orthographic.ToString();
    }
}
