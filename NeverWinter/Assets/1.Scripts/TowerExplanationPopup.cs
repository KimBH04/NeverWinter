using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerExplanationPopup : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, 128))
            {

            }
        }
    }
}