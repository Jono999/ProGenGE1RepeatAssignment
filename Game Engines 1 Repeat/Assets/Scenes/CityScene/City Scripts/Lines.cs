using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{
    private LineRenderer line;
    private bool firstMousePositionPressed;
    private List<Vector3> pointsList;
    private Vector3 firstMousePos;

    private bool secondMousePositionPressed;
    private Vector3 secondMousePos;

    // Structure for line points
    struct myLine
    {
        public Vector3 StartPoint;
        public Vector3 EndPoint;
    };
    //    -----------------------------------    
    void Awake()
    {
        // Create line renderer component and set its property
        line = gameObject.AddComponent<LineRenderer>();
       // line.material =  new Material(Shader.Find("Particles/Additive"));
        line.SetVertexCount(0);
        line.SetWidth(0.1f,0.1f);
        line.SetColors(Color.green, Color.green);
        line.useWorldSpace = true;    
        firstMousePositionPressed = false;
        pointsList = new List<Vector3>();
        
        secondMousePositionPressed = false;
        
        //StartPoint = new Vector3();
    }
    //    -----------------------------------    
    void Update () 
    {
        // If mouse button down, remove old line and set its color to green
        if(Input.GetMouseButtonDown(0))
        {
            firstMousePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            firstMousePos.z= 0;
            
            Vector3[] pairOfPoints = new Vector3[2];
            firstMousePos = pairOfPoints[0];
           
                pointsList.Add (firstMousePos);
                line.SetVertexCount (pointsList.Count);
                line.SetPosition (pairOfPoints.Length - 1, pairOfPoints[0]);
                Debug.Log("one");
             
        }
       /* else if(Input.GetMouseButtonUp(0))
        {
            firstMousePositionPressed = true;
            //pointsList.RemoveRange(0,pointsList.Count);
            //line.SetVertexCount(0);
        }*/
        firstMousePositionPressed = true;
        
        if(Input.GetMouseButtonDown(0) && firstMousePositionPressed)
        {
            secondMousePositionPressed = true;
            firstMousePositionPressed = false;
            secondMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            secondMousePos.z=0;
            
                pointsList.Add (secondMousePos);
                line.SetVertexCount (pointsList.Count);
                line.SetPosition (0, (Vector3)firstMousePos);
                line.SetPosition (1, (Vector3)secondMousePos);
                Debug.Log("two");
                
                firstMousePositionPressed = false;
                // lrend.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
                // lrend.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0));
            
            //line.SetVertexCount(0);
            //pointsList.RemoveRange(0,pointsList.Count);
           // line.SetColors(Color.green, Color.green);
        }
        
    }

}
