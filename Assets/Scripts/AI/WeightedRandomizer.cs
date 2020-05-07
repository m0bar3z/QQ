using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedRandomizer
{
    // end is excluded
    public static int WeightsArray(float[] weightsOriginal)
    {
        float[] weights = new float[weightsOriginal.Length];
        Array.Copy(weightsOriginal, weights, weightsOriginal.Length);

        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i] < 0) weights[i] = NN.Sigmoid(weights[i]);
            else weights[i] += 0.5f;
        }

        float[] sortedWeights = new float[weights.Length];
        Array.Copy(weights, sortedWeights, weights.Length);
        Array.Sort(sortedWeights);

        float[] sumedWeights = new float[weights.Length];
        Array.Copy(sortedWeights, sumedWeights, weights.Length);

        float sum = 0;
        for(int i = 0; i < sumedWeights.Length; i++)
        {
            sum += sumedWeights[i];
            sumedWeights[i] = sum;
        }

        float rnd = UnityEngine.Random.Range(0, sum);
        float result = 0;
        for(int i = 0; i < sumedWeights.Length; i++)
        {
            if (rnd > sumedWeights[i]) continue;

            result = sortedWeights[i];
            break;
        }

        for(int i = 0; i < weights.Length; i++)
        {
            if (weights[i] == result)
            {
                return i;
            }
        }

        return 0;
    }
}
