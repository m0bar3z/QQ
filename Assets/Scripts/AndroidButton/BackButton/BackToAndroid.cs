using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackToAndroid : MonoBehaviour, IBackButton
{
   public void BackButton()
    {
        Application.Quit();
    }
}
