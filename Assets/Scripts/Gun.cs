using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Range(0, 5)]
    public float betweenBullets = 0.2f;
    [Range(0, 20)]
    public int chunkSize = 3;
    public GameObject bulletPref;
    public Transform gunHole;

    private float time = 0;
    private bool waiting = false;

    private void Update()
    {
        TimerTick();
        CheckInput();
    }

    private void TimerTick()
    {
        if (waiting)
        {
            time += Time.deltaTime;
            if (time > betweenBullets)
            {
                time = 0;
                waiting = false;
            }
        }
    }

    private void CheckInput()
    {
        if (Input.GetMouseButton(0) && !waiting)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < chunkSize; i++)
        {
            waiting = true;

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = pos - gunHole.position;
            dir.z = 0;

            pos.z = 0;
            Instantiate(bulletPref, gunHole.position, Quaternion.identity).GetComponent<Bullet>().Shoot(dir);
        }
    }
}
