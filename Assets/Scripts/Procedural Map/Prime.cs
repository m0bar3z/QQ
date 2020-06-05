using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Prime
{	private static int MinKey(int[] key, bool[] set, int verticesCount)
	{
		int min = int.MaxValue, minIndex = 0;

		for (int v = 0; v < verticesCount; ++v)
		{
			if (set[v] == false && key[v] < min)
			{
				min = key[v];
				minIndex = v;
			}
		}

		return minIndex;
	}

	public static void Calculate(int[,] graph, int verticesCount, out int[] parent)
	{
		parent = new int[verticesCount];
		int[] key = new int[verticesCount];
		bool[] mstSet = new bool[verticesCount];

		for (int i = 0; i < verticesCount; ++i)
		{
			key[i] = int.MaxValue;
			mstSet[i] = false;
		}

		key[0] = 0;
		parent[0] = -1;

		for (int count = 0; count < verticesCount - 1; ++count)
		{
			int u = MinKey(key, mstSet, verticesCount);
			mstSet[u] = true;

			for (int v = 0; v < verticesCount; ++v)
			{
				if (System.Convert.ToBoolean(graph[u, v]) && mstSet[v] == false && graph[u, v] < key[v])
				{
					parent[v] = u;
					key[v] = graph[u, v];
				}
			}
		}
	}
}
