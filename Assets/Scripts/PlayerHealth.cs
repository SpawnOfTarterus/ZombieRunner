using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        var enemies = FindObjectsOfType<EnemyAI>();
        foreach(EnemyAI enemy in enemies)
        {
            enemy.RemoveTarget();
        }
        FindObjectOfType<MenuCanvasController>().ToggleGameOverMenu();
    }
}
