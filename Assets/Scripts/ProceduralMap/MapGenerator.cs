using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.Tilemaps;

/// 
/// Options:
/// 1- disable other rooms when player is not in them
/// 
/// 2- make ways by code - and fix placements
/// 
/// 3- have direction:
///     rooms are
///         big: 4 entries - 4 sides
///         small: 2 enteries - 2 sides
///         dead-end: 1 entry - 1 side
///     
///     then we can have a sense of direction, like only going one level to sides and
///     infinitely down, by going down increase the probability of good rooms
/// 

public class MapPos
{
    public Vector2 location;
    public bool l, r, d, u;

    public MapPos(Vector2 location, bool l, bool r, bool d, bool u)
    {
        this.location = location;
        this.l = l;
        this.r = r;
        this.d = d;
        this.u = u;
    }
}

public class MapGenerator : MonoBehaviour
{
    public GameObject[] maps;

    public Vector2 offset;

    public Transform Tilemap;

    public int roomsThreshold = 2;

    public PathFinder pathFinder;

    private List<Map> rooms = new List<Map>();

    private List<MapPos> unfulfilled = new List<MapPos>();
    private List<Entry> emptyEntries = new List<Entry>();

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        MapPos mp = new MapPos(Vector2.zero, false, false, false, false);
        Map newRoom = MakeNewRoom(mp);

        for (int i = 0; i < roomsThreshold; i++)
        {
            if (unfulfilled.Count > 0)
            {
                mp = unfulfilled[0];
                unfulfilled.RemoveAt(0);
                MakeNewRoom(mp);
            }
            else
            {
                break;
            }
        }

        Invoke(nameof(FulFillEntries), 1);
    }

    private void FulFillEntries()
    {
        emptyEntries.Reverse();

        for(int i = emptyEntries.Count - 1; i >= 0; i--)
        {
            Entry e = emptyEntries[i];
            emptyEntries.RemoveAt(i);
            SortedList<float, Entry> others = new SortedList<float, Entry>();

            foreach (Entry e1 in emptyEntries)
            {
                try
                {
                    others.Add((e.transform.position - e1.transform.position).magnitude, e1);
                }
                catch { }
            }

            float rnd = Random.Range(0f, 100f);
            float chance = 1f;

            foreach(KeyValuePair<float, Entry> e2 in others)
            {
                if(rnd > chance)
                {
                    emptyEntries.Remove(e2.Value);
                    pathFinder.DrawPath(
                        pathFinder.FindPath(e.transform.position, e2.Value.transform.position).ToArray()
                    );
                }
                else
                {
                    chance /= 2f;
                }
            }
        }
    }

    private Map MakeNewRoom(MapPos pos)
    {
        Map newRoom = Instantiate(maps[Random.Range(0, maps.Length)], pos.location, Quaternion.identity, Tilemap).GetComponent<Map>();
        Vector2 instantiationOffset = new Vector2(pos.l ? -newRoom.width / 2 : pos.r ? newRoom.width / 2 : 0, pos.d ? -newRoom.height / 2 : pos.u ? newRoom.height / 2 : 0);
        newRoom.transform.position += (Vector3)instantiationOffset;
        MapPos mp;

        mp = new MapPos(newRoom.transform.position + new Vector3(newRoom.width/2 + offset.x, 0), false, true, false, false);
        if (!pos.r)
            unfulfilled.Add(mp);

        mp = new MapPos(newRoom.transform.position + new Vector3(-newRoom.width/2 - offset.x, 0), true, false, false, false);
        if (!pos.l)
            unfulfilled.Add(mp);

        mp = new MapPos(newRoom.transform.position + new Vector3(0, newRoom.height/2 + offset.y), false, false, false, true);
        if (!pos.u)
            unfulfilled.Add(mp);

        mp = new MapPos(newRoom.transform.position + new Vector3(0, -newRoom.height/2 - offset.y), false, false, true, false);
        if (!pos.d)
            unfulfilled.Add(mp);

        foreach(Entry e in newRoom.entries)
        {
            emptyEntries.Add(e);
        }

        return newRoom;
    }
}
