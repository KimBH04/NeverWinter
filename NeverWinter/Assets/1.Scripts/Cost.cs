using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cost : MonoBehaviour
{
    public int Coin;
    public float GetCoin = 1f;
    public TextMeshProUGUI CoinText;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.0f)
        {
            Coin += Mathf.FloorToInt(GetCoin);
            timer -= 1.0f;
        }

        CoinText.text = ""+Coin;
        
    }
}
