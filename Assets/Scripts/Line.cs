using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    //method that turns collider list of points into Line
    public static Line LineFromCollider(List<Vector2> list)
    {
        Line line = new Line();
        List<Point> pts = new List<Point>();
        List<Segment> segs = new List<Segment>(); 

        //take points from list
        foreach(var p in list)
        {
            Point temp = new Point();
            temp.Position = p;
            pts.Add(temp);
        }

        //take segments from list
        for(int i = 0; i < list.Count; i++) 
        { 
            //taking first point of our segment
            Segment temp = new Segment();
            temp.Current = pts[i];
            pts[i].Land = temp;

            //taking next point of our segment
            int nextPointIndex = i + 1;

            //if it last point than turn it into first
            //because it is a circle
            if(nextPointIndex >= list.Count)
            {
                nextPointIndex = 0; 
            }

            temp.Next = pts[nextPointIndex];
            pts[nextPointIndex].Circle = temp; //save that this point from circle
            segs.Add(temp);
        }

        line.Points = pts;
        line.Segments = segs;
        return line;
    }
}
