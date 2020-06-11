using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManage : MonoBehaviour
{
    static SceneManage instance;
    public GameObject loadingWindowPref;
    public GameObject loadingWindowInstance;
    public Canvas mainCanvas;


    public  void ActiveLoadingWindow(int index)
    {
        mainCanvas.gameObject.SetActive(false);
        loadingWindowInstance = Instantiate(loadingWindowPref, transform.position, Quaternion.identity);
        StartCoroutine(LoadScene(index));
    }

    IEnumerator LoadScene(int index)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
                Destroy(loadingWindowInstance);
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
}
    
