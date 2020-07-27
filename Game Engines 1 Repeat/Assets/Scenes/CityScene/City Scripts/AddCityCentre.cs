using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class AddCityCentre : MonoBehaviour
{
    public void AddNewCityCentre(List<Vector3> _intersections)
    {
        Point a = new Point();
        Point b = new Point();

        for (int i = 0; i < _intersections.Count; i++)
        {
            if (i % 2 == 0 || i == 0)
            {
                a.position.x = _intersections[i].x;
                a.position.y = _intersections[i].z;
            }
            else
            {
                b.position.x = _intersections[i].x;
                b.position.y = _intersections[i].z;
                Road rb = new Road(a, b);
                //Roads.Add(rb);
                Intersection ab = new Intersection(new List<Point>(){rb.startPoint});
                //Intersections.Add(ab);
            }
        }
    
    
    }
}
