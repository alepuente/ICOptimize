using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    private NavMeshAgent navAgent;
    private int attackPosition;
    private PlayerController player;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = PlayerController.instance;
        attackPosition = Random.Range(0, player.attackPoint.Length);
        
    }

    void Update()
    {
        navAgent.destination = player.attackPoint[attackPosition].transform.position;
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position,player.transform.position) < navAgent.stoppingDistance)
        {
            //Attack
        }
    }
}