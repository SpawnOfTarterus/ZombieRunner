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
    EnemyHealth myHealth;
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
        myHealth = GetComponent<EnemyHealth>();
        target = FindObjectOfType<Player>().transform;
        currentChaseRange = normalChaseRange;
        myAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(myHealth.GetDeadBool()) { navMeshAgent.ResetPath(); return; }
        CheckForTarget();
    }

    private void CheckForTarget()
    {
        if(!target) { myAnimator.SetTrigger("isIdle");  return; }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget >= currentChaseRange) { myAnimator.SetTrigger("isIdle"); navMeshAgent.ResetPath(); return; }
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        myAnimator.SetTrigger("isProvoked");
        navMeshAgent.SetDestination(target.position);
        if (distanceToTarget > navMeshAgent.stoppingDistance) { myAnimator.SetTrigger("isProvoked"); return; }
        EngageTarget();
    }

    private void EngageTarget()
    {
        myAnimator.SetTrigger("playerInRange");
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
}
