using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeSummon : MonoBehaviour
{
    [SerializeField] private Transform barricadeVisual;

    [SerializeField]
    private GameObject barricadePrefab;


   private void Update()
   {
      if(summon&& Input.GetMouseButtonDown(0))
      {
         SummonBarricade();
            Cost.Coin -= 40;
      }
   }

    private Vector3 spawnPos;

      if (Physics.Raycast(ray, out hit, 100f, 1 << 10))
      {
         summon = false;
         SkillControl. instance.HideSkiiSetting(1);
         spawnPos = hit.point;
         int random = Random.Range(0, 2);
         float y = (random==0)?45f:135f;
         GameObject barricade = Instantiate(barricadePrefab, spawnPos, Quaternion.Euler(0,y,0));
      }
      
      
      
   }
   
   public void click()
   {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        summon = true;
   }
   
}
