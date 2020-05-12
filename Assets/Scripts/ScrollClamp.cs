using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScrollClamp : MonoBehaviour
{
    public RectTransform helpImageRect;

    public void SetRect()
    {
        if(helpImageRect.position.y < -2002f)
        {
            //helpImageRect.position.y = -2002f;
        }
    }
}
