using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManage : MonoBehaviour
{
    static SceneManage instance;
    public GameObject loadingWindowPref;
    public GameObject LWInstance;
    public Canvas mainCanvas;

    [SerializeField]
    private Button startButton = null;
    [SerializeField]
    private Button restartButton = null;

    public void LoadNext(int index)
    {
        mainCanvas.gameObject.SetActive(false);
        LWInstance = Instantiate(loadingWindowPref, transform.position, Quaternion.identity);
        StartCoroutine(LoadScene(index));
    }

    IEnumerator LoadScene(int index)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index + 1);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
                Destroy(LWInstance);
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
        mainCanvas = FindObjectOfType<Canvas>();
        if (level == 1)
        {
            startButton = GameObject.FindWithTag("startButton").GetComponent<Button>();
            startButton.onClick.AddListener(() => { LoadScene(SceneManager.GetActiveScene().buildIndex); });
        }

        /*if(level == 3)
        {
            restartButton = GameObject.FindWithTag("restartButton").GetComponent<Button>();
        }*/

        /* if(restartButton != null)
         {
             restartButton.onClick.AddListener(() => { StartLoading(2); });
         }
     }*/

    }
}
    
