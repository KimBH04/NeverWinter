using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    public float damagePerSecond = 10f;
    public float speedNuff = 0.5f;
    private float speed;
    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            float damageThisFrame = damagePerSecond * Time.deltaTime;
            EnemyCtrl enemyCtrl = other.GetComponent<EnemyCtrl>();
            originalColor = enemyCtrl.render.material.color;
            enemyCtrl.TakeDamage(damageThisFrame);         
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Unit")) 
        {         
            EnemyCtrl enemyCtrl = other.GetComponent<EnemyCtrl>();
            speed = enemyCtrl.Enemy_move_Speed;
            enemyCtrl.Enemy_move_Speed *= speedNuff;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            EnemyCtrl enemyCtrl = other.GetComponent<EnemyCtrl>();                       
            enemyCtrl.Enemy_move_Speed *= speed;
            enemyCtrl.render.material.color = originalColor;
        }
    }


}
