using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 메모장

public class EnemyCtrl : MonoBehaviour
{
    private WayContainer container;
    private int idx;

    public Transform[] movePoints;

    // 체력  #미완성#
    public float Enemy_HP;
    public float Max_Hp;
    // 이동속도
    public float Enemy_move_Speed;
    
    //능력 #미완성#
    public string Enemy_Spell;

    // 현상금
    public int Reward;
    
    //적 사망여부 
    public bool isEnemyDie = false;
    private bool isEnd = false;
    
    private Transform target;
    //public NavMeshAgent agent;
    
    void Start()
    {
        container = GameObject.Find("WayContainer").GetComponent<WayContainer>();
        Enemy_HP = Max_Hp;
    }

    private void Update()
    {
        MoveWay();
    }

    // 적이 데미지 받았을 때 쓰는 함수
    public void TakeDamage(float damage)
    {
        if (isEnemyDie)
            return;
        Enemy_HP -= damage;

        
        if (Enemy_HP <= 0)
        {
            EnemyDie();
        }
    }

    public void Attack()
    {
        GameManager.instance.Lives -= 10;
    }

    // 적이 죽었을 때 쓰는 함수  # 미완성 #
    // 애니메이션 추가 예정
    private void EnemyDie()
    {
        isEnemyDie = true;

        

        
        //agent.enabled = false;

        
        GetComponent<Collider>().enabled = false;

        Cost.Coin += Reward;
        Destroy(gameObject, 1.0f);

    }

    void MoveWay()
    {
        if (!isEnd)
        {
            Transform tr = container.WayPoints[idx];
            transform.LookAt(tr);

            transform.position = Vector3.MoveTowards(transform.position, tr.position, Enemy_move_Speed * Time.deltaTime);

            if ((transform.position - tr.position).sqrMagnitude < 0.05f)
            {
                idx++;
                if (idx >= container.WayPoints.Length)
                {
                    isEnd = true;
                }
            }
        }
    }
}
