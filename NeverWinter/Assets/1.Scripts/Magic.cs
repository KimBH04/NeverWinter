using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public Transform area;
    public GameObject Fire;
    public bool attack = false;
    

    
    void Update()
    {
        if (attack && Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 10))
            {
                attack = false;
                area.position = hit.point + new Vector3(0f, 0.1f, 0f);
                Vector3 masicPoint = hit.point + new Vector3(0f, 10f, 0f);
                GameObject fire = Instantiate(Fire, masicPoint, Quaternion.Euler(-90f, 0f, 0f));

                Meteo meteo = fire.GetComponent<Meteo>();

                float radius = meteo.radius / 5f;
                area.localScale = new Vector3(radius, radius, radius);
                meteo.magic = this;
            }
        }   
    }

    public void click()
    {
        attack = true;
    }
}
