using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float chaseRange = 5f;

    Animator myAnimator;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckForTarget();
    }

    private void CheckForTarget()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget >= chaseRange) { myAnimator.SetBool("isProvoked", false); return; }
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        myAnimator.SetBool("isProvoked", true);
        navMeshAgent.SetDestination(target.position);
        if (distanceToTarget > navMeshAgent.stoppingDistance) { myAnimator.SetBool("playerInRange", false); return; }
        EngageTarget();
    }

    private void EngageTarget()
    {
        myAnimator.SetBool("playerInRange", true);
        transform.LookAt(target);
        Debug.Log("I am attacking.");
    }
}
