using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemySpawnPoint : MonoBehaviour
{
    public float spawnDelay = 1f;
    public GameManager manager;
    public WaveContainer[] containers;

    int containerIndex = 0;
    
    
    bool isFinishedCoroutine = true;

    
   
    public void WaveStart()
    {

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Wave);
        manager.Sumonbutton.gameObject.transform.DOLocalMoveY(-658, 1f);
        manager.Wavebutton.gameObject.transform.DOLocalMoveY(-658, 1f);
        
        //manager.Sumonbutton.gameObject.SetActive(false);
        //manager.Wavebutton.gameObject.SetActive(false);
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