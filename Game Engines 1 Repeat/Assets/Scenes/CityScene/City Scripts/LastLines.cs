using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLines : MonoBehaviour
{
    public GameObject currentLineRenderer;
    public GameObject lineRendererPrefab;
    
    public GameObject lines;
    
    public Material drawingMaterial;
    
    private Vector3 previousPosition, currentPostion;
    
    private bool clickedTwice;
    private bool setRandomColor;
    private bool youNowHaveInitialPoint;
    
    private int numberOfPoints;
    
    public Camera MainCamera;
    
    private LineRenderer line;
    //private LineRenderer[] lines;
    private bool isMousePressed;
    private List<Vector3> pointsList;
    private Vector3 mousePos;
    private Vector3 mousePosTwo;
    
    private Vector3 firstMousePos;
    private Vector3 secondMousePos;
    
    void Awake()
    {
        // Create line renderer component and set its property
       // line = gameObject.AddComponent<LineRenderer>();
        // line.material =  new Material(Shader.Find("Particles/Additive"));
       // line.SetVertexCount(0);
       // line.SetWidth(0.1f,0.1f);
       // line.SetColors(Color.green, Color.green);
       // line.useWorldSpace = true; 
       
        isMousePressed = false;
        pointsList = new List<Vector3>();

        youNowHaveInitialPoint = false;

        //StartPoint = new Vector3();
    }
    
    
    void Update()
    {
        Vector3 currentPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentPosition.z = 0;
        if (Input.GetMouseButtonDown(0) && youNowHaveInitialPoint)
        {
            pointsList.Add(mousePosTwo);
            DrawLineBetweenTwoPoints();
            //currentLineRenderer = null;
            numberOfPoints = 0;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            SaveFirstPointToList();
        }

    }
    public void SaveFirstPointToList()
    {
        pointsList.Add (mousePos);
        youNowHaveInitialPoint = true;
    }
    private void DrawLineBetweenTwoPoints()
    {
        if (currentLineRenderer == null)
        {
            currentLineRenderer = Instantiate(lineRendererPrefab) as GameObject;
            
            currentLineRenderer.transform.parent = lines.transform;
            
            LineRenderer ln = currentLineRenderer.GetComponent<LineRenderer>();
            
            ln.SetPosition(0, mousePos);
            ln.SetPosition(1, mousePosTwo);

        }

        numberOfPoints++;

        /*Vector3 mousePos = Input.mousePosition;

        if(mousePos != null) { 
            Vector3 wantedPos = MainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0 -MainCamera.transform.position.z));

            LineRenderer ln = currentLineRenderer.GetComponent<LineRenderer>();

            ln.SetVertexCount(numberOfPoints);
            ln.SetPosition(numberOfPoints - 1, wantedPos);
        }*/
    }
}
