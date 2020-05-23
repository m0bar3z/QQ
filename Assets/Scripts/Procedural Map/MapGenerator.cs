using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tile floorTile, wallTile, wallTileTop;

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

    private Matrix4x4 
        leftMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90))
        , rightMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90))
        , bottomMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 180));

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
        boxes = new List<FloorBox>();
        floorTM.ClearAllTiles();
        for (int i = 0; i < boxCount; i++)
        {
            CreateRandomBox();
        }
        rooms = Room.MakeRooms(boxes);
    }

    private void CreateRandomBox()
    {
        int bWidth = Random.Range(minWidth, maxWidth);
        int bHeight = Random.Range(minHeight, maxHeight);

        int x = Random.Range(0, width - bWidth);
        int y = Random.Range(0, height - bHeight);

        FloorBox fb = new FloorBox(x, y, x + bWidth, y + bHeight);

        //floorTM.BoxFill(new Vector3Int(fb.endx, fb.endy, 0), floorTile, x, y, fb.endx, fb.endy);
        FillBox(floorTM, floorTile, x, y, fb.endx, fb.endy);

        boxes.Add(fb);
    }

    #region WALLPLACEMENT
    private void PlaceLeftWall(Vector3Int position, Tilemap tm)
    {
        tm.SetTile(position, wallTileTop);
        tm.SetTransformMatrix(position, leftMatrix);
    }

    private void PlaceRightWall(Vector3Int position, Tilemap tm)
    {
        tm.SetTile(position, wallTileTop);
        tm.SetTransformMatrix(position, rightMatrix);
    }

    private void PlaceBottomWall(Vector3Int position, Tilemap tm)
    {
        tm.SetTile(position, wallTileTop);
        tm.SetTransformMatrix(position, bottomMatrix);
    }

    private void PlaceTopWall(Vector3Int position, Tilemap tm)
    {
        tm.SetTile(position, wallTileTop);
    }
    #endregion
}