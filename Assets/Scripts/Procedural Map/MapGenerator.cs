using System.Collections;
using System.Collections.Generic;
using TriangleNet;
using TriangleNet.Meshing;
using TriangleNet.Geometry;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    #region Public Vars
    public Tile floorTile, wallTile, wallTileTop;
    public GameObject physicalRoomPrefab;

    [Header("tile maps")]
    public Tilemap floorTM;
    public Tilemap wallsTM;

    [Header("etc")]
    public SpriteRenderer noiseSample;
    public float scale = 0.1f;
    public float xOffset = 100f, yOffset = 100f, roadWidth = 3;

    [Header("Map")]
    public int width;
    public int height;
    public int minWidth, minHeight, maxWidth, maxHeight;
    public int boxCount = 10;

    public bool doGenerate = false;

    public List<Room> rooms = new List<Room>();
    public List<FloorBox> boxes = new List<FloorBox>();
    public List<PhysicalBox> physicalRooms = new List<PhysicalBox>();
    #endregion

    #region Private Vars
    private Matrix4x4 
        leftMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90))
        , rightMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90))
        , bottomMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 180));

    private float meanBoxSize = 0;

    private TriangleNet.Meshing.IMesh mesh;
    private bool hasMesh = false;
    private TriangleNet.Geometry.Vertex[] verticies;
    private Edge[] edges;
    private int[,] mapArr;

    private float minX = Mathf.Infinity;
    private float minY = Mathf.Infinity;
    private float maxX = -Mathf.Infinity;
    private float maxY = -Mathf.Infinity;

    private List<PhysicalBox> mainBoxes;
    #endregion

    private void Start()
    {
        //Generate();

        //TestRoadMaking();
    }

    private void TestRoadMaking()
    {
        // Test y path

        // make 2 boxes
        physicalRooms = new List<PhysicalBox>();
        mainBoxes = new List<PhysicalBox>();

        PhysicalBox pb0 = Instantiate(physicalRoomPrefab, new Vector3(20, 0, 0), Quaternion.identity, transform).GetComponent<PhysicalBox>();
        pb0.ApplyScale(10, 25);

        PhysicalBox pb1 = Instantiate(physicalRoomPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform).GetComponent<PhysicalBox>();
        pb1.ApplyScale(14, 20);

        mainBoxes.Add(pb0);
        mainBoxes.Add(pb1);

        physicalRooms.Add(pb0);
        physicalRooms.Add(pb1);

        // turn to arr
        int width, height;
        Boxes2Arr(out width, out height);
        AddRooms();

        // connect them
        Vector2 d = pb1.transform.position - pb0.transform.position;
        XRoad(pb0.transform.position, new Vector2Int((int)d.x, (int)d.y));

        ProcessMap();
        DrawTheMap();
    }

    private void Update()
    {
        if (doGenerate)
        {
            Generate();
            doGenerate = false;
        }

        if (hasMesh)
        {
            DrawGraph();
        }
    }

    private void DrawGraph()
    {
        foreach (Edge e in edges)
        {
            if(e.P0 >= 0 && e.P1 >= 0)
                Debug.DrawLine(new Vector2((float)verticies[e.P0].x, (float)verticies[e.P0].y), new Vector2((float)verticies[e.P1].x, (float)verticies[e.P1].y), Color.green);
        }
    }

    private void FillBox(Tilemap tm, Tile t, int startx, int starty, int endx, int endy)
    {
        for(int i = startx; i < endx; i++)
        {
            for(int j = starty; j < endy; j++)
            {
                tm.SetTile(new Vector3Int(i, j, 0), t);
            }
        }
    }

    private void Generate()
    {
        PhysicalBox.OnPlacingDone += OnPlacingDone;

        // create random rooms
        for (int i = 0; i < boxCount; i++)
        {
            CreatePhysicalRoom();
        }
    }

    private void OnPlacingDone()
    {
        // find the biggest rooms
        FindBigRooms();

        // delaunay triangulation
        Triangualte();

        // minimum spanning tree
        List<Edge> es = MinSpanningTree();

        // add 10% random edges
        AddBackRandomEdges(es);

        // turn into array
        int width, height;
        Boxes2Arr(out width, out height);

        // add rooms to array
        AddRooms();

        // place pathWays
        PlacePaths(width, height);

        ProcessMap();

        // draw the motherfuckin map!
        // use invoke to see the changes before drawing process
        Invoke(nameof(DrawTheMap), 0.1f);
    }

    private void FindBigRooms()
    {
        meanBoxSize /= physicalRooms.Count;
        meanBoxSize *= 10;
        meanBoxSize *= 1.2f;

        mainBoxes = new List<PhysicalBox>();
        foreach (PhysicalBox pb in physicalRooms)
        {
            if (pb.width * pb.height >= meanBoxSize)
            {
                pb.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
                mainBoxes.Add(pb);
            }
        }
    }

    private void Triangualte()
    {
        TriangleNet.Meshing.Algorithm.Dwyer dwyer = new TriangleNet.Meshing.Algorithm.Dwyer();
        List<TriangleNet.Geometry.Vertex> vertexList = new List<TriangleNet.Geometry.Vertex>();

        foreach (PhysicalBox pb in mainBoxes)
        {
            vertexList.Add(
                new TriangleNet.Geometry.Vertex(pb.transform.position.x, pb.transform.position.y)
            );
        }

        mesh = dwyer.Triangulate(vertexList, new Configuration());

        this.verticies = new TriangleNet.Geometry.Vertex[mesh.Vertices.Count];
        mesh.Vertices.CopyTo(this.verticies, 0);
    }

    private List<Edge> MinSpanningTree()
    {
        int[,] graph = new int[this.verticies.Length, this.verticies.Length];
        foreach (Edge e in mesh.Edges)
        {
            graph[e.P0, e.P1] = 1;
        }

        int[] parents;
        Prime.Calculate(graph, this.verticies.Length, out parents);

        List<Edge> es = new List<Edge>();

        for (int i = 0; i < this.verticies.Length; i++)
        {
            es.Add(
                new Edge(i, parents[i])
            );
        }

        return es;
    }

    private void AddBackRandomEdges(List<Edge> es)
    {
        foreach (Edge e in mesh.Edges)
        {
            bool skip = false;
            foreach (Edge ee in es)
            {
                if (ee.P0 == e.P0 && ee.P1 == e.P1)
                {
                    skip = true;
                    break;
                }
            }
            if (skip) continue;

            if (Random.Range(0, 100) <= 10)
            {
                es.Add(e);
            }
        }

        edges = es.ToArray();
        hasMesh = true;
    }

    private void Boxes2Arr(out int width, out int height)
    {
        Vector2 start = new Vector2();
        Vector2 end = new Vector2();

        foreach (PhysicalBox pb in physicalRooms)
        {
            float x = pb.transform.position.x - pb.width / 2;
            float y = pb.transform.position.y - pb.height / 2;

            float xx = pb.transform.position.x + pb.width / 2;
            float yy = pb.transform.position.y + pb.height / 2;

            if (minX > x)
            {
                minX = x;
            }

            if (minY > y)
            {
                minY = y;
            }

            if (maxX < xx)
            {
                maxX = xx;
            }

            if (maxY < yy)
            {
                maxY = yy;
            }
        }

        minX = Mathf.FloorToInt(minX);
        minY = Mathf.FloorToInt(minY);

        maxX = Mathf.FloorToInt(maxX);
        maxY = Mathf.FloorToInt(maxY);

        width = (int)(maxX - minX) + 10;
        height = (int)(maxY - minY) + 10;
        mapArr = new int[width, height];
    }

    private void AddRooms()
    {
        foreach (PhysicalBox pb in mainBoxes)
        {
            AddRoomToArr(pb);
        }

        foreach (PhysicalBox pb in physicalRooms)
        {
            if (mainBoxes.Contains(pb))
                continue;

            AddRoomToArr(pb, -1);
        }
    }

    private void PlacePaths(int width, int height)
    {
        foreach (Edge e in edges)
        {
            if (e.P0 >= 0 && e.P1 >= 0)
            {
                // get rooms
                PhysicalBox pb0, pb1;
                bool hasPath, xIntersect, yIntersect;
                PreparePath(e, out pb0, out pb1, out hasPath, out xIntersect, out yIntersect);

                Vector2 d = pb1.transform.position - pb0.transform.position;
                Vector2Int diff = new Vector2Int((int)d.x, (int)d.y);
                if (xIntersect != yIntersect)
                {
                    hasPath = true;

                    if (xIntersect)
                    {
                        // place x path
                        YRoad(pb0.transform.position, diff);
                    }
                    else
                    {
                        // place y path
                        XRoad(pb0.transform.position, diff);
                    }

                }
                else
                {
                    if (!xIntersect)
                    {
                        d = PlaceLPath(width, height, pb0, pb1, ref diff);
                    }
                }
            }
        }
    }

    private void PreparePath(Edge e, out PhysicalBox pb0, out PhysicalBox pb1, out bool hasPath, out bool xIntersect, out bool yIntersect)
    {
        pb0 = mainBoxes[e.P0];
        pb1 = mainBoxes[e.P1];

        // check intersections
        hasPath = false;
        xIntersect = false;
        yIntersect = false;
        float x0, y0, x21, x22, y21, y22;
        x0 = pb0.transform.position.x;
        y0 = pb0.transform.position.y;

        x21 = pb1.transform.position.x - pb1.width / 2;
        x22 = pb1.transform.position.x + pb1.width / 2;

        y21 = pb1.transform.position.y - pb1.height / 2;
        y22 = pb1.transform.position.y + pb1.height / 2;

        if (x0 > x21 && x0 < x22)
        {
            yIntersect = true;
        }

        if (y0 > y21 && y0 < y22)
        {
            xIntersect = true;
        }
    }

    private Vector2 PlaceLPath(int width, int height, PhysicalBox pb0, PhysicalBox pb1, ref Vector2Int diff)
    {
        Vector2 d;
        Vector2 theCenter = new Vector2(width / 2, height / 2);
        d = (Vector2)pb0.transform.position - theCenter;

        d = new Vector2(Mathf.Abs(d.x), Mathf.Abs(d.y));

        bool xvsy = Mathf.Abs(d.x) < Mathf.Abs(d.y);
        Vector2 newPos;

        if (xvsy)
        {
            XRoad(pb0.transform.position, diff);

            newPos = new Vector2(pb0.transform.position.x + diff.x - (int)roadWidth * Mathf.Sign(diff.x), pb0.transform.position.y);
            d = (Vector2)pb1.transform.position - newPos;
            diff = new Vector2Int((int)d.x, (int)d.y);

            YRoad(newPos, diff);
        }
        else
        {
            YRoad(pb0.transform.position, diff);

            newPos = new Vector2(pb0.transform.position.x, pb0.transform.position.y + diff.y - (int)roadWidth * Mathf.Sign(diff.y));
            d = (Vector2)pb1.transform.position - newPos;
            diff = new Vector2Int((int)d.x, (int)d.y);

            XRoad(newPos, diff);
        }

        print(pb0.transform.position + " -> " + pb1.transform.position + " diff:" + (pb1.transform.position - pb0.transform.position) + "|" + xvsy + "|" + pb0.transform.position + " - " + newPos);
        return d;
    }

    private void YRoad(Vector3 pb0, Vector2Int diff)
    {
        int otherValue = (int)(pb0.x - minX);

        int sign = (int)Mathf.Sign(diff.y);
        int s0 = (int)(pb0.y - minY + sign);
        int e0 = (int)(pb0.y - minY) + diff.y;

        bool canContinue = sign > 0 ? s0 < e0 : s0 > e0;

        // start from pb0 and go to the dir of diff on x
        for (int i = s0; canContinue; i += sign)
        {
            for (int j = (int)-roadWidth / 2; j <= roadWidth / 2; j++)
            {
                int sum = otherValue + j;
                if (i < 0 || i >= mapArr.GetLength(0) || sum < 0 || sum >= mapArr.GetLength(1))
                {
                    print("err");
                }

                if (mapArr[sum, i] == 0)
                {
                    mapArr[sum, i] = 1;
                }
                else
                {
                    if (mapArr[sum, i] < 0)
                    {
                        CheckAddRoomArr(sum, i, mapArr[sum, i]);
                    }
                }
            }

            canContinue = sign > 0 ? i < e0 : i > e0;
        }
    }

    private void XRoad(Vector3 pb0, Vector2Int diff)
    {
        int otherValue = (int)(pb0.y - minY);

        int sign = (int)Mathf.Sign(diff.x);
        int s0 = (int)(pb0.x - minX);
        int e0 = (int)(pb0.x - minX) + diff.x;

        bool canContinue = sign > 0 ? s0 < e0 : s0 > e0;

        // start from pb0 and go to the dir of diff on x
        for (int i = s0; canContinue; i += sign)
        {
            for (int j = (int)-roadWidth / 2; j <= roadWidth / 2; j++)
            {
                int sum = otherValue + j;
                if (i < 0 || i >= mapArr.GetLength(0) || sum < 0 || sum >= mapArr.GetLength(1))
                {
                    print("err");
                }

                if (mapArr[i, sum] == 0)
                {
                    mapArr[i, sum] = 1;
                }
                else
                {
                    if (mapArr[i, sum] < 0)
                    {
                        CheckAddRoomArr(i, sum, mapArr[i, sum]);
                    }
                }
            }

            canContinue = sign > 0 ? i < e0 : i > e0;
        }
    }

    private void CheckAddRoomArr(int x, int y, int val)
    {
        try
        {
            if (mapArr[x, y] == val)
            {
                mapArr[x, y] = -mapArr[x, y];

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        CheckAddRoomArr(x + i, y + j, val);
                    }
                }
            }
        }
        catch { }
    }

    private void AddRoomToArr(PhysicalBox pb, int sgn = 1)
    {
        int s0, s1, e0, e1;

        s0 = (int)(pb.transform.position.x - pb.width / 2 - minX);
        s1 = (int)(pb.transform.position.y - pb.height / 2 - minY);

        e0 = (int)(pb.transform.position.x + pb.width / 2 - minX);
        e1 = (int)(pb.transform.position.y + pb.height / 2 - minY);

        for (int i = s0; i < e0; i++)
        {
            for (int j = s1; j < e1; j++)
            {
                mapArr[i, j] = pb.boxNumber * sgn;
            }
        }
    }

    private void DrawTheMap()
    {
        for(int i = 0; i < mapArr.GetLength(0); i++)
        {
            for(int j = 0; j < mapArr.GetLength(1); j++)
            {
                if (mapArr[i, j] > 0)
                {
                    if (mapArr[i, j] == 2)
                    {
                        wallsTM.SetTile(new Vector3Int(i, j, 0), wallTile);
                    }
                    else
                    {
                        floorTM.SetTile(new Vector3Int(i, j, 0), floorTile);
                    }                        
                }
            }
        }
    }

    private void CreatePhysicalRoom()
    {
        int bWidth = Random.Range(minWidth, maxWidth);
        int bHeight = Random.Range(minHeight, maxHeight);

        meanBoxSize += bWidth * bHeight * 0.1f;

        int x = Random.Range(0, width - bWidth);
        int y = Random.Range(0, height - bHeight);

        PhysicalBox newRoom = Instantiate(physicalRoomPrefab, new Vector3(x, y, 0), Quaternion.identity, this.transform).GetComponent<PhysicalBox>();
        newRoom.ApplyScale(bWidth, bHeight);

        physicalRooms.Add(newRoom);
    }

    private void ProcessMap()
    {
        for(int i = 0; i < mapArr.GetLength(0); i++)
        {
            for(int j = 0; j < mapArr.GetLength(1); j++)
            {
                int val = mapArr[i, j];
                if(val > 0 && val <= 500 && val != 2)
                {
                    ProcessTile(i, j, val);

                    if(mapArr[i, j] != 2)
                        mapArr[i, j] = val + 500;
                }
            }
        }
    }

    private void ProcessTile(int x, int y, int val)
    {
        int idx0, idx1;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                try
                {
                    if ((i != 0 && j != 0) || (j == 0 && i == 0)) continue;

                    idx0 = x + i;
                    idx1 = y + j;

                    int curVal = mapArr[idx0, idx1];
                    if (curVal != val && curVal != val + 500 && curVal != 2)
                    {
                        print(x + "," + y + " " + idx0 + " ," + idx1 + " - curVal:" + curVal + " val" + val);


                        if (val == 1)
                        {
                            if (curVal <= 0)
                            {
                                mapArr[x, y] = 2;
                                return;
                            }
                        }
                        else
                        {
                            mapArr[x, y] = 2;
                            return;
                        }
                    }
                }
                catch
                {
                    mapArr[x, y] = 2;
                }
            }
        }
    }
}