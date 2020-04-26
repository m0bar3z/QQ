using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorArrow : MonoBehaviour
{
    public Enemy target;
    public SpriteRenderer spriteRenderer;
    public bool rendering, working = true;
    public Vector2 place;
    public Vector2 p00, p11, center;
    public float margin = 0.5f;

    public void SetRendering(bool input)
    {
        rendering = input;
        spriteRenderer.enabled = input;
    }

    private void Update()
    {
        if (working)
        {
            if (!target.visible)
            {
                if (!rendering)
                {
                    SetRendering(true);
                }

            }
            else
            {
                if (rendering)
                {
                    SetRendering(false);
                }
            }

            if (rendering)
            {
                FindCorners();
                place = target.transform.position;
                Vector2 dir = place - center;
                transform.position = ClampVector2(place) - dir.normalized * margin;
                transform.up = dir.normalized;
            }
        }
    }

    private void FindCorners()
    {
        p00 = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        p11 = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        center = Camera.main.ViewportToWorldPoint(new Vector2(.5f, .5f));
    }

    private Vector2 ClampVector2(Vector2 input)
    {
        return new Vector2(
            Mathf.Clamp(input.x, p00.x, p11.x),
            Mathf.Clamp(input.y, p00.y, p11.y)
        );
    }
}
