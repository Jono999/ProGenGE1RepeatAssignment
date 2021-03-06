﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lrend;
    private Vector2 mousePos;
    private Vector2 startMousePos;

    //[SerializeField] private Text distanceText;

    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        lrend = GetComponent<LineRenderer>();
        lrend.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lrend.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
            lrend.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0));
            distance = (mousePos - startMousePos).magnitude;
            //distanceText.text = distance.ToString("F2") + "meters";
        }
    }
}
