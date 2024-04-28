using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point 
{
    public Vector2 Position;
    public Point NextPoint;
    public bool IsCross;
    public Segment Land;
    public Segment Circle;

    public Point() {}

    public Point(Vector2 position, Point nextPoint, bool isCross, Segment land, Segment circle) : this()
    {
        Position = position;
        NextPoint = nextPoint;
        IsCross = isCross;
        Land = land;
        Circle = circle;
    }
}
