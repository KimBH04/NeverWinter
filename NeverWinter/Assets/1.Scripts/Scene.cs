using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void MainMove(string sceneName)
    {
        
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        //StartCoroutine(Delay());
        LodingScene.LoadScene(sceneName);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        
    }

    public void NoLoding(string sceneName)
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        SceneManager.LoadScene(sceneName);
    }

    public void MainMenu()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        SceneManager.LoadScene("Main");
    }

    public void GameTry()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    public void ArtBook(string sceneName)
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        //StartCoroutine(Delay());
        LodingScene.LoadScene(sceneName);
    }

    public void Quit()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    
    
}
