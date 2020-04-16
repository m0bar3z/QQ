using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a system to spawn blood in map
public class BloodSystem : MonoBehaviour
{
    public static BloodSystem instance;

    public GameObject bloodPrefab;
    public Vector2 size;
    public int xTiles, yTiles, spillFactor = 2;
    public bool drawGizmo;

    private Vector2[,] _bloodPoses;
    private Vector2 scale;
    private GameObject[,] _bloods;

    public void Spill(Vector2 pos, Vector2 dir)
    {
        dir = dir.normalized;
        dir = new Vector2(Mathf.RoundToInt(dir.x), Mathf.RoundToInt(dir.y));

        int x = Mathf.FloorToInt(pos.x * scale.x) + xTiles / 2;
        int y = Mathf.FloorToInt(pos.y * scale.y) + yTiles / 2;

        _bloods[ x  ,  y ].SetActive(true);

        for(int i = 0; i < spillFactor; i++)
        {
            x += (int)dir.x;
            y += (int)dir.y;

            // TODO: change this to arithmatic stuff with one cond
            if(x < 0 || x >= xTiles || y < 0 || y >= yTiles)
            {
                break;
            }
            else
            {
                _bloods[x, y].SetActive(true);
            }
        }
    }

    public void Deactivate(int x, int y)
    {
        _bloods[x, y].GetComponent<Burnable>().burning = false;
        _bloods[x, y].SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (drawGizmo)
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
                bld.GetComponent<BloodObject>().SetXY(i, j);
                bld.SetActive(false);
                _bloods[i, j] = bld;
            }
        }
    }
}
