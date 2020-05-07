using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public int input, output, hL, hN;
    public bool repeating = true;

    public float fixedDeltaTime = 0.05f;

    public event SystemTools.SimpleSystemCB OnEpisodeBegan;
    public event SystemTools.SimpleSystemCB OnEpisodeEnded;

    private float _time = 0;
    private bool resting = false;

    private NN network;
    private bool processing, active;

    public void InitializeNN()
    {
        network = new NN(input, output, hL, hN, OnReceivedOutput);
        network.Initialize();
        //network.Randomize();
    }

    public void RandomizeNN()
    {
        network.Randomize();
    }

    protected virtual void Start()
    {
        //InitializeNN();
        //OnEpisodeBegin();
    }

    protected virtual void Update()
    {
        if (active)
        {
            if (!resting)
            {
                _time += Time.deltaTime;
                if (_time > fixedDeltaTime)
                {
                    resting = true;
                    _time = 0;
                }
            }
            else
            {
                if (!processing)
                {
                    resting = false;
                    ProcessInput();
                }
            }
        }
    }

    // implemented by agent
    protected virtual void OnEpisodeBegin()
    {
        OnEpisodeBegan?.Invoke();
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
        OnEpisodeEnded?.Invoke();
        if (repeating)
        {
            OnEpisodeBegin();
        }
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
        OnActions(outputs);
        processing = false;
    }
}
