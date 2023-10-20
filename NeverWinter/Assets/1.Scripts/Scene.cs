using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    

    

    public void Move()
    {
        Debug.Log("Start");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        StartCoroutine(move());

    }

    IEnumerator move()
    {

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");

    }





    public void NoLoding(string sceneName)
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        SceneManager.LoadScene(sceneName);
    }
}
