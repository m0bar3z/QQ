using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int coins = 0;
    public Text coinsText;

    public void AddCoins()
    {
        coins++;
        SetCoins();
    }

    public void SetCoins()
    {
        coinsText.text = coins + "";
    }
}
