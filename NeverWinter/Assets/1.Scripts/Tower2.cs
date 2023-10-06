using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2 : EnemyCtrl
{
    // 타워 파는 것 #미완성#
    public GameObject TowerSelf;
    public GameObject shootPoint;
    EnemyCtrl targetUnit = null;
    public float shootDelay = 0.8f;
    public float distance = 7.0f;
    public float spin = 50f;
    public float attack = 5.0f;
    public int TowerSell;


    private float temp;

    // Start is called before the first frame update
    void Start()
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
            Vector3 viewPos = targetUnit.transform.position - gameObject.transform.position;

            
            Quaternion rot = Quaternion.LookRotation(viewPos);
            //rot.y += 90;
            //�ش� ȸ���� ��ŭ �� ���� ȸ�� ��Ŵ.
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rot, Time.deltaTime * spin);
            //������ Ÿ���� 80, ������ 50~60
            //shootPoint.transform.rotation =;
            if (shootDelay <= 0f)
            {
                BulletShoot();
                shootDelay = temp;
            }
            else
            {
                shootDelay -= Time.deltaTime;
            }
        }

    }

    // UI 미적용
    public void Sell()
    {
        
        Cost.Coin += TowerSell;
        Destroy(TowerSelf);
    }
    public void BulletShoot()
    {
        //�Ѿ��� �����Ѵ�
        GameObject bullet = Instantiate(Resources.Load<GameObject>("Objects/Bullet"));
        if (bullet)
        {
            bullet.transform.position = shootPoint.transform.position;
            Attack obj = bullet.GetComponent<Attack>();
            if (obj)
            {

                obj.MoveStart(this);
            }
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
