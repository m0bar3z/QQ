using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float fitness;
    public bool trainingDone, inTraining;

    public Agent agent;

    private Trainer _trainer;

    public void InitializeUnit(Trainer t)
    {
        agent.OnEpisodeBegan += OnTrainStart;
        agent.OnEpisodeEnded += OnTrainEnd;

        _trainer = t;

        agent.InitializeNN();
        agent.RandomizeNN();
    }

    public void StartUnit()
    {
        agent.Activate();
        agent.StartEpisode();
    }

    private void OnTrainStart()
    {
        trainingDone = false;
        inTraining = true;
    }

    private void OnTrainEnd()
    {
        fitness = agent.GetFitness();
        inTraining = false;
        trainingDone = true;
        _trainer.TrainingDone();
    }
}
