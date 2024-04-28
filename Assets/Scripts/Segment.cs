using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment 
{
    public Point Current;
    public Point Next;
    public List<Point> CrossedPoints = new List<Point>();

    public Segment(){}

    public Segment(Point current, Point next, List<Point> crossedPoints) : this()
    {
        Current = current;
        Next = next;
        CrossedPoints = crossedPoints;
    }
}
