using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public float hp = 20;
    public float Max_Hp=20;
    // Start is called before the first frame update
    void Start()
    {
        hp = Max_Hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
