using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    
    
 /// <summary>
 ///  씬 이동 함수
 /// </summary>
 /// <param name="버튼에 넣어서 씬을 이동 하자"></param>
 /// <returns> 쓰는 방법을 모르겠다면 카톡해라  </returns>
    public void SceneMove(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

 
 // 게임 속도 두배
 public void GameSpeedDouble()
 {
     Time.timeScale = 2f;
 }
 
 
 // 게임 속도 원래대로
 public void GameSpeed()
 {
     Time.timeScale = 1f;
 }
 
 
    
}
