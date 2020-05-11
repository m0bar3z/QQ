using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreText : MonoBehaviour
{
    public float duration, endValMultiplier = 2;

    private void Start()
    {
        transform.DOMove(transform.position + Vector3.up, duration);
        transform.DOScale(transform.localScale * endValMultiplier, duration).OnComplete(()=> { Destroy(gameObject); });
    }
}
