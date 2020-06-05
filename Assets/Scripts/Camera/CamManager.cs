using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamManager : MonoBehaviour
{
    private static float defSize;
    private static Camera self;

    public static void SetSize(float newSize)
    {
        self.DOOrthoSize(newSize, 1);
    }

    public static float GetDefSize()
    {
        return defSize;
    }

    private void Start()
    {
        self = GetComponent<Camera>();
        defSize = self.orthographicSize;
    }
}
