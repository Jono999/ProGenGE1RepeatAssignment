using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickLine : MonoBehaviour
{

    private List<GameObject> pointsObjects;
    
    public GameObject currentLineRenderer;
    public GameObject lineRendererPrefab;
    public GameObject lines;
    private bool clickStarted;
    private int numberOfPoints;
    
    //private Vector3 godHandEmptyPosition;
    //VillagerScript villagerScript;
    //public GameObject groundEmpty;
    //public List<GameObject> villagerObjects;
    //GameObject villagerObject;
    
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
        line = gameObject.AddComponent<LineRenderer>();
        // line.material =  new Material(Shader.Find("Particles/Additive"));
        line.SetVertexCount(0);
        line.SetWidth(0.1f,0.1f);
        line.SetColors(Color.green, Color.green);
        line.useWorldSpace = true;    
        isMousePressed = false;
        pointsList = new List<Vector3>();
        
        //StartPoint = new Vector3();
    }
 
    private void Update()
 
    {
 
        //Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && pointsList.Count > 0)

        {
            Save2ndPointToListAndDrawLine();
            //clickStarted = true;
            //Save2ndPointToListAndDrawLine();
            
           // pointsList.RemoveRange(0,pointsList.Count);
          // pointsList.Remove(mousePosTwo);
            
          //var drawline = new DrawLine();
         // drawline.GetComponent<LineRenderer>();

          // DrawLine(mousePos, mousePosTwo, Color.yellow);
          mousePosTwo = Camera.main.ScreenToWorldPoint(Input.mousePosition);

          // DrawLine(mousePos, Input.mousePosition, Color.yellow);

          //villagerObjects.Clear();
        }
 
 
        if (Input.GetMouseButtonDown(0))
 
        {
            SaveFirstPointToList();
 
        }
 
       // if (pointsList.Count > 0)
 
       // {
            //godHandEmptyPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            //villagerObjects[0].transform.position = godHandEmptyPosition;
 
       // }
 
    }
 
 
    public void SaveFirstPointToList()
 
    {
        clickStarted = false;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z=0;
        
       // Vector3[] pairOfPoints = new Vector3[2];
       // firstMousePos = pairOfPoints[0];
        
        //if (!pointsList.Contains (mousePos)) 
       // {
       
            pointsList.Add (mousePos);
            //line.SetVertexCount (pointsList.Count);
            //line.SetPosition (pointsList.Count - 1, (Vector3)pointsList [pointsList.Count - 1]);
            Debug.Log("one");
       // }
        //RaycastHit hit;
 
       // if (Physics.Raycast(transform.position, Vector3.down * 1000, out hit) && hit.collider.tag == "Villager")
 
       // {
          //  villagerObjects.Add(hit.transform.gameObject);
          //  villagerObjects[0].GetComponent<VillagerScript>().VillagerPickedUp = true;
 
        //}
    }

    public void Save2ndPointToListAndDrawLine()

    {
        if (currentLineRenderer == null)
        {
            currentLineRenderer = Instantiate(lineRendererPrefab) as GameObject;

            currentLineRenderer.transform.parent = lines.transform;

            //mousePosTwo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // mousePosTwo.z = 0;
            //if (!pointsList.Contains (mousePosTwo)) 
            //{
            pointsList.Add(mousePosTwo);
            line.SetVertexCount(pointsList.Count);
            line.SetPosition (pointsList.Count - 1, (Vector3)pointsList [pointsList.Count - 1]);
            //line.SetPosition (pointsList.Count - 1, mousePos);
            // line.SetPosition (pointsList.Count - 1, mousePosTwo);
            //line.SetPosition (pointsList.Count, (Vector3)pointsList [pointsList.Count]);

            //tempPosition = currentPosition;
            //currentPosition += direction * length;
            //DrawLine(tempPosition, currentPosition, Color.yellow);
            //DrawLine(mousePos, mousePosTwo, Color.yellow);

            Debug.Log("two");
        }

        numberOfPoints++;

        Vector3 mousePos = Input.mousePosition;

        if (mousePos != null)
        {
            Vector3 wantedPos =
                Camera.main.ScreenToWorldPoint(
                    new Vector3(mousePos.x, mousePos.y, 0 - Camera.main.transform.position.z));

            LineRenderer ln = currentLineRenderer.GetComponent<LineRenderer>();

            // lrend.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
            // lrend.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0));

            // }

            //villagerObjects[0].transform.position = groundEmpty.transform.position;
            //villagerObjects[0].GetComponent<VillagerScript>().VillagerPickedUp = false;
        }
    }

    public void DrawLine(Vector3 start, Vector3 end, Color colour)
        {
            GameObject line = new GameObject("line");
            line.transform.position = start;
            var lineRenderer = line.AddComponent<LineRenderer>();
            //lineRenderer.material = lineMaterial;
            lineRenderer.startColor = colour;
            lineRenderer.endColor = colour;
            lineRenderer.startWidth = .1f;
            lineRenderer.endWidth = .1f;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }




}