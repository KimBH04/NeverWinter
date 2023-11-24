using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dum : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(transform.GetSiblingIndex());

        transform.SetSiblingIndex(0);
    }
}
