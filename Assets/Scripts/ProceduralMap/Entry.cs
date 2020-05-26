using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Entry : MonoBehaviour
{
    [Serializable]
    public enum Type
    {
        UP, DOWN, LEFT, RIGHT
    }

    public class EntryType
    {
        public Type self, fit;
    }

    public static EntryType right = new EntryType(), left = new EntryType(), up = new EntryType(), down = new EntryType();
    public static bool initialized = false;

    public EntryType type;
    public bool r, u, d, l;

    public bool isUsed = false;

    public static void Initialize()
    {
        initialized = true;
        right.self = Type.RIGHT;
        right.fit = Type.LEFT;

        up.self = Type.UP;
        up.fit = Type.DOWN;

        down.self = Type.DOWN;
        down.fit = Type.UP;

        left.self = Type.LEFT;
        left.fit = Type.RIGHT;
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
