using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Authro : Mh
// This manages spawning of enemies
public class CrowdSystem : MonoBehaviour
{
    public static int enemiesCount = 0;

    public GameObject enemyPref;
    public Transform[] doors;

    [SerializeField]
    private float _chunckSize = 1, _betweenSpawns = 5;
    private int _lvl, maxEnemies = 10;

    private float _lvlSpeedDiff = 10;
    private float _temperatureMultiplier = 1;

    private Transform _target;
    private PlayerController _pc;

    private float _time = 0;

    public virtual void SpeedUp()
    {
        _betweenSpawns -= 0.1f * _betweenSpawns;
        if (_betweenSpawns < 0.01f)
        {
            _betweenSpawns = 0.01f;
        }
    }

    public virtual void LevelUp()
    {
        _lvl++;
        SpeedUp();
    }

    protected virtual void Start()
    {
        AssignTarget();
    }

    protected virtual void Update()
    {
        TimerTick();
    }

    private void TimerTick()
    {
        _time += Time.deltaTime;
        if (_time >= _betweenSpawns && enemiesCount < maxEnemies)
        {
            _time = 0;
            Spawn();
        }
    }

    private void AssignTarget()
    {
        try
        {
            _pc = FindObjectOfType<PlayerController>();
            _target = _pc.transform;
        }
        catch
        {
            Debug.LogError("No player in the scene");
        }
    }

    private void Spawn()
    {
        for(int i = 0; i < _chunckSize; i++)
        {
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        Vector3 spawnPos = doors[Random.Range(0, doors.Length)].position;

        Instantiate(enemyPref, spawnPos, Quaternion.identity).GetComponent<Enemy>().AssignTarget(_target);
        enemiesCount++;
    }
}