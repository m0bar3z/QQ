using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// TODO : Don't use dotween
public class FollowTarget : MonoBehaviour
{
    public float duration = 0.5f;
    public Transform target;

    private Tween _tween;

    private void Update()
    {
        _tween = transform.DOMove(
            new Vector3(target.position.x, target.position.y, transform.position.z),
            duration
        );
    }
}
