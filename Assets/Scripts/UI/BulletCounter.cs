using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    public Text number;

    public void SetNumber(int n)
    {
        number.text = n + "";
    }
}
