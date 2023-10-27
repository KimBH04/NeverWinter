using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : Attack
{
    // Start is called before the first frame update  

    //public void Update()
    //{
    //    transform.Translate(Vector3.forward * Time.deltaTime * speed);
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("dkdk0");
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Unit"))
    //    {
    //        EnemyCtrl unit = other.gameObject.GetComponent<EnemyCtrl>();
    //        if (unit)
    //        {
    //            //Damage(Random.Range(3, 6)); 데미지
    //            unit.TakeDamage(tower1.AD + Tower2.ad);
    //        }
    //        Destroy(gameObject, 0.5f);
    //    }
    //}


    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("dkdk0");
        if (other.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            EnemyCtrl unit = other.gameObject.GetComponent<EnemyCtrl>();
            if (unit)
            {
                //Damage(Random.Range(3, 6)); 데미지
                unit.TakeDamage(tower1.AD + Tower2.ad);
            }
            Destroy(gameObject, 0.1f);
        }
    }

}

