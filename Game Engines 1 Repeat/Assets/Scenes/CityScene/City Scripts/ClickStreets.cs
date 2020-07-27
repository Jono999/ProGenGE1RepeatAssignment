using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickStreets : MonoBehaviour
{

    //private List<GameObject> pointsObjects;

    public bool pointOneIsSet;

    public List<Vector3> TempSavedPairedPoints = new List<Vector3>();
    public List<Vector3> SavedPairedPoints = new List<Vector3>();

    public List<Vector3> RandomPointsForSubdivision = new List<Vector3>();

    public Vector3 mousePosOne, mousePosTwo;

    public Material lineMaterial;

    private Vector3[] pairOfPoints;

    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

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
        if (Input.GetKeyDown("space"))
        {
            GetRandomPoint();
        }

        if (Input.GetMouseButtonDown(0) && !pointOneIsSet)
        {
            mousePosOne = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosOne.z = 0;

            Vector3 startPoint = new Vector3(mousePosOne.x, mousePosOne.y, mousePosOne.z);
            
            TempSavedPairedPoints.Add(startPoint);
            SavedPairedPoints.Add(startPoint);
            RandomPointsForSubdivision.Add(startPoint);

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
        mousePosTwo.z = 0;

        Vector3 endPoint = new Vector3(mousePosTwo.x, mousePosTwo.y, mousePosTwo.z);
        
        TempSavedPairedPoints.Add(endPoint);
        SavedPairedPoints.Add(endPoint);
        RandomPointsForSubdivision.Add(endPoint);

        pairOfPoints[1] = endPoint;

        Debug.Log("endPoint is set" + endPoint);

        GameObject line = new GameObject("line");
        line.transform.position = pairOfPoints[0];
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
        lineRenderer.SetPosition(0, TempSavedPairedPoints[0]);
        lineRenderer.SetPosition(1, TempSavedPairedPoints[1]);

        TempSavedPairedPoints.RemoveRange(0, TempSavedPairedPoints.Count);

        pointOneIsSet = false;
    }

    public Vector3 GetRandomPointTest(Vector3 firstMousePos, Vector3 secondMousePos)
    {
        RandomPointsForSubdivision[0] = firstMousePos;
        RandomPointsForSubdivision[1] = secondMousePos;
        //firstMousePos = mousePosOne;
        //secondMousePos = mousePosTwo;
        float distance = Vector3.Distance(firstMousePos, secondMousePos);
        Vector3 firstPoint = firstMousePos + 0 * (secondMousePos - firstMousePos);
        Vector3 secondPoint = secondMousePos + 0 * (firstMousePos - secondMousePos);

        GameObject line = new GameObject("Pro Gen Line"); 
        //line.transform.position = RandomPosition;
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .5f;
        lineRenderer.SetPosition(0, firstPoint);
        lineRenderer.SetPosition(1, firstPoint + Vector3.forward);
        
        Debug.Log(GetRandomPointTest(firstPoint, secondPoint));
        return GetRandomPointTest(firstPoint, secondPoint);
    }

    public void GetRandomPoint()//(Vector3 localUp)
    {
        //Vector3 distance = new Vector3();
        //Vector3 randomPoint = new Vector3();
        //distance = mousePosOne - mousePosTwo;
        // randomPoint = Random.value(distance);
        
        /*Vector3 RandomPosition = new Vector3(Random.Range(mousePosOne.x, mousePosTwo.x), 
                                             Random.Range(mousePosOne.y, mousePosTwo.y), 
                                             Random.Range(mousePosOne.z, mousePosTwo.z));*/

        Vector3 RandomPosition = new Vector3(
            Random.Range(RandomPointsForSubdivision[0].x, RandomPointsForSubdivision[1].x),
            Random.Range(RandomPointsForSubdivision[0].y, RandomPointsForSubdivision[1].y),
            0);//Random.Range(RandomPointsForSubdivision[0].z, RandomPointsForSubdivision[1].z));

        //this.localUp = localUp;
        //axisA = new Vector3(localUp.y, localUp.z, localUp.x); // swapping xyz coordinates of localUp to calculate a vector perpendicular to it
        //axisB = Vector3.Cross(localUp, axisA); // using cross product of localUp and axisA to calculate a vector which is perpendicular to both
        
        //Vector3 midPoint = (pointA + pointB) / 2f;
        //Vector3 PointC = midPoint + Quaternion.AngleAxis(90.0f, Vector3.forward) * (PointB - midPoint).normalized;

        Vector3 randomPointOnLine = Vector3.Lerp(RandomPointsForSubdivision[0], RandomPointsForSubdivision[1], Random.value);// 0.3f);
        
        Vector3 distance = new Vector3();
        distance = RandomPointsForSubdivision[0] - RandomPointsForSubdivision[1];
        float floatDistance = Vector3.Distance(RandomPointsForSubdivision[0], RandomPointsForSubdivision[1]);
        Vector3 newPoint = new Vector3(RandomPosition.x, RandomPosition.y,RandomPosition.z);
        //Vector3 pointToLeft = newPoint + Quaternion.AngleAxis(90.0f, Vector3.forward) * (RandomPointsForSubdivision[1] - newPoint).normalized;
        Vector3 pointToRight = randomPointOnLine + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                     * (RandomPointsForSubdivision[1] - randomPointOnLine).normalized 
                                     * (floatDistance / 4);
        //Vector3 endPointToRight = pointToRight + distance/2;
        
        Vector3 pointToLeft = randomPointOnLine + Quaternion.AngleAxis(- 90.0f, Vector3.forward)
                               * (RandomPointsForSubdivision[1] - randomPointOnLine).normalized 
                               * (floatDistance / 4);
       // Vector3 endPointToLeft = pointToRight + distance/2;
        
       // Debug.Log(newPoint);
       // Debug.Log(endPointToLeft);
        
        GameObject proGenRightLine = new GameObject("Pro Gen Line To Right"); 
        //line.transform.position = RandomPosition;
        LineRenderer rightLineRenderer = proGenRightLine.AddComponent<LineRenderer>();
        rightLineRenderer.material = lineMaterial;
        rightLineRenderer.startWidth = .1f;
        rightLineRenderer.endWidth = .1f;
        rightLineRenderer.SetPosition(0, randomPointOnLine);//newPoint);
        rightLineRenderer.SetPosition(1, pointToRight);// endPointToLeft);
        
        GameObject proGenLeftLine = new GameObject("Pro Gen Line To Right"); 
        //line.transform.position = RandomPosition;
        LineRenderer leftLineRenderer = proGenLeftLine.AddComponent<LineRenderer>();
        leftLineRenderer.material = lineMaterial;
        leftLineRenderer.startWidth = .1f;
        leftLineRenderer.endWidth = .1f;
        leftLineRenderer.SetPosition(0, randomPointOnLine);//newPoint);
        leftLineRenderer.SetPosition(1, pointToLeft);// endPointToLeft);
        
        //Vector3 distance = new Vector3();
        //Vector3 randomPoint = new Vector3();
        //distance = mousePosOne - mousePosTwo;
        
       // Vector3 redPos = new Vector3(0,0,0);
       // Vector3 bluePos = new Vector3(6,3,6);
       // Vector3 dir = bluePos - redPos;
       // float distance = Vector3.Distance(redPos , bluePos);
 
       // Vector3 oneThird = redPos + dir * (distance * 0.3f);

    }


}
