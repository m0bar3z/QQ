using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class Room
{
    public static FloorBox[] boxes_;
    public static bool [,] intersections;

    public int x, y;
    public bool[] boxes;

    public List<Vector2Int> corners = new List<Vector2Int>();

    public static List<Room> MakeRooms(List<FloorBox> boxes)
    {
        boxes_ = boxes.ToArray();
        List<Room> rooms = new List<Room>();
        intersections = new bool[boxes.Count, boxes.Count];

        for(int i = 0; i < boxes_.Length; i++)
        {
            for(int j = 0; j < boxes_.Length; j++)
            {
                if (boxes_[i].DoIntersect(boxes_[j]))
                {
                    intersections[i, j] = true;
                    intersections[j, i] = true;
                }
            }
        }

        //Debug.Log("Intersections");
        //for (int i = 0; i < boxes_.Length; i++)
        //{
        //    string d = "";
        //    for (int j = 0; j < boxes_.Length; j++)
        //    {
        //        d += intersections[i, j] + " ";
        //    }
        //    Debug.Log(d);
        //}

        for (int i = 0; i < boxes_.Length; i++)
        {
            bool isInRoom = false;
            FloorBox b = boxes_[i];

            foreach (Room rm in rooms)
            {
                if (rm.boxes[i])
                {
                    isInRoom = true;
                    break;
                }
            }

            if (!isInRoom)
            {
                Room r = new Room(b.startx, b.starty, i);

                for(int k = 0; k < intersections.GetLength(1); k++)
                {
                    if(intersections[i, k])
                    {
                        r.AddBox(k);
                    }
                }

                rooms.Add(r);
            }
        }

        foreach(Room r in rooms)
        {
            r.AssignCorners();
        }

        return rooms;
    }


    // constructor
    public Room(int x, int y, int boxIndex)
    {
        boxes = new bool[boxes_.Length];
        this.x = x;
        this.y = y;
        boxes[boxIndex] = true;
    }

    // finds the corners of the rooom
    public void AssignCorners()
    {
        List<FloorBox> checkedBoxes = new List<FloorBox>();
        for(int i = 0; i < boxes.Length; i++)
        {
            if (!boxes[i]) continue;

            bool inside = false;
            FloorBox b = boxes_[i];

            foreach (Vector2Int corner in b.corners)
            {
                foreach (FloorBox cb in checkedBoxes)
                {

                    if (cb.IsInside(corner))
                    {
                        inside = true;
                        break;
                    }
                }
                if (!inside) corners.Add(corner);
            }

            checkedBoxes.Add(b);
        }
    }

    // adds a box to the room!
    public void AddBox(int index)
    {
        if (!boxes[index])
        {
            boxes[index] = true;
            for(int i = 0; i < intersections.GetLength(1); i++)
            {
                if (intersections[index, i])
                {
                    AddBox(i);
                }
            }
        }
    }
}
