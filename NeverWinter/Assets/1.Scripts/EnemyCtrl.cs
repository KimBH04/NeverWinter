using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : MonoBehaviour
{
    public float move_Speed = 3.0f; // 이동 속도 
    public float rotation_Speed = 120.0f; // 회전 속도 
    
    private Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("TARGET").transform;

        agent.speed = move_Speed;
        agent.angularSpeed = rotation_Speed;
        
        agent.SetDestination(target.position);
    }
}
