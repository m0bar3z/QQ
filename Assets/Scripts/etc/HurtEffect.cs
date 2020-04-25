using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HurtEffect : MonoBehaviour
{
    public SpriteRenderer sr;
    public float peakFade;
    public float duration;
    public int rewindMultiplier;

    private void Start()
    {
        FindObjectOfType<PlayerController>().health.OnDamage += Hurt;
    }

    public void Hurt()
    {
        sr.DOFade(peakFade, duration).SetEase(Ease.OutSine).OnComplete(
            () =>
            {
                sr.DOFade(0, duration * rewindMultiplier).SetEase(Ease.OutSine);
            }    
        );
    }
}
