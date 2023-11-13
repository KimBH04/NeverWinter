using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    public float damagePerSecond = 10f;
    public float speedNuff = 0.5f;

    public GameObject poisonEffect;
    private Queue<GameObject> poisonEffects = new Queue<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyCtrl eneCon = other.GetComponent<EnemyCtrl>();
            eneCon.Enemy_move_Speed *= speedNuff;
            eneCon.animator.speed *= speedNuff;

            GameObject effect = Instantiate(poisonEffect, other.transform.position, Quaternion.Euler(-90f, 0f, 0), other.transform);
            poisonEffects.Enqueue(effect);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyCtrl eneCon = other.GetComponent<EnemyCtrl>();
            eneCon.Enemy_move_Speed /= speedNuff;
            eneCon.animator.speed /= speedNuff;

            deq:
            if (poisonEffects.Count > 0)
            {
                GameObject effect = poisonEffects.Dequeue();
                if (effect == null)
                {
                    goto deq;
                }
                Destroy(effect);
            }
        }
    }
}
