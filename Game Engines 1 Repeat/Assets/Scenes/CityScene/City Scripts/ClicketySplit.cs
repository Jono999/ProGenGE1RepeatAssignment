using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicketySplit : MonoBehaviour
{
    public bool pointOneIsSet;

    public List<Vector3> TempSavedPairedPoints = new List<Vector3>();
    public List<Vector3> SavedPairedPoints = new List<Vector3>();

    public List<Vector3> RandomPointsForSubdivision = new List<Vector3>();

    public Vector3 mousePosOne;
    public Vector3 mousePosTwo;

    public Material lineMaterial;

    private Vector3[] pairOfPoints;
    
    int lineIndex = 0;

    Vector3 randomPointOnLine;

    Vector3 pointToRight;

    Vector3 pointToLeft;

    float floatDistance;

    public List<List<Vector3>> allTheLinesTempList = new List<List<Vector3>>();
    
    public List<Vector3> newLinesTempList = new List<Vector3>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        pointOneIsSet = false;

        pairOfPoints = new Vector3[2];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //GetRandomPoint();
            //MakeSetAndSubdivideEverythingInIt();

            //TakeNMake(allTheLinesTempList, RandomPointsForSubdivision, newLinesTempList);
            //TakeNMake(allTheLinesTempList, RandomPointsForSubdivision, newLinesTempList);

            TakeListAndMakeLines(allTheLinesTempList);
        }

        if (Input.GetMouseButtonDown(0) && !pointOneIsSet)
        {
            mousePosOne = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosOne.z = 0;

            Vector3 startPoint = new Vector3(mousePosOne.x, mousePosOne.y, mousePosOne.z);
            
            TempSavedPairedPoints.Add(startPoint);
            SavedPairedPoints.Add(startPoint);
            RandomPointsForSubdivision.Add(startPoint);
            
            //allTheLinesTempList.Add(RandomPointsForSubdivision); dont add this now, surely it would set index out of whack right?

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
        
        allTheLinesTempList.Add(RandomPointsForSubdivision);

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

    public void SuperSubdivide(List<List<Vector3>> allTheLines, List<Vector3> linesToBeSubDNow, List<Vector3> linesToBeSubDNext)
    {
        for (int i = 0; i < linesToBeSubDNow.Count - 1; i++)
        {
            Vector3 randomPoint = Vector3.Lerp(linesToBeSubDNow[i], linesToBeSubDNow[i + 1], Random.value);
            float distance = Vector3.Distance(linesToBeSubDNow[i], linesToBeSubDNow[i + 1]);
            Vector3 pointToTheRight = randomPoint + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                      * (linesToBeSubDNow[i] - randomPoint).normalized
                                      * (distance / 4);

            Vector3 pointToTheLeft = randomPoint + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                                     * (linesToBeSubDNow[i] - randomPoint).normalized
                                     * (distance / 4);
            
            Vector3 newRightLine = new Vector3();
            newRightLine = randomPoint - pointToTheRight;
            
            Vector3 newLeftLine = new Vector3();
            newLeftLine = randomPoint - pointToTheLeft;
            
            linesToBeSubDNext.Add(newRightLine);
            linesToBeSubDNext.Add(newLeftLine);
            
            DrawLines(linesToBeSubDNow);
        }
    }
    
    /*public void GetRandomPoint(Vector3 pointOne, Vector3 pointTwo)
    {
        Vector3 randomPoint = Vector3.Lerp(pointOne, pointTwo, Random.value);
        float distance = Vector3.Distance(pointOne, pointTwo);
    }*/

    //public void Subdivide(Vector3 pointOne, Vector3 pointTwo, Vector3 randomPoint, Vector3 pointToTheRight, Vector3 pointToTheLeft, float distance)
    public void Subdivide(List<Vector3> linesToBeSubDNow, List<Vector3> linesToBeSubDNext, Vector3 pointOne, Vector3 pointTwo)
               //and maybe a list of v3 points and do all below in a loop for each element
               //and why have points separated from the lists that they will be in? figure 
               //looks like im doing the same thing separately in 2 different ways within the same method - figure out which suits best and use only it
               // i.e points(vector 3s) or indexing the lists of vector 3s
               // i think leave it for now then refactor once, and if, every is working
    {
        //linesToBeSubDNow = RandomPointsForSubdivision;// update this name shortly - not just name but exactly what gets assigned here
        for (int i = 0; i < linesToBeSubDNow.Count - 1; i++)
        {
            Vector3 randomPoint = Vector3.Lerp(linesToBeSubDNow[i], linesToBeSubDNow[i + 1], Random.value);
            float distance = Vector3.Distance(linesToBeSubDNow[i], linesToBeSubDNow[i + 1]);
            Vector3 pointToTheRight = randomPoint + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                      * (linesToBeSubDNow[i] - randomPoint).normalized
                                      * (distance / 4);

            Vector3 pointToTheLeft = randomPoint + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                                     * (linesToBeSubDNow[i] - randomPoint).normalized
                                     * (distance / 4);
            
            Vector3 newRightLine = new Vector3();
            newRightLine = randomPoint - pointToTheRight;
            
            Vector3 newLeftLine = new Vector3();
            newLeftLine = randomPoint - pointToTheLeft;
            
            linesToBeSubDNext.Add(newRightLine);
            linesToBeSubDNext.Add(newLeftLine);
            
            DrawLines(linesToBeSubDNow);
        }
      
        
       /* Vector3 randomPoint = Vector3.Lerp(pointOne, pointTwo, Random.value);
        float distance = Vector3.Distance(pointOne, pointTwo);

        Vector3 pointToTheRight = randomPoint + Quaternion.AngleAxis(90.0f, Vector3.forward)
                          * (pointOne - randomPoint).normalized
                          * (distance / 4);

        Vector3 pointToTheLeft = randomPoint + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                         * (pointOne - randomPoint).normalized
                         * (distance / 4);
        
        //float newRightLineFloatDistance = Vector3.Distance(randomPoint, pointToTheRight);//do i need this and next?
        //float newLeftLineFloatDistance = Vector3.Distance(randomPoint, pointToTheLeft);
            
        Vector3 newRightLine = new Vector3();
        newRightLine = randomPoint - pointToTheRight;
            
        Vector3 newLeftLine = new Vector3();
        newLeftLine = randomPoint - pointToTheLeft;
            
        linesToBeSubDNext.Add(newRightLine);
        linesToBeSubDNext.Add(newLeftLine);*/

    }

    public void TakeListAndMakeLines(List<List<Vector3>> allTheLines)
    {
        List<Vector3> currentLines = new List<Vector3>();
        List<Vector3> newLines = new List<Vector3>();   
        int theIndex = 0;

        for (int i = 0; i < allTheLines.Count - 1; i++)
        { 
           /* Subdivide(allTheLines[theIndex],
                      newLines, 
                      allTheLines[theIndex][i], 
                      allTheLines[theIndex][i + 1]);*/
            Subdivide(RandomPointsForSubdivision,
                newLines, 
                RandomPointsForSubdivision[i], 
                RandomPointsForSubdivision[i + 1]);
            
           // DrawLines(allTheLines[theIndex]);
            //allTheLines.Add(newLines);
            theIndex++;
            //DrawLines(allTheLines[theIndex]);
        }
        allTheLines.Add(newLines);
    }

   // public void TakeNMake(List<List<Vector3>> allTheLines) // maybe try this tomorrow
        //List<Vector3> currentLines = new List<Vector3>();
        //List<Vector3> newLines = new List<Vector3>();
    //public void TakeNMake(int theIndex, List<List<Vector3>> allTheLines, List<Vector3> currentLines, List<Vector3> newLines)//may not need the index here
    public void TakeNMake(List<List<Vector3>> allTheLines, List<Vector3> currentLines, List<Vector3> newLines)
    {
        int theIndex = 0;
        
        allTheLines.RemoveRange(0,allTheLines.Count);
        
        allTheLines.Add(currentLines);
        Subdivide(currentLines, newLines,allTheLines[theIndex][0], allTheLines[theIndex][1]);
        DrawLines(allTheLines[theIndex]);
        //newLines.RemoveRange(0,newLines.Count);
       // newLines.Add(currentLines[0]);
        allTheLines.Add(newLines);
        theIndex++;
        Subdivide(currentLines, newLines,allTheLines[theIndex][0], allTheLines[theIndex][1]);
        DrawLines(allTheLines[theIndex]);
        //theIndex++;
       // DrawLines(allTheLines[theIndex]);
        
        //   current lines.add the list of originally saved points 
        //currentLines = RandomPointsForSubdivision;
       //// allTheLines.Add(currentLines);
        //int listOfListsIndexPresently = 0;
       //// int theIndex = 0;//allTheLines.Count - 1;//0;// listOfListsIndexPresently; //0;
       //// for (int i = 0; i < allTheLines.Count - 1; i++)
       //// {
            //theIndex = i;
            //Subdivide(currentLines[i], currentLines[i + 1]);
           //// Subdivide(currentLines, newLines,allTheLines[theIndex][i], allTheLines[theIndex][i + 1]);
            //Subdivide(currentLines, newLines,allTheLines[i][i], allTheLines[i][i + 1]);
           //// DrawLines(currentLines);
            //allTheLines.Add(newLines);// how to get and set values for newLines - got them now - well do i? - yes i think above, not below
           // theIndex += 1;// believe this and above should happen outside the loop
           //listOfListsIndexPresently += 1;

            /*foreach (Vector3 pointCoordinates in currentLines)
            {
            
            }*/
            //public void Subdivide(Vector3 pointOne, Vector3 pointTwo, Vector3 randomPoint, Vector3 pointToTheRight, Vector3 pointToTheLeft, float distance)
            //Vector3 newRandomPointOnLine = Vector3.Lerp(NewPointsForSubdivision[i], NewPointsForSubdivision[i+1], Random.value);
            
            //dont believe i need the below here
           /* float NewRightLineFloatDistance = Vector3.Distance(randomPointOnLine, pointToRight);
            float NewLeftLineFloatDistance = Vector3.Distance(randomPointOnLine, pointToLeft);
            
            Vector3 rightDistance = new Vector3();
            rightDistance = randomPointOnLine - pointToRight;
            
            Vector3 leftDistance = new Vector3();
            leftDistance = randomPointOnLine - pointToLeft;
            
            List<Vector3> NewPointsForSubdivision = new List<Vector3>();
            NewPointsForSubdivision.Add(rightDistance);
            NewPointsForSubdivision.Add(leftDistance);*/
          //// allTheLines.Add(newLines);
          //// theIndex++;
          //// i++;
          // allTheLines.Add(newLines);
       //// }
        
        //allTheLines.Add(newLines);
        //listOfListsIndexPresently += 1; //theIndex += 1;// believe this and above should happen outside the loop
        //theIndex += 1;
    }

    public void DrawLines(List<Vector3>linesToBeSubdivided)
    {
        for (int i = 0; i < linesToBeSubdivided.Count - 1; i++)
        {
            GameObject proGenRightLine = new GameObject("Pro Gen Line To Right"); 
            //line.transform.position = RandomPosition;
            LineRenderer rightLineRenderer = proGenRightLine.AddComponent<LineRenderer>();
            rightLineRenderer.material = lineMaterial; rightLineRenderer.startWidth = .1f;
            rightLineRenderer.endWidth = .1f;
            rightLineRenderer.SetPosition(0, linesToBeSubdivided[i]);// randomPointOnLine);//newPoint);
            rightLineRenderer.SetPosition(1, linesToBeSubdivided[i + 1]);//pointToRight);// endPointToLeft);
        
           /* GameObject proGenLeftLine = new GameObject("Pro Gen Line To Left"); 
            //line.transform.position = RandomPosition;
            LineRenderer leftLineRenderer = proGenLeftLine.AddComponent<LineRenderer>();
            leftLineRenderer.material = lineMaterial;
            leftLineRenderer.startWidth = .1f;
            leftLineRenderer.endWidth = .1f;
            leftLineRenderer.SetPosition(0, randomPointOnLine);//newPoint);
            leftLineRenderer.SetPosition(1, pointToLeft);// endPointToLeft);*/
        }
    }
}
