using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{




    //public void Move(string sceneName)
    //{
    //    Debug.Log("Start");
    //    AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
    //    StartCoroutine(move(sceneName));

    //}

    //IEnumerator move(string a)
    //{
    //    yield return new WaitForSeconds(0.3f);
    //    Debug.Log("dkdk");
    //    LodingScene.LoadScene(a);
    //}
    public void MainMove(string sceneName)
    {
        LodingScene.LoadScene(sceneName);
    }

    public void NoLoding(string sceneName)
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        SceneManager.LoadScene(sceneName);
    }

    



}
