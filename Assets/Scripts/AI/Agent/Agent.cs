using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public int input, output, hL, hN;
    public bool repeating = true;

    private NN network;
    private bool processing;

    protected virtual void Start()
    {
        InitializeNN();
    }

    protected virtual void Update()
    {
        if (!processing)
        {
            ProcessInput();
        }
    }

    // implemented by agent
    protected virtual void OnEpisodeBegin()
    {

    }

    protected virtual void SetInputs(float[] inputs)
    {

    }

    protected virtual void OnActions(float[] outputs)
    {

    }

    protected void SetReward(float reward)
    {
        network.fitness += reward;
    }

    protected void EndEpisode()
    {
        if (repeating)
            OnEpisodeBegin();
    }

    private void InitializeNN()
    {
        network = new NN(input, output, hL, hN, OnReceivedOutput);
        network.Initialize();
        network.Randomize();        
    }

    private void ProcessInput()
    {
        processing = true;
        float[] inputs = new float[input];
        SetInputs(inputs);
        network.SetInput(inputs);
    }

    private void OnReceivedOutput(float[] outputs)
    {
        print(outputs[0] + "," + outputs[1]);
        OnActions(outputs);
        processing = false;
    }
}
