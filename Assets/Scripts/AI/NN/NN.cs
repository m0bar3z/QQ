using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public class NN : MonoBehaviour
{
    public int input;
    public int output;
    public int hiddenLayers;
    public int hiddenNodes;

    public List<Matrix<float>> v, b;
}
