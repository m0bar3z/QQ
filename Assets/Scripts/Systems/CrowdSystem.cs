using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This manages spawning of enemies
public class CrowdSystem : MonoBehaviour
{
    private float _spawnSpeed;
    private int _lvl;

    private float _lvlSpeedDiff = 10;
    private float _temperatureMultiplier = 1;

    private Transform _target;
    private PlayerController _pc;

    public virtual void AddSpeed(float change)
    {
        _spawnSpeed += change;
    }

    public virtual void LevelUp()
    {
        _lvl++;
        AddSpeed(_lvlSpeedDiff);
    }

    protected virtual void Start()
    {
        AssignTarget();
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
}