using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Authro : Mh
// This manages spawning of enemies
public class CrowdSystem : MonoBehaviour
{
    public static int enemiesCount = 0;

    public GameObject enemyPref, arrowPref;
    public Transform[] doors;

    public float lvlUpRate = 0.2f;

    [SerializeField]
    private float _chunckSize = 1, _betweenSpawns = 5, _betweenSteps = 0.5f;
    private int _lvl, _maxEnemies = 10, _kills, _killsTillNext = 3;

    private float _lvlSpeedDiff = 10;
    private float _temperatureMultiplier = 1;

    private Transform _target;
    private PlayerController _pc;

    private float _time = 0;

    private List<IndicatorArrow> usedArrow = new List<IndicatorArrow>();
    private List<IndicatorArrow> usefulArrow = new List<IndicatorArrow>();

    public virtual void SpeedUp()
    {
        _betweenSpawns -= lvlUpRate * _betweenSpawns;
        _betweenSteps -= lvlUpRate * _betweenSteps;
        if (_betweenSpawns < 0.01f)
        {
            _betweenSpawns = 0.01f;
        }
        if(_betweenSteps < 0.05f)
        {
            _betweenSteps = 0.05f;
        }
    }

    public virtual void LevelUp()
    {
        _lvl++;
        _killsTillNext *= _killsTillNext;
        SpeedUp();
    }

    public virtual void GotKill()
    {
        enemiesCount--;
        _kills++;
        if(_kills > _killsTillNext)
        {
            _kills = 0;
            LevelUp();
        }
    }

    public virtual void GotKill(IndicatorArrow arrow)
    {
        enemiesCount--;
        _kills++;
        if (_kills > _killsTillNext)
        {
            _kills = 0;
            LevelUp();
        }

        arrow.working = false;
        arrow.SetRendering(false);
        usedArrow.Remove(arrow);
        usefulArrow.Add(arrow);
    }

    protected virtual void Start()
    {
        enemiesCount = 0;
        AssignTarget();

        Statics.instance.messageSystem.ShowMessage("Wave 1");
    }

    protected virtual void Update()
    {
        TimerTick();
    }

    private void TimerTick()
    {
        _time += Time.deltaTime;
        if (_time >= _betweenSpawns && enemiesCount < _maxEnemies)
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
        Enemy enemy = Instantiate(enemyPref, spawnPos, Quaternion.identity).GetComponent<Enemy>();
        enemy.AssignTarget(_target);
        enemy.AssignCS(this);
        enemy.timeBetweensteps = _betweenSteps;
        enemiesCount++;

        IndicatorArrow arrow;
        if (usefulArrow.Count > 0)
        {
            arrow = usefulArrow[0];
            usefulArrow.RemoveAt(0);
            arrow.working = true;
            arrow.SetRendering(true);
        }
        else
        {
            arrow = Instantiate(arrowPref, transform).GetComponent<IndicatorArrow>();
            arrow.working = true;
            arrow.SetRendering(true);
            usedArrow.Add(arrow);
        }

        arrow.target = enemy;
        enemy.indicator = arrow;
    }
}