using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBox : MonoBehaviour
{
    public static List<PhysicalBox> movingBoxes = new List<PhysicalBox>();
    public static List<PhysicalBox> stableBoxes = new List<PhysicalBox>();
    public static event SystemTools.SimpleSystemCB OnPlacingDone;

    public static int boxNum = 100;

    public Rigidbody2D rb;
    public Collider2D col;
    public bool isMoving = true;
    public float speedThreshold = 1f;
    public float width, height;
    public int boxNumber = 0;

    private Vector2 lastpos;
    private int checks = 30;

    public void ApplyScale(float x, float y)
    {
        transform.localScale = new Vector3(x, y, 1);
        width = x;
        height = y;
    }

    public bool DoIntersectX(float x)
    {
        return false;
    }

    public bool DoIntersectY(float y)
    {
        return false;
    }

    private void Awake()
    {
        movingBoxes.Add(this);
        lastpos = transform.position;
        InvokeRepeating(nameof(CheckMoving), 2, 0.2f);
        boxNumber = boxNum++;
    }

    private void CheckMoving()
    {
        if(lastpos.x == transform.position.x && lastpos.y == transform.position.y)
        {
            checks--;

            if (checks <= 0)
            {
                CancelInvoke(nameof(CheckMoving));

                stableBoxes.Add(this);
                movingBoxes.Remove(this);
                isMoving = false;

                if (movingBoxes.Count <= 0)
                {
                    foreach(PhysicalBox b in stableBoxes)
                    {
                        b.Snap();
                    }
                    OnPlacingDone?.Invoke();
                }
            }
        }
        else
        {
            checks = 30;
            lastpos = transform.position;
        }
    }

    private void Snap()
    {
        Destroy(col);
        Destroy(rb);
        transform.position = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y), 0);
    }
}
