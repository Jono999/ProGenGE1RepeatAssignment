using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPoints : MonoBehaviour
{
    public Camera cam;
    private List<Vector3> thePoints = new List<Vector3>();
    private Vector3[] Points = new Vector3[2];
    
    private LineRenderer lrend;

    // Start is called before the first frame update
    void Start()
    {
        //cam = Camera.current;
        lrend = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 mousePos = Vector3.zero;

        Vector3 endLine = new Vector3();
        Vector3 startLine = new Vector3();

        if (mouseRay.direction.y != 0)
        {
            float dstToXZPlane = Mathf.Abs(mouseRay.origin.y / mouseRay.direction.y);
            mousePos = mouseRay.GetPoint(dstToXZPlane);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //thePoints.Add(mousePos);
            startLine =  Points[0] = new Vector3(mousePos.x, mousePos.y, 0f);
            //Debug.Log(mousePos);
            Debug.Log(startLine);

           // Vector3 startLine = new Vector3(mousePos.x, mousePos.y, 0f);
           
            //lrend.SetPosition(0, new Vector3(mousePos.x, mousePos.y, 0f));
           // lrend.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f)); 
        
        }

        if (Input.GetMouseButtonDown(0))
        {
            endLine =  Points[1] = new Vector3(mousePos.x, mousePos.y, 0f);
            Debug.Log(endLine);
        }

        ReturnPointPositions(startLine, endLine);
    }

    public void ReturnPointPositions(Vector3 StartLine, Vector3 Endline)
    {
        lrend.SetPosition(0,StartLine);
        lrend.SetPosition(1,Endline);
        /*for (int i = 0; i < thePoints.Count; i++)
        {
            Vector3 startLine = thePoints[i] = new Vector3();
            Vector3 endLine = thePoints[i] = new Vector3();
        }*/
    }
}
