using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

[Serializable]
public class FloorBox
{
    public int startx, starty, endx, endy;
    public int width, height;

    public Vector2Int[] corners = new Vector2Int[4];

    public FloorBox(int sx, int sy, int ex, int ey)
    {
        startx = sx;
        starty = sy;
        endx = ex;
        endy = ey;

        width = endx - startx;
        height = endy - starty;

        corners[0] = new Vector2Int(startx, starty);
        corners[1] = new Vector2Int(endx, starty);
        corners[2] = new Vector2Int(startx, endy);
        corners[3] = new Vector2Int(endx, endy);
    }

    public bool IsInside(Vector2Int point)
    {
        if(point.x > startx && point.x < endx && point.y > starty && point.y < endy)
        {
            return true;
        }

        return false;
    }

    public bool DoIntersect(FloorBox other)
    {
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if (CheckIntersection(other, new Vector2Int(i, j)))
                    return true;
            }
        }

        return false;
    }

    private bool CheckIntersection(FloorBox other, Vector2Int offset)
    {
        bool intersection = false, xIntersect1 = false, xIntersect2 = false, yIntersect1 = false, yIntersect2 = false;

        if (other.startx + offset.x < endx && other.endx + offset.x > startx)
        {
            xIntersect1 = true;
        }

        if (startx < other.endx + offset.x && endx > other.startx + offset.x)
        {
            xIntersect2 = true;
        }

        if (other.starty + offset.y < endy && other.endy + offset.y > starty)
        {
            yIntersect1 = true;
        }

        if (starty < other.endy + offset.y && endy > other.starty + offset.y)
        {
            yIntersect2 = true;
        }

        intersection = (xIntersect1 || xIntersect2) && (yIntersect1 || yIntersect2);

        return intersection;
    }
}
