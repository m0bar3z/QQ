using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public enum MapType
    {
        BIG,
        SMALL,
        DEADEND
    }

    private void Start()
    {
        foreach(Entry e in entries)
        {
            e.map = this;
        }
    }

    public int width, height;
    public GameObject MapOBJ;

    public List<Entry> entries;

    public MapType type;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(width / 2, height / 2, 0), new Vector3(width, height, 0));
    }

    // disable if invisible

}
