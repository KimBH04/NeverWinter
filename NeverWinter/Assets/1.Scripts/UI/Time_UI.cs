using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Time_UI : MonoBehaviour
{
    [SerializeField]  private GameObject Attach_1;
    [SerializeField]private GameObject Attach_1_5;
    [SerializeField]  private GameObject Attach_2;
    [SerializeField] private GameObject GamePause;

    private float nowattach;
    


    public void Start()
    {
       
        GamePause.SetActive(false);
    }
    public void attach_1()
    {
        nowattach = 1.5f;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        Time.timeScale = 1.5f;
        Attach_1.SetActive(false);
        Attach_1_5.SetActive(true);
        //Debug.Log("1.5배");
    }
    public void attach_1_5()
    {
        nowattach = 2f;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        Time.timeScale = 2f;
        Attach_1_5.SetActive(false);
        Attach_2.SetActive(true);
        //Debug.Log("2배");
    }
    public void attach_2()
    {
        nowattach = 1f;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        Time.timeScale = 1f;
        Attach_2.SetActive(false);
        Attach_1.SetActive(true);
        //Debug.Log("1배");
    }
    
    public void gameStop()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        GamePause.SetActive(true);
       
        Time.timeScale = 0f;
        //Debug.Log("타임 스토브");
    }
    
    public void gameAgain()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        //Debug.Log("요시 카이쵸");
        GamePause.SetActive(false);
        Time.timeScale = nowattach;
        
    }

    public void GameTry()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        SceneManager.LoadScene("GameScene");
        nowattach = 1f;
        Time.timeScale = nowattach;
    }

    public void MainMenu()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        SceneManager.LoadScene("Main");
        nowattach = 1f;
        Time.timeScale = nowattach;
    }



}
