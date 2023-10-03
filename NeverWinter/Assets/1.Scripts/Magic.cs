using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Magic : MonoBehaviour
{
    public GameObject Fire;
    public bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attack == true&& Input.GetMouseButtonDown(0))
        {
            if (attack == true)
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
                Debug.Log("dk");
                point.y += 0.5f;
                Instantiate(Fire, point, Quaternion.identity);
                attack = false;
            }
        }   
    }


    public void click()
    {
        attack = true;
    }
    
}
