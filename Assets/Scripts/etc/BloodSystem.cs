using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a system to spawn blood in map
public class BloodSystem : MonoBehaviour
{
    public static BloodSystem instance;

    public GameObject bloodPrefab;
    public Vector2 size;
    public int xTiles, yTiles;

    private Vector2[,] _bloodPoses;
    private Vector2 scale;
    private GameObject[,] _bloods;

    public void Spill(Vector2 pos, Vector2 dir)
    {
        dir = dir.normalized;
        dir = new Vector2(Mathf.RoundToInt(dir.x), Mathf.RoundToInt(dir.y));

        print(pos.x);
        print(((int)pos.x + xTiles / 2) * (int)scale.x);

        _bloods[((int)pos.x + xTiles/2) * (int)scale.x, ((int)pos.y + yTiles/2) * (int)scale.y].SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);

        Vector2 tileSize = new Vector2(size.x / xTiles, size.y / yTiles);

        for (int i = 0; i < xTiles; i++)
        {
            for (int j = 0; j < yTiles; j++)
            {
                Vector2 pos = transform.position + new Vector3((i * tileSize.x) + (tileSize.x / 2) - (size.x / 2), (j * tileSize.y) + (tileSize.y / 2) - (size.y / 2), 0);
                Gizmos.DrawWireCube(pos, tileSize);
            }
        }
    }

    protected virtual void Start()
    {
        instance = this;

        PreSpawnAllBloods();
    }

    protected virtual void Update()
    {
        // for testing
        if (Input.GetMouseButtonUp(1))
        {
            Vector2 posdir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Spill(posdir, posdir);
        }
    }

    private void PreSpawnAllBloods()
    {
        scale = new Vector2(xTiles/size.x, yTiles/size.y);

        _bloodPoses = new Vector2[xTiles, yTiles];
        _bloods = new GameObject[xTiles, yTiles];

        Vector2 tileSize = new Vector2(size.x / xTiles, size.y / yTiles);

        for (int i = 0; i < xTiles; i++)
        {
            for (int j = 0; j < yTiles; j++)
            {
                Vector2 pos = transform.position + new Vector3((i * tileSize.x) + (tileSize.x / 2) - (size.x / 2), (j * tileSize.y) + (tileSize.y / 2) - (size.y / 2), 0);
                _bloodPoses[i, j] = pos;
                GameObject bld = Instantiate(bloodPrefab, pos, Quaternion.identity, transform);
                bld.transform.localScale = new Vector3(tileSize.x, tileSize.y, 1);
                bld.SetActive(false);
                _bloods[i, j] = bld;
            }
        }
    }
}
