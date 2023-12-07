using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using TMPro;



public class Tower2 : MonoBehaviour
{
    public static Tower2 instance;
    public static float PlusAD;
    public GameObject shootPoint;
    EnemyCtrl targetUnit = null;

    public float shootDelay = 0.8f;
    public float distance = 7.0f;
    public float spin = 50f;
    public float AD;
    public float Reset;
    private float Dist;
    public bool stun1 = false;
    public float animationInterval = 4.0f;
    private float timer = 0f;

    public GameObject StunImage;
    public GameObject Bullet;
    Transform head;



    private float temp;

    // Start is called before the first frame update
    private void Start()
    {
        //AD = Reset;
        stun1 = false;
        StunImage.SetActive(false);
        instance = this;
        temp = shootDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (stun1 == true)
        {
            timer += Time.deltaTime;
            if (timer >= animationInterval)
            {
                stun1 = false;
                StunImage.SetActive(false);
                timer = 0f;
            }
            else
                return;
        }

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
                    break;            //타워 공격에서 에러나면 이거 다시 재활성화
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

            Quaternion rot = Quaternion.LookRotation(viewPos);

            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rot, Time.deltaTime * spin);


            Dist = Vector3.Distance(gameObject.transform.position, targetUnit.transform.position);
            if (Dist > distance/*+2.0f*/|| targetUnit.isEnemyDie == true)
            {
                targetUnit = null;
            }
        }

    }
    public void PlusAD1(float plusad)
    {
        //Tower2.PlusAD = plusad;
        AD += plusad;
    }

    public void Stun()
    {
        StunImage.SetActive(true);
        Debug.Log("dkdk");
        stun1 = true;
    }

    public void BulletShoot(EnemyCtrl a)
    {
        GameObject bullet = Instantiate(Bullet, shootPoint.transform.position, Quaternion.identity);
        if (bullet)
        {
            bullet.transform.position = shootPoint.transform.position;
            Attack obj = bullet.GetComponent<Attack>();
            if (obj)
            {
                obj.MoveStart(this, a);
            }
        }


    }

    //public void plus(int a)
    //{
    //    if (PlusAD != a)
    //    {
    //        PlusAD += a;
    //        ad += a;
    //    }
    //}

    //public void exit(int a)
    //{
    //    if (PlusAD == a)
    //    {
    //        PlusAD -= a;
    //        ad -= a;
    //    }
    //}

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