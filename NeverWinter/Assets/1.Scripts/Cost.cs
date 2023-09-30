using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cost : MonoBehaviour
{
    public int Coin;
    public int GetCoin;
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

        CoinText.text = "" + Coin;

    }

    public void Summon()
    {
        if (Coin >= 100)
        {
            Coin -= 100;
// 유닛 랜덤 생성 넣을 예정
        }
        
        else
        {
            Debug.Log("돈없쩡");
        }
            
        
    }
    
    
}