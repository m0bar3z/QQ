using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trainer : MonoBehaviour
{
    public List<Unit> testUnits;
    public Text genShower;
    public int inTrainingCount = 0, unitsCount, gen;

    public List<float> bestFitnesses = new List<float>();
    public List<NN> bestNNs = new List<NN>();

    private void Start()
    {
        testUnits = new List<Unit>(GetComponentsInChildren<Unit>());
        unitsCount = testUnits.Count;

        InitializeUnits();
        StartTraining();

        testUnits[0].agent.network.SaveNN();
    }

    public void InitializeUnits()
    {
        foreach (Unit u in testUnits)
        {
            u.InitializeUnit(this);
        }
    }

    public void StartTraining()
    {
        genShower.text = "generation: " + gen++ + "\nBest fitness:" + bestFitnesses[bestFitnesses.Count - 1];

        foreach (Unit u in testUnits)
        {
            inTrainingCount++;
            u.StartUnit();
        }
    }

    public void TrainingDone()
    {
        inTrainingCount--;
        if (inTrainingCount <= 0)
        {
            inTrainingCount = 0;

            // marriage season begins
            MakeNewGeneration();
        }
    }

    // Sorts units by their fitness, best to worst
    private void SortUnits()
    {
        for(int i = 0; i < testUnits.Count; i++)
        {
            int maxIndex = i;
            for(int j = i; j < testUnits.Count; j++)
            {
                if(testUnits[j].fitness > testUnits[maxIndex].fitness)
                {
                    maxIndex = j;
                }
            }

            Unit temp = testUnits[maxIndex];
            testUnits[maxIndex] = testUnits[i];
            testUnits[i] = temp;
        }
    }

    private void MakeNewGeneration()
    {
        // sort by fitness
        SortUnits();

        float[] fitnesses = new float[testUnits.Count + bestNNs.Count];
        int i = 0;
        foreach(Unit u in testUnits)
        {
            fitnesses[i++] = u.fitness;
        }
        if (bestNNs.Count > 1)
        {
            fitnesses[i++] = bestNNs[0].fitness;
            fitnesses[i++] = bestNNs[1].fitness;
        }

        if (bestFitnesses.Count > 0)
        {
            if (fitnesses[0] > bestFitnesses[bestFitnesses.Count - 1])
            {
                bestFitnesses.Add(fitnesses[0]);
                bestNNs.Add(testUnits[0].agent.network);
                testUnits[0].agent.network.SaveNN();
            }
        }
        else
        {
            bestFitnesses.Add(fitnesses[0]);
            bestNNs.Add(testUnits[0].agent.network);
            bestNNs.Add(testUnits[1].agent.network);
            testUnits[0].agent.network.SaveNN();
        }

        List<NN> newGen = new List<NN>();
        // make a new generation
        for(int j = 0; j < testUnits.Count - 1; j++)
        {
            int i1 = WeightedRandomizer.WeightsArray(fitnesses);
            fitnesses[i1] = -1000;

            int i2 = WeightedRandomizer.WeightsArray(fitnesses);
            if(i2 == i1) i2 = WeightedRandomizer.WeightsArray(fitnesses);

            NN dad, mom;
            if(i1 < testUnits.Count)
            {
                dad = testUnits[i1].agent.network;
            }
            else
            {
                dad = bestNNs[i1 - testUnits.Count];
            }

            if (i2 < testUnits.Count)
            {
                mom = testUnits[i2].agent.network;
            }
            else
            {
                mom = bestNNs[i2 - testUnits.Count];
            }

            newGen.Add(NN.MakeChild(dad, mom));
        }

        for (int j = 0; j < testUnits.Count - 1; j++)
        {
            testUnits[j].agent.network = newGen[j];
        }

        testUnits[testUnits.Count - 1].agent.network.Randomize();
        // Start training again
        StartTraining();
    }
}
