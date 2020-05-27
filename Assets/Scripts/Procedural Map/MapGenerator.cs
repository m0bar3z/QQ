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
    public float xOffset = 100f, yOffset = 100f;

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
        TriangleNet.Geometry.Vertex[] verticies = new TriangleNet.Geometry.Vertex[mesh.Vertices.Count];
        mesh.Vertices.CopyTo(verticies, 0);

        foreach (Edge e in mesh.Edges)
        {
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
        meanBoxSize *= 1.7f;

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
        List<TriangleNet.Geometry.Vertex> vertecies = new List<TriangleNet.Geometry.Vertex>();

        foreach (PhysicalBox pb in mainBoxes)
        {
            vertecies.Add(
                new TriangleNet.Geometry.Vertex(pb.transform.position.x, pb.transform.position.y)
            );
        }

        mesh = dwyer.Triangulate(vertecies, new Configuration());
        hasMesh = true;

        // minimum spanning tree


        // add 10% random edges


        // add pathways


        // add colliding rooms


        // turn into array


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