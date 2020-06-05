using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleAnimation : MonoBehaviour
{
    public float multiplier = 2, duration = 1;

    private void Start()
    {
        transform.DOScale(transform.localScale * multiplier, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutExpo);
    }
}
