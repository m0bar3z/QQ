using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathNode : IComparable
{
    public Vector2 position;
    public float fitness;

    public bool used = false;

    public PathNode(Vector2 p, float f)
    {
        fitness = f;
        position = p;
    }

    public int CompareTo(object other)
    {
        PathNode otherNode = (PathNode)other;
        return fitness.CompareTo(otherNode.fitness);
    }
}

public class PathFinder : MonoBehaviour
{
    public class CameFrom
    {
        public static List<CameFrom> list = new List<CameFrom>();

        public static CameFrom GetFromV2(Vector2 input)
        {
            foreach (CameFrom cf in list)
            {
                if (cf.vector.x == input.x && cf.vector.y == input.y)
                {
                    return cf;
                }
            }

            CameFrom output = new CameFrom(input);
            list.Add(output);
            return output;
        }

        public Vector2 vector;
        public CameFrom cf;
        public float costSoFar = Mathf.Infinity;
        public bool hasCF;

        public void AssignCF(CameFrom cf)
        {
            this.cf = cf;
            this.hasCF = true;
        }

        public CameFrom(Vector2 v)
        {
            this.vector = v;
        }
    }

    public Tilemap testTilemap;
    public Tile floorTile;
    public int roadWidth = 2;

    public Transform start, end;

    public List<Vector2> path;
    public Vector2Int[] directions;

    private void PathFindTest()
    {
        testTilemap.ClearAllTiles();
        DrawPath(
            FindPath(start.position, end.position).ToArray()
        );
    }

    public void DrawPath(Vector2[] path)
    {
        int roadWidth_ = roadWidth * 2 + 1;
        foreach(Vector2 v in path)
        {
            Vector3Int vi = new Vector3Int((int)v.x, (int)v.y, 0);
            for(int i = - roadWidth_ / 2; i < roadWidth_ / 2; i++)
            {
                for (int j = -roadWidth_ / 2; j < roadWidth_ / 2; j++)
                    testTilemap.SetTile(vi + new Vector3Int(i, j, 0), floorTile);

            }
        }
    }

    public List<Vector2> FindPath(Vector2 start, Vector2 end)
    {
        FibonacciHeaps.FibonacciHeap<PathNode> heaps = new FibonacciHeaps.FibonacciHeap<PathNode>();

        CameFrom.list = new List<CameFrom>();

        Vector2 s = start;
        Vector2 e = end;

        Vector2 cur = s;
        Vector2 temp = cur;
        float diff = (cur - e).magnitude;

        int iterations = 0;
        CameFrom curCF = CameFrom.GetFromV2(cur);
        curCF.costSoFar = 0;
        while (diff > roadWidth * 2 - 1 && iterations++ < 10000)
        {
            foreach (Vector2Int dir in directions)
            {
                Vector2 newDir = dir * roadWidth;
                temp = cur + newDir;

                CameFrom tempCF = CameFrom.GetFromV2(temp);

                float costSoFar = curCF.costSoFar + 0.1f;

                if (tempCF.costSoFar > costSoFar)
                {
                    tempCF.costSoFar = costSoFar;
                    tempCF.AssignCF(curCF);

                    float fitness = EvaluatePosition(temp, e, s, tempCF);

                    PathNode p = new PathNode(temp, fitness);
                    heaps.Insert(p);
                }
            }

            try
            {
                cur = heaps.Extract().position;
            }
            catch
            {
                return null;
            }

            curCF = CameFrom.GetFromV2(cur);
            diff = (cur - e).magnitude;
        }

        path = new List<Vector2>();
        iterations = 0;
        CameFrom lastCF = CameFrom.GetFromV2(cur);
        while (lastCF != null && iterations++ < 10000)
        {
            path.Add(lastCF.vector);
            lastCF = lastCF.cf;
        }

        Vector2 last = path[path.Count-1];
        path.Add(end);

        return path;
    }

    private float EvaluatePosition(Vector2 origin, Vector2 goal, Vector2 start, CameFrom tempCF)
    {
        Vector2 diff = goal - origin;

        // TODO:
        // enable tags and layers
        Collider2D[] hits;
        hits = Physics2D.OverlapCircleAll(origin, roadWidth * 2);

        float addition = 0;
        if(hits.Length > 0)
        {
            addition = Mathf.Infinity;
        }

        return diff.magnitude + tempCF.costSoFar + addition;
    }
}
