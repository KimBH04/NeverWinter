using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 메모장

public class EnemyCtrl : MonoBehaviour
{
    // 체력  #미완성#
    public float Enemy_HP;
   
    // 이동속도
    public float Enemy_move_Speed;
    
    // 회전 속도 
    public float Enemy_rotation_Speed; 
    
    //능력 #미완성#
    public string Enemy_Spell;
    
    //적 사망여부 #미완성#
    public bool isEnemyDie = false;
    
    private Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("TARGET").transform;

        agent.speed = Enemy_move_Speed;
        agent.angularSpeed = Enemy_rotation_Speed;
        
        agent.SetDestination(target.position);
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

    // 적이 죽었을 때 쓰는 함수  # 미완성 #
    // 애니메이션 추가 예정
    private void EnemyDie()
    {
        isEnemyDie = true;

        

        
        agent.enabled = false;

        
        GetComponent<Collider>().enabled = false;

        
        Destroy(gameObject, 1.0f);

    }
}
