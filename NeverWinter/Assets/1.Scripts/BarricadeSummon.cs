using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarricadeSummon : MonoBehaviour
{
   [SerializeField]
   private GameObject barricadePrefab;

  

   private Vector3 spawnPos;
   
public bool summon = false;

   private void Update()
   {
      if(summon&& Input.GetMouseButtonDown(0))
      {
         summon = false;
         SummonBarricade();
      }
   }

   private void SummonBarricade()
   {
      SkillControl. instance.HideSkiiSetting(1);
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
         spawnPos = hit.point;
         int random = Random.Range(0, 2);
         float y = (random==0)?45f:135f;
         GameObject barricade = Instantiate(barricadePrefab, spawnPos, Quaternion.Euler(0,y,0));
      }
      
      
      
   }
   
   public void click()
   {
      summon = true;
   }
   
}
