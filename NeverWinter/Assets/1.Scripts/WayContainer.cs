using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayContainer : MonoBehaviour
{
    public List<Transform> WayPoints = new List<Transform>();

    private void Start()
    {
        GetComponentsInChildren(WayPoints);
    }
}
