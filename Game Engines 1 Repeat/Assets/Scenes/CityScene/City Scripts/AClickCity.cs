using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class AClickCity : MonoBehaviour
{

    //private List<GameObject> pointsObjects;
    bool theFirstListHasBeenDrawn = false;
    bool theSecondListHasBeenDrawn = false;
    
    List<Vector3> SecondPointsForSubdivision = new List<Vector3>();
    List<Vector3> ThirdPointsForSubdivision = new List<Vector3>();
    

    public bool pointOneIsSet;

    public List<Vector3> TempSavedPairedPoints = new List<Vector3>();
    public List<Vector3> SavedPairedPoints = new List<Vector3>();

    public List<Vector3> RandomPointsForSubdivision = new List<Vector3>();
    public List<Vector3> NewPointsForSubdivision = new List<Vector3>();

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
            if (!theFirstListHasBeenDrawn)
            {
                MakeSetAndSubdivideEverythingInItOne();
            }
            
            else if (theFirstListHasBeenDrawn && !theSecondListHasBeenDrawn)
            {
                MakeSetAndSubdivideEverythingInItTwo(SecondPointsForSubdivision);
               
               // MakeSetAndSubdivideEverythingInItOne();
            }
            
            else if (theFirstListHasBeenDrawn && theSecondListHasBeenDrawn)
            {
                MakeSetAndSubdivideEverythingInItThree(ThirdPointsForSubdivision);
                
               // MakeSetAndSubdivideEverythingInItTwo(SecondPointsForSubdivision);
               // MakeSetAndSubdivideEverythingInItOne();
                //MakeSetAndSubdivideEverythingInItOne();
            }
        }
        
       /* if (Input.GetKeyDown("space") && !theFirstListHasBeenDrawn)// && !theSecondListHasBeenDrawn)
        {
            MakeSetAndSubdivideEverythingInItOne();

        }
        else if (Input.GetKeyDown("space") && theFirstListHasBeenDrawn && !theSecondListHasBeenDrawn)
        {
            MakeSetAndSubdivideEverythingInItTwo(SecondPointsForSubdivision);
        }
       /* else if (Input.GetKeyDown("space") && theFirstListHasBeenDrawn && theSecondListHasBeenDrawn)
        {
            MakeSetAndSubdivideEverythingInItThree(ThirdPointsForSubdivision);
        }*/

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
    
    /*public bool randomIgnoreRuleModifier = true;
   
    [Range(0, 1)] 
    public float chanceToIgnoreRule = .3f;
        if (randomIgnoreRuleModifier)
    {
        if (UnityEngine.Random.value < chanceToIgnoreRule)
        {
            return;
        }
    }*/
    
    public void MakeSetAndSubdivideEverythingInItOne()
    {
        //bool theFirstListHasBeenDrawn = false;
        int lineIndex = 0;

        for (int i = 0; i < RandomPointsForSubdivision.Count - 1; i++)
        {
                Vector3 randomPointForRightLine = Vector3.Lerp(RandomPointsForSubdivision[i], RandomPointsForSubdivision[i + 1], Random.value); // 0.3f);
                Vector3 randomPointForLeftLine = Vector3.Lerp(RandomPointsForSubdivision[i], RandomPointsForSubdivision[i + 1], Random.value);

                float floatDistance =
                    Vector3.Distance(RandomPointsForSubdivision[i], RandomPointsForSubdivision[i + 1]);

                Vector3 pointToRight = randomPointForRightLine + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                       * (RandomPointsForSubdivision[i + 1] - randomPointForRightLine).normalized
                                       * (floatDistance / 4);

                Vector3 pointToLeft = randomPointForLeftLine + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                                      * (RandomPointsForSubdivision[i + 1] - randomPointForLeftLine).normalized
                                      * (floatDistance / 4);

                // float NewRightLineFloatDistance = Vector3.Distance(randomPointOnLine, pointToRight);
                // float NewLeftLineFloatDistance = Vector3.Distance(randomPointOnLine, pointToLeft);

                Vector3 rightDistance = new Vector3();
                rightDistance = randomPointForRightLine - pointToRight;

                Vector3 leftDistance = new Vector3();
                leftDistance = randomPointForLeftLine - pointToLeft;

                //List<Vector3> SecondPointsForSubdivision = new List<Vector3>();
                // SecondPointsForSubdivision.Add(rightDistance); // this and next do not give expected results
                // SecondPointsForSubdivision.Add(leftDistance);
                SecondPointsForSubdivision.Add(randomPointForRightLine); // this and next result in desired behaviour unexpectedly but only if one original line created
                SecondPointsForSubdivision.Add(pointToRight);
                SecondPointsForSubdivision.Add(randomPointForLeftLine); // probably
                SecondPointsForSubdivision.Add(pointToLeft);

                //for (int j = 0; j < NewPointsForSubdivision.Count; j++)
                // foreach (var street in RandomPointsForSubdivision)//doesnt seem to help
                // {
                GameObject proGenRightLineOne = new GameObject("Pro Gen Line To Right");
                //line.transform.position = RandomPosition;
                LineRenderer rightLineRendererOne = proGenRightLineOne.AddComponent<LineRenderer>();
                rightLineRendererOne.material = lineMaterial;
                rightLineRendererOne.startWidth = .05f;
                rightLineRendererOne.endWidth = .05f;
                rightLineRendererOne.SetPosition(0, randomPointForRightLine); //newPoint);
                rightLineRendererOne.SetPosition(1, pointToRight); // endPointToLeft);

                GameObject proGenLeftLineOne = new GameObject("Pro Gen Line To Right");
                //line.transform.position = RandomPosition;
                LineRenderer leftLineRendererOne = proGenLeftLineOne.AddComponent<LineRenderer>();
                leftLineRendererOne.material = lineMaterial;
                leftLineRendererOne.startWidth = .05f;
                leftLineRendererOne.endWidth = .05f;
                leftLineRendererOne.SetPosition(0, randomPointForLeftLine); //newPoint);
                leftLineRendererOne.SetPosition(1, pointToLeft); //-pointToRight);//pointToLeft);// endPointToLeft);

                i++; // dont know exactly what this doing or if I need it - turns out this is what makes everything work like i want
                // i += 1; // breaks game when more items added to list
                // i += 2;// game doesnt crash when items added with this, but doesnt work like i want

                //theFirstListHasBeenDrawn = true;
           // }
        }
        
        theFirstListHasBeenDrawn = true;
    }

    public void MakeSetAndSubdivideEverythingInItTwo(List<Vector3> TheSecondList)
    {

        //bool theSecondListHasBeenDrawn = false; // needs to be global
        int lineIndex = 0;

        for (int i = 0; i < TheSecondList.Count - 1; i++)
        {
            Vector3 randomPointForRightLine = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], Random.value);
            Vector3 randomPointForLeftLine = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], Random.value);

            float floatDistance = Vector3.Distance(TheSecondList[i], TheSecondList[i + 1]);

            Vector3 pointToRight = randomPointForRightLine + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                   * (TheSecondList[i + 1] - randomPointForRightLine).normalized
                                   * (floatDistance / 2);

            Vector3 pointToLeft = randomPointForLeftLine + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                                  * (TheSecondList[i + 1] - randomPointForLeftLine).normalized
                                  * (floatDistance / 2);

            //float NewRightLineFloatDistance = Vector3.Distance(randomPointOnLine, pointToRight);
            //float NewLeftLineFloatDistance = Vector3.Distance(randomPointOnLine, pointToLeft);

           // Vector3 rightDistance = new Vector3();
           // rightDistance = randomPointOnLine - pointToRight;

           // Vector3 leftDistance = new Vector3();
            //leftDistance = randomPointOnLine - pointToLeft;

            //List<Vector3> ThirdPointsForSubdivision = new List<Vector3>();
            ThirdPointsForSubdivision.Add(randomPointForRightLine);
            ThirdPointsForSubdivision.Add(pointToRight);
            ThirdPointsForSubdivision.Add(randomPointForLeftLine); // probably
            ThirdPointsForSubdivision.Add(pointToLeft);

            //for (int j = 0; j < NewPointsForSubdivision.Count; j++)
            // foreach (var street in RandomPointsForSubdivision)//doesnt seem to help
            // {
            GameObject proGenRightLineOne = new GameObject("Pro Gen Line To Right");
            //line.transform.position = RandomPosition;
            LineRenderer rightLineRendererOne = proGenRightLineOne.AddComponent<LineRenderer>();
            rightLineRendererOne.material = lineMaterial;
            rightLineRendererOne.startWidth = .05f;
            rightLineRendererOne.endWidth = .05f;
            rightLineRendererOne.SetPosition(0, randomPointForRightLine); //newPoint);
            rightLineRendererOne.SetPosition(1, pointToRight); // endPointToLeft);

            GameObject proGenLeftLineOne = new GameObject("Pro Gen Line To Right");
            //line.transform.position = RandomPosition;
            LineRenderer leftLineRendererOne = proGenLeftLineOne.AddComponent<LineRenderer>();
            leftLineRendererOne.material = lineMaterial;
            leftLineRendererOne.startWidth = .05f;
            leftLineRendererOne.endWidth = .05f;
            leftLineRendererOne.SetPosition(0, randomPointForLeftLine); //newPoint);
            leftLineRendererOne.SetPosition(1, pointToLeft); //-pointToRight);//pointToLeft);// endPointToLeft);

            i++;
            // theSecondListHasBeenDrawn = true;
        }
        theSecondListHasBeenDrawn = true;
    }
    
    public void MakeSetAndSubdivideEverythingInItThree(List<Vector3> TheThirdList)
    {
        int lineIndex = 0;

        for (int i = 0; i < TheThirdList.Count - 1; i++)
        {
            Vector3 randomPointForRightLine = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], Random.value);
            Vector3 randomPointForLeftLine = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], Random.value);

            float floatDistance = Vector3.Distance(TheThirdList[i], TheThirdList[i + 1]);

            Vector3 pointToRight = randomPointForRightLine + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                   * (TheThirdList[i + 1] - randomPointForRightLine).normalized
                                   * (floatDistance / 3);

            Vector3 pointToLeft = randomPointForLeftLine + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                                  * (TheThirdList[i + 1] - randomPointForLeftLine).normalized
                                  * (floatDistance / 3);

           // float NewRightLineFloatDistance = Vector3.Distance(randomPointOnLine, pointToRight);
            //float NewLeftLineFloatDistance = Vector3.Distance(randomPointOnLine, pointToLeft);

           // Vector3 rightDistance = new Vector3();
            //rightDistance = randomPointOnLine - pointToRight;

           // Vector3 leftDistance = new Vector3();
           // leftDistance = randomPointOnLine - pointToLeft;

            //List<Vector3> ThirdPointsForSubdivision = new List<Vector3>();
            //ThirdPointsForSubdivision.Add(randomPointOnLine);
           // ThirdPointsForSubdivision.Add(pointToRight);
            //ThirdPointsForSubdivision.Add(randomPointOnLine); // probably
            //ThirdPointsForSubdivision.Add(pointToLeft);

            //for (int j = 0; j < NewPointsForSubdivision.Count; j++)
            // foreach (var street in RandomPointsForSubdivision)//doesnt seem to help
            // {
            GameObject proGenRightLineOne = new GameObject("Pro Gen Line To Right");
            //line.transform.position = RandomPosition;
            LineRenderer rightLineRendererOne = proGenRightLineOne.AddComponent<LineRenderer>();
            rightLineRendererOne.material = lineMaterial;
            rightLineRendererOne.startWidth = .05f;
            rightLineRendererOne.endWidth = .05f;
            rightLineRendererOne.SetPosition(0, randomPointForRightLine); //newPoint);
            rightLineRendererOne.SetPosition(1, pointToRight); // endPointToLeft);

            GameObject proGenLeftLineOne = new GameObject("Pro Gen Line To Right");
            //line.transform.position = RandomPosition;
            LineRenderer leftLineRendererOne = proGenLeftLineOne.AddComponent<LineRenderer>();
            leftLineRendererOne.material = lineMaterial;
            leftLineRendererOne.startWidth = .05f;
            leftLineRendererOne.endWidth = .05f;
            leftLineRendererOne.SetPosition(0, randomPointForLeftLine); //newPoint);
            leftLineRendererOne.SetPosition(1, pointToLeft); //-pointToRight);//pointToLeft);// endPointToLeft);

            i++;

            //theSecondListHasBeenDrawn = true;
        }
        //theSecondListHasBeenDrawn = true;
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

    public void ControlPoints(List<Vector3>linesToBeSubdivided)
    {
        // I want to make it so several random lines can be drawn all along any line but never from same point
        // The possibility to spawn these new lines ideally should depend on the length of the line being spawned from
        // Accounting for all points along a line means I can spawn in buildings wherever there are no lines
        // Just need to figure out a check for this
        // So now when player clicks subdivide any already existing lines sprout new lines depending on how long the lines are
        // if the lines are less than a certain length the no longer sprout
        // maybe I should consider screen height and width and make sprouted lines subject to these parameters
        // Ideally lines would not sprout if they were to cross other already existing lines
        
        for (int i = 0; i < linesToBeSubdivided.Count - 1; i++)
        {
            Vector3 onePoint = Vector3.Lerp(linesToBeSubdivided[i], linesToBeSubdivided[i + 1], 0.1f);
            Vector3 twoPoint = Vector3.Lerp(linesToBeSubdivided[i], linesToBeSubdivided[i + 1], 0.2f);
            Vector3 threePoint = Vector3.Lerp(linesToBeSubdivided[i], linesToBeSubdivided[i + 1], 0.3f);
            Vector3 fourPoint = Vector3.Lerp(linesToBeSubdivided[i], linesToBeSubdivided[i + 1], 0.4f);
            Vector3 fivePoint = Vector3.Lerp(linesToBeSubdivided[i], linesToBeSubdivided[i + 1], 0.5f);
            Vector3 sixPoint = Vector3.Lerp(linesToBeSubdivided[i], linesToBeSubdivided[i + 1], 0.6f);
            Vector3 sevenPoint = Vector3.Lerp(linesToBeSubdivided[i], linesToBeSubdivided[i + 1], 0.7f);
            Vector3 eightPoint = Vector3.Lerp(linesToBeSubdivided[i], linesToBeSubdivided[i + 1], 0.8f);
            Vector3 ninePoint = Vector3.Lerp(linesToBeSubdivided[i], linesToBeSubdivided[i + 1], 0.9f);
            
            List<Vector3> AllPoints = new List<Vector3>();
            
            
            Vector3 randomPointForRightLine = Vector3.Lerp(RandomPointsForSubdivision[i], RandomPointsForSubdivision[i + 1], Random.value); // 0.3f);
            Vector3 randomPointForLeftLine = Vector3.Lerp(RandomPointsForSubdivision[i], RandomPointsForSubdivision[i + 1], Random.value);
        }
            
    }
}
