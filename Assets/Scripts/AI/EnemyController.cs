using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Person
{
    public Transform target;

    public void CheckFacingAgent(Vector3 position)
    {
        CheckFacing(position);
    }

    public void Shoot(Vector2 dir)
    {
        rightHand.Trigger(dir);
    }

    public Vector2[] GetSensorsData()
    {
        List<Vector2> hitPoints = new List<Vector2>();

        // dont touch these!
        float fullangle = 360;
        float angle = 0;

        Color c = Color.white;

        int iterations = 36;

        for (int i = 0; i < iterations; i++)
        {
            c.g = angle / fullangle;
            c.b = 1 - angle / fullangle;

            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector3.right;

            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(transform.position, dir, 10);

            RaycastHit2D hit = new RaycastHit2D();
            bool hasPoint = false;
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider.gameObject == gameObject || h.collider.gameObject == rightHand.gameObject) continue;

                hit = h;
                hasPoint = true;
                break;
            }

            Vector2 hitP = hasPoint ? hit.point : dir * 10 + (Vector2)transform.position;
            hitPoints.Add(hitP);
            Debug.DrawLine(transform.position, hitP, c);

            angle += fullangle / iterations;
        }

        return hitPoints.ToArray();
    }

    protected override void OnDie()
    {
        // no deaths in training :>
    }
}
