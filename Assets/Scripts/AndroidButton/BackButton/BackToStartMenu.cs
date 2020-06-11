using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackToStartMenu : MonoBehaviour, IBackButton
{
    public HelpController helpController;
    public void BackButton()
    {
        helpController.LoadStartMenuScene();
    }
}
