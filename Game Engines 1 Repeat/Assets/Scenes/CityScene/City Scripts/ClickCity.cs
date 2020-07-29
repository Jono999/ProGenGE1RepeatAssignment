using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCity : MonoBehaviour
{

    //private List<GameObject> pointsObjects;

    public bool pointOneIsSet;

    public List<Vector3> TempSavedPairedPoints = new List<Vector3>();
    public List<Vector3> SavedPairedPoints = new List<Vector3>();

    public List<Vector3> RandomPointsForSubdivision = new List<Vector3>();

    public Vector3 mousePosOne;
    public Vector3 mousePosTwo;

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
            //GetRandomPoint();
            MakeSetAndSubdivideEverythingInIt();
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
        Vector3 RandomPosition = new Vector3(
            Random.Range(RandomPointsForSubdivision[0].x, RandomPointsForSubdivision[1].x),
            Random.Range(RandomPointsForSubdivision[0].y, RandomPointsForSubdivision[1].y),
            0);//Random.Range(RandomPointsForSubdivision[0].z, RandomPointsForSubdivision[1].z));

        Vector3 randomPointOnLine = Vector3.Lerp(RandomPointsForSubdivision[0], RandomPointsForSubdivision[1], Random.value);// 0.3f);
        
       // Vector3 distance = new Vector3();
       // distance = RandomPointsForSubdivision[0] - RandomPointsForSubdivision[1];
       // Vector3 newPoint = new Vector3(RandomPosition.x, RandomPosition.y,RandomPosition.z);
        
        float floatDistance = Vector3.Distance(RandomPointsForSubdivision[0], RandomPointsForSubdivision[1]);
        
        Vector3 pointToRight = randomPointOnLine + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                     * (RandomPointsForSubdivision[1] - randomPointOnLine).normalized 
                                     * (floatDistance / 4);

        Vector3 pointToLeft = randomPointOnLine + Quaternion.AngleAxis(- 90.0f, Vector3.forward)
                                      * (RandomPointsForSubdivision[1] - randomPointOnLine).normalized 
                                      * (floatDistance / 4);

        GameObject proGenRightLine = new GameObject("Pro Gen Line To Right"); 
        //line.transform.position = RandomPosition;
        LineRenderer rightLineRenderer = proGenRightLine.AddComponent<LineRenderer>();
        rightLineRenderer.material = lineMaterial;
        rightLineRenderer.startWidth = .1f;
        rightLineRenderer.endWidth = .1f;
        rightLineRenderer.SetPosition(0, randomPointOnLine);//newPoint);
        rightLineRenderer.SetPosition(1, pointToRight);// endPointToLeft);
        
        GameObject proGenLeftLine = new GameObject("Pro Gen Line To Left"); 
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

    public void MakeSetAndSubdivideEverythingInIt()
    {
        int lineIndex = 0;

        Vector3 randomPointOnLine;

        Vector3 pointToRight;

        Vector3 pointToLeft;

        float floatDistance;
        
        for (int i = 0; i < RandomPointsForSubdivision.Count - 1; i++)
        {
            /* Vector3 RandomPosition = new Vector3(
            Random.Range(RandomPointsForSubdivision[i].x, RandomPointsForSubdivision[i+1].x),
            Random.Range(RandomPointsForSubdivision[i].y, RandomPointsForSubdivision[i+1].y),
            0);//Random.Range(RandomPointsForSubdivision[0].z, RandomPointsForSubdivision[1].z));*/

            // Vector3 distance = new Vector3();
            // distance = RandomPointsForSubdivision[0] - RandomPointsForSubdivision[1];
            // Vector3 newPoint = new Vector3(RandomPosition.x, RandomPosition.y,RandomPosition.z);
            
            //Vector3 randomPointOnLine = Vector3.Lerp(SavedPairedPoints[i], SavedPairedPoints[i+1], Random.value);// 0.3f);
            
            randomPointOnLine = Vector3.Lerp(RandomPointsForSubdivision[i], RandomPointsForSubdivision[i+1], Random.value);// 0.3f);
        
            floatDistance = Vector3.Distance(RandomPointsForSubdivision[i], RandomPointsForSubdivision[i+1]);
        
            pointToRight = randomPointOnLine + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                                     * (RandomPointsForSubdivision[i+1] - randomPointOnLine).normalized 
                                                     * (floatDistance / 4);

            pointToLeft = randomPointOnLine + Quaternion.AngleAxis(- 90.0f, Vector3.forward)
                                                    * (RandomPointsForSubdivision[i+1] - randomPointOnLine).normalized 
                                                    * (floatDistance / 4);
            
            
            //Vector3 newRandomPointOnLine = Vector3.Lerp(NewPointsForSubdivision[i], NewPointsForSubdivision[i+1], Random.value);
            float NewRightLineFloatDistance = Vector3.Distance(randomPointOnLine, pointToRight);
            float NewLeftLineFloatDistance = Vector3.Distance(randomPointOnLine, pointToLeft);
            
            Vector3 rightDistance = new Vector3();
            rightDistance = randomPointOnLine - pointToRight;
            
            Vector3 leftDistance = new Vector3();
            leftDistance = randomPointOnLine - pointToLeft;
            
            List<Vector3> NewPointsForSubdivision = new List<Vector3>();
            NewPointsForSubdivision.Add(rightDistance);
            NewPointsForSubdivision.Add(leftDistance);
            
            //NewPointsForSubdivision[i] = rightDistance;
            //NewPointsForSubdivision.Add(RandomPointsForSubdivision[i]);
            //NewPointsForSubdivision.Add(randomPointOnLine); // probably
            //NewPointsForSubdivision.Add(pointToLeft);

            List<Vector3> makeLines = new List<Vector3>();
             makeLines.Add(randomPointOnLine);
             makeLines.Add(pointToRight);

             //RandomPointsForSubdivision.RemoveRange(0,RandomPointsForSubdivision.Count);
             //RandomPointsForSubdivision.Add(randomPointOnLine);
             //RandomPointsForSubdivision.Add(pointToRight);
            
            //List<GameObject> proGenLines = new List<GameObject>();
            /*for (int j = 0; j < proGenLines.Count; j++)
            {
                proGenLines[j] = new GameObject("Pro Gen Line To Right"); 
                //line.transform.position = RandomPosition;
                LineRenderer rightLineRenderer = proGenLines[j].AddComponent<LineRenderer>();
                rightLineRenderer.material = lineMaterial;
                rightLineRenderer.startWidth = .1f;
                rightLineRenderer.endWidth = .1f;
                rightLineRenderer.SetPosition(0, RandomPointsForSubdivision[i]);//randomPointOnLine);//newPoint);
                rightLineRenderer.SetPosition(1, RandomPointsForSubdivision[i +1]);//pointToRight);// endPointToLeft);
            }*/
            
            GameObject proGenRightLine = new GameObject("Pro Gen Line To Right"); 
            //line.transform.position = RandomPosition;
            LineRenderer rightLineRenderer = proGenRightLine.AddComponent<LineRenderer>();
            rightLineRenderer.material = lineMaterial;
            rightLineRenderer.startWidth = .1f;
            rightLineRenderer.endWidth = .1f;
            rightLineRenderer.SetPosition(0, randomPointOnLine);//RandomPointsForSubdivision [RandomPointsForSubdivision.Count - 2]);//makeLines[makeLines.Count -2]);//RandomPointsForSubdivision[i]);//randomPointOnLine);//newPoint);
            rightLineRenderer.SetPosition(1, pointToRight);//RandomPointsForSubdivision [RandomPointsForSubdivision.Count - 1]);//makeLines[makeLines.Count -1]);//RandomPointsForSubdivision[i +1]);//pointToRight);// endPointToLeft);*/
            i++;

            //RandomPointsForSubdivision.RemoveRange(0,RandomPointsForSubdivision.Count);
            //RandomPointsForSubdivision.Add(NewPointsForSubdivision[i]);

            /*for (int j = 0; j < NewPointsForSubdivision.Count - 1; j++)
            //foreach (var street in RandomPointsForSubdivision)//doesnt seem to help
            {
                GameObject proGenRightLineOne = new GameObject("Pro Gen Line To Right"); 
                //line.transform.position = RandomPosition;
                LineRenderer rightLineRendererOne = proGenRightLineOne.AddComponent<LineRenderer>();
                rightLineRendererOne.material = lineMaterial;
                rightLineRendererOne.startWidth = .1f;
                rightLineRendererOne.endWidth = .1f;
                rightLineRendererOne.SetPosition(0, NewPointsForSubdivision[j]);//newPoint);
                rightLineRendererOne.SetPosition(1, NewPointsForSubdivision[j+1]);// endPointToLeft);
                
                RandomPointsForSubdivision.RemoveRange(0,RandomPointsForSubdivision.Count); //doesnt work in this loop
                //RandomPointsForSubdivision.Add(street);
                RandomPointsForSubdivision.Add(NewPointsForSubdivision[j]);
                RandomPointsForSubdivision.Add(NewPointsForSubdivision[j+1]);

                //SavedPairedPoints.RemoveRange(0,SavedPairedPoints.Count);
                //SavedPairedPoints.Add(randomPointOnLine);
                //SavedPairedPoints.Add(pointToRight);
                //SavedPairedPoints.Add(randomPointOnLine);
                //SavedPairedPoints.Add(pointToLeft);
                
                //NewPointsForSubdivision.Remove(i);
                
                //RandomPointsForSubdivision.Add(randomPointOnLine);
                //RandomPointsForSubdivision.Add(pointToRight);
                //j += 2;

                //RandomPointsForSubdivision[lineIndex] = i;
                //lineIndex += 2;
                
                GameObject proGenLeftLineOne = new GameObject("Pro Gen Line To Right"); 
                //line.transform.position = RandomPosition;
                LineRenderer leftLineRendererOne = proGenLeftLineOne.AddComponent<LineRenderer>();
                leftLineRendererOne.material = lineMaterial;
                leftLineRendererOne.startWidth = .1f;
                leftLineRendererOne.endWidth = .1f;
                //leftLineRendererOne.SetPosition(0, NewPointsForSubdivision[j]);//randomPointOnLine);//newPoint);
                //leftLineRendererOne.SetPosition(1, NewPointsForSubdivision[j + 2]);//pointToLeft);//-pointToRight);//pointToLeft);// endPointToLeft);
                
                //RandomPointsForSubdivision.Add(randomPointOnLine);
               // RandomPointsForSubdivision.Add(pointToLeft);
               // j += 2;
            }
            RandomPointsForSubdivision.RemoveRange(0,RandomPointsForSubdivision.Count); //doesnt work in this loop
            //RandomPointsForSubdivision.Add(street);
            RandomPointsForSubdivision.Add(NewPointsForSubdivision[0]);
            RandomPointsForSubdivision.Add(NewPointsForSubdivision[1]);*/

            //RandomPointsForSubdivision.Add(randomPointOnLine);
            //RandomPointsForSubdivision.Add(pointToRight);

            //RandomPointsForSubdivision.RemoveRange(0,RandomPointsForSubdivision.Count);
            //RandomPointsForSubdivision[i] = rightDistance;
            //RandomPointsForSubdivision.Add(rightDistance);
            //RandomPointsForSubdivision.Add(leftDistance);

            //RandomPointsForSubdivision.RemoveRange(0,RandomPointsForSubdivision.Count);// this works but only for originally created line
            //RandomPointsForSubdivision.Add(randomPointOnLine);// i think i need to make a loop out of these elements. maybe i need a subdivide method?
            //RandomPointsForSubdivision.Add(pointToRight);
            //RandomPointsForSubdivision.Add(pointToLeft);

            /*for (int j = 0; j <= RandomPointsForSubdivision.Count; j++) // this loop has same effect as above
            {
                RandomPointsForSubdivision.RemoveRange(0,RandomPointsForSubdivision.Count);
                RandomPointsForSubdivision.Add(randomPointOnLine);
                RandomPointsForSubdivision.Add(pointToRight);
                RandomPointsForSubdivision.Add(randomPointOnLine);
                RandomPointsForSubdivision.Add(pointToLeft);
                //j += 2;
            }*/

            // RandomPointsForSubdivision.RemoveRange(0,RandomPointsForSubdivision.Count);
            // RandomPointsForSubdivision.Add(NewPointsForSubdivision[0]);
            // RandomPointsForSubdivision.Add(NewPointsForSubdivision[1]);

            /* GameObject proGenRightLine = new GameObject("Pro Gen Line To Right"); 
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
             leftLineRenderer.SetPosition(1, pointToLeft);//-pointToRight);//pointToLeft);// endPointToLeft);*/
            //this is original and works

            // RandomPointsForSubdivision[i] = rightDistance;

            //i++; // dont know exactly what this doing or if I need it
            // i += 1; // breaks game when more items added to list
            // i += 2;// game doesnt crash when items added with this, but doesnt work like i want
        }
        
        
        foreach (var street in RandomPointsForSubdivision)
        {
            //foreach (var position in fixRoadCandidates)
            //Dictionary<Vector3Int, GameObject> roadDictionary = new Dictionary<Vector3Int, GameObject>();
            //HashSet<Vector3Int> fixRoadCandidates = new HashSet<Vector3Int>();
            //List<Direction> neighbourDirections = PlacementHelper.FindNeighbour(position, roadDictionary.Keys);
            //Quaternion rotation = Quaternion.identity;
            //roadDictionary[position] = Instantiate(roadEnd, position, rotation, transform);

            //GetRandomPoint(); // just creates a bunch of intersections only on first line created all at once, putting all code
            // from the method here also does the same thing
            
        }
    }
}
