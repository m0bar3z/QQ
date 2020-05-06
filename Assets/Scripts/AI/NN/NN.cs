using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using System;

public class NN
{
    // Statics
    public static float Sigmoid(float value)
    {
        float k = (float)Math.Exp(value);
        return k / (1.0f + k);
    }

    public static NN MakeChild(NN dad, NN mom)
    {

        return null;
    }

    public delegate void NNCB(float[] outputs);

    // Vars
    public float fitness = 0;

    private int input;
    private int output;

    private event NNCB OnOutputReady;

    private int hiddenLayers;
    private int hiddenNodes;

    public bool testing = false;
    private float[] testInput = { 1, 2, 3 };

    public List<Matrix<float>> v, b;
    private List<Matrix<float>> weights;

    // Constructor
    public NN(int input, int output, int hiddenL, int hiddenN, NNCB outputCB)
    {
        this.input = input;
        this.output = output;
        this.hiddenLayers = hiddenL;
        this.hiddenNodes = hiddenN;
        this.OnOutputReady += outputCB;
    }

    // Functions
    public void Initialize()
    {
        InitializeWeights();
        InitializeValues();

        if (testing)
        {
            AllZero();
            SetInput(testInput);
        }

        PrintTheWeights();
    }

    public void SetInput(float[] inputs)
    {
        for(int i = 0; i < input; i++)
        {
            v[0][i, 0] = inputs[i];
        }

        DoTheMath();

        if (testing)
        {
            Debug.Log("results");
            Debug.Log(v[v.Count - 1].ToString());
        }

        List<float> outputs = new List<float>();
        for(int i = 0; i < output; i++) 
        {
            outputs.Add(v[v.Count - 1][i, 0]);
        }
        OnOutputReady?.Invoke(outputs.ToArray());
    }

    public void Randomize()
    {
        for (int i = 0; i < weights.Count - 1; i++)
        {
            weights[i] = Matrix<float>.Build.Random(hiddenNodes, i == 0 ? input : hiddenNodes);
        }
        weights[weights.Count - 1] = Matrix<float>.Build.Random(output, hiddenNodes);

        for (int i = 0; i < b.Count - 1; i++)
        {
            b[i] = (Matrix<float>.Build.Random(hiddenNodes, 1));
        }
        b[b.Count - 1] = Matrix<float>.Build.Random(output, 1);
    }

    public void AllZero()
    {
        for (int i = 0; i < weights.Count - 1; i++)
        {
            weights[i] = Matrix<float>.Build.Dense(hiddenNodes, i == 0 ? input : hiddenNodes);
        }
        weights[weights.Count - 1] = Matrix<float>.Build.Dense(output, hiddenNodes);

        for (int i = 0; i < b.Count - 1; i++)
        {
            b[i] = (Matrix<float>.Build.Dense(hiddenNodes, 1));
        }
        b[b.Count - 1] = Matrix<float>.Build.Dense(output, 1);
    }

    private void DoTheMath()
    {
        for(int i = 0; i < v.Count - 1; i++)
        {
            v[i+1] = (weights[i] * v[i]) + b[i];
            Sigmoid(i + 1);
        }
    }

    private void Sigmoid(int layer)
    {
        for(int i = 0; i < v[layer].RowCount; i++)
        {
            v[layer][i, 0] = Sigmoid(v[layer][i, 0]);
        }
    }

    private void InitializeWeights()
    {
        weights = new List<Matrix<float>>();

        for (int i = 0; i < hiddenLayers; i++)
        {
            weights.Add(Matrix<float>.Build.Random(hiddenNodes, i == 0 ? input : hiddenNodes));
        }

        weights.Add(Matrix<float>.Build.Random(output, hiddenNodes));
    }

    private void InitializeValues()
    {
        v = new List<Matrix<float>>();
        b = new List<Matrix<float>>();

        v.Add(Matrix<float>.Build.Random(input, 1));

        for (int i = 0; i < hiddenLayers; i++)
        {
            v.Add(Matrix<float>.Build.Random(hiddenNodes, 1));
            b.Add(Matrix<float>.Build.Random(hiddenNodes, 1));
        }

        v.Add(Matrix<float>.Build.Random(output, 1));
        b.Add(Matrix<float>.Build.Random(output, 1));
    }

    private void PrintValues()
    {
        Debug.Log("values");
        for (int i = 0; i < v.Count; i++)
        {
            Debug.Log(v[i].ToString());
        }
    }

    private void PrintTheWeights()
    {
        Debug.Log("weights: ");

        for (int i = 0; i < hiddenLayers + 1; i++)
        {
            Debug.Log(weights[i].ToString());
        }
    }
}
