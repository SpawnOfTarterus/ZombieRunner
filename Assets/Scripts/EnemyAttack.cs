using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float damage = 40f;

    EnemyAI enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    public void AttackHitEvent()
    {
        target = enemyAI.GetTarget();
        if(!target) { return; }
        target.GetComponent<PlayerHealth>().DealDamage(Mathf.RoundToInt(damage));
    }



}
