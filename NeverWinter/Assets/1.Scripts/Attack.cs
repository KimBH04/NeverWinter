using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Tower2 tower1 = null;
    public bool isMove = true;
    public int lifeTime = 100;
    EnemyCtrl target = null;
    Transform a;
    //EnemyCtrl targetHead = null;
    //public float AD = 10.0f;  
    // Start is called before the first frame update

    public void MoveStart(Tower2 tower, EnemyCtrl b)
    {
        target = b;
        tower1 = tower;
        transform.rotation = tower1.shootPoint.transform.rotation;
        isMove = true;
    }

    void Start()
    {
        a = target.transform.Find("Target");
        Debug.Log(a);
        Destroy(gameObject, 5.0f);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // 대상이 없으면 총알을 파괴
            return;
        }

        Vector3 direction = a.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (isMove)
        {
            //transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }

        //if (direction.magnitude <= distanceThisFrame)
        //{
        //    HitTarget(); // 총알이 대상에 도달하면 대상을 공격
        //    return;
        //}
    }

    //private void OnCollisionEnter(Collision collision)
    //{
        
    //}
    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log(collision);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            EnemyCtrl unit = collision.gameObject.GetComponent<EnemyCtrl>();
            if (unit)
            {
                //Damage(Random.Range(3, 6)); 데미지
                Debug.Log(unit);
                unit.TakeDamage(tower1.AD/*+Tower2.ad*/);
            }
            Destroy(gameObject);
        }

       
    }
    //IEnumerator Disapear()
    //{
    //    
    //    isMove = false;
    //    yield return new WaitForSeconds(1.0f);

    //   
    //    Destroy(gameObject);
    //}
}
