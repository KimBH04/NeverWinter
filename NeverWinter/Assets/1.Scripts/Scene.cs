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
        
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    
    
}
