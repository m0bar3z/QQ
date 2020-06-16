using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShaker : MonoBehaviour
{
    public static IEnumerator CameraShake(float duration, float magnitude)
    {
        Camera.main.DOShakePosition(duration, magnitude, 15);
        print(Time.deltaTime);
        yield return null;
    }

}
