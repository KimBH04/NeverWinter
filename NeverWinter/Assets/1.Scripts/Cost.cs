using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class Cost : MonoBehaviour
{
    
   // public static Cost instance;
    
    public GameObject[] Towers;
    
    public static int Coin=600;
    public int GetCoin;
    public TextMeshProUGUI CoinText;
    public GameObject NoMoneyText;
    public Transform SummonPos; 
    
    private float timer;

    private void Awake()
    {
        Coin = 600;
        // if (instance != null && instance != this)
        // {
        //     Destroy(this.gameObject);
        //     return;
        // }
        //
        //
        // instance = this;
        // DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
      //   timer += Time.deltaTime;
      //   if (timer >= 1.0f)
      //   {
      //   Coin += Mathf.FloorToInt(GetCoin);
      //    timer -= 1.0f;
      // }
      
     CoinText.text = "" + Coin;

    }

    

    public void Summon()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Sum);
        
        if (Coin >= 100)
        {
            Coin -= 100;
            SummonRandomTower();

        }
        
        else
        {
            NoMoneyText.SetActive(true);
            StartCoroutine(TextOff(1));

        }
            
        
    }
    
    private void SummonRandomTower()
    {
        int randomIndex = Random.Range(0, Towers.Length);
        GameObject randomTower = Instantiate(Towers[randomIndex], SummonPos.position, Quaternion.identity);
    }

    IEnumerator TextOff(float delay)
    {
       
        yield return new WaitForSeconds(delay);
        NoMoneyText.SetActive(false);
        
    }
    
    
    
}