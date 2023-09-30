using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_UI : MonoBehaviour
{
    public GameObject Attach_1;
    public GameObject Attach_1_5;
    public GameObject Attach_2;
    public GameObject GamePause;
    public GameObject GameAgain;
    
    
    public void attach_1()
    {
        Time.timeScale = 1.5f;
        Attach_1.SetActive(false);
        Attach_1_5.SetActive(true);
        Debug.Log("1.5배");
    }
    public void attach_1_5()
    {
        Time.timeScale = 2f;
        Attach_1_5.SetActive(false);
        Attach_2.SetActive(true);
        Debug.Log("2배");
    }
    public void attach_2()
    {
        Time.timeScale = 1f;
        Attach_2.SetActive(false);
        Attach_1.SetActive(true);
        Debug.Log("1배");
    }
    
    public void gameStop()
    {
        GamePause.SetActive(false);
        GameAgain.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("타임 스토브");
    }
    
    public void gameAgain()
    {
        GamePause.SetActive(true);
        GameAgain.SetActive(false);
        
        Time.timeScale = 1f;
        Debug.Log("요시 카이쵸");
    }



}
