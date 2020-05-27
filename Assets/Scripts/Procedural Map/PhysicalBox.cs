using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBox : MonoBehaviour
{
    public static List<PhysicalBox> movingBoxes = new List<PhysicalBox>();
    public static event SystemTools.SimpleSystemCB OnPlacingDone;

    public Rigidbody2D rb;
    public Collider2D col;
    public bool isMoving = true;
    public float speedThreshold = 1f;
    public float width, height;

    private Vector2 lastpos;
    private int checks = 10;

    public void ApplyScale(float x, float y)
    {
        transform.localScale = new Vector3(x, y, 1);
        width = x;
        height = y;
    }

    private void Start()
    {
        movingBoxes.Add(this);
        lastpos = transform.position;
        InvokeRepeating(nameof(CheckMoving), 2, 0.2f);
    }

    private void CheckMoving()
    {
        if(lastpos.x == transform.position.x && lastpos.y == transform.position.y)
        {
            checks--;

            if (checks <= 0)
            {
                CancelInvoke(nameof(CheckMoving));

                movingBoxes.Remove(this);
                isMoving = false;

                Snap();

                if (movingBoxes.Count <= 0)
                {
                    OnPlacingDone?.Invoke();
                }
            }
        }
        else
        {
            checks = 10;
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
