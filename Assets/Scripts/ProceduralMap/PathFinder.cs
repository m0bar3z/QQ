using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

public class V2
{
    public static  List<V2> list = new List<V2>();

    public float x, y;
    public V2 cf;

    public static int IndexOf(V2 input)
    {
        int i = 0;
        foreach (V2 v in list)
        {
            if (v.x == input.x && v.y == input.y)
            {
                return i;
            }
            i++;
        }

        return -1;
    }

    public static void AssignToExisting(V2 v)
    {
        int index = IndexOf(v);
        if (index >= 0)
        {
            v = list[index];
        }
        else
        {
            list.Add(v);
        }
    }

    public V2(float xx, float yy, V2 cf = null)
    {
        x = xx;
        y = yy;
        this.cf = cf;        
    }

    public override bool Equals(object obj)
    {
        V2 other = (V2)obj;

        return other.x == this.x && other.y == this.y;
    }
}

public class PathFinder : MonoBehaviour
{
    public Tilemap testTilemap;
    public int roadWidth = 3;

    public Transform start, end;

    //public List<PathNode> nodes;
    public List<Vector2> path;

    private Dictionary<V2, float> costSoFar = new Dictionary<V2, float>();

    private void OnDrawGizmos()
    {
        if (path.Count > 0)
        {
            foreach (Vector2 v in path)
            {
                Gizmos.DrawSphere(v, roadWidth);
            }
        }
    }

    private void Start()
    {
        Vector2 s = start.position;
        Vector2 cur, temp;
        Vector2 e = end.position;

        s.x = Mathf.Floor(s.x) + 0.5f;
        s.y = Mathf.Floor(s.y) + 0.5f;

        PathNode curNode = new PathNode(s, 0);
        cur = new Vector2(s.x, s.y);
        V2 curV2 = new V2(cur.x, cur.y);
        V2.AssignToExisting(curV2);
        curV2.cf = curV2;

        V2 vv = new V2(cur.x, cur.y);
        V2.AssignToExisting(vv);
        costSoFar[vv] = 0;

        FibonacciHeaps.FibonacciHeap<PathNode> heaps = new FibonacciHeaps.FibonacciHeap<PathNode>();

        int iterations = 0;
        while ((cur - e).magnitude > roadWidth && iterations++ < 500) {
            for (int i = -roadWidth; i <= roadWidth; i += roadWidth)
            {
                for (int j = -roadWidth; j <= roadWidth; j += roadWidth)
                {
                    if (i == 0 && j == 0) continue;

                    temp = new Vector2(cur.x + i, cur.y + j);

                    V2 tempV2 = new V2(temp.x, temp.y);
                    V2.AssignToExisting(tempV2);

                    curV2 = new V2(cur.x, cur.y);
                    V2.AssignToExisting(curV2);

                    // see if its going back
                    int index = V2.IndexOf(curV2);
                    if(index >= 0 && V2.list[index].cf.x == tempV2.x && V2.list[index].cf.y == tempV2.y)
                        continue;

                    //V2 v = cameFrom[];
                    //if (v.x == temp.x && v.y == temp.y) continue;

                    //cameFrom[tempV2] = new V2(cur.x, cur.y);
                    tempV2.cf = curV2;

                    float fitness = EvaluatePosition(temp, e, s);
                    heaps.Insert(
                        new PathNode(temp, fitness)
                    );
                }
            }
            cur = heaps.Extract().position;
        }

        curV2 = new V2(cur.x, cur.y);
        V2.AssignToExisting(curV2);
        int index_ = V2.IndexOf(curV2);
        curV2 = V2.list[index_];
        V2.AssignToExisting(curV2);

        V2 cameFrom_ = curV2.cf;
        iterations = 0;
        while ((cameFrom_.x != s.x|| cameFrom_.y != s.y) && iterations++ < 100)
        {
            print(iterations);

            path.Add(cur);
            cur = new Vector2(cameFrom_.x, cameFrom_.y);

            curV2 = cameFrom_;
            cameFrom_ = curV2.cf;
        }

        path.Add(cur);
    }

    private float EvaluatePosition(Vector2 origin, Vector2 goal, Vector2 start)
    {
        Collider2D[] hits;
        hits = Physics2D.OverlapCircleAll(origin, roadWidth);

        if(hits.Length > 0)
        {
            return 10000000;
        }

        Vector2 startDiff = start - origin;
        Vector2 endDiff = goal - origin;

        V2 originV2 = new V2(origin.x, origin.y);
        V2.AssignToExisting(originV2);

        int index = V2.IndexOf(originV2);
        V2 originCF = V2.list[index].cf;


        try
        {
            costSoFar[originV2] = costSoFar[originCF] + 0.1f;
        }
        catch
        {
            costSoFar[originV2] = 0.1f;
        }

        return costSoFar[originV2] + endDiff.magnitude;
    }
}
