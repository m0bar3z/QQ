using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GetBuildIndex : MonoBehaviour
{
    static int activeSceneIndex;
    
    static int ActiveScene
    {
        get { return SceneManager.GetActiveScene().buildIndex; }
    }

    static int NextScene
    {
        get { return SceneManager.GetActiveScene().buildIndex + 1;  }
    }
}
