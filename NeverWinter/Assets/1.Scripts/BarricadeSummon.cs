using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
         spawnPos = hit.point;
         GameObject barricade = Instantiate(barricadePrefab, spawnPos, Quaternion.identity);
      }
      
      
      
   }
   
   public void click()
   {
      summon = true;
   }
   
}
