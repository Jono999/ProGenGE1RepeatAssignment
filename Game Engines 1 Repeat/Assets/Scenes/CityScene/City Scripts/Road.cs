using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Road
{
    public Point startPoint;
    public Point endPoint;

    public Road(Point a, Point b)
    {
        startPoint = new Point(a.position, this);
        endPoint = new Point(b.position, this);
    }

    public Point GetOther(Point main)
    {
        return startPoint.Equals(main) ? endPoint : startPoint;
    }

    public float Length()
    {
        return Vector2.Distance(startPoint.position, endPoint.position);
    }

    public override bool Equals(object other)
    {
        Road otherRoad = other as Road;
        return startPoint.Equals(otherRoad.startPoint) && endPoint.Equals(otherRoad.endPoint) ||
               startPoint.Equals(otherRoad.endPoint) && endPoint.Equals(otherRoad.startPoint);
    }
    
    
}
