using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Intersection
{
    public List<Point> Points;

    public Intersection(List<Point> points)
    {
        Points = points;
    }

    public override bool Equals(object other)
    {
        int c = 0;
       // foreach (Point p in inter.Points)
           // if (this.Points.Exists(f => f == p))
                c++;

       // if (c == this.Points.Count && inter.Points.Count)
            return true;

        return false;
    }
}
