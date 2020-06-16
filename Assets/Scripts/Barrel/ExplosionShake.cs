using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionShake : MonoBehaviour
{

    public void ShakingCamera()
    {
        Camera.main.DOShakeRotation(1f, 20, 10);
    }
     bool Shaking
    {
        get
        {
            if (PlayerPrefsManager.CameraShakeIsActive)
                return true;
            else
                return false;
        }
    }
    void Start()
    {
        
    }
}
