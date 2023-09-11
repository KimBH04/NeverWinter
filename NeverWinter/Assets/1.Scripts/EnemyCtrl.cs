using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 메모장

public class EnemyCtrl : MonoBehaviour
{
    // 체력
    public float Enemy_HP;
   
    // 이동속도
    public float Enemy_move_Speed;
    
    // 회전 속도 
    public float Enemy_rotation_Speed; 
    
    //능력 #미완성#
    public string Enemy_Spell;
    
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
}
