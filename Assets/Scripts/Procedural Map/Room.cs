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
