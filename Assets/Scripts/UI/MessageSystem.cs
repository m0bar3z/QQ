using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageSystem : MonoBehaviour
{
    public Text text;
    public float showTime = 1;

    public void ShowMessage(string mssg)
    {
        text.text = mssg;
        text.DOFade(1, showTime).OnComplete(
            () =>
            {
                text.DOFade(0, showTime);
            }    
        );
    }
}
