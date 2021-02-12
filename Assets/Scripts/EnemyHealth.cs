using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    bool dead = false;
    int currentHealth;

    public bool GetDeadBool()
    {
        return dead;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if(dead) { return; }
        GetComponent<EnemyAI>().ShotAt();
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        GetComponent<Animator>().SetTrigger("isDead");
    }


}
