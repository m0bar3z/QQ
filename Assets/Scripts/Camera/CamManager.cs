using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamManager : MonoBehaviour
{
    private static float defSize;
    private static Camera self;
    private static float resetTime;


    public static void SetSize(float newSize)
    {
        self.DOOrthoSize(newSize, 1);
    }

    public static float GetDefSize()
    {
         return defSize;
    }

    public void ResetCameraRotation(float duration)
    {
        resetTime = duration;
        Invoke(nameof(ResetRotation), resetTime);
    }
    private void ResetRotation()
    {
        Camera.main.transform.DORotate(Vector3.zero, resetTime);
    }

    private void Start()
    {
        self = GetComponent<Camera>();
        defSize = self.orthographicSize;
    }
}
