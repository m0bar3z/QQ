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

    public NN network;

    private float _time = 0;
    private bool resting = false;

    [SerializeField]
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

    public float GetFitness()
    {
        return network.fitness;
    }

    public void Activate()
    {
        active = true;
        processing = false;
    }

    public void StartEpisode()
    {
        network.fitness = 0;
        OnEpisodeBegin();
    }

    protected virtual void Start()
    {
        //InitializeNN();
        //OnEpisodeBegin();
    }

    // TODO: optimize this shit!
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
                //if (!processing)
                //{
                    resting = false;
                    ProcessInput();
                //}
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

    protected virtual void BeforeEndingEpisode()
    {

    }

    protected void SetReward(float reward)
    {
        if(active)
            network.fitness += reward;
    }

    protected void EndEpisode()
    {
        BeforeEndingEpisode();
        if (repeating)
        {
            OnEpisodeBegin();
        }
        else
        {
            active = false;
        }
        OnEpisodeEnded?.Invoke();
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
