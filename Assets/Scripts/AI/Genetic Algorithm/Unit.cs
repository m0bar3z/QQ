using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float fitness;

    private Agent _agent;

    public void InitializeUnit()
    {
        _agent.OnEpisodeBegan += OnTrainStart;
        _agent.OnEpisodeEnded += OnTrainEnd;

        _agent.InitializeNN();
        _agent.RandomizeNN();
    }

    private void OnTrainStart()
    {

    }

    private void OnTrainEnd()
    {

    }
}
