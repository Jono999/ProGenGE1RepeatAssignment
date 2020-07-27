using UnityEngine;
using System.Collections;

public class MultipleLines : MonoBehaviour
{
    //public GameObject circlePointPrefab;
    public GameObject currentLineRenderer;
    public GameObject lineRendererPrefab;
    public Material drawingMaterial;
    private Vector3 previousPosition, currentPostion;
    private bool clickStarted;
    private int numberOfPoints;
    private bool setRandomColor;
    public Camera MainCamera;
    public GameObject lines;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        Vector3 currentPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentPosition.z = -5.0f;
        if (Input.GetMouseButtonDown(0))
        {
            TouchSpaceHandle();

            clickStarted = true;
        }
        else if (clickStarted)
        {
            TouchSpaceHandle();
        }

        if (Input.GetMouseButtonUp(0))
        {
            clickStarted = false;
            currentLineRenderer = null;
            numberOfPoints = 0;

        }

    }

    private void TouchSpaceHandle()
    {

        if (currentLineRenderer == null)
        {
            currentLineRenderer = Instantiate(lineRendererPrefab) as GameObject;
            
            currentLineRenderer.transform.parent = lines.transform;
            //setRandomColor = true;
        }

        numberOfPoints++;

        Vector3 mousePos = Input.mousePosition;

        if(mousePos != null) { 
        Vector3 wantedPos = MainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0 -MainCamera.transform.position.z));

        LineRenderer ln = currentLineRenderer.GetComponent<LineRenderer>();

       /* if (setRandomColor)
        {

            Color[] colors = new Color[6];
            colors[0] = Color.red;
            colors[1] = Color.black;
            colors[2] = Color.green;
            colors[3] = Color.blue;
            colors[4] = Color.cyan;
            colors[5] = new Color(255, 165, 0);

            ln.material.color = colors[Random.Range(0, colors.Length)];
            setRandomColor = false;
        }*/


        ln.SetVertexCount(numberOfPoints);
        ln.SetPosition(numberOfPoints - 1, wantedPos);
        }
    }


   /* private void InstatiateCirclePoint(Vector3 pos, Transform parent)
    {
        GameObject currentCircle = (GameObject)Instantiate(circlePointPrefab);
        currentCircle.transform.parent = parent;
        currentCircle.GetComponent<Renderer>().material = drawingMaterial;
        currentCircle.transform.position = pos;

    }*/


}
