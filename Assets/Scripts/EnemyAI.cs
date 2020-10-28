using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float chaseRange = 5f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckForTarget();
    }

    private void CheckForTarget()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget >= chaseRange) { isProvoked = false; return; }
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        isProvoked = true;
        navMeshAgent.SetDestination(target.position);
        if (distanceToTarget > navMeshAgent.stoppingDistance) { return; }
        EngageTarget();
    }

    private void EngageTarget()
    {
        transform.LookAt(target);
        Debug.Log("I am attacking.");
    }
}
