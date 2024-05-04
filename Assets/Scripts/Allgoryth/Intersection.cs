using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Algoryth for intersecting polygons
public class Intersection 
{
    /*
     * function that checks if 2 lines intersecting
     * p1 and p2 first line
     * p3 and p4 second line
     */
    public static bool IsIntersecting(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        bool isIntersecting = false;
        float denominator = (p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y);

        //denominator == 0 => lines ||
        if (denominator != 0) 
        {
            float a = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denominator;
            float b = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denominator;

            //If a and b are between 0 and 1 that lines intersecting
            if (a >= 0 && a <= 1 && b >= 0 && b <= 1)
            {
                isIntersecting = true;
            }
        }

        return isIntersecting;
    }

    /*
     * function that return point where lines intersecting
     * A and B first line
     * C and D second line
     */
    public static Vector2 GetIntersection(Vector2 A, Vector2 B, Vector2 C, Vector2 D) 
    {
        float top = (D.x - C.x) * (A.y - C.y) - (D.y - C.y) * (A.x - C.x);
        float bottom = (D.y - C.y) * (B.x - A.x) - (D.x - C.x) * (B.y - A.y);
        float t = top / bottom;

        Vector2 result = Vector2.Lerp(A, B, t);
        return result;
    }
}
