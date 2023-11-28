using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject TextObj;
    [SerializeField] private Text text;

    public float spawnDelay = 1f;
    public GameManager manager;
    public WaveContainer[] containers;

    int containerIndex = 0;
    
    
    bool isFinishedCoroutine = true;

    
   
    public void WaveStart()
    {
        
        
        if (Cost.Coin >= 450)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
            TextObj.SetActive(true);
            text.text = "타워를 소환하여 주세요.";
            Invoke(nameof(Hide1),1f);
            return;
        }
        else
        {
            GridTower.PlayClick = false;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Wave);
            manager.Sumonbutton.transform.DOLocalMoveY(-658, 1f);
            manager.Wavebutton.transform.DOLocalMoveY(-658, 1f);
            
        }
       
        
        
        // manager.Sumonbutton.gameObject.SetActive(false);
        // manager.Wavebutton.gameObject.SetActive(false);
        //Debug.Log(manager.wavecount);


        if (isFinishedCoroutine)
        {
            isFinishedCoroutine = false;
            StartCoroutine(EnemySpawn());
        }
        else
        {
            //Debug.Log("Unfinished wave!");
        }
    }
    private void Hide1()
    {
        TextObj.SetActive(false);
    }
    public IEnumerator EnemySpawn()
    {
        if (containerIndex >= containers.Length)
        {
            isFinishedCoroutine = true;
            yield break;
        }

        for (; ; )
        {
             GameObject enemy = containers[containerIndex].GetEnemy();
            if (enemy == null)
            {
               
                break;
            }
            Instantiate(enemy, transform.position, Quaternion.Euler(0f, 0f, 90f));
            yield return new WaitForSeconds(spawnDelay);
        }
        containerIndex++;
        isFinishedCoroutine = true;
    }

   
}