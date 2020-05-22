using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tile floorTile, wallTile, wallTileLeft, wallTileRight, wallTileTop, wallTileBottom;

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

    private List<Room> rooms = new List<Room>();

    private void Start()
    {
        Generate();
    }

    private void Update()
    {
        if (doGenerate)
        {
            Generate();
            doGenerate = false;
        }
    }

    private void Generate()
    {
        floorTM.ClearAllTiles();
        for (int i = 0; i < boxCount; i++)
        {
            CreateRandomBox();
        }
    }

    private void CreateRandomBox()
    {
        int bWidth = Random.Range(minWidth, maxWidth);
        int bHeight = Random.Range(minHeight, maxHeight);

        int x = Random.Range(0, width - bWidth);
        int y = Random.Range(0, height - bHeight);

        print(x + "," + y + "," + bWidth + "," + bHeight + ",");

        floorTM.BoxFill(new Vector3Int(x + bWidth, y + bHeight, 0), floorTile, x, y, x + bWidth, y + bHeight);
    }
}