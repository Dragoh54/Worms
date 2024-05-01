using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

/*
 * Class for destroying land
 */
public class Destroyer : MonoBehaviour
{
    [SerializeField] PolygonCollider2D _landCollider;
    [SerializeField] PolygonCollider2D _circleCollider;
    [SerializeField] int _testIterations = 10;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DoCut();
        }
    }

    public void DoCut()
    {
        //decomposing circle collider into Line
        List<Vector2> circlePointsPositions = _circleCollider.GetPath(0).ToList();
        for(int i=0; i<circlePointsPositions.Count; i++)
        {
            //Transform local position of point to global
            circlePointsPositions[i] = _circleCollider.transform.TransformPoint(circlePointsPositions[i]);
        }
        //make a line for every spline
        Line circleLine =Line.LineFromCollider(circlePointsPositions);

        //list of all land segments
        List<List<Point>> allSplines = new List<List<Point>>();

        //go through all collider paths
        for(int p=0; p<_landCollider.pathCount; p++) 
        {
            List<Vector2> linePointsPositions = _landCollider.GetPath(p).ToList();
            for(int i = 0; i < linePointsPositions.Count; i++)
            {
                //Transform local position of point to global
                linePointsPositions[i] = _landCollider.transform.TransformPoint(linePointsPositions[i]);
            }
            Line landLine = Line.LineFromCollider(linePointsPositions);

            //check landLine points for outsiding from circle
            for(int i = 0; i<landLine.Points.Count; i++)
            {
                //if closest point to circle is first point 
                //than reorder landLine 
                if (_circleCollider.ClosestPoint(landLine.Points[0].Position) == landLine.Points[0].Position)
                {
                    ReorderList(landLine.Points);
                    ReorderList(landLine.Segments);
                }
                else
                {
                    break;
                }
            }

            var res = Substraction(landLine, circleLine);
            allSplines.InsertRange(0, res);
        }

        _landCollider.GetComponent<Land>().SetPath(allSplines);
    }

    public List<List<Point>> Substraction(Line landLine, Line circleLine)
    {
        //set basic nextPoin for circle
        for(int i = 0; i< circleLine.Points.Count; i++)
        {
            int nextIndex = GetNext(i, circleLine.Points.Count, false);
            circleLine.Points[i].NextPoint = circleLine.Points[nextIndex];
        }

        //check all segments for intersecting points
        for(int l=0; l<landLine.Segments.Count; l++)
        {
            Segment landSegment = landLine.Segments[l];
            Vector2 currentLandPont = landLine.Segments[l].Current.Position;
            Vector2 nextLandPoint = landLine.Segments[l].Next.Position;

            //checking which circle points intersecting land points
            for(int c = 0;  c < circleLine.Segments.Count; c++)
            {
                Segment circleSegment = circleLine.Segments[c];
                Vector2 currentCirclePoint = circleLine.Segments[c].Current.Position;
                Vector2 nextCirclePoint = circleLine.Segments[c].Next.Position;

                if (Intersection.IsIntersecting(currentLandPont, nextLandPoint, currentCirclePoint, nextCirclePoint))
                {
                    Vector2 position = Intersection.GetIntersection(currentLandPont, nextLandPoint, currentCirclePoint, nextCirclePoint);
                    Point crossPoint = new Point();
                    crossPoint.Position = position;
                    crossPoint.Land = landSegment;
                    crossPoint.Circle = circleSegment;
                    crossPoint.IsCross = true;
                    landSegment.CrossedPoints.Add(crossPoint);
                    circleSegment.CrossedPoints.Add(crossPoint);
                }
            }
        }

        //create new array knowing intersection points
        RecalculateLine(landLine);
        RecalculateLine(circleLine);

        //put in braces for using the same varables
        {
            List<Point> allPoints = new List<Point>(landLine.Points);
            bool isOnLand = true;
            Point start = allPoints[0];
            while (allPoints.Count > 0)
            {
                Point currentPoint = allPoints[0];
                //check if current point outside circle
                if (_circleCollider.ClosestPoint(currentPoint.Position) == currentPoint.Position || currentPoint.IsCross)
                {
                    allPoints.RemoveAt(0);
                    continue;
                }

                //collect all points and order NextPoint
                for (int i = 0; i < _testIterations; i++)
                {
                    Line currentLine;
                    bool ccw; //contr clock wise
                    if (isOnLand)
                    {
                        currentLine = landLine;
                        ccw = true;
                    }
                    else
                    {
                        currentLine = circleLine;
                        ccw = false;
                    }

                    int currentIndex = currentLine.Points.IndexOf(currentPoint);
                    int nextIndex = GetNext(currentIndex, currentLine.Points.Count, ccw);
                    currentPoint.NextPoint = currentLine.Points[nextIndex];
                    allPoints.Remove(currentPoint);
                    if (currentPoint.NextPoint.IsCross)
                    {
                        isOnLand = !isOnLand;
                    }
                    currentPoint = currentPoint.NextPoint;
                    if (start == currentPoint) break;
                }
            }
        }

        //now we will collect all our points
        { 
            List<List<Point>> allSplines = new List<List<Point>>();
            List<Point> allPoints = new List<Point>(landLine.Points);
            while(allPoints.Count > 0)
            {
                Point currentPoint = allPoints[0];
                //check if current point outside circle
                if (_circleCollider.ClosestPoint(currentPoint.Position) == currentPoint.Position || currentPoint.IsCross)
                {
                    allPoints.RemoveAt(0);
                    continue;
                }
                else
                {
                    List<Point> newSpline = new List<Point>();
                    allSplines.Add(newSpline);

                    // Собираем точки по цепочке
                    Point startPoint = currentPoint;
                    Point point = currentPoint;

                    newSpline.Add(point);
                    allPoints.Remove(point);
                    for (int i = 0; i < _testIterations; i++)
                    {
                        point = point.NextPoint;
                        if (point == startPoint) break;
                        newSpline.Add(point);
                        if (allPoints.Contains(point))
                            allPoints.Remove(point);
                    }
                }
            }
            return allSplines;
        }
    }

    public void RecalculateLine(Line line)
    {
        List<Point> newPoints = new List<Point>();
        for (int s = 0; s < line.Segments.Count; s++)
        {
            Segment segment = line.Segments[s];
            newPoints.Add(segment.Current);
            if (segment.CrossedPoints.Count > 0)
            {
                segment.CrossedPoints.Sort((p1, p2) =>
                Vector3.Distance(segment.Current.Position, p1.Position).
                    CompareTo(Vector3.Distance(segment.Current.Position, p2.Position)));
            }
            newPoints.AddRange(segment.CrossedPoints);
        }
        line.Points = newPoints;
    }

    void ReorderList<T>(List<T> list)
    {
        var first = list[0];
        for (int i = 0; i < list.Count; i++)
        {
            if (i == list.Count - 1)
            {
                list[i] = first;
            }
            else
            {
                list[i] = list[i + 1];
            }
        }
    }

    int GetNext(int index, int length, bool isCCW)
    {
        int nextIndex = index + (isCCW ? 1 : -1);
        if (nextIndex >= length)
        {
            nextIndex = 0;
        }
        else if (nextIndex < 0)
        {
            nextIndex = length - 1;
        }
        return nextIndex;
    }
}
 