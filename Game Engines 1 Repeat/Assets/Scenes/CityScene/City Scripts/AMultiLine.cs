using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMultiLine : MonoBehaviour
{

    public bool pointOneIsSet;
    
    public List<Vector3> SavedPairedPoints = new List<Vector3>();

    public Vector3 mousePosOne, mousePosTwo;

    public Material lineMaterial;

    private Vector3[] pairOfPoints;
    
    // Start is called before the first frame update
    void Start()
    {
       // mousePosOne = new Vector3();
       pointOneIsSet = false;
       
       pairOfPoints = new Vector3[2];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !pointOneIsSet)
        {
            mousePosOne = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosOne.z=0;
            
            Vector3 startPoint = new Vector3(mousePosOne.x, mousePosOne.y, mousePosOne.z);
            SavedPairedPoints.Add(startPoint);
            
            pairOfPoints[0] = startPoint;
            
            Debug.Log("startPoint is " + startPoint);

            pointOneIsSet = true;
        }
        else if (Input.GetMouseButtonDown(0) && pointOneIsSet)
        {
            DrawLineBetweenTheTwoPoints();
        }
    }

    public void DrawLineBetweenTheTwoPoints()
    {
        mousePosTwo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosTwo.z=0;
            
        Vector3 endPoint = new Vector3(mousePosTwo.x, mousePosTwo.y, mousePosTwo.z);
        SavedPairedPoints.Add(endPoint);
        
        pairOfPoints[1] = endPoint;
        
        Debug.Log("endPoint is set" + endPoint);
        
        GameObject line = new GameObject("line");
        line.transform.position = pairOfPoints[0];
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
        lineRenderer.SetPosition(0, SavedPairedPoints[0]);
        lineRenderer.SetPosition(1, SavedPairedPoints[1]);
        
        SavedPairedPoints.RemoveRange(0,SavedPairedPoints.Count);

        pointOneIsSet = false;
    }
}
