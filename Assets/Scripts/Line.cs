using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Class for decomposing an object
 * into an array of points and segments
 */
[System.Serializable]
public class Line 
{
    public List<Point> Points;
    public List<Segment> Segments;
}
