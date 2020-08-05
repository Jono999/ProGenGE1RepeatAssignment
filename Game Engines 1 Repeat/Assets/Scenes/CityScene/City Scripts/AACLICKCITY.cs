using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AACLICKCITY : MonoBehaviour
{
    //private List<GameObject> pointsObjects;
    bool theFirstListHasBeenDrawn = false;
    bool theSecondListHasBeenDrawn = false;

    private bool overallSubdivisionControl = true;
    
    List<Vector3> FirstPointsForSubdivision = new List<Vector3>();
    List<Vector3> SecondPointsForSubdivision = new List<Vector3>();
    List<Vector3> ThirdPointsForSubdivision = new List<Vector3>();
    
    List<Vector3> ListToSpawnBuildings = new List<Vector3>();
    

    public bool pointOneIsSet;

    public List<Vector3> TempSavedPairedPoints = new List<Vector3>();
    public List<Vector3> SavedPairedPoints = new List<Vector3>();

    public List<Vector3> RandomPointsForSubdivision = new List<Vector3>();
    public List<Vector3> NewPointsForSubdivision = new List<Vector3>();

    public Vector3 mousePosOne;
    public Vector3 mousePosTwo;

    public Material leftLineMaterial;
    public Material rightLineMaterial;

    public Material[] RoadMaterials = new Material[4];
    public Material[] HouseMaterials = new Material[3];

    private Vector3[] pairOfPoints;

    // Start is called before the first frame update
    void Start()
    {
        pointOneIsSet = false;
        pairOfPoints = new Vector3[2];
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnBuildings(ListToSpawnBuildings);
        }
        
        if (Input.GetKeyDown("space"))
        {
            if (!theFirstListHasBeenDrawn && overallSubdivisionControl)
            {
                //MakeSetAndSubdivideEverythingInItOne();
                
                ControlPoints(FirstPointsForSubdivision);
                
                //ListToAssignPoints(FirstPointsForSubdivision);
            }
            
            else if (theFirstListHasBeenDrawn && !theSecondListHasBeenDrawn && overallSubdivisionControl)
            {
               // ControlPoints(FirstPointsForSubdivision);
                
                MakeSetAndSubdivideEverythingInItTwo(SecondPointsForSubdivision);

                // MakeSetAndSubdivideEverythingInItOne();
            }
            
            else if (theFirstListHasBeenDrawn && theSecondListHasBeenDrawn && overallSubdivisionControl)
            {
               // ControlPoints(FirstPointsForSubdivision);

              //  MakeSetAndSubdivideEverythingInItTwo(SecondPointsForSubdivision);
                
                MakeSetAndSubdivideEverythingInItThree(ThirdPointsForSubdivision);

                //theFirstListHasBeenDrawn = false;

                //MakeSetAndSubdivideEverythingInItThree(ThirdPointsForSubdivision);

                //overallSubdivisionControl = false;

                //MakeSetAndSubdivideEverythingInItTwo(SecondPointsForSubdivision);
                //MakeSetAndSubdivideEverythingInItOne();
                //MakeSetAndSubdivideEverythingInItOne();
            }
        }

        if (Input.GetMouseButtonDown(0) && !pointOneIsSet)
        {
            mousePosOne = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosOne.z = 0;

            Vector3 startPoint = new Vector3(mousePosOne.x, mousePosOne.y, mousePosOne.z);
            
            TempSavedPairedPoints.Add(startPoint);
            SavedPairedPoints.Add(startPoint);
            RandomPointsForSubdivision.Add(startPoint);
            
            FirstPointsForSubdivision.Add(startPoint);

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
        
        FirstPointsForSubdivision.Add(endPoint);

        pairOfPoints[1] = endPoint;

        Debug.Log("endPoint is set" + endPoint);

        GameObject line = new GameObject("line");
        line.transform.position = pairOfPoints[0];
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = RoadMaterials [Random.Range(0, RoadMaterials.Length)];
        lineRenderer.startWidth = .05f;
        lineRenderer.endWidth = .05f;
        lineRenderer.SetPosition(0, TempSavedPairedPoints[0]);
        lineRenderer.SetPosition(1, TempSavedPairedPoints[1]);

        TempSavedPairedPoints.RemoveRange(0, TempSavedPairedPoints.Count);

        pointOneIsSet = false;
    }

    public void SpawnBuildings(List<Vector3> HousePoints)
    {
        for (int i = 0; i < HousePoints.Count - 1; i++)
        {
            Vector3 pointToRight = HousePoints[i] + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                   * (HousePoints[i + 1] - HousePoints[i]).normalized;

            float rightDistance = Vector3.Distance( HousePoints[i],pointToRight);

            Vector3 rightStop = HousePoints[i] + Quaternion.AngleAxis(90.0f, Vector3.forward)
                              * (HousePoints[i + 1] - HousePoints[i]).normalized
                              * rightDistance / 10;
            
            Vector3 rightStart = HousePoints[i] + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                * (HousePoints[i + 1] - HousePoints[i]).normalized
                                * rightDistance / 5;
            

            Vector3 pointToLeft = HousePoints[i] + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                                  * (HousePoints[i + 1] - HousePoints[i]).normalized;
                                  
            float leftDistance = Vector3.Distance(HousePoints[i],pointToLeft);

            Vector3 leftStop = HousePoints[i] + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                                * (HousePoints[i + 1] - HousePoints[i]).normalized
                                * leftDistance / 10;
            
            Vector3 leftStart = HousePoints[i] + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                               * (HousePoints[i + 1] - HousePoints[i]).normalized
                               * leftDistance / 5;

                     GameObject HouseRight = new GameObject("House Right");
                     //HouseRight.transform.position = HousePoints[i];
                     LineRenderer rightLineRendererOne = HouseRight.AddComponent<LineRenderer>();
                     rightLineRendererOne.material = HouseMaterials [Random.Range(0, HouseMaterials.Length)];
                     rightLineRendererOne.startWidth = .1f;
                     rightLineRendererOne.endWidth = .1f;
                     rightLineRendererOne.SetPosition(0, rightStart);//HousePoints[i]);
                     rightLineRendererOne.SetPosition(1, rightStop);

                     GameObject HouseLeft = new GameObject("House Left");
                     //line.transform.position = RandomPosition;
                     LineRenderer leftLineRendererOne = HouseLeft.AddComponent<LineRenderer>();
                     leftLineRendererOne.material = HouseMaterials [Random.Range(0, HouseMaterials.Length)];
                     leftLineRendererOne.startWidth = .1f;
                     leftLineRendererOne.endWidth = .1f;
                     leftLineRendererOne.SetPosition(0, leftStart); //newPoint);
                     leftLineRendererOne.SetPosition(1, leftStop); //-pointToRight);//pointToLeft);// endPointToLeft);*/

                 // Debug.Log(HousePoints.Count);
                  //Debug.Log("the index is" + randomRightIndex);
                  
                  //i++;
                  ++i;
        }
        //theFirstListHasBeenDrawn = true;
       // HousePoints.RemoveRange(0,HousePoints.Count);
    }
    
     public void ControlPoints(List<Vector3>TheFirstList)
    {
        // I want to make it so several random lines can be drawn all along any line but never from same point
        // The possibility to spawn these new lines ideally should depend on the length of the line being spawned from *
        // Accounting for x points along a line means I can spawn in buildings wherever there are no lines
        // Just need to figure out a check for this
        // So now when player clicks subdivide, any already existing lines sprout new lines, depending on how long the lines are
        // if the lines are less than a certain length they no longer sprout new lines *
        // maybe I should consider screen height and width and make sprouted lines subject to these parameters
        // Ideally lines would not sprout if they were to cross other already existing lines
        // So how will this work? At the moment all new lines get assigned to be drawn from a random point
        // now I want them to be assigned randomly to a specific point but never the same point twice
        // and i want this to happen for new lines on both left and right sides
        // and I want any unassigned points to be remembered so as to spawn buildings when called to do so 
        // buildings will need to spawn at these points plus a certain distance from the point so as to separate from line
        // i can develop a function that takes in x amount of lists and spawns buildings at all of the points on these lists

        for (int i = 0; i < TheFirstList.Count - 1; i++)
        {
            Vector3 zeroPoint = Vector3.Lerp(TheFirstList[i], TheFirstList[i + 1], 0.0f);
            Vector3 onePoint = Vector3.Lerp(TheFirstList[i], TheFirstList[i + 1], 0.1f);
            Vector3 twoPoint = Vector3.Lerp(TheFirstList[i], TheFirstList[i + 1], 0.2f);
            Vector3 threePoint = Vector3.Lerp(TheFirstList[i], TheFirstList[i + 1], 0.3f);
            Vector3 fourPoint = Vector3.Lerp(TheFirstList[i], TheFirstList[i + 1], 0.4f);
            Vector3 fivePoint = Vector3.Lerp(TheFirstList[i], TheFirstList[i + 1], 0.5f);
            Vector3 sixPoint = Vector3.Lerp(TheFirstList[i], TheFirstList[i + 1], 0.6f);
            Vector3 sevenPoint = Vector3.Lerp(TheFirstList[i], TheFirstList[i + 1], 0.7f);
            Vector3 eightPoint = Vector3.Lerp(TheFirstList[i], TheFirstList[i + 1], 0.8f);
            Vector3 ninePoint = Vector3.Lerp(TheFirstList[i], TheFirstList[i + 1], 0.9f);
            Vector3 tenPoint = Vector3.Lerp(TheFirstList[i], TheFirstList[i + 1], 1.0f);

            List<Vector3> AllPoints = new List<Vector3>();
            
            AllPoints.Add(zeroPoint);
            ListToSpawnBuildings.Add(onePoint);
            AllPoints.Add(twoPoint);
            ListToSpawnBuildings.Add(threePoint);
            AllPoints.Add(fourPoint);
            ListToSpawnBuildings.Add(fivePoint);
            AllPoints.Add(sixPoint);
            ListToSpawnBuildings.Add(sevenPoint);
            AllPoints.Add(eightPoint);
            ListToSpawnBuildings.Add(ninePoint);
            AllPoints.Add(tenPoint);

            //i++;
        //}

        // below is not functional for what i want// no longer true
            // do i need two separate for loops or nested for loops or just one for loop?//not sure
            // like function below, remove x amount of numbers and add to list for subdivision// just subject them to subdivision, no new list
            // the leftovers get added to a temp list to be held for further subdivision//nope
            // whatever is left after subdivision is used to spawn buildings//yep but how
            // so i have a max of nine spawn points on each side of the line//ok
            // so a max of five road spawn points i think should be good then min four leftover for buildings//ok
            // so so far what i have works for lines but will not work for buildings
            // i need to refactor into smaller methods
            
           // for (int j = 0; j < TheFirstList.Count - 1; j++)
            //{
            int randomRightIndex = Random.Range(0, AllPoints.Count - 1);
            Vector3 chosenRightPoint = AllPoints[randomRightIndex];
            AllPoints.RemoveAt(randomRightIndex);
            int randomLeftIndex = Random.Range(0, AllPoints.Count - 1);
            Vector3 chosenLeftPoint = AllPoints[randomLeftIndex];
            AllPoints.RemoveAt(randomLeftIndex);
            //  Loop lines 10-13 as many times as needed
            
            float floatDistance = Vector3.Distance(TheFirstList[i], TheFirstList[i + 1]);
            float minDistance = floatDistance / 2;
           
          //Vector3 randomLength = new Vector3(Mathf.Lerp(minimum, maximum, t), 0, 0); // maybe useful in an alternative structure

         // if (AllPoints.Count >= 4 && floatDistance > 3) // this statement might need to just include drawing lines alone
         // {
              //float floatDistance = Vector3.Distance(linesToBeSubdivided[i], linesToBeSubdivided[i + 1]);

              Vector3 pointToRight = chosenRightPoint + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                     * (TheFirstList[i + 1] - chosenRightPoint).normalized
                                     * (floatDistance / 2); //(floatDistance / 3);//

              Vector3 pointToLeft = chosenLeftPoint + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                                    * (TheFirstList[i + 1] - chosenLeftPoint).normalized
                                    * (floatDistance / 2); //(floatDistance / 3);

              //Vector3 randomRightLength = Vector3.Lerp(chosenRightPoint, pointToRight, Random.value); // Randomising Line lengths within parameters
              //Vector3 randomLeftLength = Vector3.Lerp(chosenLeftPoint, pointToLeft, Random.value);//mistake might be in the order here

              Vector3 randomRightLength = Vector3.Lerp(pointToRight, chosenRightPoint, Random.value); // Reversed the equation
              Vector3 randomLeftLength = Vector3.Lerp(pointToLeft, chosenLeftPoint, Random.value); // makes no difference

              SecondPointsForSubdivision.Add(randomRightLength); // this and next result in desired behaviour unexpectedly but only if one original line created
              SecondPointsForSubdivision.Add(chosenRightPoint);
              SecondPointsForSubdivision.Add(randomLeftLength); // probably
              SecondPointsForSubdivision.Add(chosenLeftPoint);

              //if (AllPoints.Count <= 5 && floatDistance > 2) // this statement might need to just include drawing lines alone
              // {

              if (minDistance > 1) // this min distance control does give a better effect but still behaviour not quite what i want
              {
                  GameObject proGenRightLineOne = new GameObject("Pro Gen Line To Right");
                  //line.transform.position = RandomPosition;
                  LineRenderer rightLineRendererOne = proGenRightLineOne.AddComponent<LineRenderer>();
                  rightLineRendererOne.material = RoadMaterials [Random.Range(0, RoadMaterials.Length)];;
                  //rightLineMaterial.color = RoadColours[Random.Range(0, RoadColours.Length)];
                  //rightLineRendererOne.material.color = RoadColours[Random.Range(0, RoadColours.Length)];
                  rightLineRendererOne.startWidth = .05f;
                  rightLineRendererOne.endWidth = .05f;
                  rightLineRendererOne.SetPosition(0, randomRightLength); //newPoint);
                  rightLineRendererOne.SetPosition(1, chosenRightPoint); // endPointToLeft);

                  GameObject proGenLeftLineOne = new GameObject("Pro Gen Line To Right");
                  //line.transform.position = RandomPosition;
                  LineRenderer leftLineRendererOne = proGenLeftLineOne.AddComponent<LineRenderer>();
                  leftLineRendererOne.material = RoadMaterials [Random.Range(0, RoadMaterials.Length)];;
                  //leftLineMaterial.color = RoadColours[Random.Range(0, RoadColours.Length)];
                  //leftLineRendererOne.material.color = RoadColours[Random.Range(0, RoadColours.Length)];
                  leftLineRendererOne.startWidth = .05f;
                  leftLineRendererOne.endWidth = .05f;
                  leftLineRendererOne.SetPosition(0, randomLeftLength); //newPoint);
                  leftLineRendererOne.SetPosition(1,
                      chosenLeftPoint); //-pointToRight);//pointToLeft);// endPointToLeft);

                  Debug.Log(AllPoints.Count);
                  Debug.Log("the index is " + randomRightIndex);
              }
              // Debug.Log(randomLeftIndex);
              // i++;
          //}
           // }
           //else if (AllPoints.Count <= 4)
           //{
               //ListToSpawnBuildings.Add(AllPoints[i]);
             //  Debug.Log("Here is where buildings will spawn" + ListToSpawnBuildings[i]);
          // }
            //}

           i++;
           //AllPoints.RemoveAt(randomRightIndex);
           //AllPoints.RemoveAt(randomLeftIndex);
          // Debug.Log(AllPoints.Count);
           //i += randomLeftIndex + randomRightIndex;
            }
        theFirstListHasBeenDrawn = true;// - put back in once building spawning is sorted
        
        Debug.Log("first drawn");
    }

    public void MakeSetAndSubdivideEverythingInItTwo(List<Vector3> TheSecondList)
    {
        for (int i = 0; i < TheSecondList.Count - 1; i++)
        {
            Vector3 zeroPoint = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], 0.0f);
            Vector3 onePoint = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], 0.1f);
            Vector3 twoPoint = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], 0.2f);
            Vector3 threePoint = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], 0.3f);
            Vector3 fourPoint = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], 0.4f);
            Vector3 fivePoint = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], 0.5f);
            Vector3 sixPoint = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], 0.6f);
            Vector3 sevenPoint = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], 0.7f);
            Vector3 eightPoint = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], 0.8f);
            Vector3 ninePoint = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], 0.9f);
            Vector3 tenPoint = Vector3.Lerp(TheSecondList[i], TheSecondList[i + 1], 1.0f);

            List<Vector3> AllPoints = new List<Vector3>();
            
            AllPoints.Add(zeroPoint);
            ListToSpawnBuildings.Add(onePoint);
            AllPoints.Add(twoPoint);
            ListToSpawnBuildings.Add(threePoint);
            AllPoints.Add(fourPoint);
            ListToSpawnBuildings.Add(fivePoint);
            AllPoints.Add(sixPoint);
            ListToSpawnBuildings.Add(sevenPoint);
            AllPoints.Add(eightPoint);
            ListToSpawnBuildings.Add(ninePoint);
            AllPoints.Add(tenPoint);

            int randomRightIndex = Random.Range(0, AllPoints.Count - 1);
            Vector3 chosenRightPoint = AllPoints[randomRightIndex];
            AllPoints.RemoveAt(randomRightIndex);
            int randomLeftIndex = Random.Range(0, AllPoints.Count - 1);
            Vector3 chosenLeftPoint = AllPoints[randomLeftIndex];
            AllPoints.RemoveAt(randomLeftIndex);

            float floatDistance = Vector3.Distance(TheSecondList[i], TheSecondList[i + 1]);
            float minDistance = floatDistance / 2;

                Vector3 pointToRight = chosenRightPoint + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                       * (TheSecondList[i + 1] - chosenRightPoint).normalized
                                       * (floatDistance / 2); //);// (floatDistance / 3);//

                Vector3 pointToLeft = chosenLeftPoint + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                                      * (TheSecondList[i + 1] - chosenLeftPoint).normalized
                                      * (floatDistance / 2); //);//(floatDistance / 3);

                Vector3 randomRightLength = Vector3.Lerp(chosenRightPoint, pointToRight, Random.value); // Randomising Line lengths within parameters
                Vector3 randomLeftLength = Vector3.Lerp(chosenLeftPoint, pointToLeft, Random.value); //mistake might be in the order here

                ThirdPointsForSubdivision.Add(randomRightLength); // this and next result in desired behaviour unexpectedly but only if one original line created
                ThirdPointsForSubdivision.Add(chosenRightPoint);
                ThirdPointsForSubdivision.Add(randomLeftLength); // probably
                ThirdPointsForSubdivision.Add(chosenLeftPoint);

                if (minDistance > 1)
                {
                    GameObject proGenRightLineOne = new GameObject("Pro Gen Line To Right");
                    //line.transform.position = RandomPosition;
                    LineRenderer rightLineRendererOne = proGenRightLineOne.AddComponent<LineRenderer>();
                    rightLineRendererOne.material = RoadMaterials [Random.Range(0, RoadMaterials.Length)];;
                    rightLineRendererOne.startWidth = .05f;
                    rightLineRendererOne.endWidth = .05f;
                    rightLineRendererOne.SetPosition(0, randomRightLength); //newPoint);
                    rightLineRendererOne.SetPosition(1, chosenRightPoint); // endPointToLeft);

                    GameObject proGenLeftLineOne = new GameObject("Pro Gen Line To Right");
                    //line.transform.position = RandomPosition;
                    LineRenderer leftLineRendererOne = proGenLeftLineOne.AddComponent<LineRenderer>();
                    leftLineRendererOne.material = RoadMaterials [Random.Range(0, RoadMaterials.Length)];;
                    leftLineRendererOne.startWidth = .05f;
                    leftLineRendererOne.endWidth = .05f;
                    leftLineRendererOne.SetPosition(0, randomLeftLength); //newPoint);
                    leftLineRendererOne.SetPosition(1, chosenLeftPoint); //-pointToRight);//pointToLeft);// endPointToLeft);
                }
                i++;
        }
        theSecondListHasBeenDrawn = true;
        
        theFirstListHasBeenDrawn = false;
        Debug.Log("seconddrawn");
    }

    public void MakeSetAndSubdivideEverythingInItThree(List<Vector3> TheThirdList)
    {
         for (int i = 0; i < TheThirdList.Count - 1; i++)
        {
            Vector3 zeroPoint = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], 0.0f);
            Vector3 onePoint = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], 0.1f);
            Vector3 twoPoint = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], 0.2f);
            Vector3 threePoint = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], 0.3f);
            Vector3 fourPoint = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], 0.4f);
            Vector3 fivePoint = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], 0.5f);
            Vector3 sixPoint = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], 0.6f);
            Vector3 sevenPoint = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], 0.7f);
            Vector3 eightPoint = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], 0.8f);
            Vector3 ninePoint = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], 0.9f);
            Vector3 tenPoint = Vector3.Lerp(TheThirdList[i], TheThirdList[i + 1], 1.0f);

            List<Vector3> AllPoints = new List<Vector3>();
            
            AllPoints.Add(zeroPoint);
            ListToSpawnBuildings.Add(onePoint);
            AllPoints.Add(twoPoint);
            ListToSpawnBuildings.Add(threePoint);
            AllPoints.Add(fourPoint);
            ListToSpawnBuildings.Add(fivePoint);
            AllPoints.Add(sixPoint);
            ListToSpawnBuildings.Add(sevenPoint);
            AllPoints.Add(eightPoint);
            ListToSpawnBuildings.Add(ninePoint);
            AllPoints.Add(tenPoint);

            int randomRightIndex = Random.Range(0, AllPoints.Count - 1);
            Vector3 chosenRightPoint = AllPoints[randomRightIndex];
            AllPoints.RemoveAt(randomRightIndex);
            int randomLeftIndex = Random.Range(0, AllPoints.Count - 1);
            Vector3 chosenLeftPoint = AllPoints[randomLeftIndex];
            AllPoints.RemoveAt(randomLeftIndex);

            float floatDistance = Vector3.Distance(TheThirdList[i], TheThirdList[i + 1]);
            float minDistance = floatDistance / 2;

                Vector3 pointToRight = chosenRightPoint + Quaternion.AngleAxis(90.0f, Vector3.forward)
                                       * (TheThirdList[i + 1] - chosenRightPoint).normalized
                                       * (floatDistance / 2); //(floatDistance / 3);//

                Vector3 pointToLeft = chosenLeftPoint + Quaternion.AngleAxis(-90.0f, Vector3.forward)
                                      * (TheThirdList[i + 1] - chosenLeftPoint).normalized
                                      * (floatDistance / 2); //(floatDistance / 3);

                Vector3 randomRightLength = Vector3.Lerp(chosenRightPoint, pointToRight, Random.value); // Randomising Line lengths within parameters
                Vector3 randomLeftLength = Vector3.Lerp(chosenLeftPoint, pointToLeft, Random.value); //mistake might be in the order here

                if (minDistance > 1)
                {
                    GameObject proGenRightLineOne = new GameObject("Pro Gen Line To Right");
                    //line.transform.position = RandomPosition;
                    LineRenderer rightLineRendererOne = proGenRightLineOne.AddComponent<LineRenderer>();
                    rightLineRendererOne.material = RoadMaterials [Random.Range(0, RoadMaterials.Length)];;
                    //rightLineMaterial.color = RoadColours[Random.Range(0, RoadColours.Length)];
                    //rightLineRendererOne.material.color = RoadColours[Random.Range(0, RoadColours.Length)];
                    rightLineRendererOne.startWidth = .05f;
                    rightLineRendererOne.endWidth = .05f;
                    rightLineRendererOne.SetPosition(0, randomRightLength); //newPoint);
                    rightLineRendererOne.SetPosition(1, chosenRightPoint); // endPointToLeft);

                    GameObject proGenLeftLineOne = new GameObject("Pro Gen Line To Right");
                    //line.transform.position = RandomPosition;
                    LineRenderer leftLineRendererOne = proGenLeftLineOne.AddComponent<LineRenderer>();
                    leftLineRendererOne.material = RoadMaterials [Random.Range(0, RoadMaterials.Length)];;
                    //leftLineMaterial.color = RoadColours[Random.Range(0, RoadColours.Length)];
                    //leftLineRendererOne.material.color = RoadColours[Random.Range(0, RoadColours.Length)];
                    leftLineRendererOne.startWidth = .05f;
                    leftLineRendererOne.endWidth = .05f;
                    leftLineRendererOne.SetPosition(0, randomLeftLength); //newPoint);
                    leftLineRendererOne.SetPosition(1, chosenLeftPoint); //-pointToRight);//pointToLeft);// endPointToLeft);
                }

                i++;

            //overallSubdivisionControl = false; // THIS NEEDS TO BE TWEAKED, ITS TOO BRUTE FORCE
        }
         theFirstListHasBeenDrawn = false;
         theSecondListHasBeenDrawn = false;
         Debug.Log("third drawn");
         
         //SecondPointsForSubdivision.RemoveRange(0, SecondPointsForSubdivision.Count);
    }
}
