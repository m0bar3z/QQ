using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Entry : MonoBehaviour
{
    [Serializable]
    public enum Directions
    {
        UP, DOWN, LEFT, RIGHT
    }

    public class EntryType
    {
        public Directions self, fit;
    }

    public static EntryType right = new EntryType(), left = new EntryType(), up = new EntryType(), down = new EntryType();
    public static bool initialized = false;

    public Map map;

    public EntryType type;
    public bool r, u, d, l;

    public bool isUsed = false;

    public static void Initialize()
    {
        initialized = true;
        right.self = Directions.RIGHT;
        right.fit = Directions.LEFT;

        up.self = Directions.UP;
        up.fit = Directions.DOWN;

        down.self = Directions.DOWN;
        down.fit = Directions.UP;

        left.self = Directions.LEFT;
        left.fit = Directions.RIGHT;
    }

    private void Awake()
    {
        if(!initialized)
            Initialize();

        if (r)
            type = right;

        if (l)
            type = left;

        if (u)
            type = up;

        if (d)
            type = down;
    }
}
