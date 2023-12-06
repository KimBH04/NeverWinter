using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Time_UI : MonoBehaviour
{
    [SerializeField] private GameObject Attach_1;
    [SerializeField] private GameObject Attach_1_5;
    [SerializeField] private GameObject Attach_2;
    [SerializeField] private GameObject GamePause;
    private GameManager manager;
    private float nowattach;



    public void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        for (int i = 0; i < 3; i++)
        {
            manager.Pub[i] = manager.Pub1[i];
            manager.Cannon[i] = manager.Cannon1[i];
            manager.Xbow[i] = manager.Xbow1[i];
            manager.Magic[i] = manager.Magic1[i];
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        SceneManager.LoadScene("GridGameScene");
        nowattach = 1f;
        Time.timeScale = nowattach;
    }

    public void MainMenu()
    {
        for (int i = 0; i < 3; i++)
        {
            manager.Pub[i] = manager.Pub1[i];
            manager.Cannon[i] = manager.Cannon1[i];
            manager.Xbow[i] = manager.Xbow1[i];
            manager.Magic[i] = manager.Magic1[i];
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        SceneManager.LoadScene("Main");
        nowattach = 1f;
        Time.timeScale = nowattach;
    }

    public void GoToNewMap(string newMap)
    {
        for (int i = 0; i < 3; i++)
        {
            Transform childTransform_1 = manager.Magic[i].transform.Find("Magic");
            Transform childTransform_10 = manager.Magic1[i].transform.Find("Magic");

            Transform childTransform_2 = manager.Cannon[i].transform.Find("Cannon");
            Transform childTransform_20 = manager.Cannon1[i].transform.Find("Cannon");

            Transform childTransform_3 = manager.Xbow[i].transform.Find("Xbow");
            Transform childTransform_30 = manager.Xbow1[i].transform.Find("Xbow");

            Transform childTransform_4 = manager.Pub[i].transform.Find("intersection");
            Transform childTransform_40 = manager.Pub1[i].transform.Find("intersection");


            Tower2 childScript_1 = childTransform_1.GetComponent<Tower2>();
            Tower2 childScript_10 = childTransform_10.GetComponent<Tower2>();

            childScript_1.AD = childScript_10.AD;


            Tower2 childScript_2 = childTransform_2.GetComponent<Tower2>();
            Tower2 childScript_20 = childTransform_20.GetComponent<Tower2>();
            childScript_2.AD = childScript_20.AD;

            Tower2 childScript_3 = childTransform_3.GetComponent<Tower2>();
            Tower2 childScript_30 = childTransform_30.GetComponent<Tower2>();

            childScript_3.AD = childScript_30.AD;

            Pub childScript_4 = childTransform_4.GetComponent<Pub>();
            Pub childScript_40 = childTransform_40.GetComponent<Pub>();

            childScript_4.attackBoost = childScript_40.attackBoost;

        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        SceneManager.LoadScene(newMap);
    }
}
