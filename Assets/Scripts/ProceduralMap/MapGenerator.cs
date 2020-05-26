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

public class MapGenerator : MonoBehaviour
{
    public GameObject[] maps;

    public GameObject up_down, left_right, up_left, up_right, down_left, down_right;
    public Dictionary<Entry.Type, Dictionary<Entry.Type, GameObject>> joints;

    public Transform Tilemap;

    public int roomsThreshold = 10;

    private List<Map> rooms = new List<Map>();
    private List<Entry> unfulfilledChannels = new List<Entry>();

    private void Awake()
    {
        joints = new Dictionary<Entry.Type, Dictionary<Entry.Type, GameObject>>();

        foreach(Entry.Type t in (Entry.Type[])System.Enum.GetValues(typeof(Entry.Type)))
        {
            joints[t] = new Dictionary<Entry.Type, GameObject>();
        }

        joints[Entry.Type.UP][Entry.Type.DOWN] = up_down;
        joints[Entry.Type.DOWN][Entry.Type.UP] = up_down;

        joints[Entry.Type.LEFT][Entry.Type.RIGHT] = left_right;
        joints[Entry.Type.RIGHT][Entry.Type.LEFT] = left_right;

        joints[Entry.Type.DOWN][Entry.Type.LEFT] = down_left;
        joints[Entry.Type.LEFT][Entry.Type.DOWN] = down_left;

        joints[Entry.Type.DOWN][Entry.Type.RIGHT] = down_right;
        joints[Entry.Type.RIGHT][Entry.Type.DOWN] = down_right;

        joints[Entry.Type.UP][Entry.Type.LEFT] = up_left;
        joints[Entry.Type.LEFT][Entry.Type.UP] = up_left;

        joints[Entry.Type.UP][Entry.Type.RIGHT] = up_right;
        joints[Entry.Type.RIGHT][Entry.Type.UP] = up_right;
    }

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        int iterations = 0;

        // make the first room randomly
        Map newMap = Instantiate(maps[Random.Range(0, maps.Length)], Vector3.zero, Quaternion.identity, Tilemap).GetComponent<Map>();

        // add enteries to unfulfilled
        foreach(Entry e in newMap.entries)
        {
            unfulfilledChannels.Add(e);
        }

        // make rooms untill fulfilling threshold
        while(rooms.Count < roomsThreshold || iterations++ > 100)
        {
            // pick random entry
            int rnd = 0;
            Entry re = unfulfilledChannels[rnd];
            unfulfilledChannels.RemoveAt(rnd);

            // fulfill entry
            FulFillEntry(re);

        }

        // fill the rest with prop rooms (shop - boss_fight - gem_room)

    }

    // generates random room to fulfill entry
    private void FulFillEntry(Entry exit)
    {
        // make random room
        int rnd = Random.Range(0, maps.Length);
        GameObject newMapGO = Instantiate(maps[rnd], Tilemap);
        Map newMap = newMapGO.GetComponent<Map>();

        // pick random entry that is not the same as exit
        Entry re = null;
        foreach(Entry e_ in newMap.entries)
        {
            if(e_.type.self != exit.type.self)
            {
                re = e_;
            }
        }

        // initialize appropriate joint
        GameObject jointPrefab = joints[exit.type.fit][re.type.fit];
        Channel newJoint = Instantiate(jointPrefab, Tilemap).GetComponent<Channel>();

        // find appropriate entry to joint
        Entry e1 = null;
        Entry e2 = null;
        foreach(Entry e in newJoint.enteries)
        {
            if(e.type.self == exit.type.fit)
            {
                e1 = e;
            }
            if(e.type.self == re.type.fit)
            {
                e2 = e;
            }
        }

        // place joint
        Vector3 diff = newJoint.transform.position - e1.transform.position;
        newJoint.transform.position = exit.transform.position + diff;

        // place room
        diff = newMap.transform.position - re.transform.position;
        newMap.transform.position = e2.transform.position + diff;
        re.isUsed = true;

        // add room to list
        rooms.Add(newMap);

        // add unfulfilled to list
        foreach(Entry entry in newMap.entries)
        {
            if (!entry.isUsed)
            {
                unfulfilledChannels.Add(entry);
            }
        }

    }
}
