using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pub : MonoBehaviour 
{
    [SerializeField] private int Rank;

    [SerializeField] private GameObject diameter;

    //private bool isTower = false;
    private Tower2 istower1;
 
    //public Collider[] colliderList;
    public int a = 0;
    //Tower2 TowerCheck = null;
    private void Start()
    {
        diameter.SetActive(false);      
    }

    private void Update()
    {
        //colliderList = Physics.OverlapSphere(transform.position, 3.5f, LayerMask.GetMask("TOWER1"));

        Collider[] colliderList = Physics.OverlapSphere(transform.position, 3.5f, LayerMask.GetMask("TOWER1"));

        for (int i = 0; i < colliderList.Length; i++)
        {
            Tower2 searchTarget = colliderList[i].GetComponent<Tower2>();
            searchTarget.plus(a);
            //TowerCheck = searchTarget;
        }


        //if (TowerCheck == null)
        //{
        //    Collider[] colliderList1 = Physics.OverlapSphere(transform.position, 300f, LayerMask.GetMask("TOWER1"));

        //    for (int i = 0; i < colliderList1.Length; i++)
        //    {
        //        Tower2 searchTarget = colliderList1[i].GetComponent<Tower2>();
        //        searchTarget.exit(a);
        //    }
        //}

        //TowerCheck = null;

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Debug.Log(istower1.PlusAD);
    //    //Debug.Log(colliderList.Length);
    //    if (other.gameObject.layer == LayerMask.NameToLayer("TOWER1"))
    //    {
    //        Debug.Log(istower1.PlusAD);
    //        istower1 = other.gameObject.GetComponent<Tower2>();  //colliderList[colliderList.Length-1].GetComponent<Tower2>();
    //        Debug.Log(istower1.PlusAD);
    //        istower1.PlusAD += 5;
    //    }

    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    //for (int i = 0; i < colliderList.Length; i++)
    //    //{
    //    //    istower1 = colliderList[colliderList.Length - 1].GetComponent<Tower2>();
    //    //    istower1.PlusAD -= 5;
    //    //}
    //    if (other.transform.tag == "dd")
    //    {
    //        istower1 = other.GetComponent<Tower2>();
    //        istower1.exit(a);
    //    }

    //}


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
