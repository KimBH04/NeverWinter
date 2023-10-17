using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : Attack
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            EnemyCtrl unit = collision.gameObject.GetComponent<EnemyCtrl>();
            if (unit)
            {
                //Damage(Random.Range(3, 6)); µ¥¹ÌÁö
                unit.TakeDamage(tower1.AD + Tower2.ad);
            }
            Destroy(gameObject, 0.5f);
        }
    }
}

