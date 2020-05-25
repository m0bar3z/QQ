using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToSM : MonoBehaviour, IBackButton
{
    public OptionsController optionsController;
    public void BackButton()
    {
        optionsController.LoadStartMenuScene();
    }
}
