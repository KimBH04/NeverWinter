using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower2 : MonoBehaviour
{
    // 타워 파는 것 #미완성#
    public GameObject TowerSelf;
    public GameObject shootPoint;
    EnemyCtrl targetUnit = null;
    public static float shootdelay = 1.0f;
    public static float ad = 0f;
    public float shootDelay = 0.8f;
    public float distance = 7.0f;
    public float spin = 50f;
    public int TowerSell;
    public float AD;
    private float Dist;
    public GameObject Bullet;
    public float PlusAD;
    Transform head;




    private float temp;

    // Start is called before the first frame update
    private void Start()
    {
        temp = shootDelay;
    }

    // Update is called once per frame
    void Update()
    {       
        if (targetUnit == null)
        {
            Collider[] colliderList = Physics.OverlapSphere(transform.position, distance, LayerMask.GetMask("Unit"));

            for (int i = 0; i < colliderList.Length; i++)
            {
                EnemyCtrl searchTarget = colliderList[i].GetComponent<EnemyCtrl>();
                if (searchTarget) //&& searchTarget.isDie == false)
                {
                    //StartCoroutine(BulletBustShoot2());
                    targetUnit = searchTarget;
                    break;
                }
            }
        }
        
        if (targetUnit != null)
        {
            head = targetUnit.transform.Find("Target");
            Vector3 viewPos = head.transform.position - gameObject.transform.position;

            if (shootDelay <= 0f)
            {     
                BulletShoot(targetUnit);

                shootDelay = temp;
            }
            else
            {
                shootDelay -= Time.deltaTime;
            }

            Quaternion rot = Quaternion.LookRotation(viewPos) ;
            
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rot, Time.deltaTime * spin);

            
            Dist = Vector3.Distance(gameObject.transform.position, targetUnit.transform.position);
            if (Dist > distance+2.0f|| targetUnit.isEnd != true)
            {
                targetUnit = null;
            }
        }

    }

    
    public void BulletShoot(EnemyCtrl a)
    {
        //�Ѿ��� �����Ѵ�
        GameObject bullet = Instantiate(Bullet, shootPoint.transform.position, Quaternion.identity);
        if (bullet)
        {
            bullet.transform.position = shootPoint.transform.position;
            Attack obj = bullet.GetComponent<Attack>();
            if (obj)
            {
                obj.MoveStart(this,a);
            }
        }
    }

    public void plus(int a)
    {
        if (PlusAD != a)
        {
            PlusAD += a;
            ad += a;
            Debug.Log(ad);
        }
    }

    public void exit(int a)
    {
        if (PlusAD == a)
        {
            PlusAD -= a;
            ad -= a;
        }
    }

    /*public IEnumerator BulletBustShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            //�Ѿ� �߻�
            BulletShoot();
            //0.2�� �� ��� ������
            yield return new WaitForSeconds(0.2f);

        }
    }*/
    public IEnumerator BulletBustShoot2()
    {
        yield return new WaitForSeconds(5.0f);

    }
}
