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

    public FloorBox(int sx, int sy, int ex, int ey)
    {
        startx = sx;
        starty = sy;
        endx = ex;
        endy = ey;

        width = endx - startx;
        height = endy - starty;
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
        bool intersection = false, xIntersect = false, yIntersect = false;

        if ((other.startx + offset.x < endx && other.endx + offset.x > startx) || (startx < other.endx + offset.x && endx > other.startx + offset.x))
        {
            xIntersect = true;
        }

        if ((other.starty + offset.y < endy && other.endy + offset.y > starty) || (starty < other.endy + offset.y && endy > other.starty + offset.y))
        {
            yIntersect = true;
        }

        intersection = yIntersect && xIntersect;

        return intersection;
    }
}
