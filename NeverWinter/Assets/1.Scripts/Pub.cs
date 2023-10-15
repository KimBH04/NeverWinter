using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pub : MonoBehaviour 
{
    [SerializeField] private int Rank;

    [SerializeField] private GameObject diameter;

    private bool isTower = false;



    private void Start()
    {
        diameter.SetActive(false);
    }

    private void Update()
    {
        
    }

    


    private void OnMouseDown()
    {
        diameter.SetActive(true);
    }

    private void OnMouseExit()
    {
        diameter.SetActive(false);
    }


    void WaveRest()
    {
        Cost.Coin += 100 * Rank;
    }





}
