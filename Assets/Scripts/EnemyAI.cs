using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float normalChaseRange = 10f;

    Transform target;
    Animator myAnimator;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool provoked = false;
    float awareTimer = 30f;
    float provokedChaseRange = 500f;
    float currentChaseRange;

    public void RemoveTarget()
    {
        target = null;
    }

    public Transform GetTarget()
    {
        return target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, currentChaseRange);
    }

    void Start()
    {
        target = FindObjectOfType<Player>().transform;
        currentChaseRange = normalChaseRange;
        myAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckForTarget();
    }

    private void CheckForTarget()
    {
        if(!target) { SetAnimationStateToIdle();  return; }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget >= currentChaseRange) { myAnimator.SetBool("isProvoked", false); navMeshAgent.ResetPath(); return; }
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
    }

    public void ShotAt()
    {
        currentChaseRange = provokedChaseRange;
        StartCoroutine(TimeToChase());
    }

    IEnumerator TimeToChase()
    {
        yield return new WaitForSeconds(awareTimer);
        currentChaseRange = normalChaseRange;
    }

    private void SetAnimationStateToIdle()
    {
        myAnimator.SetBool("playerInRange", false);
        myAnimator.SetBool("isProvoked", false);
    }

}
