using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
   
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            takeCastle();
    }

    public void takeCastle()
    {
        GameManager.instance.Lives--;
    }
    
    
    
}
