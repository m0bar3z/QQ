using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreText : MonoBehaviour
{
    public float duration, endValMultiplier = 2;
    public TextMesh text;

    private void Start()
    {
        transform.DOMove(transform.position + Vector3.up, duration);
        transform.DOScale(transform.localScale * endValMultiplier, duration).OnComplete(()=> { Destroy(gameObject); });

        DOTween.To(() => text.color, (x) => { text.color = x; }, new Color(1, 1, 1, 0f), duration);
    }
}
