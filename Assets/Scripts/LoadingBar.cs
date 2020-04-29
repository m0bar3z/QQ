using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    static LoadingBar instance;

    public int nextSceneIndex;

    [SerializeField]
    private Button startButton = null;
    [SerializeField]
    private Button restartButton = null;

    public void StartLoading(string levelName)
    {
        SceneManager.LoadScene("Loading");
        StartCoroutine(LoadScene(levelName));
    }

    IEnumerator LoadScene(string levelName)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if(asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null; 
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(level == 2)
        {
            startButton = GameObject.FindWithTag("startButton").GetComponent<Button>();
        }

        /*if(level == 3)
        {
            restartButton = GameObject.FindWithTag("restartButton").GetComponent<Button>();
        }*/

        if(startButton != null)
        { 
            startButton.onClick.AddListener(() => {StartLoading("Game"); });
        }

        if(restartButton != null)
        {
            restartButton.onClick.AddListener(() => { StartLoading("Game"); });
        }
    }
}
