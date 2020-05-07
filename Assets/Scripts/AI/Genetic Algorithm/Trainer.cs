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
        print("units initialized");
    }

    public void StartTraining()
    {
        genShower.text = "generation: " + gen++ + "\nBest fitness:" + bestFitnesses[bestFitnesses.Count - 1];

        foreach (Unit u in testUnits)
        {
            inTrainingCount++;
            u.StartUnit();
        }
        print("units started");
    }

    public void TrainingDone()
    {
        inTrainingCount--;
        if (inTrainingCount <= 0)
        {
            inTrainingCount = 0;

            // marriage season begins
            print("training done! new gen in progress");
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

        float[] fitnesses = new float[testUnits.Count];
        int i = 0;
        foreach(Unit u in testUnits)
        {
            fitnesses[i++] = u.fitness;
        }

        bestFitnesses.Add(fitnesses[0]);
        List<NN> newGen = new List<NN>();

        print("sorted");

        // save the best of last generation
        testUnits[0].agent.network.SaveNN();

        // make a new generation
        for(int j = 0; j < testUnits.Count; j++)
        {
            int i1 = WeightedRandomizer.WeightsArray(fitnesses);
            fitnesses[i1] = -1000;

            int i2 = WeightedRandomizer.WeightsArray(fitnesses);
            while(i2 == i1) i2 = WeightedRandomizer.WeightsArray(fitnesses);

            newGen.Add(NN.MakeChild(testUnits[i1].agent.network, testUnits[i2].agent.network));
        }

        print("made children");

        for (int j = 0; j < testUnits.Count; j++)
        {
            testUnits[j].agent.network = newGen[j];
        }

        print("assigned children");
        // Start training again
        StartTraining();
    }
}
