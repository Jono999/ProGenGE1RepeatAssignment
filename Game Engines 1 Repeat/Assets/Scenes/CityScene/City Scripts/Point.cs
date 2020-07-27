using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public Vector2 position;
    public Road road;
    
    public Point(){ }
    
    public Point(Vector2 _position, Road _road = null)
    {
        position = new Vector2(_position.x, _position.y);
        road = _road;
    }

    public Vector3 GetVector3()
    {
        return new Vector3(position.x, 0, position.y);
    }

    public override bool Equals(object other)
    {
        return (Vector2.Distance((other as Point).position, position) < 0.01f);
    }
}
