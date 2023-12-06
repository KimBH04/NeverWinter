using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pub : MonoBehaviour
{
    [SerializeField] private int Rank;

    //[SerializeField] private GameObject diameter;
    public float attackBoost;
    //public float attackRange = 5f; // 공격 범위
    private void Start()
    {
        //diameter.SetActive(false);      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TOWER")) //
        {
            GridTower playerAttack = other.GetComponent<GridTower>();
            other.GetComponent<GridTower>().TowerADUP(attackBoost);
            Debug.Log("플레이어의 공격력이 올라갔습니다: " + playerAttack);
        }

    }

    public void plisad(float plus)
    {
        attackBoost += plus;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TOWER"))
        {
            GridTower playerAttack = other.GetComponent<GridTower>();
            other.GetComponent<GridTower>().TowerADminus(attackBoost);
            Debug.Log("플레이어의 공격력이 올라갔습니다: " + playerAttack);
        }
        Debug.Log(attackBoost);
    }

    //private void OnMouseDown()
    //{
    //    diameter.SetActive(true);
    //}

    //private void OnMouseExit()
    //{
    //    diameter.SetActive(false);
    //}


    void WaveRest()
    {
        Cost.Coin += 100 * Rank;
    }
}