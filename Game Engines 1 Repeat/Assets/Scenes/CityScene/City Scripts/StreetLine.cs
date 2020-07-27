using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StreetLine : MonoBehaviour
{
    private LineRenderer streetLine;
    private Vector3 mousePos;
    public Material streetLineMaterial;
    private int currentLines = 0;
    
   // public Camera cam;
    private List<Vector3> thePoints = new List<Vector3>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (streetLine == null)
            {
                CreateStreetLine();
            }
            
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            streetLine.SetPosition(0, mousePos);
            streetLine.SetPosition(1, mousePos);
        }
        else if (Input.GetMouseButtonUp(0) && streetLine)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            streetLine.SetPosition(1, mousePos);
            streetLine = null;
            currentLines++;
        }
        else if (Input.GetMouseButton(0) && streetLine)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            streetLine.SetPosition(1, mousePos);
        }

    }
    void CreateStreetLine()
    {
        streetLine = new GameObject("StreetLine" + currentLines).AddComponent<LineRenderer>();
        streetLine.material = streetLineMaterial;
        streetLine.positionCount = 2;
        streetLine.startWidth = .15f;
        streetLine.endWidth = .15f;
        streetLine.useWorldSpace = true;
        streetLine.numCapVertices = 50;
    }
    
}
