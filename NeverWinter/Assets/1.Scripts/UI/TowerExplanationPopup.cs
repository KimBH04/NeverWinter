using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerExplanationPopup : MonoBehaviour
{
    private Tower tower;
    private Transform tr;
    private Func move;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(tr.position);
    }

    public void OpenPopup(Tower tower, Transform screenPos, Func func)
    {
        this.tower = tower;
        tr = screenPos;
        move = func;

        gameObject.SetActive(true);
    }

    public void OnMoveStart() => move();
}

public delegate void Func();