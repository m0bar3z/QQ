using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using TriangleNet;
using TriangleNet.Meshing;
using TriangleNet.Geometry;
using UnityEngine.UIElements;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
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

    private Matrix4x4 
        leftMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90))
        , rightMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90))
        , bottomMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 180));

    private float meanBoxSize = 0;

    private TriangleNet.Meshing.IMesh mesh;
    private bool hasMesh = false;
    private TriangleNet.Geometry.Vertex[] verticies;
    private Edge[] edges;

    private void Start()
    {
        //Generate();
        floorTM.BoxFill(new Vector3Int(85, 75, 0), floorTile, 41, 45, 85, 75);
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
        meanBoxSize /= physicalRooms.Count;
        meanBoxSize *= 10;
        meanBoxSize *= 1.2f;

        List<PhysicalBox> mainBoxes = new List<PhysicalBox>();
        foreach(PhysicalBox pb in physicalRooms)
        {
            if(pb.width * pb.height >= meanBoxSize)
            {
                pb.GetComponent<SpriteRenderer>().color = Color.red;
                mainBoxes.Add(pb);
            }
        }

        // delaunay triangulation
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

        // minimum spanning tree
        int[,] graph = new int[this.verticies.Length, this.verticies.Length];
        foreach (Edge e in mesh.Edges)
        {
            graph[e.P0, e.P1] = 1;
        }

        int[] parents;
        Prime.Calculate(graph, this.verticies.Length, out parents);

        List<Edge> es = new List<Edge>();

        for(int i = 0; i < this.verticies.Length; i++)
        {
            es.Add(
                new Edge(i, parents[i])
            );
        }

        // add 10% random edges
        foreach (Edge e in mesh.Edges)
        {
            bool skip = false;
            foreach(Edge ee in es)
            {
                if (ee.P0 == e.P0 && ee.P1 == e.P1)
                {
                    skip = true;
                    break;
                }
            }
            if (skip) continue;

            if(Random.Range(0, 100) <= 10)
            {
                es.Add(e);
            }
        }

        edges = es.ToArray();
        hasMesh = true;

        // turn into array
        Vector2 start = new Vector2();
        Vector2 end = new Vector2();

        float minX = Mathf.Infinity;
        float minY = Mathf.Infinity;
        float maxX = -Mathf.Infinity;
        float maxY = -Mathf.Infinity;

        foreach(PhysicalBox pb in physicalRooms)
        {
            float x = pb.transform.position.x - pb.width / 2;
            float y = pb.transform.position.y - pb.height / 2;

            if(minX > x)
            {
                minX = x;
            }

            if(minY > y)
            {
                minY = y;
            }

            if(maxX < x)
            {
                maxX = x;
            }

            if(maxY < y)
            {
                maxY = y;
            }
        }

        minX = Mathf.FloorToInt(minX);
        minY = Mathf.FloorToInt(minY);

        maxX = Mathf.FloorToInt(maxX);
        maxY = Mathf.FloorToInt(maxY);

        int width = (int)(maxX - minX);
        int height = (int)(maxY - minY);

        int[,] mapArr = new int[width, height];

        int boxNum = 100;
        foreach(PhysicalBox pb in physicalRooms)
        {
            int s0, s1, e0, e1;

            s0 = (int)(pb.transform.position.x - pb.width / 2 - minX);
            s1 = (int)(pb.transform.position.y - pb.height / 2 - minY);

            e0 = (int)(pb.transform.position.x + pb.width / 2 - minX);
            e1 = (int)(pb.transform.position.y + pb.height / 2 - minY);

            for (int i = s0; i < e0; i++)
            {
                for(int j = s1; j < e1; j++)
                {
                    mapArr[i, j] = boxNum;
                }
            }

            boxNum++;
        }

        // place doors


        // place walls

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
}