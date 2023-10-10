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
    //public float AD = 10.0f;  
    // Start is called before the first frame update

    public void MoveStart(Tower2 tower)
    {

        tower1 = tower;
        transform.rotation = tower1.shootPoint.transform.rotation;
        isMove = true;
    }

    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    void Update()
    {
        if (isMove)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
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
                //Damage(Random.Range(3, 6)); µ¥¹ÌÁö
                unit.TakeDamage(tower1.AD);
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
